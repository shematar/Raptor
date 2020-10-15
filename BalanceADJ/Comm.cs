﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports; //串口通信类
using System.Threading;//线程申明
using System.Windows.Forms;

namespace BalanceADJ
{
    class Comm
    {
        public delegate void EventHandle(byte[] readBuffer);
        public event EventHandle DataReceived;

        public SerialPort serialPort;
        Thread thread;
        volatile bool _keepReading;

        public Comm()
        {
            serialPort = new SerialPort();
            thread = null;
            _keepReading = false;
        }

        public void InitComm(string PortName, int BaudRate, int DataBit, string MyParity, string MyStopBits)
        {
            serialPort.PortName= PortName;
            serialPort.BaudRate=BaudRate;
            serialPort.DataBits=DataBit;
            
            switch (MyParity)
            {
                case "0":
                    serialPort.Parity = System.IO.Ports.Parity.None;
                    break;
                case "1":
                    serialPort.Parity = System.IO.Ports.Parity.Odd;
                    break;
                case "2":
                    serialPort.Parity = System.IO.Ports.Parity.Even;
                    break;
                case "3":
                    serialPort.Parity = System.IO.Ports.Parity.Mark;
                    break;
                case "4":
                    serialPort.Parity = System.IO.Ports.Parity.Space;
                    break;
            }
            switch (MyStopBits)
            {
                case "0":
                    serialPort.StopBits=System.IO.Ports.StopBits.None;
                    break;
                case "1":
                    serialPort.StopBits = System.IO.Ports.StopBits.One;
                    break;
                case "2":
                    serialPort.StopBits = System.IO.Ports.StopBits.Two;
                    break;
                case "1.5":
                    serialPort.StopBits = System.IO.Ports.StopBits.OnePointFive;
                    break;
            }
        }

        public bool IsOpen
        {
            get
            {
                return serialPort.IsOpen;
            }
        }

        private void StartReading()
        {
            if (!_keepReading)
            {
                _keepReading = true;
                thread = new Thread(new ThreadStart(ReadPort));
                thread.Start();
            }
        }

        private void StopReading()
        {
            if (_keepReading)
            {
                _keepReading = false;
                thread.Join();
                thread = null;
            }
        }

        private void ReadPort()
        {
            while (_keepReading)
            {
                if (serialPort.IsOpen)
                {
                    int count = serialPort.BytesToRead;
                    
                    if (count > 0)
                    {
                        byte[] readBuffer = new byte[count];
                        try
                        {
                            Application.DoEvents();
                            serialPort.Read(readBuffer, 0, count);
                            if (DataReceived != null)
                                DataReceived(readBuffer);
                            Thread.Sleep(50);
                        }
                        catch (TimeoutException)
                        {
                        }
                    }
                    Thread.Sleep(100);
                }
            }
        }

        public bool Open()
        {
            Close();
            serialPort.Open();
            if (serialPort.IsOpen)
            {
                StartReading();
                return true;
            }
            else
            {
                return false;
                //MessageBox.Show("串口打开失败！");
            }
        }

        public void Close()
        {
            StopReading();
            serialPort.Close();
        }

        public void WritePort(byte[] send, int offSet, int count)
        {
            if (IsOpen)
            {
                serialPort.Write(send, offSet, count);
            }
        }
    }
}
