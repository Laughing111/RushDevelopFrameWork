using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using System.IO;


namespace RDFW
{
    public class portTest : MonoBehaviour
    {
        #region 定义串口属性
        //定义基本信息
        public string portName = "COM3";//串口名
        public int baudRate = 9600;//波特率
        public Parity parity = Parity.None;//效验位
        public int dataBits = 8;//数据位
        public StopBits stopBits = StopBits.One;//停止位
        SerialPort sp = null;
        Thread dataReceiveThread;
        //发送的消息
        //string message = "";
        public List<byte> listReceive = new List<byte>();
        char[] strchar = new char[100];//接收的字符信息转换为字符数组信息
        string str;
        #endregion
        private void Awake()
        {
            if (File.Exists(Application.streamingAssetsPath + "/Port.txt"))
            {
                portName = File.ReadAllText(Application.streamingAssetsPath + "/Port.txt");
                Debug.Log(portName);
            }


        }
        void Start()
        {
            OpenPort();
            dataReceiveThread = new Thread(new ThreadStart(DataReceiveFunction));
            dataReceiveThread.Start();
        }
        void Update()
        {

        }

        #region 创建串口，并打开串口
        public void OpenPort()
        {
            //创建串口
            sp = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
            sp.ReadTimeout = 400;
            try
            {
                sp.Open();
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }
        #endregion



        #region 程序退出时关闭串口
        void OnApplicationQuit()
        {
            WriteData("2");
            ClosePort();
        }

        //关闭串口
        public void ClosePort()
        {
            try
            {
                sp.Close();
                dataReceiveThread.Abort();
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }
        #endregion


        /// <summary>
        /// 打印接收的信息
        /// </summary>
        void PrintData()
        {
            for (int i = 0; i < listReceive.Count; i++)
            {
                strchar[i] = (char)(listReceive[i]);
                str = new string(strchar);
            }
            Debug.Log(str);
        }

        #region 接收数据
        void DataReceiveFunction()
        {
            #region 按单个字节发送处理信息，不能接收中文
            //while (sp != null && sp.IsOpen)
            //{
            //    Thread.Sleep(1);
            //    try
            //    {
            //        byte addr = Convert.ToByte(sp.ReadByte());
            //        sp.DiscardInBuffer();
            //        listReceive.Add(addr);
            //        PrintData();
            //    }
            //    catch
            //    {
            //        //listReceive.Clear();
            //    }
            //} 
            #endregion


            #region 按字节数组发送处理信息，信息缺失
            byte[] buffer = new byte[1024];
            int bytes = 0;
            while (true)
            {
                if (sp != null && sp.IsOpen)
                {
                    try
                    {
                        bytes = sp.Read(buffer, 0, buffer.Length);//接收字节
                        if (bytes == 0)
                        {
                            continue;
                        }
                        else
                        {
                            string strbytes = Encoding.Default.GetString(buffer);
                            Debug.Log(strbytes);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.GetType() != typeof(ThreadAbortException))
                        {
                        }
                    }
                }
                Thread.Sleep(10);
            }
            #endregion
        }
        #endregion

        #region 发送数据
        public void WriteData(string dataStr)
        {
            if (sp.IsOpen)
            {
                sp.Write(dataStr);
            }
        }
    }
    #endregion
}
