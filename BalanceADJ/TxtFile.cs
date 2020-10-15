using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BalanceADJ
{
    class TxtFile
    {
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

        //写TXT文件
        public void WriteTxtFile(string MyData,string FilepPath,bool IsAdd)
        {
            //检测文件夹是否存在，不存在则创建
            if (File.Exists(FilepPath))
            {
                //定义编码方式
                byte[] mybyte = Encoding.UTF8.GetBytes(MyData);
                string mystr1 = Encoding.UTF8.GetString(mybyte);

                if (IsAdd)
                {
                    //追加写入文件
                    File.AppendAllText(FilepPath, mystr1); //添加至文件
                }
                else
                {
                    //覆盖写入文件
                    File.WriteAllText(FilepPath, mystr1);    //写入并覆盖文件内容
                }
            }
        }

        //编辑TXT文件
        /// <summary>
        /// 编辑TXT文件
        /// </summary>
        /// <param name="curLine"> 行号 </param>
        /// <param name="newLineValue"> 值 </param>
        /// <param name="path"> 文件路径 </param>
        public static void EditFile(int curLine, string newLineValue, string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("utf-8"));
            string line = sr.ReadLine();
            StringBuilder sb = new StringBuilder();
            for (int i = 1; line != null; i++)
            {
                sb.Append(line + "\r\n");
                if (i != curLine - 1)
                    line = sr.ReadLine();
                else
                {
                    sr.ReadLine();
                    line = newLineValue;
                }
            }
            sr.Close();
            fs.Close();
            FileStream fs1 = new FileStream(path, FileMode.Open, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs1);
            sw.Write(sb.ToString());
            sw.Close();
            fs.Close();
        } 
    }
}
