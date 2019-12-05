using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CommonUtil
{
    public static class EncryptHelper
    {

        /// <summary>
        /// 密钥，且必须为8位数字或字符，【##尚不确定是否区分大小写或其他字符##】
        /// </summary>
        private static string key = "20160628";

        /// <summary>
        /// App同步时用到的加密
        /// </summary>
        /// <param name="sourceString">要加密的字符串</param>
        /// <returns>已加密的字符串</returns>
        public static string AppSyncEncrypt(string sourceString)
        {
            byte[] btKey = Encoding.UTF8.GetBytes(key);
            byte[] btIV = Encoding.UTF8.GetBytes(key);

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Encoding.UTF8.GetBytes(sourceString);
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// App同步时用到的解密
        /// </summary>
        /// <param name="encryptString">要解密的字符串</param>
        /// <returns>已解密的字符串</returns>
        public static string AppSyncDecrypt(string encryptString)
        {
            //转义特殊字符
            encryptString = encryptString.Replace("-", "+").Replace("_", "/").Replace("~", "=");

            byte[] inputByteArray = Convert.FromBase64String(encryptString);

            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = ASCIIEncoding.ASCII.GetBytes(key);
                des.IV = ASCIIEncoding.ASCII.GetBytes(key);

                System.IO.MemoryStream ms = new System.IO.MemoryStream();

                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }

                string str = Encoding.UTF8.GetString(ms.ToArray());

                ms.Close();

                return str;
            }

        }


        #region Sha256

        /// <summary>
        /// sha256加密
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string Sha256Encryption(this string context)
        {
            var sha256Data = Encoding.Default.GetBytes(context);
            var sha256 = new SHA256Managed();
            var result = sha256.ComputeHash(sha256Data);
            return BitConverter.ToString(result).Replace("-", "").ToUpper();
        }

        #endregion

    }
}
