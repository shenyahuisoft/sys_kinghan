using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;

namespace CommonUtil
{

    /// ;summary; 
    /// UnZip 类用于解压缩一个 zip 文件。 
    /// ;/summary; 
    public class UnZipDir
    {
        /// ;summary; 
        /// 解压缩一个 zip 文件。 
        /// ;/summary; 
        /// ;param name="zipFileName";要解压的 zip 文件。;/param; 
        /// ;param name="extractLocation";zip 文件的解压目录。;/param; 
        /// ;param name="password";zip 文件的密码。;/param; 
        /// ;param name="overWrite";是否覆盖已存在的文件。;/param; 
        public UnZipDir(string zipFileName, string extractLocation, string password, bool overWrite)
        {
            #region 实现
            string myExtractLocation = extractLocation;
            if (myExtractLocation == "")
                myExtractLocation = Directory.GetCurrentDirectory();
            if (!myExtractLocation.EndsWith(@"\"))
                myExtractLocation = myExtractLocation + @"\";

            ZipInputStream s = new ZipInputStream(File.OpenRead(zipFileName));
            s.Password = password;
            try
            {
                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    string directoryName = "";
                    string pathToZip = "";
                    pathToZip = theEntry.Name;

                    if (pathToZip != "")
                        directoryName = Path.GetDirectoryName(pathToZip) + @"\";
                    string fileName = Path.GetFileName(pathToZip);
                    Directory.CreateDirectory(myExtractLocation + directoryName);
                    if (fileName != "")
                    {
                        try
                        {
                            if ((File.Exists(myExtractLocation + directoryName + fileName) && overWrite) || (!File.Exists(myExtractLocation + directoryName + fileName)))
                            {
                                FileStream streamWriter = File.Create(myExtractLocation + directoryName + fileName);
                                int size = 2048;
                                byte[] data = new byte[2048];
                                while (true)
                                {
                                    size = s.Read(data, 0, data.Length);
                                    if (size > 0)
                                        streamWriter.Write(data, 0, size);
                                    else
                                        break;
                                }
                                streamWriter.Close();
                            }
                        }
                        catch (ZipException ex)
                        {
                            FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "log.txt", FileMode.OpenOrCreate, FileAccess.Write);
                            StreamWriter sw = new StreamWriter(fs);
                            sw.WriteLine(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                s.Close();
            }
            #endregion
        }

        /// ;summary; 
        /// 解压缩一个 zip 文件。 
        /// ;/summary; 
        /// ;param name="zipFileName";要解压的 zip 文件。;/param; 
        /// ;param name="extractLocation";zip 文件的解压目录。;/param; 
        /// ;param name="password";zip 文件的密码。;/param; 
        public UnZipDir(string zipFileName, string extractLocation, string password)
            : this(zipFileName, extractLocation, password, true)
        {
        }

        /// ;summary; 
        /// 解压缩一个 zip 文件。 
        /// ;/summary; 
        /// ;param name="zipFileName";要解压的 zip 文件。;/param; 
        /// ;param name="extractLocation";zip 文件的解压目录。;/param; 
        /// ;param name="overWrite";是否覆盖已存在的文件。;/param; 
        public UnZipDir(string zipFileName, string extractLocation, bool overWrite)
            : this(zipFileName, extractLocation, "", overWrite)
        {
        }

        /// ;summary; 
        /// 解压缩一个 zip 文件。 
        /// ;/summary; 
        /// ;param name="zipFileName";要解压的 zip 文件。;/param; 
        /// ;param name="extractLocation";zip 文件的解压目录。;/param; 
        public UnZipDir(string zipFileName, string extractLocation)
            : this(zipFileName, extractLocation, "", true)
        {
        }
    }

    /// ;summary; 
    /// UnZip 类用于解压缩一个 zip 文件。 
    /// ;/summary; 
    public class ZipDir
    {
        /// <summary>
        /// 压缩一个 zip 文件
        /// </summary>
        /// <param name="zipFileName">要生成的 zip 文件名</param>
        /// <param name="fileList">要压缩的文件名列表</param>
        /// <param name="ziplevel">压缩等级</param>
        public ZipDir(string zipFileName, Hashtable fileList, int ziplevel)
        {
            #region 实现
            ZipOutputStream zipoutputstream = new ZipOutputStream(File.Create(zipFileName));
            zipoutputstream.SetLevel(ziplevel);
            Crc32 crc = new Crc32();
            foreach (DictionaryEntry item in fileList)
            {
                FileStream fs = File.OpenRead(item.Key.ToString());
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                ZipEntry entry = new ZipEntry(item.Value.ToString());
                entry.Size = fs.Length;
                fs.Close();
                crc.Reset();
                crc.Update(buffer);
                entry.Crc = crc.Value;
                zipoutputstream.PutNextEntry(entry);
                zipoutputstream.Write(buffer, 0, buffer.Length);
            }
            zipoutputstream.Finish();
            zipoutputstream.Close();
            #endregion
        }

        /// <summary>
        /// 压缩一个 zip 文件
        /// </summary>
        /// <param name="zipFileName">要生成的 zip 文件名</param>
        /// <param name="fileList">要压缩的文件名列表</param>
        public ZipDir(string zipFileName, Hashtable fileList)
            : this(zipFileName, fileList, 6)
        {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="kvList"></param>
        /// <param name="tempFolder"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static byte[] ZipFileBinarys(IList<KVPair> kvList, string tempFolder, out string fileName)
        {
            int ziplevel = 6;
            fileName = tempFolder + Guid.NewGuid().ToString() + ".zip";

            #region 实现

            if (!Directory.Exists(tempFolder))
            {
                Directory.CreateDirectory(tempFolder);
            }

            ZipOutputStream zipoutputstream = new ZipOutputStream(File.Create(fileName));
            zipoutputstream.SetLevel(ziplevel);
            Crc32 crc = new Crc32();
            foreach (KVPair kv in kvList)
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

                ZipEntry entry = new ZipEntry(kv.Key);
                entry.Size = byteArray.Length;
                crc.Reset();
                crc.Update(byteArray);
                entry.Crc = crc.Value;
                zipoutputstream.PutNextEntry(entry);
                zipoutputstream.Write(byteArray, 0, byteArray.Length);
            }
            zipoutputstream.Finish();
            zipoutputstream.Close();

            FileStream stream = new FileStream(fileName, FileMode.Open);
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            stream.Close();

            #endregion

            //File.Delete(fileName);//删除缓存文件

            return buffer;
        }



    }
}