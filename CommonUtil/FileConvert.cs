using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CommonUtil
{
    public class FileConvert
    {
        /// <summary>
        /// 获取文件名称
        /// </summary>
        /// <param name="fileFullName"></param>
        /// <returns></returns>
        public static string GetFileName(string fileFullName)
        {
            int startIndex = fileFullName.LastIndexOf('\\') + 1;

            int endIndex = fileFullName.LastIndexOf('.');
            if (endIndex < 0)
            {
                endIndex = fileFullName.Length;
            }

            int length = endIndex - startIndex;

            return fileFullName.Substring(startIndex, length);
        }

        /// <summary>
        /// 获取文件后缀
        /// </summary>
        /// <param name="fileFullName"></param>
        /// <returns></returns>
        public static string GetFileExtention(string fileFullName)
        {
            if (fileFullName.LastIndexOf('.') > 0)
            {
                return fileFullName.Substring(fileFullName.LastIndexOf('.') + 1);
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获取文件全名
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileExtention"></param>
        /// <returns></returns>
        public static string GetFileFullName(string fileName, string fileExtention)
        {
            if (fileExtention.Length > 0)
            {
                return fileName + "." + fileExtention;
            }
            else
            {
                return fileName;
            }
        }

        /// <summary>
        /// 获取文件大小显示文本
        /// </summary>
        /// <param name="fileSize"></param>
        /// <param name="format">b,k,k1,k2,k3,m,m1,m2,m3,g,g1,g2,g3,空字符串</param>
        /// <returns></returns>
        public static string GetFileSizeString(long fileSize, string format)
        {
            decimal kSize = (decimal)fileSize / 1024;
            decimal mSize = kSize / 1024;
            decimal gSize = mSize / 1024;

            switch (format.ToLower())
            {
                case "b": return fileSize + "B";
                case "k": return kSize.ToString("0.00") + "KB";
                case "k1": return kSize.ToString("0.0") + "KB";
                case "k2": return kSize.ToString("0.00") + "KB";
                case "k3": return kSize.ToString("0.000") + "KB";
                case "m": return mSize.ToString("0.00") + "MB";
                case "m1": return mSize.ToString("0.0") + "MB";
                case "m2": return mSize.ToString("0.00") + "MB";
                case "m3": return mSize.ToString("0.000") + "MB";
                case "g": return gSize.ToString("0.00") + "GB";
                case "g1": return gSize.ToString("0.0") + "GB";
                case "g2": return gSize.ToString("0.00") + "GB";
                case "g3": return gSize.ToString("0.000") + "GB";
                default:
                    if (gSize > 1) return gSize.ToString("0.00") + "GB";
                    if (mSize > 1) return mSize.ToString("0.00") + "MB";
                    if (kSize > 1) return kSize.ToString("0.00") + "KB";
                    return fileSize + "B";
            }
        }

        /// <summary>
        /// 将二进制写入文件
        /// </summary>
        /// <param name="path">要保存的文件路径</param>
        /// <param name="buffer">文件二进制</param>
        public static void SaveFile(string path, byte[] buffer)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            FileStream fileStream = new FileStream(path, FileMode.Create);
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            binaryWriter.Write(buffer);
            binaryWriter.Close();
        }

        /// <summary>
        /// 读取文件二进制
        /// </summary>
        /// <param name="path">要读取的文件路径</param>
        /// <returns></returns>
        public static byte[] ReadFile(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllBytes(path);
            }
            return null;
        }

        /// <summary>
        /// 读取文件文本
        /// </summary>
        /// <param name="path">要读取的文件路径</param>
        /// <returns></returns>
        public static string ReadFileString(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            return "";
        }

    }

}
