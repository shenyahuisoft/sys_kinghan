using System;
using System.Data;
using System.Configuration;
using System.Web;

namespace Entity
{
    public class PathHelper
    {

        public static string GetRootPath()
        {
            return new EntityManager().SystemEnvironment.MapPath;
        }


        public static string GetRootUrl()
        {
            return System.Configuration.ConfigurationManager.AppSettings["MapUrl"].ToString();
        }



        #region Library

        /// <summary>
        /// 获取资料库的绝对路径
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static string GetLibraryFolderPath()
        {
            string path = GetAttachmentFolderPath() + "Library\\";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            return path;
        }




        /// <summary>
        /// 获取资料库图片的绝对路径
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static string GetLibraryCoverFolderPath()
        {
            string path = GetAttachmentFolderPath() + "LibraryCover\\";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            return path;
        }

        /// <summary>
        /// 获取资料库的绝对路径
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static string GetLibraryFolderPath(string subFolder)
        {
            string path = GetLibraryFolderPath() + subFolder + "\\";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            return path;
        }

        #endregion


        #region Temp

        /// <summary>
        /// 获取临时文件夹的绝对路径
        /// </summary>
        /// <returns></returns>
        public static string GetTempFolderPath()
        {
            string path = GetRootPath() + "Temp\\";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            return path;
        }

        /// <summary>
        /// 获取临时文件夹的绝对路径
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static string GetTempFolderPath(string subFolder)
        {
            string path = GetTempFolderPath() + subFolder + "\\";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            return path;
        }

        /// <summary>
        /// 获取临时输入文件夹的绝对路径
        /// </summary>
        /// <returns></returns>
        public static string GetInputTempFolderPath()
        {
            string path = GetRootPath() + "Temp\\Input\\";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            return path;
        }

        /// <summary>
        /// 获取临时输入文件夹的绝对路径
        /// </summary>
        /// <param name="subFolder"></param>
        /// <returns></returns>
        public static string GetInputTempFolderPath(string subFolder)
        {
            string path = GetInputTempFolderPath() + subFolder + "\\";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            return path;
        }

        /// <summary>
        /// 获取临时输出文件夹的绝对路径
        /// </summary>
        /// <returns></returns>
        public static string GetOutputTempFolderPath()
        {
            string path = GetRootPath() + "Temp\\Output\\";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            return path;
        }

        /// <summary>
        /// 获取临时输出文件夹的绝对路径
        /// </summary>
        /// <param name="subFolder"></param>
        /// <returns></returns>
        public static string GetOutputTempFolderPath(string subFolder)
        {
            string path = GetOutputTempFolderPath() + subFolder + "\\";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            return path;
        }

        #endregion


        #region Attachment

        #region 根目录

        #region 绝对路径

        /// <summary>
        /// 获取附件文件夹绝对路径(根目录)
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static string GetAttachmentFolderPath()
        {
            string path = GetRootPath() + "Attachment\\";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            return path;
        }

        /// <summary>
        /// 获取附件文件夹绝对路径
        /// </summary>
        /// <param name="subFolder"></param>
        /// <returns></returns>
        public static string GetAttachmentFolderPath(string subFolder)
        {
            string path = GetAttachmentFolderPath() + subFolder + "\\";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            return path;
        }

        #endregion

        #region 相对路径

        /// <summary>
        /// 获取附件文件夹相对路径(根目录)
        /// </summary>
        /// <returns></returns>
        public static string GetAttachmentRelativeFolderPath()
        {
            return "Attachment\\";
        }

        /// <summary>
        /// 获取附件文件夹相对路径
        /// </summary>
        /// <param name="subFolder"></param>
        /// <returns></returns>
        public static string GetAttachmentRelativeFolderPath(string subFolder)
        {
            return GetAttachmentRelativeFolderPath() + subFolder + "\\";
        }

        #endregion

        #endregion

        #region 照片文件夹

        #region 绝对路径

        /// <summary>
        /// 获取照片附件文件夹的绝对路径
        /// </summary>
        /// <returns></returns>
        public static string GetPhotoAttachmentFolderPath()
        {
            string path = GetAttachmentFolderPath() + "Photo\\";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            return path;
        }

        /// <summary>
        /// 获取照片附件文件夹的绝对路径
        /// </summary>
        /// <param name="subFolder"></param>
        /// <returns></returns>
        public static string GetPhotoAttachmentFolderPath(string subFolder)
        {
            string path = GetPhotoAttachmentFolderPath() + subFolder + "\\";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            return path;
        }

        #endregion

        #region 相对路径

        /// <summary>
        /// 获取照片附件文件夹的相对路径
        /// </summary>
        /// <returns></returns>
        public static string GetPhotoAttachmentRelativeFolderPath()
        {
            return GetAttachmentRelativeFolderPath() + "Photo\\";
        }

        /// <summary>
        /// 获取照片附件文件夹的相对路径
        /// </summary>
        /// <param name="subFolder"></param>
        /// <returns></returns>
        public static string GetPhotoAttachmentRelativeFolderPath(string subFolder)
        {
            return GetPhotoAttachmentRelativeFolderPath() + subFolder + "\\";
        }

        #endregion

        #endregion

        #endregion


    }

}
