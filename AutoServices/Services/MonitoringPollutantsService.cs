using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoServices.Models.MonitoringPollutants;
using AutoServices.Models.SaveYCJCService;
using CommonUtil;
using Cowboy.Sockets;
using Topshelf.Logging;

namespace AutoServices.Services
{
    /// <summary>
    /// 开封项目
    /// </summary>
    public class MonitoringPollutantsService
    {
        private TcpSocketClient _client = null;

        public static string IpAddress = "47.104.195.12";
        public static int Port = 12222;
        public bool isEnd = false;

        public static readonly LogWriter _log = HostLogger.Get<DustMonitoringService>();

        public MonitoringPollutantsService()
        {
            try
            {
                var config = new TcpSocketClientConfiguration();
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
                Console.WriteLine(string.Format("MonitoringPollutants：TCP server Exception {0}.", ex.Message));
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
            isEnd = true;
        }

        private void client_ServerDataReceived(object sender, TcpServerDataReceivedEventArgs e)
        {
            byte[] ByteTemp = new byte[e.DataLength];
            Buffer.BlockCopy(e.Data, e.DataOffset, ByteTemp, 0, e.DataLength);

            //string dataStr = BitConverter.ToString(e.Data, e.DataOffset, e.DataLength).Replace("-", " ");//Encoding.ASCII.GetString(data1, 0, recv);

            isEnd = true;
        }


        /// <summary>
        /// 噪音数据上报
        /// 
        //string.Format("&&DataTime={0};" +
        //        "a01001-Rtd={1},a01001-Flag=N;" +
        //        "a01002-Rtd={2},a01002-Flag=N;" +
        //        "a01006-Rtd={3},a01006-Flag=N;" +
        //        "a01007-Rtd={4},a01007-Flag=N;" +
        //        "a01008-Rtd={5},a01008-Flag=N;" +
        //        "a34001-Rtd={6},a34001-Flag=N;" +
        //        "a34002-Rtd={7},a34002-Flag=N;" +
        //        "a34004-Rtd={8},a34004-Flag=N;" +
        //        "La-Rtd={9},La-Flag=N;" +
        //        "longitude={10};latitude={11};&&",
        //                DateTime.Now.ToString("yyyyMMddhhmmss"),
        //        _baseDataModel.temperature,     //温度 摄氏度
        //        _baseDataModel.humidity,     //湿度 %
        //        0,     //气压 千帕
        //        _baseDataModel.windSpeed,     //风速 米/秒
        //        _baseDataModel.windDirection,     //风向 角度
        //        _baseDataModel.TSP,     //总悬浮颗粒物 TSP
        //        _baseDataModel.PM10,     //可吸入颗粒物 PM10
        //        _baseDataModel.PM25,     //细微颗粒物 PM2.5
        //        _baseDataModel.noise,     //噪声 分贝
        //        _baseDataModel.longitude,
        //        _baseDataModel.latitude
        //        )
        /// </summary>
        public void UploadData(BaseDataModel _baseDataModel)
        {
            RequstModel requstModel = new RequstModel();
            StringBuilder cpsb = new StringBuilder();
            cpsb.Append("&&").AppendFormat("DataTime={0};", DateTime.Now.ToString("yyyyMMddhhmmss"));

            if (!string.IsNullOrEmpty(_baseDataModel.temperature))//温度 摄氏度
            {
                cpsb.AppendFormat("a01001-Rtd={0},a01001-Flag=N;", _baseDataModel.temperature);
            }
            if (!string.IsNullOrEmpty(_baseDataModel.humidity))//湿度 %
            {
                cpsb.AppendFormat("a01002-Rtd={0},a01002-Flag=N;", _baseDataModel.humidity);
            }
            if (!string.IsNullOrEmpty(_baseDataModel.windSpeed))//风速 米/秒
            {
                cpsb.AppendFormat("a01007-Rtd={0},a01007-Flag=N;", _baseDataModel.windSpeed);
            }
            if (!string.IsNullOrEmpty(_baseDataModel.windDirection))//风向 角度
            {
                cpsb.AppendFormat("a01008-Rtd={0},a01008-Flag=N;", _baseDataModel.windDirection);
            }
            if (!string.IsNullOrEmpty(_baseDataModel.TSP))//总悬浮颗粒物 TSP
            {
                cpsb.AppendFormat("a34001-Rtd={0},a34001-Flag=N;", _baseDataModel.TSP);
            }
            if (!string.IsNullOrEmpty(_baseDataModel.PM10))//可吸入颗粒物 PM10
            {
                cpsb.AppendFormat("a34002-Rtd={0},a34002-Flag=N;", _baseDataModel.PM10);
            }
            if (!string.IsNullOrEmpty(_baseDataModel.PM25))//细微颗粒物 PM2.5
            {
                cpsb.AppendFormat("a34004-Rtd={0},a34004-Flag=N;", _baseDataModel.PM25);
            }
            if (!string.IsNullOrEmpty(_baseDataModel.noise))//噪声 分贝
            {
                cpsb.AppendFormat("La-Rtd={0},La-Flag=N;", _baseDataModel.noise);
            }
            if (!string.IsNullOrEmpty(_baseDataModel.longitude))//经度
            {
                cpsb.AppendFormat("longitude={0};", _baseDataModel.longitude);
            }
            if (!string.IsNullOrEmpty(_baseDataModel.latitude))//纬度
            {
                cpsb.AppendFormat("latitude={0};", _baseDataModel.latitude);
            }
            cpsb.Append("&&");

            string resultStr = requstModel.ToString(new DataModel()
            {
                QN = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                ST = "39",
                CN = "2011",
                PW = "123456",
                MN = _baseDataModel.deviceAddress,
                Flag = "5",
                CP = cpsb.ToString(),
            });
            byte[] sendData = Encoding.ASCII.GetBytes(resultStr);
            tcpSend(sendData);

            //do
            //{
            //    Thread.Sleep(1000);
            //    if (isEnd)
            //    {
            //        break;
            //    }
            //} while (true);

            _log.InfoFormat("{0}{1} MonitoringPollutantsTask 数据上传完成 设备号：{2}", DateTime.Now, Environment.NewLine, _baseDataModel.deviceAddress);
            _client.Close();
            _client.Dispose();
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
