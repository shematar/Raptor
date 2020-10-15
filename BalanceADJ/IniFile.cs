using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Collections;
using System.Windows.Forms;

namespace BalanceADJ
{
    class IniFile
    {
        //参数定义
        string filepath = Application.StartupPath + "\\INI\\Kind.ini";

        #region 导入DLL函数
        [DllImport("kernel32.dll")]
        public extern static int GetPrivateProfileString(string segName, string keyName, string sDefault, StringBuilder buffer, int nSize, string fileName);

        //public extern static int GetPrivateProfileStringA(string segName, string keyName, string sDefault, byte[] buffer, int iLen, string fileName); // ANSI版本

        [DllImport("kernel32.dll")]
        public extern static int GetPrivateProfileSection(string segName, StringBuilder buffer, int nSize, string fileName);

        [DllImport("kernel32.dll")]
        public extern static int WritePrivateProfileSection(string segName, string sValue, string fileName);

        [DllImport("kernel32.dll")]
        public extern static int WritePrivateProfileString(string segName, string keyName, string sValue, string fileName);

        [DllImport("kernel32.dll")]
        public extern static int GetPrivateProfileSectionNamesA(byte[] buffer, int iLen, string fileName);

        #endregion

        //读INI文件
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strAreaName">字段名</param>
        /// <param name="strKeyName">键名</param>
        /// <param name="strValue">键值</param>
        /// <param name="strFilePath">文件路径</param>
        /// <returns></returns>
        public string ReaderIni(string strAreaName, string keyName, string strDefValue,string strFilePath)
        {
            StringBuilder stringBuilder = new StringBuilder(256);
            GetPrivateProfileString(strAreaName, keyName, strDefValue, stringBuilder, 256, strFilePath);
            return stringBuilder.ToString();
        }
        

        //写INI文件
        /// <summary>
        /// 写INI文件
        /// </summary>
        /// <param name="strAreaName">字段名</param>
        /// <param name="strKeyName">键名</param>
        /// <param name="strValue">键值</param>
        /// <param name="strFilePath">文件路径</param>
        /// <returns></returns>
        public bool WriteIni(string strAreaName, string strKeyName, string strValue, string strFilePath)
        {
            if (File.Exists(strFilePath))
            {
                WritePrivateProfileString(strAreaName, strKeyName, strValue, strFilePath);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// 返回该配置文件中所有Section名称的集合
        public ArrayList ReadSections()
        {
            byte[] buffer = new byte[65535];
            int rel = GetPrivateProfileSectionNamesA(buffer, buffer.GetUpperBound(0), filepath);
            int iCnt, iPos;
            ArrayList arrayList = new ArrayList();
            string tmp;
            if (rel > 0)
            {
                iCnt = 0; iPos = 0;
                for (iCnt = 0; iCnt < rel; iCnt++)
                {
                    if (buffer[iCnt] == 0x00)
                    {
                        tmp = System.Text.ASCIIEncoding.Default.GetString(buffer, iPos, iCnt - iPos).Trim();
                        iPos = iCnt + 1;
                        if (tmp != "")
                            arrayList.Add(tmp);
                    }
                }
            }
            return arrayList;
        }        
    }
}
