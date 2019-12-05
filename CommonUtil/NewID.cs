using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CommonUtil
{
    public class NewID
    {

        /// <summary>
        /// 获取GUID
        /// </summary>
        /// <returns></returns>
        public static string GetGuid()
        {
            return Guid.NewGuid().ToString().ToUpper();
        }

        /// <summary>
        /// 获取剔除横岗的ID
        /// </summary>
        /// <returns></returns>
        public static string GetID()
        {
            return Guid.NewGuid().ToString().ToUpper().Replace("-", "");
        }

        /// <summary>
        /// 获取16位数字ID
        /// </summary>
        /// <returns></returns>
        public static string GetTicksID()
        {
            return DateTime.Now.Ticks.ToString().Substring(2);
        }
    }
}