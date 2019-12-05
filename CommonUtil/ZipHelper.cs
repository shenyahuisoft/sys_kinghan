using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;

namespace CommonUtil
{
    /// <summary>
    /// 压缩解压缩
    /// </summary>
    public class ZipHelper
    {

        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="sourceArchiveFileName">D:\data.zip</param>
        /// <param name="destinationDirectoryName">D:\data\unzip</param>
        public void ExtractZip(string sourceArchiveFileName, string destinationDirectoryName)
        {
            ZipFile.ExtractToDirectory(sourceArchiveFileName, destinationDirectoryName);
        }

        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="sourceDirectoryName">D:\data</param>
        /// <param name="destinationArchiveFileName">D:\data.zip</param>
        public void CreateZip(string sourceDirectoryName, string destinationArchiveFileName)
        {
            ZipFile.CreateFromDirectory(sourceDirectoryName, destinationArchiveFileName);
        }

        /// <summary>
        /// 打包压缩文件并返回二进制
        /// </summary>
        /// <param name="kvList"></param>
        /// <param name="tempFolder"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public byte[] ZipFileBinarys(IList<KVPair> fileList, string tempFolder)
        {
            string sourceDirectoryName = tempFolder + Guid.NewGuid().ToString();
            string destinationArchiveFileName = sourceDirectoryName + ".zip";

            if (!Directory.Exists(sourceDirectoryName))
            {
                Directory.CreateDirectory(sourceDirectoryName);
            }

            foreach (KVPair kv in fileList)
            {
                if (kv.Value == null)
                {
                    continue;
                }

                byte[] byteArray;
                if (kv.Value.GetType().Equals(typeof(byte[])))
                {
                    byteArray = (byte[])kv.Value;
                }
                else
                {
                    byteArray = System.Text.Encoding.UTF8.GetBytes(kv.Value.ToString());
                }

                FileConvert.SaveFile(sourceDirectoryName + "/" + kv.Key, byteArray);
            }

            ZipFile.CreateFromDirectory(sourceDirectoryName, destinationArchiveFileName);

            FileStream stream = new FileStream(destinationArchiveFileName, FileMode.Open);
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            stream.Close();

            return buffer;
        }

    }
}