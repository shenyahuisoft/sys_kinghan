using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Reflection;

namespace CommonUtil
{
    public class ConvertArrayList
    {
        /// <summary>
        /// 将ArrayList转换成String
        /// </summary>
        /// <param name="list">要转换的ArrayList</param>
        /// <returns>转换得到的String</returns>
        public static string ToString(ArrayList list)
        {
            StringBuilder strBuild = new StringBuilder(" ");
            for (int i = 0; i < list.Count; i++)
            {
                strBuild.Append(list[i].ToString());
            }
            return strBuild.ToString();
        }

    }
}
