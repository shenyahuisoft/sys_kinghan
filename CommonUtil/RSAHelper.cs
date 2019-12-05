using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CommonUtil
{

    public class RSAHelper
    {

        public RSAHelper()
        {
        }

        /// <summary>
        /// 获取服务器公钥（XML）
        /// </summary>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public string GetPublicKeyXML(out string privateKey)
        {
            //使用默认密钥创建RSACryptoServiceProvider 对象
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            //显示包含公钥/私钥对的XML 表示形式，如果只显示公钥，将参数改为false 即可
            privateKey = rsa.ToXmlString(true);
            string publickKey = rsa.ToXmlString(false);
            return publickKey;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="input">文本</param>
        /// <param name="publickKey">公钥</param>
        /// <returns></returns>
        public string Encrypt(string input, string publickKey)
        {
            try
            {
                UTF8Encoding enc = new UTF8Encoding();
                byte[] bytes = enc.GetBytes(input);
                RSACryptoServiceProvider crypt = new RSACryptoServiceProvider();
                crypt.FromXmlString(publickKey);
                bytes = crypt.Encrypt(bytes, false);
                string encryttext = Convert.ToBase64String(bytes);
                return encryttext;
            }
            catch (Exception ex)
            {
                return "ERROR:" + ex.Message;
            }
        }



        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encryptedString">加密的文本</param>
        /// <param name="privateKey">私钥</param>
        /// <returns></returns>
        public string Decrypt(string encryptedString, string privateKey)
        {
            try
            {
                RSACryptoServiceProvider crypt = new RSACryptoServiceProvider();
                UTF8Encoding enc = new UTF8Encoding();
                byte[] bytes = Convert.FromBase64String(encryptedString);
                //byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(encryptedString);

                crypt.FromXmlString(privateKey);
                byte[] decryptbyte = crypt.Decrypt(bytes, false);
                string decrypttext = enc.GetString(decryptbyte);
                return decrypttext;
            }
            catch (Exception ex)
            {
                return "ERROR:" + ex.Message;
            }
        }

        private Encoding _encode = Encoding.UTF8;

        public Encoding Encode
        {
            get { return _encode; }
            set { _encode = value; }
        }

        public string EncryptString(string EncryptString, string publickKey)
        {
            if (string.IsNullOrEmpty(EncryptString))
            {
                throw new NotSupportedException();
            }
            var encryptData = Convert.FromBase64String(EncryptString);
            var decryptData = this.decryptor(encryptData, publickKey);
            var decryptString = this.Encode.GetString(decryptData);
            return decryptString;
        }

        private byte[] decryptor(byte[] EncryptDada, string publickKey)
        {
            if (EncryptDada == null || EncryptDada.Length <= 0)
            {
                throw new NotSupportedException();
            }

            RSACryptoServiceProvider crypt = new RSACryptoServiceProvider();
            crypt.FromXmlString(publickKey);
            var decrtpyData = crypt.Decrypt(EncryptDada, false);
            return decrtpyData;
        }

        #region 基于证书的解密（解密IOS端传入的加密数据）
        #region 已注释代码
        //public string RSADecrypt(string rawText, RSACryptoServiceProvider rsa)
        //{
        //    try
        //    {
        //        int KEYLENGTH = 128;
        //        int BLOCKSIZE = KEYLENGTH - 11;
        //        var encryptedBytes = Convert.FromBase64String(rawText);
        //        //var encryptedBytes = Encoding.GetEncoding("utf-8").GetBytes(rawText);
        //        int numBlock = encryptedBytes.Length / KEYLENGTH;
        //        byte[] rawResult = new byte[0];
        //        var buffer = new byte[KEYLENGTH];
        //        for (var i = 0; i < numBlock; i++)
        //        {
        //            Array.Copy(encryptedBytes, i * KEYLENGTH, buffer, 0, buffer.Length);
        //            var decryptedBytes = rsa.Decrypt(buffer, false);
        //            var resultBuffer = new byte[rawResult.Length + decryptedBytes.Length];
        //            Array.Copy(rawResult, resultBuffer, rawResult.Length);
        //            Array.Copy(decryptedBytes, 0, resultBuffer, rawResult.Length, decryptedBytes.Length);
        //            rawResult = resultBuffer;
        //        }
        //        var plaintext = Encoding.UTF8.GetString(rawResult);
        //        return plaintext;
        //    }
        //    catch (CryptographicException e)
        //    {
        //        return e.Message;
        //    }
        //}

        //public RSACryptoServiceProvider GetPrivateKey()
        //{
        //    var clientCert = GetRSACertificate();
        //    var privateKry = (RSACryptoServiceProvider)clientCert.PrivateKey;
        //    return privateKry;
        //} 
        #endregion

        private X509Certificate2 GetRSACertificate()
        {
            string CERT = System.Configuration.ConfigurationSettings.AppSettings["Credential"].ToString();
            X509Certificate2 clientCert = null;
            if (clientCert == null)
            {
                var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly);
                foreach (var certificate in store.Certificates)
                {
                    if (certificate.GetNameInfo(X509NameType.SimpleName, false) == CERT)
                    {
                        clientCert = certificate;
                        break;
                    }
                }
            }
            return clientCert;
        }

        //private static X509Certificate2 GetRSACertificate()
        //{

        //    string CERT = System.Configuration.ConfigurationSettings.AppSettings["Credential"].ToString();

        //    X509Certificate2 clientCert = null;
        //    if (clientCert == null)
        //    {
        //        var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
        //        store.Open(OpenFlags.ReadOnly);
        //        foreach (var certificate in store.Certificates)
        //        {
        //            if (certificate.GetNameInfo(X509NameType.SimpleName, false) == CERT)
        //            {
        //                clientCert = certificate;
        //                break;
        //            }
        //        }
        //    }

        //    return clientCert;
        //}

        /// <summary>
        /// 获取服务器证书私钥（XML）
        /// </summary>
        /// <returns></returns>
        public string GetPrivateKeyXML()
        {
            var clientCert = GetRSACertificate();
            var privateKey = (RSACryptoServiceProvider)clientCert.PrivateKey;
            return privateKey.ToXmlString(true);
        }

        /// <summary>
        /// 获取服务器证书公钥（base64string）
        /// </summary>
        public string GetPublicKey()
        {
            var clientCert = GetRSACertificate();
            string publickey = Convert.ToBase64String(clientCert.GetRawCertData());
            return publickey;
        }

        #region 注释--解密方法
        //public static string RSADecrypt(string rawText, RSACryptoServiceProvider rsa)
        //{
        //    try
        //    {
        //        var encryptedBytes = Convert.FromBase64String(rawText);
        //        int numBlock = encryptedBytes.Length / KEYLENGTH;
        //        byte[] rawResult = new byte[0];
        //        var buffer = new byte[KEYLENGTH];
        //        for (var i = 0; i < numBlock; i++)
        //        {
        //            Array.Copy(encryptedBytes, i * KEYLENGTH, buffer, 0, buffer.Length);
        //            var decryptedBytes = rsa.Decrypt(buffer, false);
        //            var resultBuffer = new byte[rawResult.Length + decryptedBytes.Length];
        //            Array.Copy(rawResult, resultBuffer, rawResult.Length);
        //            Array.Copy(decryptedBytes, 0, resultBuffer, rawResult.Length, decryptedBytes.Length);
        //            rawResult = resultBuffer;
        //        }

        //        var plaintext = Encoding.UTF8.GetString(rawResult);
        //        return plaintext;
        //    }
        //    catch (CryptographicException e)
        //    {
        //        return e.Message;
        //    }
        //} 
        #endregion   

 
        #endregion

    }

}

