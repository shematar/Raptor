using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports; //串口通信类
using System.Threading.Tasks;
using System.Threading;//线程申明
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections;

namespace BalanceADJ
{
    public partial class MainForm : Form
    {
        //测试用
        //double temp=0;

        #region 进程相关定义
        //子窗体定义
        Setting fmSetting = null;
        //NewKind fmNewKind = null;
        form_ProductBarcode fmProduc = null;
        form_MudBarcode fmMud = null;

        //线程定义
        Thread th_ProcessDeal = null;
        Thread th_PlcCmdDeal=null;
        Thread th_LaserDataChtShow = null;
        Thread th_BalanceDataDeal = null;
        Thread th_PhoenixCmdDeal = null;
        //Thread th_ComFaceProcessDeal = null;

        //Phoenix共享文件相关
        public string str_PhoenixCmd = string.Empty;

        //作业流程控制定义
        public bool b_ProcessEnable = false;//作业开始标志
        public string str_ProcessCtl = string.Empty;
        AutoResetEvent faceOkEvent = new AutoResetEvent(false);//键盘确认事件通知
        //AutoResetEvent faceNgEvent = new AutoResetEvent(false);//事件通知

        #endregion

        #region 机种相关定义
        public string str_KindName="RAPTOR";
        static public string PRESET_2000rpm = "PPreset_2000rpm";
        static public string PRESET_8V = "PowerPreset_8V";
        static public string PRESET_18V = "PowerPreset_18V";
        static public string A_NO = "Pro_ANumber";
        static public string MA_NO = "MPro_ANumber";
        static public string MUD_NO_H = "Mud_NumberHead";
        static public string LASER_NO = "LaserNo";
        static public string LASER_SPEC = "LaserSpec";
        static public string LASER_MaxPt = "LaserMaxPt";
        //static public string LASER_N = "LaserSpecN";
        static public string Cor_Mass = "CorMass";

        public double db_PPreset_2000rpm = 0;
        public double db_PowerPre8v = 0;
        public double db_PowerPre18v = 0;
        public string str_ProANumber = "";
        public string str_MProANnumber = "";
        public string str_MudNumber = "";
        public string str_LaserNo = "";
        public double db_LaserSpec = 0;
        public int db_LaserMaxPt = 0;
        //public double db_LaserSpecN = 0;
        public double db_CorMass = 0;

        IniFile iniKind = new IniFile();//创建INI机种文件对象

        #endregion

        #region LOG记录相关定义

        TxtFile txtLog = new TxtFile();
        TxtFile txtFcData = new TxtFile();
        public string strLogLine = string.Empty;
        public string strFcDataLine = string.Empty;

        #endregion

        #region  参数定义

        //系统初始化完成标志
        public bool b_IsCommPlcInitComplete = false;
        public bool b_IsCommBlInitComplete = false;
        public bool b_IsCommFcInitComplete = false;
        public bool b_IsPowerInitComplete = false;
        public bool b_IsManual = false;
        public bool b_IsMasterChk = false;
        public bool b_IsManualFaceChk = false;
        public bool b_IsManualBlance = false;

        //文件相关定义
        public string TxtFilePath = "C:\\PHOENIX\\Sequence\\temp\\Start.txt";
        public string CurKindTxtFilePath = Application.StartupPath + "\\INI\\CurrentKind.txt";
        public string LogFilePath = "C:\\BalanceAdj\\";
        public string FcDataFilePath = "C:\\BalanceAdj\\FCDATA\\";

        /// <summary>
        /// 串口通信变量定义
        /// </summary>
        public string cmd_FromPLC = string.Empty;
        public string data_FromBalance = string.Empty;
        public string data_FromSurface = string.Empty;
        public double db_DataSurface = 0;
        static public string BAUDRATE   = "BaudRate";
        static public string DATABIT = "DataBit";
        static public string PARITY = "Parity";
        static public string STOPBIT = "StopBit";

        IniFile iniCom = new IniFile();//创建INI串口文件对象

        Comm comm_Balance = new Comm();
        Comm comm_Plc = new Comm();
        Comm comm_Surface = new Comm();
        /*
        private bool b_comm_Bl_IsExist = false;
        private bool b_comm_Pl_IsExist = false;
        private bool b_comm_Sf_IsExist = false;
        */
        public bool plc_ComDataRecIsEnable = false; //是否可以接收PLC串口数据
        public bool bal_ComDataRecIsEnable = false;
        public bool sur_ComDataRecIsEnable = false;
      
        /// <summary>
        /// 电源相关变量定义
        /// </summary>
        private const byte CH_A_36V = 1;    //通道A：36V
        private const byte CH_B_18V = 2;    //通道B：18V
        private const byte CH_C_8V = 3;     //通道C：8V
        private const byte PRESET_1 = 1;    //预设值
        private const byte PRESET_2 = 2;
        private const byte PRESET_3 = 3;
        private const byte PRESET_4 = 4;
        private const byte POWER_ON = 1;        //打开电源标示符
        private const byte POWER_OFF = 0;       //关闭电源标示符
        private int PowerDeviceId;              // 定义设备ID。
        double Voiltage_Value=0;
        public int rpm_2000 = 1;
        public int rpm_7200 = 2;
        public int rpm_type3 = 3;
               
        /// <summary>
        /// 面平衡传感器相关变量定义
        /// </summary>
        // CHART相关定义
        //private double tiltMaxValueP = 0.158;
        //private double tiltMaxValueN = -0.158;
        private Queue<double> dataQueue = new Queue<double>(1000);//定义一个数据队列
        private Queue<double> tiltMaxQueueP = new Queue<double>(1000);//定义一个数据队列
        private Queue<double> tiltMaxQueueN = new Queue<double>(1000);//定义一个数据队列
        public bool b_IsFirstFaceChk = true;
        private bool b_EnableRTS = true;//缓冲状态使能
        private string strStEdPtCnt = string.Empty;
        private const int DATALENTH = 1000;//数据数量
        public int iFaceDataCnt = 0;
        private int iCnt = 0;//数据计数
        public double[] face_data = new double[DATALENTH];//数据保存
        //private int curValue = 0;
        private int num = 1;//每次删除增加几个点
        public double rtvalue = -1;
        public Int16 iCachePtPos = 1;
        public bool b_IsFcEnable = false;

        //PLC相关定义
        public string str_PLCRecCache = string.Empty;

        //激光传感器指令相关定义
        //byte[] bt_SendData;
        byte[] bt_SendLaserCmd;
        byte[] bt_TmSendLaserCmd;
        private string strFaceComCmd=string.Empty;
        //private string strCmdBase2 = "%01#RMD**"+"\r";
        private string strCmdBase = "%01#**" + "\r";
        //private string strCmdtest = "%01#ROA**" + "\r";
        public double db_FcResult = 0;
        public double db_FcMax = -10;
        public double db_FcMin = 10;
        public double db_FcAvg = 0;
        private string str_FaceCacheStatus = string.Empty;
        private string str_FaceCacheCount = string.Empty;
        private string str_FaceCacheData = string.Empty;
        private string str_FaceDataTemp = string.Empty;
        private string str_cmdtemp = string.Empty;

        /// <summary>
        /// 动平衡仪相关变量定义
        /// </summary>
        DataGridViewRow dr_Before = new DataGridViewRow();
        DataGridViewRow dr_After = new DataGridViewRow();
        public int i_row = 0;         //数据写入默认第一行
        public int dr_Bef = 0;
        public int dr_Aft = 1;
        public int cell_RPM = 1;
        public int cell_VibAgl = 2;
        public int cell_VibAmt = 3;
        public int cell_CorAgl = 4;
        public int cell_CorMas = 5;
        public string strRpm = string.Empty;
        public string strVibAgl = string.Empty;
        public string strVibAmt = string.Empty;
        public string strCorAgl = string.Empty;
        public string strCormas = string.Empty;
        //public string strRpm = string.Empty;
        public double db_BlanceResult = 0;   
  
        #endregion

        #region 窗体控制
        //启动主应用程序
        private void MainForm_Load(object sender, EventArgs e)
        {
            App_Init();
            InitChart();
            Thread_Create();
            this.KeyPreview = true;//获取或设置一个值，该值指示在将键事件传递到具有焦点的控件前，窗体是否将接收此键事件。
            Control.CheckForIllegalCrossThreadCalls = false;    //不对线程的调用控件进行捕获
        }

        //退出主应用程序
        private void MainForm_Close(object sender, FormClosedEventArgs e)
        {
            comm_Surface.Close();
            comm_Balance.Close();
            comm_Plc.Close();
            Thread_Kill();
        }

        //创建线程
        private void Thread_Create()
        {
            //创建PLC命令处理线程            
            th_PhoenixCmdDeal = new Thread(PhoenixCmdDeal);//Phoenix通信线程
            th_PlcCmdDeal = new Thread(PlcCmdDeal);//PLC通信线程
            th_LaserDataChtShow = new Thread(LaserDataDeal);
            th_BalanceDataDeal = new Thread(BalanceDataDeal);
            th_ProcessDeal = new Thread(ProcessDeal);
            //th_ComFaceProcessDeal = new Thread(LaserDataDeal);
            
            th_PhoenixCmdDeal.Start();
            th_PlcCmdDeal.Start();
            th_LaserDataChtShow.Start();
            th_BalanceDataDeal.Start();
            th_ProcessDeal.Start();
            //th_ComFaceProcessDeal.Start();
            
        }

        //结束线程
        private void Thread_Kill()
        {
            //技术PLC命令处理线程
            
            th_PlcCmdDeal.Abort();
            th_LaserDataChtShow.Abort();
            th_BalanceDataDeal.Abort();
            th_ProcessDeal.Abort();
            //th_ComFaceProcessDeal.Abort();
            th_PhoenixCmdDeal.Abort();
            
        }

        //文本框变化事件处理(滚动条自动滚到最底端)
        private void txtBox_message_TextChanged(object sender, EventArgs e)
        {
            if (txtBox_message.Text.Length>100)
            {
                txtBox_message.Text.Remove(0, txtBox_message.Text.IndexOf("\r")+1);
            }
            txtBox_message.SelectionStart = txtBox_message.Text.Length;
            txtBox_message.ScrollToCaret(); 
        }

        //打开设置窗口        
        private void startFormSetting()
        {
            if (fmSetting == null)
            {
                this.Enabled = false;
                fmSetting = new Setting();
                fmSetting.Owner = this;
                fmSetting.StartPosition = FormStartPosition.CenterScreen;
                fmSetting.Show();
            }
            else
            {
                if (fmSetting.IsDisposed)
                {
                    this.Enabled = false;
                    fmSetting = new Setting();
                    fmSetting.Owner = this;
                    fmSetting.StartPosition = FormStartPosition.CenterScreen;
                    fmSetting.Show();
                }
            }
        }

        //显示产品二维码扫描窗口
        public void ShowFormProduct()
        {
            if (!cbx_IsManul.Checked)
            {
                this.Enabled = false;
                fmProduc = new form_ProductBarcode();
                fmProduc.Owner = this;
                fmProduc.StartPosition = FormStartPosition.CenterScreen;
                fmProduc.Show();
            }
        }

        //显示平衡泥二维码扫描窗口
        public void ShowFormMud()
        {
            this.Enabled = false;
            byte[] answer = System.Text.Encoding.Default.GetBytes("STOK");
            comm_Plc.WritePort(answer, 0, answer.Length);//回复PLC作业开始
            fmMud = new form_MudBarcode();
            fmMud.Owner = this;
            fmMud.StartPosition = FormStartPosition.CenterScreen;
            fmMud.Show();
        }
        #endregion
                
        public MainForm()
        {            
            InitializeComponent();            
        }
        
        #region 机种设置初始化
        //从INI读取机种信息
        public string ReadIniKind(string KindName)
        {
            string strPath = Application.StartupPath + "\\INI\\Kind.ini";
            //string strOne = System.IO.Path.GetFileNameWithoutExtension(strPath);//去除路径后的文件名
            string value = "";
            if (File.Exists(strPath))
            {
                value += iniKind.ReaderIni(KindName, PRESET_2000rpm, "", strPath) + ",";
                value += iniKind.ReaderIni(KindName, PRESET_8V, "", strPath) + ",";
                value += iniKind.ReaderIni(KindName, PRESET_18V, "", strPath) + ",";
                value += iniKind.ReaderIni(KindName, A_NO, "", strPath) + ",";
                value += iniKind.ReaderIni(KindName, MA_NO, "", strPath) + ",";
                value += iniKind.ReaderIni(KindName, MUD_NO_H, "", strPath) + ",";
                value += iniKind.ReaderIni(KindName, LASER_NO, "", strPath) + ",";
                value += iniKind.ReaderIni(KindName, LASER_SPEC, "", strPath) + ",";
                value += iniKind.ReaderIni(KindName, LASER_MaxPt, "", strPath) + ",";
                //value += iniKind.ReaderIni(KindName, LASER_N, "", strPath) + ",";
                value += iniKind.ReaderIni(KindName, Cor_Mass, "", strPath);
            }
            else
            {
                MessageBox.Show("机种配置文件[Kind.ini]未找到");
            }
            return value;
        }

        //机种信息初始化
        public void InitKind()
        {
            string[] strTemp;
            string value;
            str_KindName = ReadTxtFile(CurKindTxtFilePath);
            if (str_KindName != string.Empty)
            {
                lbl_CurrentKind.Text = str_KindName;
                value = ReadIniKind(str_KindName);//根据机种名从INI获取相关信息
                strTemp = value.Split(new Char[] { ',' });
                db_PPreset_2000rpm = Convert.ToDouble(strTemp[0]);
                db_PowerPre8v = Convert.ToDouble(strTemp[1]);
                db_PowerPre18v = Convert.ToDouble(strTemp[2]);
                str_ProANumber = strTemp[3];
                str_MProANnumber =strTemp[4];
                str_MudNumber = strTemp[5];
                str_LaserNo = strTemp[6];
                db_LaserSpec = Convert.ToDouble(strTemp[7]);
                db_LaserMaxPt = Convert.ToInt32(strTemp[8]);
                //db_LaserSpecN = Convert.ToDouble(strTemp[8]);
                db_CorMass = Convert.ToDouble(strTemp[9]);
                CatchPtFormat(1,50);
            }
            else
            {
                MessageBox.Show("未选定机种，请进入设置选择机种");
            }
        }
        #endregion

        #region 串口通信

        //从INI读取串口信息
        string ReadIniCom(string strComName)
        {
            string strPath = Application.StartupPath + "\\INI\\ComSetting.ini";
            //string strOne = System.IO.Path.GetFileNameWithoutExtension(strPath);//去除路径后的文件名
            string value = "";
            if (File.Exists(strPath))
            {
                value += iniCom.ReaderIni(strComName, BAUDRATE, "", strPath) + ",";
                value += iniCom.ReaderIni(strComName, DATABIT, "", strPath) + ",";
                value += iniCom.ReaderIni(strComName, PARITY, "", strPath) + ",";
                value += iniCom.ReaderIni(strComName, STOPBIT, "", strPath);
            }
            else
            {
                MessageBox.Show("串口配置文件[ComSetting.ini]未找到");
            }
            return value;
        }               

        //初始化串口
        private void InitComm()
        {
            string strPath = Application.StartupPath + "\\INI\\ComSetting.ini";
            string strOne = System.IO.Path.GetFileNameWithoutExtension(strPath);
            string comseting="";
            string[] value;
            
            string[] mySerial=SerialPort.GetPortNames();
            if (mySerial.Length > 0)
            {
                for (int i = 0; i < mySerial.Length; i++)
                {
                    switch (mySerial[i])
                    {
                        case "COM8"://动平衡仪
                            //从INI文件读取串口配置
                            comseting = ReadIniCom(mySerial[i]);
                            value = comseting.Split(new Char[] { ',' });

                            //初始化串口参数
                            comm_Balance.InitComm(mySerial[i], Convert.ToInt32(value[0]), Convert.ToInt32(value[1]), value[2], value[3]);

                            //初始化成功添加串口事件
                            if (comm_Balance.Open())
                            {
                                comm_Balance.DataReceived += new Comm.EventHandle(comm_BalanceDataReceived);
                                bal_ComDataRecIsEnable = true;
                                b_IsCommBlInitComplete = true;
                               
                            }
                            else
                            {
                                txtBox_message.Text = "Warn:COM8初始化失败" + "\r\n";
                                //MessageBox.Show("打开"+mySerial[i]+"失败");
                            }
                            break;
                        
                        case "COM2"://PLC
                            //从INI文件读取串口配置
                            comseting = ReadIniCom(mySerial[i]);
                            value = comseting.Split(new Char[] { ',' });

                            //初始化串口参数                            
                            comm_Plc.InitComm(mySerial[i], Convert.ToInt32(value[0]), Convert.ToInt32(value[1]), value[2], value[3]);
                            if (!comm_Plc.IsOpen)
                            {
                                try
                                {
                                    comm_Plc.Open();
                                    comm_Plc.DataReceived += new Comm.EventHandle(comm_PlcDataReceived);
                                    plc_ComDataRecIsEnable = true;
                                    b_IsCommPlcInitComplete = true;
                                }
                                catch 
                                { 
                                    txtBox_message.Text = "Warn:COM2初始化失败" + "\r\n"; 
                                }
                                /*
                                if (comm_Plc.Open())
                                {
                                    comm_Plc.DataReceived += new Comm.EventHandle(comm_PlcDataReceived);
                                    plc_ComDataRecIsEnable = true;
                                    b_IsCommPlcInitComplete = true;
                                }
                                else
                                {
                                    txtBox_message.Text = "Warn:COM2初始化失败" + "\r\n";
                                    //MessageBox.Show("打开" + mySerial[i] + "失败");
                                }
                                */
                            }
                            break;
                         
                        case "COM3"://面平衡激光传感器
                            //从INI文件读取串口配置                                
                            comseting = ReadIniCom(mySerial[i]);
                            value = comseting.Split(new Char[] { ',' });
                            //初始化串口参数
                            comm_Surface.InitComm(mySerial[i], Convert.ToInt32(value[0]), Convert.ToInt32(value[1]), value[2], value[3]);
                            if (comm_Surface.Open())
                            {
                                //bt_SendData = System.Text.Encoding.Default.GetBytes(strCmdBase);//读取激光传感器数据命令
                                comm_Surface.DataReceived += new Comm.EventHandle(comm_SurfaceDataReceived);
                                sur_ComDataRecIsEnable = true;
                                b_IsCommFcInitComplete = true;
                            }
                            else
                            {
                                txtBox_message.Text = "Warn:COM3初始化失败" + "\r\n";
                                //txtBox_message.Text += "打开" + mySerial[i] + "失败";                                    
                            }

                            break;
                        default:
                            break;
                    }
                }
                if (bal_ComDataRecIsEnable
                    && plc_ComDataRecIsEnable
                    && sur_ComDataRecIsEnable)
                {
                    txtBox_message.Text += "串口初始化成功" + "\r\n";
                }
                else
                {
                    if (!bal_ComDataRecIsEnable)
                    {
                        txtBox_message.Text += "Warn:COM1初始化失败" + "\r\n";
                    }
                    if (!plc_ComDataRecIsEnable)
                    {
                        txtBox_message.Text += "Warn:COM2初始化失败" + "\r\n";
                    }
                    if (!sur_ComDataRecIsEnable)
                    {
                        txtBox_message.Text += "Warn:COM8初始化失败" + "\r\n";
                    }
                }
            }
            else
            {
                txtBox_message.Text+="Warn:未检测到本机器串口"+"\r\n";
            }

        }
           
        //接收到动平衡仪数据处理
        void comm_BalanceDataReceived(byte[] readBuffer)
        {
            if (bal_ComDataRecIsEnable && b_ProcessEnable)//接收数据使能
            {
                bal_ComDataRecIsEnable = false;//停止接收数据
                if (readBuffer.Length >= 1)
                {
                    byte[] byteRead = readBuffer;
                    data_FromBalance = ByteToString(byteRead);
                    comm_Balance.serialPort.DiscardInBuffer();
                }
            }
            comm_Balance.serialPort.DiscardInBuffer();
        }

        //接收到PLC数据处理
        void comm_PlcDataReceived(byte[] readBuffer)
        {
            str_PLCRecCache += ByteToString(readBuffer);//读取COM缓存数据至字符串中
            comm_Plc.serialPort.DiscardInBuffer();      //清除COM缓存数据
            if (str_PLCRecCache.StartsWith("2")  //命令以02开头，03结尾
                && str_PLCRecCache.Contains("3")
                && plc_ComDataRecIsEnable)
            {
                int startpos = str_PLCRecCache.IndexOf("2");//起始符位置
                int endpos = str_PLCRecCache.IndexOf("3");//结束符位置
                //处理数据截取
                cmd_FromPLC = str_PLCRecCache.Substring(startpos + 1, endpos - 1);
                //删除已截取的数据
                if (endpos + 1 == str_PLCRecCache.Length)//只有1组命令
                {
                    str_PLCRecCache = string.Empty;
                }
                else//去除第一组命令之前数据
                {
                    //str_PLCRecCache = string.Empty;
                    str_PLCRecCache = str_PLCRecCache.Substring((endpos + 1), (str_PLCRecCache.Length - endpos - 1));
                }
                sur_ComDataRecIsEnable = false;                
            }            
        }
        
        //接收到面平衡传感器数据处理
        void comm_SurfaceDataReceived(byte[] readBuffer)
        {
            str_FaceDataTemp += ByteToString(readBuffer);//读取COM缓存数据至字符串中
            comm_Surface.serialPort.DiscardInBuffer();  //清除COM缓存数据
            if (str_FaceDataTemp.StartsWith("%")
                && str_FaceDataTemp.Contains("\r")
                && sur_ComDataRecIsEnable)
            {                
                int startpos = str_FaceDataTemp.IndexOf("%");//起始符位置
                int endpos = str_FaceDataTemp.IndexOf("\r");//结束符位置
                //处理数据截取
                strFaceComCmd = str_FaceDataTemp.Substring(startpos + 4, endpos - 6);
                //删除已截取的数据
                if (endpos + 1 == str_FaceDataTemp.Length)//只有1组**
                {
                    str_FaceDataTemp = string.Empty;
                }
                else//去除第一组**之前数据
                {
                    str_FaceDataTemp = str_FaceDataTemp.Substring((endpos + 1), (str_FaceDataTemp.Length - endpos - 1));
                }
                sur_ComDataRecIsEnable = false;
            }
            //txtBox_message.Text += data_FromSurface;//测试用       
        }

        #endregion

        #region 电源控制

        //电源初始化
        private void InitPow()
        {
            PowerDeviceId = TMI_Api.TMI_HandleOpen("PW-A", "USB:15:1");
            if (PowerDeviceId > 0)
            {
                TMI_Api.TMI_MainOutput(PowerDeviceId, POWER_OFF);//电源输出OFF
                TMI_Api.TMI_Voltage(PowerDeviceId, CH_B_18V, PRESET_1, db_PowerPre18v);//驱动电压12V
                TMI_Api.TMI_Voltage(PowerDeviceId, CH_C_8V, PRESET_1, db_PPreset_2000rpm);//速度控制初始电压0.8V 2000rpm

                TMI_Api.TMI_OutputSel(PowerDeviceId, CH_C_8V, POWER_ON);// 选择CHC作为输出
                TMI_Api.TMI_Display(PowerDeviceId, CH_C_8V);// CHC指示灯点亮
                txtBox_message.Text += "OK：电源连接正常" + "\r\n";
                b_IsPowerInitComplete = true;
            }
            else
            {
                txtBox_message.Text += "Warn:电源未连接" + "\r\n";
            }
        }

        //电源调速电压设置
        public void SetSpeedVoltage(int speed_type )
        {
            if (PowerDeviceId > 0)
            {
                switch (speed_type)
                {
                    case 1://2000rpm初始值
                        TMI_Api.TMI_Voltage(PowerDeviceId, CH_C_8V, PRESET_1, db_PPreset_2000rpm);//速度2000rpm初始电压1.0V
                        break;

                    case 2://7200rpm初始值
                        TMI_Api.TMI_Voltage(PowerDeviceId, CH_C_8V, PRESET_1, db_PowerPre8v);//速度7200rpm初始电压1.4V
                        break;
                }
            }
            else
            {
                txtBox_message.Text += "Warn:未检测到电源"+"\r\n";
            }
        }


        /// <summary>
        /// 电源输出控制
        /// </summary>
        /// <param name="OnOrOff">POWER_ON :打开电源   POWER_OFF:关闭电源</param>
        private void PowerControl(byte OnOrOff)
        {
            TMI_Api.TMI_MainOutput(PowerDeviceId, OnOrOff);
        }


        //调速电压调整(键盘按键响应事件 UP & DOWN)
        #endregion 

        #region 按钮功能
        
        //面平衡状态查询定时器
        private void timer_wave_Tick(object sender, EventArgs e)
        {
            //定时读取累计状态
            bt_TmSendLaserCmd = System.Text.Encoding.Default.GetBytes(Laser_Cmd_Package("RTS", "", -1, 5)); 
            comm_Surface.WritePort(bt_TmSendLaserCmd, 0, bt_TmSendLaserCmd.Length);
            //comm_Surface.WritePort(bt_SendData, 0, bt_SendData.Length);//读数据指令发送
        }

        //面平衡数据超时定时器
        private void timer_Face_Tick(object sender, EventArgs e)
        {
            StartFaceChk();             //开始面平衡检测
            Thread.Sleep(200);
            timer_wave.Start();         //启动定时器检测是否检测完成
        }

        //电压调整BAR
        private void trackBar_Vol_KeyDown(object sender, KeyEventArgs e)
        {
            if (b_ProcessEnable)
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        if (PowerDeviceId > 0)
                        {
                            TMI_Api.TMI_VoltageQ(PowerDeviceId, CH_C_8V, PRESET_1, out Voiltage_Value);
                            if (cb_HighSpeed.Checked)
                            {
                                Voiltage_Value -= 0.01;
                            }
                            else
                            {
                                Voiltage_Value -= 0.001;
                            }
                            TMI_Api.TMI_Voltage(PowerDeviceId, CH_C_8V, PRESET_1, Voiltage_Value);
                            lb_Voiltage.Text = Voiltage_Value.ToString();
                        }
                        else
                        {
                            MessageBox.Show("电源未连接！");
                        }
                        break;
                    case Keys.Down:
                        if (PowerDeviceId > 0)
                        {
                            TMI_Api.TMI_VoltageQ(PowerDeviceId, CH_C_8V, PRESET_1, out Voiltage_Value);
                            if (cb_HighSpeed.Checked)
                            {
                                Voiltage_Value += 0.01;
                            }
                            else
                            {
                                Voiltage_Value += 0.001;
                            }
                            TMI_Api.TMI_Voltage(PowerDeviceId, CH_C_8V, PRESET_1, Voiltage_Value);
                            lb_Voiltage.Text = Voiltage_Value.ToString();
                        }
                        else
                        {
                            MessageBox.Show("电源未连接！");
                        }
                        break;
                    case Keys.Enter:
                        if (!b_IsFcEnable)
                        {
                            b_IsFcEnable = true;
                        }
                        //faceOkEvent.Set();//确认转速后按回车
                        break;

                    case Keys.Escape:
                        //    faceNgEvent.Set();
                        break;
                }
            }
        }
        
        //开始运行按钮
        private void bt_Start_Click(object sender, EventArgs e)
        {
            if (bt_Start.Text == "开始运行")
            {
                bt_Start.Text = "停止运行";
                bt_Start.BackColor = Color.LightGreen;
                b_ProcessEnable=true;
                bt_Setting.Enabled = false;
                bt_Init.Enabled = false;
                bt_Power.Enabled = false;
                cbx_IsManul.Enabled = false;
                cbx_FaceChk.Enabled = false;
                //
                cbx_Balance.Enabled = false;
                cbx_MasterChk.Enabled = false;
                //初始化面平衡传感器
                InitFaceLaser();
            }
            else
            {
                b_ProcessEnable = false;
                bt_Start.Text = "开始运行";
                bt_Start.BackColor = Control.DefaultBackColor;
                bt_Setting.Enabled = true;
                bt_Init.Enabled = true;
                bt_Power.Enabled = true;
                cbx_IsManul.Enabled = true;
                if (cbx_IsManul.Checked)
                {
                    cbx_FaceChk.Enabled = true;
                    cbx_Balance.Enabled = true;
                }
                cbx_MasterChk.Enabled = true;
                if (str_ProcessCtl != "")
                {
                    str_ProcessCtl = "STOP";    //面平衡NG，停止调整
                }
            }
        }

        //设置按钮
        private void bt_Setting_Click(object sender, EventArgs e)
        {
            startFormSetting();
            this.timer_wave.Stop();
        }

        //手动
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_IsManul.Checked)
            {
                b_IsManual = true;//传递给扫描窗口
                b_ProcessEnable = true;
                cbx_FaceChk.Enabled = true;
                cbx_Balance.Enabled = true;
                cbx_MasterChk.Enabled = false;
                bt_Start.Enabled = false;
                bt_Setting.Enabled = false;
                bt_Init.Enabled = false;
                bt_Power.Enabled = false;
                //初始化面平衡传感器
                InitFaceLaser();
            }
            else
            {
                b_IsManual = false;//传递给扫描窗口
                b_ProcessEnable = false;
                b_IsManualFaceChk = false;
                b_IsManualBlance = false;
                cbx_FaceChk.Enabled = false;
                cbx_Balance.Enabled = false;
                cbx_FaceChk.Checked = false;
                cbx_Balance.Checked = false;
                cbx_MasterChk.Enabled = true;
                bt_Start.Enabled = true;
                bt_Setting.Enabled = true;
                bt_Init.Enabled = true;
                bt_Power.Enabled = true;
                if (str_ProcessCtl != "")
                {
                    str_ProcessCtl = "STOP";    //面平衡NG，停止调整
                }
            }
        }
        //点检模式
        private void cbx_MasterChk_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_MasterChk.Checked)
            {
                if (cbx_FaceChk.Checked)
                {
                    cbx_FaceChk.Enabled = false;
                }
                if (cbx_Balance.Checked)
                {
                    cbx_Balance.Enabled = false;
                }
                b_IsMasterChk = true;//传递给扫描窗口
                cbx_IsManul.Enabled = false;
                bt_Setting.Enabled = false;
            }
            else
            {
                if (cbx_FaceChk.Checked)
                {
                    cbx_FaceChk.Enabled = true;
                }
                if (cbx_Balance.Checked)
                {
                    cbx_Balance.Enabled = true;
                }
                b_IsMasterChk = false;//传递给扫描窗口
                cbx_IsManul.Enabled = true;
                bt_Setting.Enabled = true;
            }
        }
        //面平衡检查
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_FaceChk.Checked)
            {
                bt_Start.Enabled = true;
                b_IsManualFaceChk = true;
            }
            else
            {
                b_IsManualFaceChk = false;
                if (!cbx_Balance.Checked)
                {
                    bt_Start.Enabled = false;
                }
            }
        }

        //动平衡检查
        private void cbx_Balance_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_Balance.Checked)
            {
                b_IsManualBlance = true;
                bt_Start.Enabled = true;
            }
            else
            {
                if (!cbx_FaceChk.Checked)
                {
                    bt_Start.Enabled = false;
                }
                b_IsManualBlance = false;
            }
        }
        #endregion

        #region 功能函数

        //程序启动初始化
        private void App_Init()
        {
            //串口初始化
            InitComm();            

            //机种初始化
            InitKind();

            //创建LOG文件
            LogFileCreate();

            //电源初始化
            InitPow();                        

            //窗口初始化
            InitGridView();
        }

        //byte转换成string
        public string ByteToString(byte[] byteData)
        {
            return System.Text.Encoding.Default.GetString(byteData);
        }
        
        //读TXT文件
        public string ReadTxtFile( string FilePath)
        {
            //string filePath = Application.StartupPath + "\\TXT\\Phoenix.txt";
            try
            {
                if (File.Exists(FilePath))
                {
                    return File.ReadAllText(FilePath);
                }
                else
                {
                    txtBox_message.Text += "Phoenix通信文件不存在" + "\r\n";
                    return "ERR";
                }
            }
            catch (Exception ex)
            {
                txtBox_message.Text += ex.Message + "\r\n";
                return "ERR";
            }
        }

        //写TXT文件
        public void WriteTxtFile(string data)
        {
            //检测文件夹是否存在，不存在则创建
            if (File.Exists(TxtFilePath))
            {
                //定义编码方式
                byte[] mybyte = Encoding.UTF8.GetBytes(data);
                string mystr1 = Encoding.UTF8.GetString(mybyte);
                //写入文件
                //File.AppendAllText(filePath, mystr1); //添加至文件
                File.WriteAllText(TxtFilePath, mystr1);    //写入并覆盖文件内容
            }
        }

        //获取子窗口的产品序列号和平衡泥号码
        public void GetProMudNum(string strProNum)
        {
            strLogLine += strProNum;
        }

        //清除串口缓存字符串
        public void ClearComCache()
        {
            data_FromBalance = string.Empty;
            str_FaceDataTemp = string.Empty;
            str_PLCRecCache = string.Empty;
        }

        //紧急停止后处理
        void PLCEMG_Deal()
        {

        }

        #endregion

        #region LOG文件记录
        
        //创建LOG文件
        private void LogFileCreate()
        {
            LogFilePath += DateTime.Today.ToString("yyyy");
            if (!Directory.Exists(LogFilePath))
            {
                Directory.CreateDirectory(LogFilePath);             
            }
            if (!File.Exists(LogFilePath + "\\" + DateTime.Today.ToString("MM-dd") + ".txt"))
            {
                File.WriteAllText(LogFilePath + "\\" + DateTime.Today.ToString("MM-dd") + ".txt", null);
            }

            //创建面平衡数据记录文件
            FcDataFilePath += DateTime.Today.ToString("yyyy");
            if (!Directory.Exists(FcDataFilePath))
            {
                Directory.CreateDirectory(FcDataFilePath);
            }
            if (!File.Exists(FcDataFilePath + "\\" + DateTime.Today.ToString("MM-dd") + ".txt"))
            {
                File.WriteAllText(FcDataFilePath + "\\" + DateTime.Today.ToString("MM-dd") + ".txt", null);
            } 
        }

        //记录面平衡数据
        private void FaceDataAdd(bool IsOK)
        {
            if (!IsOK)
            {
                strLogLine = strLogLine.Insert(0, "NG,");//面平衡NG提示符插入
            }
            strLogLine += db_FcResult.ToString() + "," + db_FcMax.ToString() + "," + db_FcMin.ToString() + ",";
        }

        //记录动平衡数据
        private void BalanceDataAdd()
        {
            if (i_row == 0)
            {
                strLogLine = strLogLine.Insert(0, "OK,");
            }
            //test
            strLogLine += strRpm + "," + strVibAgl + "," + strVibAmt + "," + strCorAgl + "," + strCormas + ",";
        }

        //记录东平衡数据
        private void SaveLog()
        {
            string curdate = DateTime.Today.ToString("yyyy-MM-dd") + "," + DateTime.Now.ToString("hh:mm:ss") + ",";
            strLogLine += "\r\n";
            strLogLine = strLogLine.Insert(3, curdate);
            LogFilePath += "\\" + DateTime.Today.ToString("MM-dd") + ".txt";
            txtLog.WriteTxtFile(strLogLine, LogFilePath, true);
            strLogLine = string.Empty;
            LogFilePath = string.Empty;
        }
        //清除数据暂存字符串
        private void CleanLogStr()
        {
            strLogLine = string.Empty;
        }

        #endregion
            
        #region 动平衡相关
        //DATAGRIDVIEW初始化
        public void InitGridView()
        {
            int index1 = dgv_Balance.Rows.Add(dr_Before);
            int index2 = dgv_Balance.Rows.Add(dr_After);
            dgv_Balance.Rows[index1].Cells[0].Value = "调整前";
            dgv_Balance.Rows[index2].Cells[0].Value = "调整后";
        }

        //DATAGRIDVIEW数据写入
        public void WriteGridView()
        {
            if(data_FromBalance.StartsWith("\r")    //字符串以0D开头
                &&data_FromBalance.EndsWith("\n")   //字符串以0A结尾
                &&data_FromBalance.Length==177)     //字符串长度176
            {
                strRpm = data_FromBalance.Substring(30, 4);//转速从30位开始，长度4位(XXXX)
                strVibAgl = data_FromBalance.Substring(34, 3);//角偏移量从34位开始，长度3位(XXX)
                strVibAmt = data_FromBalance.Substring(37, 5);//重心偏差从34位开始，长度5位(X.XXX)
                strCorAgl = data_FromBalance.Substring(66, 3);//角补正从66位开始，长度3位(XXX)
                strCormas = data_FromBalance.Substring(69, 5);//重心补正从69位开始，长度3位(XXX)
                dgv_Balance.Rows[i_row].Cells[cell_RPM].Value = strRpm;
                dgv_Balance.Rows[i_row].Cells[cell_VibAgl].Value = strVibAgl;
                dgv_Balance.Rows[i_row].Cells[cell_VibAmt].Value = strVibAmt;
                dgv_Balance.Rows[i_row].Cells[cell_CorAgl].Value = strCorAgl;
                dgv_Balance.Rows[i_row].Cells[cell_CorMas].Value = strCormas;
                db_BlanceResult = Convert.ToDouble( data_FromBalance.Substring(69, 5)); //平衡泥补正量
                if (db_BlanceResult > db_CorMass)//超出规格背景为红色
                {
                    dgv_Balance[cell_CorMas, i_row].Style.BackColor = Color.LightPink;//重心补正量单元格背景色设置
                    //str_ProcessCtl = "BCHK";
                }
                else if((db_BlanceResult < db_CorMass)
                        &&(db_CorMass>0))
                {
                    dgv_Balance[cell_CorMas, i_row].Style.BackColor = Color.LightGreen;//规格内背景为绿色
                    //str_ProcessCtl = "STOP";//结束作业流程
                }
            }
        }

        //清除GridView
        public void ClearGridView()
        {
            for(int i=0;i<2;i++)
            {
                dgv_Balance.Rows[i].Cells[cell_RPM].Value = string.Empty;
                dgv_Balance.Rows[i].Cells[cell_VibAgl].Value = string.Empty;
                dgv_Balance.Rows[i].Cells[cell_VibAmt].Value = string.Empty;
                dgv_Balance.Rows[i].Cells[cell_CorAgl].Value = string.Empty;
                dgv_Balance.Rows[i].Cells[cell_CorMas].Value = string.Empty;
                dgv_Balance[cell_CorMas, i].Style.BackColor = Color.White;
            }
            i_row = 0;//定位到第一行
        }

        //动平衡仪数据处理(th_BalanceDataDeal线程调用)
        public void BalanceDataDeal()
        {
            MethodInvoker MethInvo = new MethodInvoker(WriteGridView);
            while (true)
            {
                if (!bal_ComDataRecIsEnable)
                {                    
                    BeginInvoke(MethInvo);
                    bal_ComDataRecIsEnable = true;
                }
                Thread.Sleep(50);
            }
        }
        #endregion

        #region PLC通信相关
        //Plc命令处理(th_PlcCmdDeal线程调用)
        public void PlcCmdDeal()
        {
            MethodInvoker MethInvo = new MethodInvoker(ShowFormProduct);
            while(true)
            {                
                switch (cmd_FromPLC)
                {
                    case "START":
                        cmd_FromPLC = "";

                        if (!cbx_IsManul.Checked )//非手动模式打开扫码窗口（自动&点检）
                        {
                            BeginInvoke(MethInvo);                                                      
                        }
                        else
                        {
                            if (b_IsManualFaceChk && b_IsManualBlance)//手动动平衡 && 面平衡
                            {
                                str_ProcessCtl = "START";
                            }
                            else if (b_IsManualFaceChk && !b_IsManualBlance)//手动面平衡
                            {
                                str_ProcessCtl = "START";
                            }
                            else if (!b_IsManualFaceChk && b_IsManualBlance)//手动动平衡
                            {
                                str_ProcessCtl = "BADJ";
                            }
                        }
                        plc_ComDataRecIsEnable = true;
                        break;

                    case "ORG":
                        //ORG命令处理
                        cmd_FromPLC = "";
                        plc_ComDataRecIsEnable = true;
                        break;

                    case "EMG":                        
                        cmd_FromPLC = "";
                        str_ProcessCtl = "STOP";//作业介绍
                        plc_ComDataRecIsEnable = true;
                        break;

                    case "STOP":                        
                        cmd_FromPLC = "";
                        
                        plc_ComDataRecIsEnable = true;
                        break;

                    case "BCHK":
                        cmd_FromPLC = "";
                        str_ProcessCtl = "BCHK";
                        plc_ComDataRecIsEnable = true;
                        break;

                     default:
                         cmd_FromPLC = "";
                         plc_ComDataRecIsEnable = true;
                         break;                    
                }
                Thread.Sleep(50);
            }
        }

        //PLC应答指令封装 0x02开头 0x03结尾
        public string PlcCmdPackage(string PlcCmd)
        {
            byte[] tmp = new byte[2];
            tmp[0] = 0x02;
            tmp[1] = 0x03;
            string temp = ByteToString(tmp);
            temp = temp.Insert(1, PlcCmd);
            return temp;
        }
        #endregion

        #region 面平衡相关
        
        //波形图表初始化
        private void InitChart()
        {
            //定义图表区域
            this.cht_FaceWave.ChartAreas.Clear();
            ChartArea chartArea1 = new ChartArea("C1");
            this.cht_FaceWave.ChartAreas.Add(chartArea1);

            //定义存储和显示点的容器
            this.cht_FaceWave.Series.Clear();
            Series series_TiltX_Value = new Series("S1");    //创建数据面平衡序列
            Series series_TiltLimitP = new Series("S2");     //创建上限值序列
            Series series_TiltLimitN = new Series("S3");     //创建上限值序列
            series_TiltX_Value.ChartArea = "C1";
            series_TiltLimitP.ChartArea = "C1";
            series_TiltLimitN.ChartArea = "C1";
            cht_FaceWave.Series.Add(series_TiltX_Value);
            cht_FaceWave.Series.Add(series_TiltLimitP);
            cht_FaceWave.Series.Add(series_TiltLimitN);

            //设置图表显示样式
            cht_FaceWave.ChartAreas[0].AxisX.Enabled = AxisEnabled.True;
            cht_FaceWave.ChartAreas[0].AxisX.LabelStyle.Enabled = true;
            cht_FaceWave.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Black;
            cht_FaceWave.ChartAreas[0].AxisX.MajorTickMark.LineColor = Color.White;
            cht_FaceWave.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            cht_FaceWave.ChartAreas[0].AxisY.LabelStyle.Enabled = true;
            cht_FaceWave.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;
            cht_FaceWave.ChartAreas[0].AxisY.MajorTickMark.LineColor = Color.White;
            //初始化值
            //cht_FaceWave.ChartAreas[0].AxisY.Minimum = -0.8;//从机种INI中获取上下限
            //cht_FaceWave.ChartAreas[0].AxisY.Maximum = 0.8;
            cht_FaceWave.ChartAreas[0].AxisY.Interval = 0.1;
            cht_FaceWave.ChartAreas[0].AxisX.Interval = 1;
            cht_FaceWave.ChartAreas[0].BackColor = Color.Black;

            this.cht_FaceWave.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            this.cht_FaceWave.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;

            //设置标题
            cht_FaceWave.Titles.Clear();
            cht_FaceWave.Titles.Add("S01");
            cht_FaceWave.Titles[0].Text = "Wave Of Planeness";
            cht_FaceWave.Titles[0].ForeColor = Color.White;
            cht_FaceWave.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);

            //设置图表显示样式
            cht_FaceWave.Series[0].Color = Color.Red;
            cht_FaceWave.Series[0].ChartType = SeriesChartType.Line;
            cht_FaceWave.Series[0].Points.Clear();
            cht_FaceWave.Series[0].BorderWidth = 2;

            cht_FaceWave.Series[1].Color = Color.Yellow;
            cht_FaceWave.Series[1].ChartType = SeriesChartType.Line;
            cht_FaceWave.Series[1].Points.Clear();
            cht_FaceWave.Series[1].BorderWidth = 2;

            cht_FaceWave.Series[2].Color = Color.Yellow;
            cht_FaceWave.Series[2].ChartType = SeriesChartType.Line;
            cht_FaceWave.Series[2].Points.Clear();
            cht_FaceWave.Series[2].BorderWidth = 2;

            //显示上限标示线
            /*
            this.cht_FaceWave.Series[1].Points.Clear();
            for (int i = 0; i < 200; i++)
            {
                tiltMaxQueueP.Enqueue(db_LaserSpecD);
            }
            for (int i = 0; i < tiltMaxQueueP.Count; i++)
            {
                this.cht_FaceWave.Series[1].Points.AddXY((i + 1), db_LaserSpecD);//从机种INI获取上下限值
            }
            //显示下限标示线
            this.cht_FaceWave.Series[2].Points.Clear();
            for (int i = 0; i < 200; i++)
            {
                tiltMaxQueueN.Enqueue(db_LaserSpecN);
            }
            for (int i = 0; i < tiltMaxQueueP.Count; i++)
            {
                this.cht_FaceWave.Series[2].Points.AddXY((i + 1), db_LaserSpecN);//从机种INI获取上下限值
            }
            */ 
        }

        //更新图表队列中的值
        private void UpdateQueueValue()
        {
            if (dataQueue.Count > 1000)
            {
                //先出列
                for (int i = 0; i < num; i++)
                {
                    dataQueue.Dequeue();
                }
            }
            for (int i = 0; i < num; i++)
            {
                dataQueue.Enqueue(db_DataSurface);
            }
        }
                
        //激光传感器命令封装
        public string Laser_Cmd_Package(string Cmd,string Symbol,int Data,int DataLenth)
        {
             
            string cmdtmp;
            cmdtmp = strCmdBase.Insert(strCmdBase.IndexOf("#")+1, Cmd);
            if (Symbol != string.Empty)
            {
                cmdtmp = cmdtmp.Insert(strCmdBase.IndexOf("#") + 4, "+");
                if (Data >= 0)
                {
                    cmdtmp = cmdtmp.Insert(strCmdBase.IndexOf("#") + 5, Data.ToString().PadLeft(DataLenth, '0'));
                }
            }
            else
            {
                if (Data >= 0)
                {
                    cmdtmp = cmdtmp.Insert(strCmdBase.IndexOf("#") + 4, Data.ToString().PadLeft(DataLenth, '0'));
                }
            }
            return cmdtmp;
            //bt_SendLaserCmd = System.Text.Encoding.Default.GetBytes(cmdtmp);//读取激光传感器数据命令            
        }

        //面平衡累计点数格式化
        public int CatchPtFormat(int startpos,int cnt)
        {
            string tmp=string.Empty;
            tmp = startpos.ToString();
            tmp += (startpos+cnt-1).ToString().PadLeft(5, '0');//每次读50个点
            return Convert.ToInt32(tmp);
        }

        //面平衡检测数据计算
        public double FaceDataCal(string mydata)
        {
            double value = 0;            
            int Ppos = 0;
            if (mydata!="")
            {
                rtvalue = 1;
                for (int i = 0; i < 50; i++)
                {
                    if (i == 0 )//第一个数据
                    {
                        if (mydata.Substring(0, 1) == "+")//正数
                        {
                            value += Math.Round(Convert.ToDouble(mydata.Substring(1, 7)) / 10000, 4);
                        }
                        else
                        {
                            value += -Math.Round(Convert.ToDouble(mydata.Substring(1, 7)) / 10000, 4);
                        }
                        face_data[iFaceDataCnt] = Math.Round(value, 4);//记录数据
                        if (value > db_FcMax)//最大值比较
                        {
                            db_FcMax = value;
                        }
                        if (value < db_FcMin)//最小值比较
                        {
                            db_FcMin = value;
                        }
                        mydata = mydata.Substring(8, mydata.Length - 8);//删除第一个数据
                    }
                    else//差分数据处理
                    {
                        if (mydata.IndexOf('+') >= 0)//有正数的情况
                        {
                            if (mydata.IndexOf('+') == 0)//下一个是正数
                            {
                                mydata = mydata.Substring(1, mydata.Length - 1);//删除最前面的符号
                                //Ppos = mydata.IndexOf('+');
                                if (mydata.IndexOf('+') > 0 || mydata.IndexOf('-') > 0)//不是最后一个数
                                {
                                    if (mydata.IndexOf('+') > 0 && mydata.IndexOf('-') > 0)//有正有负
                                    {
                                        if (mydata.IndexOf('+') > mydata.IndexOf('-'))//前面还有负数
                                        {
                                            Ppos = mydata.IndexOf('-');//
                                            value += Math.Round(Convert.ToDouble(mydata.Substring(0, Ppos)) / 10000, 4);
                                            face_data[iFaceDataCnt] = Math.Round(value,4);//记录数据
                                            if (value > db_FcMax)//最大值比较
                                            {
                                                db_FcMax = value;
                                            }
                                            if (value < db_FcMin)//最小值比较
                                            {
                                                db_FcMin = value;
                                            }
                                            mydata = mydata.Substring(Ppos, mydata.Length - Ppos);
                                        }
                                        else//用下一个'+'号位置定位
                                        {
                                            Ppos = mydata.IndexOf('+');//
                                            value += Math.Round(Convert.ToDouble(mydata.Substring(0, Ppos)) / 10000, 4);
                                            face_data[iFaceDataCnt] = Math.Round(value, 4);//记录数据
                                            if (value > db_FcMax)//最大值比较
                                            {
                                                db_FcMax = value;
                                            }
                                            if (value < db_FcMin)//最小值比较
                                            {
                                                db_FcMin = value;
                                            }
                                            mydata = mydata.Substring(Ppos, mydata.Length - Ppos);
                                        }
                                    }
                                    else if (mydata.IndexOf('+') > 0 && mydata.IndexOf('-') < 0)//全正
                                    {
                                        Ppos = mydata.IndexOf('+');//
                                        value += Math.Round(Convert.ToDouble(mydata.Substring(0, Ppos)) / 10000, 4);
                                        face_data[iFaceDataCnt] = Math.Round(value, 4);//记录数据
                                        if (value > db_FcMax)//最大值比较
                                        {
                                            db_FcMax = value;
                                        }
                                        if (value < db_FcMin)//最小值比较
                                        {
                                            db_FcMin = value;
                                        }
                                        mydata = mydata.Substring(Ppos, mydata.Length - Ppos);
                                    }
                                    else//全负
                                    {
                                        Ppos = mydata.IndexOf('-');
                                        value += Math.Round(Convert.ToDouble(mydata.Substring(0, Ppos)) / 10000, 4);
                                        face_data[iFaceDataCnt] = Math.Round(value, 4);//记录数据
                                        if (value > db_FcMax)//最大值比较
                                        {
                                            db_FcMax = value;
                                        }
                                        if (value < db_FcMin)//最小值比较
                                        {
                                            db_FcMin = value;
                                        }
                                        mydata = mydata.Substring(Ppos, mydata.Length - Ppos);
                                    }
                                }
                                else//最后一个数
                                {
                                    value += Math.Round(Convert.ToDouble(mydata) / 10000,4);
                                    face_data[iFaceDataCnt] = Math.Round(value, 4);//记录数据
                                    if (value > db_FcMax)//最大值比较
                                    {
                                        db_FcMax = value;
                                    }
                                    if (value < db_FcMin)//最小值比较
                                    {
                                        db_FcMin = value;
                                    }
                                    mydata = string.Empty;
                                }
                                Ppos = 0;
                            }
                            else//下一个是负数
                            {
                                mydata = mydata.Substring(1, mydata.Length - 1);//删除符号
                                if (mydata.IndexOf('+') > 0 || mydata.IndexOf('-') > 0)//不是最后一个数                                
                                {
                                    if (mydata.IndexOf('+') > 0 && mydata.IndexOf('-') > 0)//有正有负
                                    {
                                        if (mydata.IndexOf('+') > mydata.IndexOf('-'))//前面还有负数
                                        {
                                            Ppos = mydata.IndexOf('-');//
                                            value -= Math.Round(Convert.ToDouble(mydata.Substring(0, Ppos)) / 10000, 4);
                                            face_data[iFaceDataCnt] = Math.Round(value, 4);//记录数据
                                            if (value > db_FcMax)//最大值比较
                                            {
                                                db_FcMax = value;
                                            }
                                            if (value < db_FcMin)//最小值比较
                                            {
                                                db_FcMin = value;
                                            }
                                            mydata = mydata.Substring(Ppos, mydata.Length - Ppos);
                                        }
                                        else//用下一个'+'号位置定位
                                        {
                                            Ppos = mydata.IndexOf('+');//
                                            value -= Math.Round(Convert.ToDouble(mydata.Substring(0, Ppos)) / 10000, 4);
                                            face_data[iFaceDataCnt] = Math.Round(value, 4);//记录数据
                                            if (value > db_FcMax)//最大值比较
                                            {
                                                db_FcMax = value;
                                            }
                                            if (value < db_FcMin)//最小值比较
                                            {
                                                db_FcMin = value;
                                            }
                                            mydata = mydata.Substring(Ppos, mydata.Length - Ppos);
                                        }
                                    }
                                    else if (mydata.IndexOf('+') > 0 && mydata.IndexOf('-') < 0)//全正
                                    {
                                        Ppos = mydata.IndexOf('+');//
                                        value -= Math.Round(Convert.ToDouble(mydata.Substring(0, Ppos)) / 10000, 4);
                                        face_data[iFaceDataCnt] = Math.Round(value, 4);//记录数据
                                        if (value > db_FcMax)//最大值比较
                                        {
                                            db_FcMax = value;
                                        }
                                        if (value < db_FcMin)//最小值比较
                                        {
                                            db_FcMin = value;
                                        }
                                        mydata = mydata.Substring(Ppos, mydata.Length - Ppos);
                                    }
                                    else//全负
                                    {
                                        Ppos = mydata.IndexOf('-');//
                                        value -= Math.Round(Convert.ToDouble(mydata.Substring(0, Ppos)) / 10000, 4);
                                        face_data[iFaceDataCnt] = Math.Round(value, 4);//记录数据
                                        if (value > db_FcMax)//最大值比较
                                        {
                                            db_FcMax = value;
                                        }
                                        if (value < db_FcMin)//最小值比较
                                        {
                                            db_FcMin = value;
                                        }
                                        mydata = mydata.Substring(Ppos, mydata.Length - Ppos);
                                    }
                                }
                                else//最后一个数
                                {
                                    value -= Math.Round(Convert.ToDouble(mydata) / 10000,4);
                                    face_data[iFaceDataCnt] = Math.Round(value, 4);//记录数据
                                    if (value > db_FcMax)//最大值比较
                                    {
                                        db_FcMax = value;
                                    }
                                    if (value < db_FcMin)//最小值比较
                                    {
                                        db_FcMin = value;
                                    }
                                    mydata = string.Empty;
                                }
                                Ppos = 0;
                            }
                        }
                        else//没有正数的情况
                        {
                            mydata = mydata.Substring(1, mydata.Length - 1);//删除最前面的符号
                            Ppos = mydata.IndexOf('-');
                            if (Ppos > 0)//后面还有数据
                            {
                                value -= Math.Round(Convert.ToDouble(mydata.Substring(0, Ppos)) / 10000,4);
                                face_data[iFaceDataCnt] = Math.Round(value, 4);//记录数据
                                if (value > db_FcMax)//最大值比较
                                {
                                    db_FcMax = value;
                                }
                                if (value < db_FcMin)//最小值比较
                                {
                                    db_FcMin = value;
                                }
                                mydata = mydata.Substring(Ppos, mydata.Length - Ppos);
                                Ppos = 0;
                            }
                            else//最后一个数
                            {
                                value -= Math.Round(Convert.ToDouble(mydata) / 10000,4);
                                face_data[iFaceDataCnt] = Math.Round(value, 4);//记录数据
                                if (value > db_FcMax)//最大值比较
                                {
                                    db_FcMax = value;
                                }
                                if (value < db_FcMin)//最小值比较
                                {
                                    db_FcMin = value;
                                }
                                mydata = string.Empty;
                                Ppos = 0;
                            }
                        }
                    }
                    iFaceDataCnt++;
                }
            }
            mydata = string.Empty;
            return rtvalue;
        }

        //清除检测数据
        public void FaceDataClear()
        {
            for (int i = 0; i < DATALENTH; i++)
            {
                face_data[i] = 0;
            }
        }

        //图表显示
        public void ShowChart()
        {
            this.cht_FaceWave.Series[0].Points.Clear();
            this.cht_FaceWave.ChartAreas[0].AxisY.Minimum = db_FcMin;//从机种INI中获取上下限
            this.cht_FaceWave.ChartAreas[0].AxisY.Maximum = db_FcMax;
            for (int i = 0; i < db_LaserMaxPt; i++)
            {
                this.cht_FaceWave.Series[0].Points.AddXY(i + 1, face_data[i]);
            }
            FaceDataClear();
            db_FcResult = 0;
            db_FcMax = -10;
            db_FcMin = 10;
        }

        //保存面平衡数据至文件中
        public void SaveFaceDataToFile()
        {
            for (int i = 0; i < db_LaserMaxPt; i++)
            {
                strFcDataLine += Math.Round( face_data[i],3).ToString()+",";
            }
            strFcDataLine += "\r\n";
            txtFcData.WriteTxtFile(strFcDataLine, FcDataFilePath + "\\" + DateTime.Today.ToString("MM-dd") + ".txt", true);
            strFcDataLine = string.Empty;
            //ShowChart();            
        }

        //激光传感器数据处理(th_LaserDataChtShow线程调用)
        public void LaserDataDeal()
        {
            int iRLB_PosNum = 0;
            while (true)
            {
                if (strFaceComCmd.Length >= 3
                    && !sur_ComDataRecIsEnable)
                {
                    str_cmdtemp = strFaceComCmd.Substring(0, 3);//前3位是命令
                }
                switch (str_cmdtemp)
                {
                    case "WBS"://缓冲开始指令后清除应答信息  `
                        str_cmdtemp = string.Empty;
                        strFaceComCmd = string.Empty;

                        //测试用////
                        Thread.Sleep(6000);
                        bt_SendLaserCmd = System.Text.Encoding.Default.GetBytes(Laser_Cmd_Package("RLD", "", -1, 0));
                        comm_Surface.WritePort(bt_SendLaserCmd, 0, bt_SendLaserCmd.Length);
                        ///////////////////
                        
                        sur_ComDataRecIsEnable = true;
                        break;

                    case "WBD"://缓冲初始化开始
                        str_cmdtemp = string.Empty;
                        strFaceComCmd = string.Empty;
                        bt_SendLaserCmd = System.Text.Encoding.Default.GetBytes(Laser_Cmd_Package("WBR", "+", 1, 5));//缓冲率为1
                        comm_Surface.WritePort(bt_SendLaserCmd, 0, bt_SendLaserCmd.Length);
                        sur_ComDataRecIsEnable = true;
                        break;

                    case "WBR":
                        str_cmdtemp = string.Empty;
                        strFaceComCmd = string.Empty;
                        bt_SendLaserCmd = System.Text.Encoding.Default.GetBytes(Laser_Cmd_Package("WBC", "+", db_LaserMaxPt, 5));//缓冲累计数1000
                        comm_Surface.WritePort(bt_SendLaserCmd, 0, bt_SendLaserCmd.Length);
                        sur_ComDataRecIsEnable = true;
                        break;

                    case "WBC":
                        str_cmdtemp = string.Empty;
                        strFaceComCmd = string.Empty;
                        bt_SendLaserCmd = System.Text.Encoding.Default.GetBytes(Laser_Cmd_Package("WTP", "+", 500, 5));//缓冲触发点500
                        comm_Surface.WritePort(bt_SendLaserCmd, 0, bt_SendLaserCmd.Length);
                        sur_ComDataRecIsEnable = true;
                        break;

                    case "WTP":
                        str_cmdtemp = string.Empty;
                        strFaceComCmd = string.Empty;
                        bt_SendLaserCmd = System.Text.Encoding.Default.GetBytes(Laser_Cmd_Package("WTL", "+", 0, 5));//触发延时0
                        comm_Surface.WritePort(bt_SendLaserCmd, 0, bt_SendLaserCmd.Length);
                        sur_ComDataRecIsEnable = true;
                        break;

                    case "WTL":
                        str_cmdtemp = string.Empty;
                        strFaceComCmd = string.Empty;
                        bt_SendLaserCmd = System.Text.Encoding.Default.GetBytes(Laser_Cmd_Package("WTR", "+", 0, 5));//输入定时ON
                        comm_Surface.WritePort(bt_SendLaserCmd, 0, bt_SendLaserCmd.Length);
                        sur_ComDataRecIsEnable = true;
                        break;

                    case "WTR":
                        str_cmdtemp = string.Empty;
                        strFaceComCmd = string.Empty;
                        bt_SendLaserCmd = System.Text.Encoding.Default.GetBytes(Laser_Cmd_Package("WBL", "+", 0, 7));//阀值设置为0
                        comm_Surface.WritePort(bt_SendLaserCmd, 0, bt_SendLaserCmd.Length);
                        sur_ComDataRecIsEnable = true;
                        break;

                    case "WBL"://缓冲初始化结束
                        str_cmdtemp = string.Empty;
                        strFaceComCmd = string.Empty;
                        sur_ComDataRecIsEnable = true;

                        break;

                    case "RTS"://缓冲状态查询，由定时器发送
                        str_cmdtemp = string.Empty;
                        str_FaceCacheStatus = strFaceComCmd.Substring(8, 1);    //截取缓存状态
                        strFaceComCmd = string.Empty;
                        if (str_FaceCacheStatus == "3"
                            && b_EnableRTS)//缓冲完成&RTS允许
                        {
                            b_EnableRTS = false;
                            str_FaceDataTemp = string.Empty;//清空接受缓存字符串
                            bt_SendLaserCmd = System.Text.Encoding.Default.GetBytes(Laser_Cmd_Package("RLD", "", -1, 0));
                            comm_Surface.WritePort(bt_SendLaserCmd, 0, bt_SendLaserCmd.Length);
                        }
                        sur_ComDataRecIsEnable = true;
                        break;
                    ////////////////////////////
                    ////////////////////////////
                    case "RLD":
                        str_cmdtemp = string.Empty;
                        //
                        iCachePtPos = Convert.ToInt16(strFaceComCmd.Substring(4, 5));
                        if (iCachePtPos >= db_LaserMaxPt
                            &&iCachePtPos <= 3000)
                        {
                            iRLB_PosNum = CatchPtFormat(iCachePtPos - db_LaserMaxPt + 1, 50);//计算起始点和结束点位置
                        }
                        else
                        {
                            iRLB_PosNum = CatchPtFormat(3000 - db_LaserMaxPt + iCachePtPos + 1, 50);//最多3000个点
                        }
                        iCnt++;
                        bt_SendLaserCmd = System.Text.Encoding.Default.GetBytes(Laser_Cmd_Package("RLB", "", iRLB_PosNum, 10));
                        comm_Surface.WritePort(bt_SendLaserCmd, 0, bt_SendLaserCmd.Length);
                        Thread.Sleep(80);
                        sur_ComDataRecIsEnable = true;
                        break;

                    case "RLB":
                        str_cmdtemp = string.Empty;
                        if (strFaceComCmd.Length > 3)
                        {
                            str_FaceCacheData = strFaceComCmd.Substring(3, strFaceComCmd.Length - 3);    //去头去尾得到数据
                        }
                        if (iCnt > 0 && str_FaceCacheData.Length > 1)
                        {
                            FaceDataCal(str_FaceCacheData);//面平衡数据处理******************************************************************************************************
                        }
                        if (iCnt < (db_LaserMaxPt / 50))//每次读取50个点
                        {
                            //iRLB_PosNum = CatchPtFormat(iCnt * 50 + 1, 50);//计算起始点和结束点位置
                            if (iCachePtPos >= db_LaserMaxPt
                                && iCachePtPos <= 3000)
                            {
                                iRLB_PosNum = CatchPtFormat((iCachePtPos - db_LaserMaxPt) + iCnt * 50 + 1, 50);//计算起始点和结束点位置
                            }
                            else
                            {
                                iRLB_PosNum = CatchPtFormat((3000 - db_LaserMaxPt + iCachePtPos) + iCnt * 50 + 1, 50);//最多3000个点
                            }
                            iCnt++;
                            bt_SendLaserCmd = System.Text.Encoding.Default.GetBytes(Laser_Cmd_Package("RLB", "", iRLB_PosNum, 10));
                            comm_Surface.WritePort(bt_SendLaserCmd, 0, bt_SendLaserCmd.Length);
                        }
                        else
                        {
                            iCnt = 0;
                            iFaceDataCnt = 0;
                            str_cmdtemp = string.Empty;
                            strFaceComCmd = string.Empty;
                            b_EnableRTS = true;//允许RTS查询缓冲状态
                            str_ProcessCtl = "FCAL";//进入面平衡结果判断
                        }
                        Thread.Sleep(80);
                        sur_ComDataRecIsEnable = true;
                        break;

                    default:
                        str_cmdtemp = string.Empty;
                        strFaceComCmd = string.Empty;
                        sur_ComDataRecIsEnable = true;
                        break;
                }
                Thread.Sleep(50);
            }
        }

        private int CachePtFormat(int p1, int p2)
        {
            throw new NotImplementedException();
        }

        //初始化面平衡传感器
        public void InitFaceLaser()
        {
            bt_SendLaserCmd = System.Text.Encoding.Default.GetBytes(Laser_Cmd_Package("WBD", "+", 0, 5));//缓冲模式-〉连续模式
            comm_Surface.WritePort(bt_SendLaserCmd, 0, bt_SendLaserCmd.Length);
        }

        //开始面平衡检查
        public void StartFaceChk()
        {
            bt_SendLaserCmd = System.Text.Encoding.Default.GetBytes(Laser_Cmd_Package("WBS", "+", 1, 5));//开始缓冲数据
            comm_Surface.WritePort(bt_SendLaserCmd, 0, bt_SendLaserCmd.Length);
        }

        //缓冲状态检查
        public void FaceStateChk()
        {
            bt_TmSendLaserCmd = System.Text.Encoding.Default.GetBytes(Laser_Cmd_Package("RTS", "", -1, 5));
            comm_Surface.WritePort(bt_TmSendLaserCmd, 0, bt_TmSendLaserCmd.Length);
        }
        
        //停止面平衡检查
        public void StopFaceChk()
        {
            bt_SendLaserCmd = System.Text.Encoding.Default.GetBytes(Laser_Cmd_Package("WBS", "+", 0, 5));//停止缓冲数据
            comm_Surface.WritePort(bt_SendLaserCmd, 0, bt_SendLaserCmd.Length);
        }
        
        //面平衡数据读取
        public void ReadFaceData()
        {
            bt_SendLaserCmd = System.Text.Encoding.Default.GetBytes(Laser_Cmd_Package("RLB", "", Convert.ToInt32(strStEdPtCnt), strStEdPtCnt.Length));
            comm_Surface.WritePort(bt_SendLaserCmd, 0, bt_SendLaserCmd.Length);
        }

        //面平衡结果计算
        void FcResultCal()
        {
            if((db_FcMax >= 0 && db_FcMin >= 0)
                ||(db_FcMax < 0 && db_FcMin < 0))              
            {
                //db_FcResult = Math.Round(Math.Abs(db_FcMax) - Math.Abs(db_FcMin), 4);
                db_FcResult = Math.Round(db_FcMax - db_FcMin, 4);
            }
            else if (db_FcMax >= 0 && db_FcMin < 0)
            {
                db_FcResult = Math.Round(Math.Abs(db_FcMax) + Math.Abs(db_FcMin), 4);
            }
        }

        #endregion        

        #region Phoenix通信相关
        public void PhoenixCmdDeal()
        {
            str_PhoenixCmd = ReadTxtFile(TxtFilePath);
            switch (str_PhoenixCmd)
            {
                case "START":
                    str_PhoenixCmd = string.Empty;
                    WriteTxtFile("");   //清空文件
                    //str_ProcessCtl = "START";//启动作业                    
                    break;

                case "STOP":
                    str_PhoenixCmd = string.Empty;
                    WriteTxtFile("");   //清空文件
                    str_ProcessCtl = "STOP";//作业结束
                    break;

                case "BCHK":
                    str_PhoenixCmd = string.Empty;
                    WriteTxtFile("");   //清空文件
                    str_ProcessCtl = "BCHK";
                    break;

                default:

                    break;
            }
            Thread.Sleep(100);
        }

        #endregion

        #region 作业流程
        //打开定时器发送面震检测指令
        public void StartTimer()
        {
            timer_wave.Start();
        }

        //关闭定时器停止检测面震
        public void StopTimer()
        {
            timer_wave.Stop();
        }

        //界面显示初始化
        private void InitIF()
        {
            ClearGridView();            //清除GridView的数据 
            lb_OkNg.Text = string.Empty;
            lb_OkNg.BackColor = Control.DefaultBackColor;
            lb_FcMax.Text = (0.000).ToString();
            lb_FcMin.Text = (0.000).ToString();
            lb_FaceResult.Text = (0.000).ToString();
        }

        //作业流程(th_ProcessDeal线程调用)
        public void ProcessDeal()
        {
            
            byte[] answerOK = System.Text.Encoding.Default.GetBytes(PlcCmdPackage("BLOK"));//通知PLC动平衡OK  
            byte[] answerNG = System.Text.Encoding.Default.GetBytes(PlcCmdPackage("BLNG"));//通知PLC动平衡NG
            byte[] answerFNG = System.Text.Encoding.Default.GetBytes(PlcCmdPackage("FCNG"));//通知PLC作业结束
            byte[] answerSTP = System.Text.Encoding.Default.GetBytes(PlcCmdPackage("STOP"));//通知PLC作业结束
            MethodInvoker MethInvo_StartTimer = new MethodInvoker(StartTimer);
            MethodInvoker MethInvo_StopTimer = new MethodInvoker(StopTimer);
            MethodInvoker MethInvo_ShowChart = new MethodInvoker(ShowChart);
            while(true)
            {
                switch (str_ProcessCtl)
                {
                    case "START"://开始作业流程
                        //MessageBox.Show(strLogLine);//测试用
                        InitIF();//初始化显示界面
                        lb_AdjResult.Text = "请按上下键调整面平衡转速，然后按[回车]";
                        SetSpeedVoltage(rpm_2000);  //面平衡检测速度2000rpm
                        lb_Voiltage.Text = db_PPreset_2000rpm.ToString();
                        PowerControl(POWER_ON);     //打开电源              
                        //faceOkEvent.WaitOne();      //等待回车确认
                        bt_test.Enabled = false;
                        str_ProcessCtl = "FACE";
                        break;

                    case"STOP"://结束作业流程
                        str_ProcessCtl = string.Empty;
                        PowerControl(POWER_OFF);    //关闭电源
                        lb_AdjResult.Text = "待机中"; 
                        comm_Plc.WritePort(answerSTP, 0, answerSTP.Length);//通知PLC作业结束
                        CleanLogStr();//清除数据暂存字符串
                        lb_Voiltage.Text = "0.000".ToString();
                        b_IsFirstFaceChk = true;
                        b_IsFcEnable = false;
                        break;

                    case "FACE"://面平衡检查
                        if (b_IsFcEnable)
                        {
                            b_IsFcEnable = false;
                            str_ProcessCtl = string.Empty;
                            lb_AdjResult.Text = "面震检测中……";
                            StartFaceChk();             //开始面平衡检测
                            Thread.Sleep(4000);
                            FaceStateChk();
                        }
                        break;

                    case "FCAL"://面平衡检查
                        FcResultCal();
                        lb_FaceResult.Text = db_FcResult.ToString();
                        lb_FcMax.Text = db_FcMax.ToString();
                        lb_FcMin.Text = db_FcMin.ToString();
                        bt_test.Enabled = true;
                        if (db_FcResult > db_LaserSpec)//面平衡规格外
                        {
                            str_ProcessCtl = string.Empty;    
                            lb_OkNg.Text = "NG";
                            lb_OkNg.BackColor = Color.Red;
                            lb_AdjResult.Text = "面平衡检测NG";
                            PowerControl(POWER_OFF);            //关闭电源
                            //"FCNG"写入TXT文件
                            WriteTxtFile("FCNG");       //通知Phoenix面平衡NG
                            comm_Plc.WritePort(answerFNG, 0, answerSTP.Length);//通知PLC作业结束
                            FaceDataAdd(false);              //面平衡NG数据临时保存到字符串
                            SaveLog();//保存数据至LOG       
                            ClearComCache();//清除COM缓存字符串
                        }
                        else//面平衡规格内
                        {
                            #region 自动作业
                            if (!b_IsManual)//非手动模式（自动或点检）
                            {
                                if (b_IsFirstFaceChk)
                                {
                                    str_ProcessCtl = "BADJ";    //面平衡OK，进行动平衡调整
                                    FaceDataAdd(true);              //第一次面平衡OK数据临时保存到字符串
                                    b_IsFirstFaceChk = false;
                                }
                                else
                                {
                                    str_ProcessCtl = string.Empty;
                                    lb_OkNg.Text = "OK";
                                    lb_OkNg.BackColor = Color.Green;
                                    FaceDataAdd(true);              //第二次面平衡OK数据临时保存到字符串
                                    SaveLog();//保存数据至LOG
                                    PowerControl(POWER_OFF);    //关闭电源
                                    comm_Plc.WritePort(answerOK, 0, answerOK.Length);  //通知PLC动平衡OK
                                    lb_AdjResult.Text = "待机中";
                                    ClearComCache();//清除COM缓存字符串
                                    b_IsFirstFaceChk = true;
                                }
                            }
                            #endregion

                            #region 手动作业
                            else
                            {
                                if (b_IsManualFaceChk && b_IsManualBlance)  //手动动平衡 && 面平衡
                                {
                                    if (b_IsFirstFaceChk)
                                    {
                                        str_ProcessCtl = "BADJ";    //面平衡OK，进行动平衡调整
                                        FaceDataAdd(true);              //第一次面平衡OK数据临时保存到字符串
                                        b_IsFirstFaceChk = false;
                                    }
                                    else
                                    {
                                        str_ProcessCtl = string.Empty;
                                        lb_OkNg.Text = "OK";
                                        lb_OkNg.BackColor = Color.Green;
                                        FaceDataAdd(true);              //第二次面平衡OK数据临时保存到字符串
                                        SaveLog();//保存数据至LOG
                                        PowerControl(POWER_OFF);    //关闭电源
                                        comm_Plc.WritePort(answerOK, 0, answerOK.Length);  //通知PLC动平衡OK
                                        lb_AdjResult.Text = "待机中";
                                        ClearComCache();//清除COM缓存字符串
                                        b_IsFirstFaceChk = true;
                                    }
                                }
                                else if (b_IsManualFaceChk && !b_IsManualBlance)//手动面平衡
                                {
                                    str_ProcessCtl = string.Empty;
                                    lb_OkNg.Text = "OK";
                                    lb_OkNg.BackColor = Color.Green;
                                    FaceDataAdd(true);              //手动面平衡OK数据临时保存到字符串
                                    SaveLog();//保存数据至LOG
                                    PowerControl(POWER_OFF);    //关闭电源
                                    comm_Plc.WritePort(answerOK, 0, answerOK.Length);  //通知PLC  OK
                                    lb_AdjResult.Text = "待机中";
                                    ClearComCache();//清除COM缓存字符串
                                    b_IsFirstFaceChk = true;
                                }
                            }
                            #endregion
                        }
                        SaveFaceDataToFile();                        
                        BeginInvoke(MethInvo_ShowChart);//打开面振检测定时器
                        //FaceDataClear();
                        //Thread.Sleep(50);
                        //db_FcResult = 0;
                        //db_FcMax = -10;
                        //db_FcMin = 10;
                        break;

                    case "BADJ"://动平衡调整
                        if (lb_AdjResult.Text != "等待动平衡测定")
                        {
                            SetSpeedVoltage(rpm_7200);  //动平衡检测速度7200rpm
                            PowerControl(POWER_ON);     //打开电源
                            lb_Voiltage.Text = db_PowerPre8v.ToString();
                            lb_AdjResult.Text = "等待动平衡测定";
                        }
                        if (db_BlanceResult != 0)
                        {
                            PowerControl(POWER_OFF);            //关闭电源
                            if (db_BlanceResult > db_CorMass)   //动平衡结果规格外
                            {
                                //WriteTxtFile("BLNG");           //通知Phoenix动平衡NG
                                str_ProcessCtl = string.Empty;            //清空指令等待PLC指令进入BCHK
                                b_IsFirstFaceChk = true;
                                comm_Plc.WritePort(answerNG, 0, answerNG.Length);  //通知PLC动平衡NG
                                if (i_row == 0)
                                {
                                    BalanceDataAdd();//暂存数据到字符串
                                }
                                i_row = 1;//数据行写入第二行                                
                                
                                lb_AdjResult.Text = "请加配重，然后按【绿色】按钮";
                            }
                            if (db_BlanceResult < db_CorMass//动平衡结果规格内
                                && db_BlanceResult>0)
                            {
                                str_ProcessCtl = string.Empty;        //结束作业
                                //WriteTxtFile("BLOK");           //通知Phoenix动平衡OK
                                comm_Plc.WritePort(answerOK, 0, answerOK.Length);  //通知PLC动平衡OK
                                BalanceDataAdd();//暂存数据到字符串
                                SaveLog();//保存数据至LOG
                                CleanLogStr();//清除数据暂存字符串
                                lb_AdjResult.Text = "待机中";
                                lb_Voiltage.Text = "0.000".ToString();
                                b_IsFirstFaceChk = true;
                                lb_OkNg.Text = "OK";
                                lb_OkNg.BackColor = Color.Green;
                                i_row = 0;//等待下一个作业，数据行写一行
                                //str_ProcessCtl = "STOP";        //结束作业
                            }
                            db_BlanceResult = 0;                //清除调整结果
                        }
                        Thread.Sleep(50);
                        break;

                    case "BCHK"://动平衡检查
                            //WriteTxtFile("");
                            if (lb_AdjResult.Text != "等待动平衡测定")
                            {
                                SetSpeedVoltage(rpm_7200);  //动平衡检测速度7200rpm
                                PowerControl(POWER_ON);     //打开电源
                                lb_AdjResult.Text = "等待动平衡测定";
                            }
                            if (db_BlanceResult != 0)
                            {
                                PowerControl(POWER_OFF);     //关闭电源
                                if (db_BlanceResult > db_CorMass)//动平衡结果规格外
                                {
                                    i_row = 1;//数据行写入第二行
                                    //WriteTxtFile("BLNG");           //通知Phoenix动平衡NG
                                    comm_Plc.WritePort(answerNG, 0, answerNG.Length);//通知PLC动平衡NG
                                    lb_AdjResult.Text = "请加配重，然后按【绿色】按钮";
                                    db_BlanceResult = 0;
                                    str_ProcessCtl = string.Empty;            //进入动平衡确认
                                }
                                else if (db_BlanceResult < db_CorMass//动平衡结果规格内
                                    && db_BlanceResult > 0)
                                {                                    
                                    i_row = 0;                  //等待下一个作业，数据行写一行   
                                    lb_AdjResult.Text = "请按上下键调整动平衡转速，然后按[回车]";
                                    str_ProcessCtl = "FCHK";    //**** 再确认面平衡 ****   
                                }
                            }
                        Thread.Sleep(50);
                        break;

                    case "FCHK"://动平衡调整结束后再确认面平衡
                        if (b_IsFcEnable)
                        {
                            b_IsFcEnable = false;
                            str_ProcessCtl = string.Empty;
                            //PowerControl(POWER_ON);     //打开电源
                            //faceOkEvent.WaitOne();      //等待回车确认
                            bt_test.Enabled = false;
                            lb_AdjResult.Text = "面震检测中……";
                            StartFaceChk();             //开始面平衡检测
                            Thread.Sleep(4000);
                            FaceStateChk();
                        }
                        break;

                    default:
                        break;
                }
                Thread.Sleep(50);   
            }
        }
        #endregion

        //按钮-面平衡
        private void bt_ReInit_Click(object sender, EventArgs e)
        {
            StartFaceChk();             //开始面平衡检测
            Thread.Sleep(200);
            timer_wave.Start();         //启动定时器检测是否检测完成 
        }

        #region 测试用

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFaceDataToFile();
            if (bt_Power.Text == "PowerOn")
            {
                InitPow();
                TMI_Api.TMI_Voltage(PowerDeviceId, CH_C_8V, PRESET_1, 1.2);
                TMI_Api.TMI_Voltage(PowerDeviceId, CH_B_18V, PRESET_1, 12);
                TMI_Api.TMI_OutputSel(PowerDeviceId, CH_C_8V, POWER_ON);
                TMI_Api.TMI_MainOutput(PowerDeviceId, POWER_ON);
                bt_Power.Text = "PowerOff";
                bt_Power.BackColor = Color.Red;
            }
            else
            {
                TMI_Api.TMI_MainOutput(PowerDeviceId, POWER_OFF);
                bt_Power.Text = "PowerOn";
                bt_Power.BackColor = Color.LightGray;  
            }
        }
        
        
        //外围设备再初始化
        private void bt_Init_Click(object sender, EventArgs e)
        {
            //串口初始化
            InitComm();

            //电源初始化
            InitPow();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            fmProduc = new form_ProductBarcode();
            fmProduc.Owner = this;
            fmProduc.StartPosition = FormStartPosition.CenterScreen;
            fmProduc.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ReadFaceData();
            //bt_SendLaserCmd = System.Text.Encoding.Default.GetBytes(Laser_Cmd_Package("RLB", "", Convert.ToInt32(strStEdPtCnt), strStEdPtCnt.Length));
            //comm_Surface.WritePort(bt_SendLaserCmd, 0, bt_SendLaserCmd.Length);
        }
        #endregion         
    }
}
