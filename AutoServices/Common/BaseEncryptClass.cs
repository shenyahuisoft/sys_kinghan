using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AutoServices.Common
{
    /// <summary>
    /// 加密基础类
    /// </summary>
    public class BaseEncryptClass
    {

        #region 变量

        /// <summary>
        /// 密钥
        /// </summary>
        private string m_CstrKey = "qJzGEh6hESZDVJeCnFPGuxzaiB7NLQM3";

        /// <summary>
        /// 加密矢量
        /// </summary>
        private string m_CstrVector = "qcDY6X+aPLw=";

        #endregion


        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseEncryptClass()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strKey">密钥</param>
        public BaseEncryptClass(string strKey)
        {
            m_CstrKey = strKey;
        }

        #endregion


        #region Encrypt

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="encryptValue">要加密的字符串</param>
        /// <returns>加密字符串</returns>
        public virtual string Encrypt(string encryptValue)
        {
            //--------------------------------------------------------------------------------
            //数据检查
            if (encryptValue == null)
            {
                return null;
            }
            if (encryptValue.Equals(""))
            {
                return "";
            }
            //--------------------------------------------------------------------------------
            //设定加密器
            SymmetricAlgorithm objCSP = new TripleDESCryptoServiceProvider();
            //设置加密的密钥
            objCSP.Key = Convert.FromBase64String(m_CstrKey);
            //设置加密的矢量
            objCSP.IV = Convert.FromBase64String(m_CstrVector);
            //设置加密的运算模式
            objCSP.Mode = System.Security.Cryptography.CipherMode.ECB;
            //设置置加密算法的填充模式
            objCSP.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            //--------------------------------------------------------------------------------
            //建立加密器
            ICryptoTransform objCryptoTransform = objCSP.CreateEncryptor(objCSP.Key, objCSP.IV);
            if (objCryptoTransform == null)
            {
                return null;
            }
            //--------------------------------------------------------------------------------
            //获得以UTF8编码格式的子节数组
            byte[] aryByte = Encoding.UTF8.GetBytes(encryptValue);
            //--------------------------------------------------------------------------------
            //定义加密流
            MemoryStream objMemoryStream = new MemoryStream();
            CryptoStream objCryptoStream = new CryptoStream(objMemoryStream,
                objCryptoTransform, CryptoStreamMode.Write);
            //--------------------------------------------------------------------------------
            if (objCryptoStream == null)
            {
                return null;
            }
            objCryptoStream.Write(aryByte, 0, aryByte.Length);
            //--------------------------------------------------------------------------------
            aryByte = null;
            //--------------------------------------------------------------------------------
            //释放并关闭加密流
            objCryptoStream.FlushFinalBlock();
            objCryptoStream.Close();
            objCryptoStream = null;
            //--------------------------------------------------------------------------------
            return Convert.ToBase64String(objMemoryStream.ToArray());
        }

        #endregion


        #region Decrypt

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="decryptValue">要解密的字符串</param>
        /// <returns>解密的字符串</returns>
        public virtual string Decrypt(string decryptValue)
        {
            //--------------------------------------------------------------------------------
            //数据检查
            if (decryptValue == null)
            {
                return null;
            }
            if (decryptValue.Equals(""))
            {
                return "";
            }
            //--------------------------------------------------------------------------------
            //设定加密器
            SymmetricAlgorithm objCSP = new TripleDESCryptoServiceProvider();
            //设置加密的密钥
            objCSP.Key = Convert.FromBase64String(m_CstrKey);
            //设置加密的矢量
            objCSP.IV = Convert.FromBase64String(m_CstrVector);
            //设置加密的运算模式
            objCSP.Mode = System.Security.Cryptography.CipherMode.ECB;
            //设置置加密算法的填充模式
            objCSP.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            //--------------------------------------------------------------------------------
            //建立解密器
            ICryptoTransform objCryptoTransform = objCSP.CreateDecryptor(objCSP.Key, objCSP.IV);
            if (objCryptoTransform == null)
            {
                return null;
            }
            //--------------------------------------------------------------------------------
            //把要解密的字符串变为字符数组
            byte[] aryByte = Convert.FromBase64String(decryptValue);
            //--------------------------------------------------------------------------------
            //定义解密流
            MemoryStream objMemoryStream = new MemoryStream();
            CryptoStream objCryptoStream = new CryptoStream(objMemoryStream,
                objCryptoTransform, CryptoStreamMode.Write);
            //--------------------------------------------------------------------------------
            if (objCryptoStream == null)
            {
                return null;
            }
            objCryptoStream.Write(aryByte, 0, aryByte.Length);
            //--------------------------------------------------------------------------------
            aryByte = null;
            //--------------------------------------------------------------------------------
            //释放并关闭加密流
            objCryptoStream.FlushFinalBlock();
            objCryptoStream.Close();
            objCryptoStream = null;
            //--------------------------------------------------------------------------------
            return Encoding.UTF8.GetString(objMemoryStream.ToArray());
        }

        #endregion

    }
}
