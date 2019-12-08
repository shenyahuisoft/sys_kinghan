using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoServices.Models;
using AutoServices.Models.SaveYCJCService;
using CommonUtil;
using Cowboy.Sockets;
using Topshelf.Logging;

namespace AutoServices.Services
{
    /// <summary>
    /// 西安项目
    /// 在线扬尘监控
    /// 低 字 节 在 前 ， 高 字 节 在 后
    /// 
    /// 设备连接服务器之后，应先发送注册帧，否则服务器对该设备的数据不予处理。
    /// 
    /// </summary>
    public class DustMonitoringService
    {
        public TcpSocketClient _client = null;

        public static string IpAddress = "183.203.96.67";
        public static int Port = 10012;

        public bool isEnd = false;

        private BaseDataModel baseDataModel { get; set; }


        public static readonly LogWriter _log = HostLogger.Get<DustMonitoringService>();
        /// <summary>
        /// 设置设备id
        /// </summary>
        /// <param name="deviceId"></param>
        public DustMonitoringService()
        {
            try
            {
                var config = new TcpSocketClientConfiguration();
                //config.FrameBuilder = new FixedLengthFrameBuilder(13);
                //config.FrameBuilder = new LengthPrefixedFrameBuilder(2, true);
                config.FrameBuilder = new RawBufferFrameBuilder();

                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(IpAddress), Port);//服务器的IP和端口
                _client = new TcpSocketClient(remoteEP, config);
                _client.ServerConnected += client_ServerConnected;
                _client.ServerDisconnected += client_ServerDisconnected;
                _client.ServerDataReceived += client_ServerDataReceived;
                _client.Connect();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("DustMonitoring：TCP server Exception {0}.", ex.Message));
            }
            
        }


        private void client_ServerConnected(object sender, TcpServerConnectedEventArgs e)
        {
            Console.WriteLine(string.Format("TCP server {0} has connected.", e.RemoteEndPoint));
            //连接上服务器发送注册针
        }

        private void client_ServerDisconnected(object sender, TcpServerDisconnectedEventArgs e)
        {
            Console.WriteLine(string.Format("TCP server {0} has disconnected.", e.RemoteEndPoint));
        }

        private void client_ServerDataReceived(object sender, TcpServerDataReceivedEventArgs e)
        {
            byte[] ByteTemp = new byte[e.DataLength];
            Buffer.BlockCopy(e.Data, e.DataOffset, ByteTemp, 0, e.DataLength);

            //string dataStr = BitConverter.ToString(e.Data, e.DataOffset, e.DataLength).Replace("-", " ");//Encoding.ASCII.GetString(data1, 0, recv);

            ResultCode resultCode = DustMonitoringTools.GetResultCode(ByteTemp);
            if (resultCode == ResultCode.Success)
            {
                CMDCodeEnum codeEnum = DustMonitoringTools.CheckMsgType(ByteTemp);
                if (codeEnum == CMDCodeEnum.RegisterResult)
                {
                    //登记成功
                    _log.InfoFormat(DateTime.Now + Environment.NewLine + "登记成功");
                    string data = "{\"DataType\":\"Min\",\"DeviceId\":\"" + baseDataModel.deviceAddress + "\",\"DB\":\"" + baseDataModel.noise + "\",\"RecDate\":\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\"}";

                    UploadNoiseData(data);
                }
                else if (codeEnum == CMDCodeEnum.NoiseUploadResult)
                {
                    //噪音数据上传
                    _log.InfoFormat(DateTime.Now + Environment.NewLine + "噪音数据上传成功");
                    string data = "{\"DataType\":\"Min\",\"DeviceId\":\"" + baseDataModel.deviceAddress + "\",\"Dust\":\"0\",\"PM10\":\"" + baseDataModel.PM10 + "\",\"PM2.5\":\"" + baseDataModel.PM25 + "\",\"AirPressure\":\"" + baseDataModel.sensorCount + "\",\"Temperature\":\"" + baseDataModel.temperature + "\",\"Humidity\":\"" + baseDataModel.humidity + "\",\"WindDirection\":\"" + baseDataModel.windDirection + "\",\"WindSpeed\":\"" + baseDataModel.windSpeed + "\",\"RecDate\":\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\"}";
                    DustUploadData(data);
                }
                else if (codeEnum == CMDCodeEnum.DustUploadResult)
                {
                    //扬尘数据上传成功
                    _log.InfoFormat(DateTime.Now + Environment.NewLine + "扬尘数据上传成功");
                    isEnd = true;
                }
            }
            else
            {
                switch (resultCode)
                {
                    case ResultCode.NotFinddatafram:
                        _log.InfoFormat("{0}{1} 设备号：{2} 服务器没有找到数据帧", DateTime.Now, Environment.NewLine, baseDataModel.deviceAddress);
                        break;
                    case ResultCode.NoLogin:
                        _log.InfoFormat("{0}{1} 设备号：{2} 未登录", DateTime.Now, Environment.NewLine, baseDataModel.deviceAddress);
                        break;
                    case ResultCode.SystemError:
                        _log.InfoFormat("{0}{1} 设备号：{2} 服务器内部异常", DateTime.Now, Environment.NewLine, baseDataModel.deviceAddress);
                        break;
                    case ResultCode.DataRormateError:
                        _log.InfoFormat("{0}{1} 设备号：{2} 数据格式错误", DateTime.Now, Environment.NewLine, baseDataModel.deviceAddress);
                        break;
                    case ResultCode.None:
                        _log.InfoFormat("{0}{1} 设备号：{2} 未定义", DateTime.Now, Environment.NewLine, baseDataModel.deviceAddress);
                        break;
                }
                isEnd = true;
            }
        }

        /// <summary>
        /// 获取发送数据
        /// </summary>
        /// <returns></returns>
        public byte[] getData(SendDataMode sendDataMode)
        {
            return TypeHelper.strToToHexByte(sendDataMode.ToString());
        }

        /// <summary>
        /// 注册登记
        /// 15 01 01 00 00 00 00 17 00 7b 22 44 65 76 69 63 65 49 64 22 3a 22 74 65 73 74 31 32 33 34 22 7d 5A 02
        /// </summary>
        public void Regester(BaseDataModel _baseDataModel)
        {
            baseDataModel = _baseDataModel;

            string data = "{\"DeviceId\":\"" + baseDataModel.deviceAddress + "\"}";
            sendDataMethod(data, CMDCode.Register);
            int tryTimes = 0;
            do
            {
                Thread.Sleep(500);
                if (isEnd)
                {
                    break;
                }
                if (tryTimes >= 6)
                {
                    _client.Close();
                    _log.InfoFormat("{0}{1} DustMonitoringService 设备号：{2} 服务器响应超时链接已断开", DateTime.Now, Environment.NewLine, baseDataModel.deviceAddress);
                    break;
                }
                tryTimes++;
            } while (true);

            _client.Close();
            _client.Dispose();
        }

        /// <summary>
        /// 噪音数据上报
        /// </summary>
        public void UploadNoiseData(string data)
        {
            sendDataMethod(data, CMDCode.NoiseUpload);
        }

        /// <summary>
        /// 扬尘数据上报
        /// </summary>
        public void DustUploadData(string data)
        {
            sendDataMethod(data, CMDCode.DustUpload);
        }


        /// <summary>
        /// 上传数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="CMDCode"></param>
        /// <returns></returns>
        private void sendDataMethod(string data, string CMDCode)
        {
            SendDataMode sendDataMode = new SendDataMode();
            sendDataMode.CMD = CMDCode;
            sendDataMode.ID = 1.ToString("X2");
            sendDataMode.CID = TypeHelper.ReverseUShortBytes(0).ToString("X4");
            sendDataMode.TID = TypeHelper.ReverseUShortBytes(0).ToString("X4");
            sendDataMode.DL = TypeHelper.ReverseUShortBytes((ushort)data.Length).ToString("X4");
            sendDataMode.Data = TypeHelper.StringToHexString(data, Encoding.ASCII);

            byte[] sendData = getData(sendDataMode);
            tcpSend(sendData);
        }

        private void tcpSend(byte[] sendData)
        {
            if (_client.State == TcpSocketConnectionState.Connected)
            {
                _client.Send(sendData);//Encoding.ASCII.GetBytes(input)
            }
        }
    }
}
