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
    public partial class Setting : Form
    {
        string strPath = Application.StartupPath + "\\INI\\CurrentKind.txt";
        IniFile inikind = new IniFile();

        NewKind fmNew = null;
        EditKind fmEdit = null;
        
        public Setting()
        {
            InitializeComponent();
        }

        //按钮-返回
        private void bt_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();       
        }

        //窗口启动
        private void Setting_Load(object sender, EventArgs e)
        {
            InitKind();
        }

        //关闭窗口
        private void Setting_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            MainForm mf = (MainForm)this.Owner;
            mf.Enabled = true;
            mf.InitKind();
        }

        #region 功能函数
        
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

        //初始化机种信息
        private void InitKind()
        {
            string str_CurKind;
            ArrayList kindArrayList = new ArrayList();

            //从KIND.INI文件获取机种列表
            kindArrayList = inikind.ReadSections();
            if (kindArrayList.Count > 0)
            {
                for (int i = 0; i < kindArrayList.Count; i++)
                {
                    cbx_KindSelect.Items.Add(kindArrayList[i]);
                }
            }
            
            //从CurrentKind.txt文件读取当前机种
            str_CurKind = ReadTxtFile();
            if (str_CurKind != "")
            {
                if (cbx_KindSelect.Items.Contains(str_CurKind))
                {
                    cbx_KindSelect.Text = str_CurKind;
                }

            }
        }

        //
        private void KindRead()
        {
            string str_CurKind;
            //从CurrentKind.txt文件读取当前机种
            str_CurKind = ReadTxtFile();
            if (str_CurKind != "")
            {
                if (cbx_KindSelect.Items.Contains(str_CurKind))
                {
                    cbx_KindSelect.Text = str_CurKind;
                }
                
            }

        }

        #endregion

        //按钮-添加新机种
        private void bt_NewKind_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            fmNew = new NewKind();
            fmNew.Owner = this;
            fmNew.StartPosition = FormStartPosition.CenterScreen;
            fmNew.Show();
        }

        //按钮-编辑
        private void bt_EditKind_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            fmEdit = new EditKind();
            fmEdit.Owner = this;
            fmEdit.StartPosition = FormStartPosition.CenterScreen;
            fmEdit.Show();
        }

        //按钮-删除
        private void bt_DeleteKind_Click(object sender, EventArgs e)
        {

        }

        //按钮-机种确定
        private void bt_KindSelect_Click(object sender, EventArgs e)
        {
            if (cbx_KindSelect.Text != "")
            {
                WriteTxtFile(cbx_KindSelect.Text);
                MessageBox.Show("当前机种切换为 [ " +cbx_KindSelect.Text+ " ]");
            }
        }

        //ComboBox机种选择操作
        private void cbx_KindSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
           // KindRead();
        }
    }
}
