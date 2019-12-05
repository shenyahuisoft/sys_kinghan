using System;
using System.Collections;
using System.Data;
using DatabaseLayer;
using System.Security.Cryptography;
using System.Text;

namespace Entity
{
    public class EncryptTools
    {

        /// <summary>
        /// �ַ�������
        /// </summary>
        /// <param name="estr"></param>
        /// <returns></returns>
        public static string Decrypt(string estr)
        {
            BaseEncryptClass class2 = new BaseEncryptClass();
            return class2.Decrypt(estr);
        }

        /// <summary>
        /// �ַ�������
        /// </summary>
        /// <param name="ostr"></param>
        /// <returns></returns>
        public static string Encrypt(string ostr)
        {
            BaseEncryptClass class2 = new BaseEncryptClass();
            return class2.Encrypt(ostr);
        }

        /// <summary>
        /// ������ܣ�����
        /// </summary>
        /// <param name="ostr"></param>
        /// <returns></returns>
        public static string EncryptPassword(string ostr)
        {
            return EncryptMD5(Encrypt(ostr));
        }

        /// <summary>
        /// �ַ���MD5����
        /// </summary>
        /// <param name="ostr"></param>
        /// <returns></returns>
        public static string EncryptMD5(string ostr)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(ostr));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

    }
}
