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

namespace BalanceADJ
{
    public partial class NewKind : Form
    {
        string strPath = Application.StartupPath + "\\INI\\CurrentKind.txt";
        string strIniPath = Application.StartupPath + "\\INI\\Kind.ini";
        IniFile inikind = new IniFile();
        string str_CurKind = string.Empty;

        
        public NewKind()
        {
            InitializeComponent();
        }

        //关闭窗口
        private void NewKind_FormClosed(object sender, FormClosedEventArgs e)
        {
            Setting mf = (Setting)this.Owner;
            mf.Enabled = true;
        }

        //读TXT文件
        public string ReadTxtFile()
        {
            try
            {
                if (File.Exists(strPath))
                {
                    return File.ReadAllText(strPath);
                }
                else
                {
                    MessageBox.Show("文件不存在");
                    return "ERR";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "ERR";
            }
        }

        //写TXT文件
        public void WriteTxtFile(string data)
        {
            //检测文件夹是否存在，不存在则创建
            if (File.Exists(strPath))
            {
                //定义编码方式
                byte[] mybyte = Encoding.UTF8.GetBytes(data);
                string mystr1 = Encoding.UTF8.GetString(mybyte);
                //写入文件
                //File.AppendAllText(filePath, mystr1); //添加至文件
                File.WriteAllText(strPath, mystr1);    //写入并覆盖文件内容
            }
        }
        
        //保存前控件内容判断
        private bool ControlChk()
        {
            if (txt_NewKindName.Text == string.Empty)
            {
                MessageBox.Show("请熟入机种名！");
                return false;
            }
            if (txt_Pre_8V_Low.Text == string.Empty)
            {
                MessageBox.Show("请熟入低速电压初始值！");
                return false;
            }
            if (txt_Pre_8V.Text == string.Empty)
            {
                MessageBox.Show("请熟入高速电压初始值！");
                return false;
            }
            if (txt_Pre_18V.Text == string.Empty)
            {
                MessageBox.Show("请熟入驱动电压初始值！");
                return false;
            }
            if (txt_ProANo.Text == string.Empty)
            {
                MessageBox.Show("请熟入产品A番号！");
                return false;
            }
            if (txt_MasterProANo.Text == string.Empty)
            {
                MessageBox.Show("请熟入点检部品A番号！");
                return false;
            }
            if (txt_Mud_No.Text == string.Empty)
            {
                MessageBox.Show("请熟入平衡泥识别值！");
                return false;
            }
            if (txt_LaserSpec.Text == string.Empty)
            {
                MessageBox.Show("请熟入面平衡规格值！");
                return false;
            }
            if (txt_LaserMaxPt.Text == string.Empty)
            {
                MessageBox.Show("请熟入面平衡测定点数！");
                return false;
            }
            if (txt_CorMass.Text == string.Empty)
            {
                MessageBox.Show("请熟入动平衡！");
                return false;
            }
            return true;
        }

        //保存新机种参数
        private void SaveKind()
        {
            inikind.WriteIni(txt_NewKindName.Text.ToUpper(), "PPreset_2000rpm", txt_Pre_8V_Low.Text,strIniPath);
            inikind.WriteIni(txt_NewKindName.Text.ToUpper(), "PowerPreset_8V", txt_Pre_8V.Text, strIniPath);
            inikind.WriteIni(txt_NewKindName.Text.ToUpper(), "PowerPreset_18V", txt_Pre_18V.Text, strIniPath);            
            inikind.WriteIni(txt_NewKindName.Text.ToUpper(), "Pro_ANumber", txt_ProANo.Text, strIniPath);
            inikind.WriteIni(txt_NewKindName.Text.ToUpper(), "MPro_ANumber", txt_MasterProANo.Text, strIniPath);
            inikind.WriteIni(txt_NewKindName.Text.ToUpper(), "Mud_NumberHead", txt_Mud_No.Text, strIniPath);
            inikind.WriteIni(txt_NewKindName.Text.ToUpper(), "LaserNo", cbx_LaserNo.Text, strIniPath);
            inikind.WriteIni(txt_NewKindName.Text.ToUpper(), "LaserSpec", txt_LaserSpec.Text, strIniPath);
            inikind.WriteIni(txt_NewKindName.Text.ToUpper(), "LaserMaxPt", txt_LaserMaxPt.Text, strIniPath);
            inikind.WriteIni(txt_NewKindName.Text.ToUpper(), "CorMass", txt_CorMass.Text, strIniPath);
        }

        //按钮-返回
        private void bt_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //编辑框事件
        private void txt_LaserMaxPt_TextChanged(object sender, EventArgs e)
        {
            Int16 value=0;
            if (txt_LaserMaxPt.Text != string.Empty)
            {
                value = Convert.ToInt16(txt_LaserMaxPt.Text);
                if (value <= 1000)
                {
                    if (value % 50 != 0)
                    {
                        MessageBox.Show("请输入50的倍数！");
                        txt_LaserMaxPt.Text = "200";
                    }
                }
                else
                {
                    MessageBox.Show("数据不能大于1000！");
                    txt_LaserMaxPt.Text = "1000";
                }
            }
        }

        //获取当前机种信息
        private void Kind_Load(object sender, EventArgs e)
        {
            
        }

        private void bt_NewKindSave_Click(object sender, EventArgs e)
        {
            if (ControlChk())
            {
                SaveKind();
                MessageBox.Show("新建机种成功");
            }
        }
    }
}
