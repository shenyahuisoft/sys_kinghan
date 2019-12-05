using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using AutoServices.SaveYCJCService01;

namespace AutoServices.Common
{
    /// <summary>
    /// 使用说明
    /// 引用dll:System.ServiceModel，System.ServiceModel.Web
    /// var emsReceiveService = WcfHelper.CreateWcfServiceByUrl<IService1>("http://message.dcjet.com.cn/SmsGateway.svc", "basicHttpBinding");
    /// emsReceiveService 直接就可以调用到你wcf里的方法。
    /// emsReceiveService.SendMessage("", "", "");
    /// </summary>
    public class WcfHelper
    {
        #region Wcf服务工厂

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public static T CreateWcfServiceByUrl<T>(string url)
        {
            return CreateWcfServiceByUrl<T>(url, "wsHttpBinding");
        }

        /// <summary>
        /// 泛型类调用方式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="bing"></param>
        /// <returns></returns>
        public static T CreateWcfServiceByUrl<T>(string url, string bing)
        {
            if (string.IsNullOrEmpty(url)) throw new NotSupportedException("this url isn`t Null or Empty!");
            var address = new EndpointAddress(url);
            Binding binding = CreateBinding(bing);
            var factory = new ChannelFactory<T>(binding, address);
            return factory.CreateChannel();
        }

        /// <summary>
        /// 固定客户端调用方式
        /// </summary>
        /// <param name="url"></param>
        /// <param name="bing"></param>
        /// <returns></returns>
        public static SaveYCJCServicePortTypeClient CreateWcfServiceByUrl(string url, string bing)
        {
            if (string.IsNullOrEmpty(url)) throw new NotSupportedException("this url isn`t Null or Empty!");
            var address = new EndpointAddress(url);
            Binding binding = CreateBinding(bing);
            return new SaveYCJCService01.SaveYCJCServicePortTypeClient(binding, address);
        }
        #endregion

        #region 创建传输协议

        /// <summary>
        /// 创建传输协议
        /// </summary>
        /// <param name="binding">传输协议名称</param>
        /// <returns></returns>
        private static Binding CreateBinding(string binding)
        {
            Binding bindinginstance = null;
            if (binding.ToLower() == "basichttpbinding")
            {
                var ws = new BasicHttpBinding
                {
                    MaxBufferSize = 2147483647,
                    MaxBufferPoolSize = 2147483647,
                    MaxReceivedMessageSize = 2147483647,
                    ReaderQuotas = { MaxStringContentLength = 2147483647 },
                    CloseTimeout = new TimeSpan(0, 10, 0),
                    OpenTimeout = new TimeSpan(0, 10, 0),
                    ReceiveTimeout = new TimeSpan(0, 10, 0),
                    SendTimeout = new TimeSpan(0, 10, 0)
                };

                bindinginstance = ws;
            }
            else if (binding.ToLower() == "netnamedpipebinding")
            {
                var ws = new NetNamedPipeBinding { MaxReceivedMessageSize = 65535000 };
                bindinginstance = ws;
            }
            else if (binding.ToLower() == "netpeertcpbinding")
            {
                var ws = new NetPeerTcpBinding { MaxReceivedMessageSize = 65535000 };
                bindinginstance = ws;
            }
            else if (binding.ToLower() == "nettcpbinding")
            {
                var ws = new NetTcpBinding { MaxReceivedMessageSize = 65535000, Security = { Mode = SecurityMode.None } };
                bindinginstance = ws;
            }
            else if (binding.ToLower() == "wsdualhttpbinding")
            {
                var ws = new WSDualHttpBinding { MaxReceivedMessageSize = 65535000 };

                bindinginstance = ws;
            }
            else if (binding.ToLower() == "webhttpbinding")
            {
                var ws = new WebHttpBinding { MaxReceivedMessageSize = 65535000 };
                bindinginstance = ws;
            }
            else if (binding.ToLower() == "wsfederationhttpbinding")
            {
                var ws = new WSFederationHttpBinding { MaxReceivedMessageSize = 65535000 };
                bindinginstance = ws;
            }
            else if (binding.ToLower() == "wshttpbinding")
            {
                var ws = new WSHttpBinding(SecurityMode.None) { MaxReceivedMessageSize = 65535000 };
                ws.Security.Message.ClientCredentialType = System.ServiceModel.MessageCredentialType.Windows;
                ws.Security.Transport.ClientCredentialType = System.ServiceModel.HttpClientCredentialType.Windows;
                bindinginstance = ws;
            }
            return bindinginstance;

        }

        #endregion
    }
}
