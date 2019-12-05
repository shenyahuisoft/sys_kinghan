using System;
using System.IO;
using System.Linq;

namespace CommonUtil
{
    public class UploadFileHelper
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileContent">文件内容（ base64）</param>
        /// <param name="filePath"> 完整的文件路径</param>
        /// <param name="readAsDataUrl"></param>
        /// <returns></returns>
        public static void Upload(string fileContent, string filePath, bool readAsDataUrl = false)
        {
            if (readAsDataUrl && fileContent.IndexOf(',') != -1)
            {
                fileContent = fileContent.Split(',').LastOrDefault();
            }
            fileContent = fileContent == null ? string.Empty : fileContent;
            var fileBytes = Convert.FromBase64String(fileContent);
            var file = new FileInfo(filePath);
            var dr = file.Directory;
            if (!dr.Exists)
            {
                dr.Create();
            }
            //生成文件
            var fsForWrite = new FileStream(filePath, FileMode.Create);
            try
            {
                //将字节数组写入文件
                fsForWrite.Write(fileBytes, 0, fileBytes.Length);
                fsForWrite.Flush();
            }
            finally
            {
                //关闭文件
                fsForWrite.Close();
                fsForWrite.Dispose();
            }

        }
        public static void Deletd(string filePath)
        {
            try
            {
                File.Delete(filePath);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
