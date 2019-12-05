using System;
using System.Net;
using System.Text;
using System.Threading;
using AutoServices.Models.SaveYCJCService;
using AutoServices.Models.TBJDataService;
using Cowboy.Sockets;
using Topshelf.Logging;

namespace AutoServices.Services
{
    /// <summary>
    /// 特必佳 数据上传
    /// </summary>
    public class TBJDataUploadService
    {
        private TcpSocketClient _client = null;

        public static string IpAddress = "36.99.46.212";
        public static int Port = 9161;
        public bool isEnd = false;

        public static readonly LogWriter _log = HostLogger.Get<TBJDataUploadService>();

        public TBJDataUploadService()
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
                Console.WriteLine(string.Format("TBJDataUpload：TCP server Exception {0}.", ex.Message));
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
            string dataStr = BitConverter.ToString(e.Data, e.DataOffset, e.DataLength).Replace("-", " ");

            string resultStr = Encoding.ASCII.GetString(e.Data, e.DataOffset, e.DataLength);

            isEnd = true;
        }

        /// <summary>
        /// 噪音数据上报
        /// </summary>
        public void UploadData(BaseDataModel _baseDataModel)
        {
            RequstModel requstModel = new RequstModel();
            string resultStr = requstModel.ToString(new DataModel()
            {
                Humidity = _baseDataModel.humidity,
                Noise = _baseDataModel.noise,
                PM10 = _baseDataModel.PM10,
                PM25 = _baseDataModel.PM25,
                Temperature = _baseDataModel.temperature,
                WindDirection = _baseDataModel.windDirection,
                WindSpeed = _baseDataModel.windSpeed,

                TerminalNumber = _baseDataModel.deviceAddress,
                DeviceAddress = _baseDataModel.deviceAddress
            });
            byte[] sendData = Encoding.ASCII.GetBytes(resultStr);
            tcpSend(sendData);

            do
            {
                Thread.Sleep(1000);
                if (isEnd)
                {
                    break;
                }
            } while (true);

            //_log.InfoFormat("{0}{1} MonitoringPollutantsTask 数据上传完成 设备号：{2}", DateTime.Now, Environment.NewLine, _baseDataModel.deviceAddress);
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
