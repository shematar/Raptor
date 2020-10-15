using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
//using System.Threading.Tasks;
using System.Threading;//线程申明

namespace BalanceADJ
{
    public partial class form_ProductBarcode : Form
    {
        private string str_ProNo = string.Empty;
        private string str_IniFilePath= Application.StartupPath + "\\INI\\Kind.ini";
        private string CurKindTxtFilePath = Application.StartupPath + "\\INI\\CurrentKind.txt";
        private string LogFilePath = "C:\\BalanceAdj\\";

         
        IniFile iniProductBar= new IniFile();
        TxtFile txtLogFile = new TxtFile();

        string strCurretKind = string.Empty;
        string strPro_ANumber = string.Empty;
        string strMPro_ANumber = string.Empty;

        public form_ProductBarcode()
        {
            InitializeComponent();
            strCurretKind = ReadTxtFile(CurKindTxtFilePath);
        }

        //读TXT文件
        public string ReadTxtFile(string FilePath)
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    return File.ReadAllText(FilePath);
                }
                else
                {
                    return "ERR";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        //获取当前机种的A番号
        public string GetANum()
        {
            strPro_ANumber = iniProductBar.ReaderIni(strCurretKind, "Pro_ANumber", "", str_IniFilePath);
            if (strPro_ANumber != string.Empty)
            {
                return strPro_ANumber;
            }
            else
            {
                return string.Empty;
            }
        }

        //获取当前机种的A番号
        public string GetMANum()
        {
            strMPro_ANumber = iniProductBar.ReaderIni(strCurretKind, "MPro_ANumber", "", str_IniFilePath);
            if (strMPro_ANumber != string.Empty)
            {
                return strMPro_ANumber;
            }
            else
            {
                return string.Empty;
            }
        }

        //写入LOG文件
        private void WriteLog()
        {
            LogFilePath += DateTime.Today.ToString("yyyy") + "\\" + DateTime.Today.ToString("MM-dd") + ".txt";
            if(File.Exists(LogFilePath))
            {
                txtLogFile.WriteTxtFile(txt_ProductCode.Text.Trim(), LogFilePath, true);
            }
        }

        private void form_ProductBarcode_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm mf = (MainForm)this.Owner;
            if (!mf.b_IsMasterChk)//非点检模式（自动模式）
            {
                mf.ShowFormMud();//显示扫描平衡泥对话框
                mf.GetProMudNum(txt_ProductCode.Text.Substring(0, 17) + ",");
            }
            else //（点检模式）无需扫平衡泥
            {
                mf.str_ProcessCtl = "START";//
                mf.GetProMudNum(txt_ProductCode.Text.Substring(0, 17) + ",");
            }            
            mf.Enabled = true;
        }

        private void txt_ProductCode_TextChanged(object sender, EventArgs e)
        {
            if (txt_ProductCode.Text.Length==17)                       
            {                       
                if (txt_ProductCode.Text.Substring(0,9) == GetANum()//自动模式
                    ||txt_ProductCode.Text.Substring(0,9)==GetMANum())//点检模式
                {
                    //WriteLog();//产品序列号写入LOG
                    this.Close();
                }
                else
                {
                    txt_ProductCode.Text = string.Empty;
                } 
            }             
        }

        private void form_ProductBarcode_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tempdata = txt_ProductCode.Text;
            int lentemp = tempdata.Length;
        }
    }
}
