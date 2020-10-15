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
    public partial class EditKind : Form
    {
        #region 参数定义

        string strPath = Application.StartupPath + "\\INI\\CurrentKind.txt";
        string strIniPath = Application.StartupPath + "\\INI\\Kind.ini";
        IniFile inikind = new IniFile();
        string str_CurKind = string.Empty;

        #endregion
        public EditKind()
        {
            InitializeComponent();
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

        //读取INI文件的机种信息
        private void LoadCurrentKind()
        {
            str_CurKind = ReadTxtFile();
            if (str_CurKind != string.Empty)
            {
                txt_NewKindName.Text = str_CurKind;
                txt_Pre_8V_Low.Text = inikind.ReaderIni(str_CurKind, "PPreset_2000rpm", "", strIniPath);
                txt_Pre_8V.Text = inikind.ReaderIni(str_CurKind, "PowerPreset_8V", "", strIniPath);
                txt_Pre_18V.Text = inikind.ReaderIni(str_CurKind, "PowerPreset_18V", "", strIniPath);
                txt_ProANo.Text = inikind.ReaderIni(str_CurKind, "Pro_ANumber", "", strIniPath);
                txt_MasterProANo.Text = inikind.ReaderIni(str_CurKind, "MPro_ANumber", "", strIniPath);
                txt_Mud_No.Text = inikind.ReaderIni(str_CurKind, "Mud_NumberHead", "", strIniPath);
                cbx_LaserNo.Text = inikind.ReaderIni(str_CurKind, "LaserNo", "", strIniPath);
                txt_LaserSpec.Text = inikind.ReaderIni(str_CurKind, "LaserSpec", "", strIniPath);
                txt_LaserMaxPt.Text = inikind.ReaderIni(str_CurKind, "LaserMaxPt", "", strIniPath);
                txt_CorMass.Text = inikind.ReaderIni(str_CurKind, "CorMass", "", strIniPath);
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

        //保存机种参数
        private void SaveKind()
        {
            inikind.WriteIni(txt_NewKindName.Text.ToUpper(), "PPreset_2000rpm", txt_Pre_8V_Low.Text, strIniPath);
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

        private void EditKind_FormClosed(object sender, FormClosedEventArgs e)
        {
            Setting mf = (Setting)this.Owner;
            mf.Enabled = true;
        }

        private void bt_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bt_NewKindSave_Click(object sender, EventArgs e)
        {
            if (ControlChk())
            {
                MessageBox.Show("参数保存成功！");
                SaveKind();
            }            
        }

        private void EditKind_Load(object sender, EventArgs e)
        {            
            LoadCurrentKind();            
        }
    }
}
