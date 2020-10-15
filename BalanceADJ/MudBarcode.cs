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

namespace BalanceADJ
{
    public partial class form_MudBarcode : Form
    {
        private string str_ProNo = string.Empty;
        private string str_IniFilePath = Application.StartupPath + "\\INI\\Kind.ini";
        private string CurKindTxtFilePath = Application.StartupPath + "\\INI\\CurrentKind.txt";
        //private string LogFilePath = "C:\\BalanceAdj\\";

        public string ProductNo = string.Empty;

        IniFile iniProductBar = new IniFile();
        string strCurretKind = string.Empty;
        string strPro_MudNumber = string.Empty;

        public form_MudBarcode()
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
        public string GetMudNum()
        {
            strPro_MudNumber = iniProductBar.ReaderIni(strCurretKind, "Mud_NumberHead", "", str_IniFilePath);
            if (strPro_MudNumber != string.Empty)
            {
                return strPro_MudNumber;
            }
            else
            {
                return string.Empty;
            }
        }

        private void form_MudBarcode_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm mf = (MainForm)this.Owner;
            mf.str_ProcessCtl = "START";
            mf.GetProMudNum(txt_MudCode.Text.Substring(0, 6) + ","); //
            mf.Enabled = true;
        }
        
        private void txt_MudCode_TextChanged(object sender, EventArgs e)
        {
            if (txt_MudCode.Text.Length == 6)
            {
                if (txt_MudCode.Text.Substring(0, 2) == GetMudNum())
                {
                    this.Close();
                }
                else
                {
                    txt_MudCode.Text = string.Empty;
                }
            }
        }
    }
}
