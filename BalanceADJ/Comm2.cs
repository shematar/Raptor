using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace BalanceADJ
{
    class Comm2
    {
 
        private SerialPort ComDevice = new SerialPort();
         /// <summary>
        /// 初始化端口
        /// </summary>
        public void init()
        {
 
            btnSend.Enabled = false;
            cmbPort.Items.AddRange(SerialPort.GetPortNames());
            if (cmbPort.Items.Count > 0)
            {
                cmbPort.SelectedIndex = 0;
            }
             //波特率
            cmbBaudRate.Items.Add("110");
            cmbBaudRate.Items.Add("300");
            cmbBaudRate.Items.Add("1200");
            cmbBaudRate.Items.Add("2400");
            cmbBaudRate.Items.Add("4800");
            cmbBaudRate.Items.Add("9600");
            cmbBaudRate.Items.Add("19200");
            cmbBaudRate.Items.Add("38400");
            cmbBaudRate.Items.Add("57600");
            cmbBaudRate.Items.Add("115200");
            cmbBaudRate.Items.Add("230400");
            cmbBaudRate.Items.Add("460800");
            cmbBaudRate.Items.Add("921600");
            cmbBaudRate.SelectedIndex = 5;
 
            //数据位
            cmbDataBits.Items.Add("5");
            cmbDataBits.Items.Add("6");
            cmbDataBits.Items.Add("7");
            cmbDataBits.Items.Add("8");
            cmbDataBits.SelectedIndex = 3;
 
            //停止位
            cmbStopBit.Items.Add("1");
            cmbStopBit.SelectedIndex = 0;
 
            //佼验位
            cmbParity.Items.Add("无");
            cmbParity.SelectedIndex = 0;
 
            ComDevice.DataReceived += new SerialDataReceivedEventHandler(Com_DataReceived);//绑定事件
 
        }
 
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
 
        }
 
        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
 
  
            if (cmbPort.Items.Count <= 0)
            {
                MessageBox.Show("没有发现串口,请检查线路！");
                return;
            }
            if (ComDevice.IsOpen == false)
            {
                ComDevice.PortName = cmbPort.SelectedItem.ToString();
                ComDevice.BaudRate = Convert.ToInt32(cmbBaudRate.SelectedItem.ToString());
                ComDevice.Parity = (Parity)Convert.ToInt32(cmbParity.SelectedIndex.ToString());
                ComDevice.DataBits = Convert.ToInt32(cmbDataBits.SelectedItem.ToString());
                ComDevice.StopBits = (StopBits)Convert.ToInt32(cmbStopBit.SelectedItem.ToString());
                try
                {
                    ComDevice.Open();
                    btnSend.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                btnOpen.Text = "关闭串口";
            }else{
                try
                {
                    ComDevice.Close();
                    btnSend.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                btnOpen.Text = "打开串口";
            }
            cmbPort.Enabled = !ComDevice.IsOpen;
            cmbBaudRate.Enabled = !ComDevice.IsOpen;
            cmbParity.Enabled = !ComDevice.IsOpen;
            cmbDataBits.Enabled = !ComDevice.IsOpen;
            cmbStopBit.Enabled = !ComDevice.IsOpen;
        }
 
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool SendData(byte[] data) {
            if (ComDevice.IsOpen)
            {
                try
                {
                    ComDevice.Write(data, 0, data.Length);//发送数据
                    ComDevice.Write(txtSendData.Text);
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else {
                MessageBox.Show("串口未打开", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
 
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
         private void Com_DataReceived(object sender, SerialDataReceivedEventArgs e){
             if (ComDevice.IsOpen)
             {
                 byte[] ReDatas = new byte[ComDevice.BytesToRead];
                 ComDevice.Read(ReDatas, 0, ReDatas.Length);//读取数据
                 this.AddData(ReDatas);//输出数据
             }
             else {
                 MessageBox.Show("请先打开串口");
             }
           
        }
 
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="data"></param>
         public void AddData(byte[] data)
         {
             if (radioButton1.Checked)
             {
                 StringBuilder sb = new StringBuilder();
                 for (int i = 0; i < data.Length; i++)
                 {
                     sb.AppendFormat("{0:x2}" + " ", data[i]);
                 }
                 AddContent(sb.ToString().ToUpper());
             }
             else if (radioButton4.Checked)
             {
                 AddContent(new ASCIIEncoding().GetString(data));
             }
             else if (radioButton3.Checked)
             {
                 AddContent(new UTF8Encoding().GetString(data));
             }
             else if (radioButton2.Checked)
             {
                 AddContent(new UnicodeEncoding().GetString(data));
             }
             else
             { }
 
             lblRevCount.Invoke(new MethodInvoker(delegate
             {
                 lblRevCount.Text = (int.Parse(lblRevCount.Text) + data.Length).ToString();
             }));
         }
 
 
        /// <summary>
        /// 输入到显示区域
        /// </summary>
        /// <param name="content"></param>
         private void AddContent(string content)
         {
             this.BeginInvoke(new MethodInvoker(delegate
             {
                 if (chkAutoLine.Checked && txtShowData.Text.Length > 0)
                 {
                     txtShowData.AppendText("\r\n");
                 }
                 txtShowData.AppendText(content);
             }));
         }
        /// <summary>
        /// 发送button事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            byte[] sendData = null;
            if(rbtnSendHex.Checked){
                sendData = strToHexByte(txtSendData.Text.Trim());
            }else if(rbtnSendASCII.Checked){
                sendData = Encoding.ASCII.GetBytes(txtSendData.Text.Trim());
            }else if(rbtnSendUTF8.Checked){
                sendData = Encoding.UTF8.GetBytes(txtSendData.Text.Trim());
            }
            else if (rbtnSendUnicode.Checked)
            {
                sendData = Encoding.Unicode.GetBytes(txtSendData.Text.Trim());
            }
            else {
                sendData = Encoding.ASCII.GetBytes(txtSendData.Text.Trim());
            }
 
 
            if (this.SendData(sendData))
            {
                lblSendCount.Invoke(new MethodInvoker(delegate
                {
                    lblSendCount.Text = (int.Parse(lblSendCount.Text) + txtSendData.Text.Length).ToString();
                }));
 
            }
            else { 
                
            }
        }
 
        /// <summary>
        /// 字符串转换16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        private byte[] strToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Replace(" ", ""), 16);
            return returnBytes;
        }
 
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
 
        /// <summary>
        /// 清空接收区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearRev_Click(object sender, EventArgs e)
        {
            txtShowData.Clear();
        }
 
        /// <summary>
        /// 清空发送区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearSend_Click(object sender, EventArgs e)
        {
            txtSendData.Clear();
        }   
    }
}

