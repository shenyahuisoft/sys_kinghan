using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtil
{
    public static class HttpClientHelper
    {
        /// <summary>
        /// 模拟Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string HttpGet(string url)
        {
            try
            {
                var serviceAddress = url;
                var request = (HttpWebRequest)WebRequest.Create(serviceAddress);
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";
                var response = (HttpWebResponse)request.GetResponse();
                var strm = response.GetResponseStream();
                var sr = new StreamReader(strm, Encoding.UTF8);
                string line;
                var sb = new StringBuilder();
                while ((line = sr.ReadLine()) != null)
                {
                    sb.Append(line + Environment.NewLine);
                }
                sr.Close();
                strm.Close();
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 模拟Post请求
        /// </summary>
        /// <param name="postUrl"></param>
        /// <param name="postdata">JsonData</param>
        /// <returns></returns>
        public static string HttpPost(string postUrl, string postdata)
        {
            try
            {
                var cc = new CookieContainer();
                var request = (HttpWebRequest)WebRequest.Create(postUrl);
                request.Method = "Post";
                request.ContentType = "application/json;charset=UTF-8";
                var postdatabyte = Encoding.UTF8.GetBytes(postdata);
                request.ContentLength = postdatabyte.Length;
                request.AllowAutoRedirect = false;
                request.CookieContainer = cc;
                request.KeepAlive = true;

                //提交请求
                var stream = request.GetRequestStream();
                stream.Write(postdatabyte, 0, postdatabyte.Length);
                stream.Close();

                //接收响应
                var response = (HttpWebResponse)request.GetResponse();
                response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);

                //CookieCollection cook = response.Cookies;
                ////Cookie字符串格式
                //string strcrook = request.CookieContainer.GetCookieHeader(request.RequestUri);
                var strm = response.GetResponseStream();

                var sr = new StreamReader(strm, Encoding.UTF8);

                string line;

                var sb = new StringBuilder();

                while ((line = sr.ReadLine()) != null)
                {
                    sb.Append(line + Environment.NewLine);
                }
                sr.Close();
                strm.Close();
                return sb.ToString();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 模拟表单提交
        /// </summary>
        /// <param name="url"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static string FormPost(string url, Dictionary<string, string> dic)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            #region 添加Post 参数
            StringBuilder builder = new StringBuilder();
            int i = 0;
            foreach (var item in dic)
            {
                if (i > 0)
                    builder.Append("&");
                builder.AppendFormat("{0}={1}", item.Key, item.Value);
                i++;
            }
            byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        /// <summary>
        /// http下载文件
        /// </summary>
        /// <param name="url">下载文件地址</param>
        /// <param name="path">文件存放地址，包含文件名</param>
        /// <returns></returns>
        public static Stream HttpDownload(string url, string path)
        {
            //string tempPath = System.IO.Path.GetDirectoryName(path) + @"\temp";
            //System.IO.Directory.CreateDirectory(tempPath);  //创建临时文件目录
            //string tempFile = tempPath + @"\" + System.IO.Path.GetFileName(path) + ".temp"; //临时文件
            //if (System.IO.File.Exists(tempFile))
            //{
            //    System.IO.File.Delete(tempFile);    //存在则删除
            //}
            try
            {
                // 设置参数
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //发送请求并获取相应回应数据
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                Stream responseStream = response.GetResponseStream();

                return responseStream;

                /*
                //创建本地文件写入流
                //Stream stream = new FileStream(tempFile, FileMode.Create);
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    //stream.Write(bArr, 0, size);
                    fs.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                }
                //stream.Close();
                fs.Close();
                responseStream.Close();

                return fs;

                // System.IO.File.Move(tempFile, path);
                //  return true;
                */
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
