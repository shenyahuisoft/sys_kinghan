using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Reflection;

namespace CommonUtil
{
    public class ConvertIList
    {
        /// <summary>
        /// 将IList转换成DataTable
        /// </summary>
        /// <param name="type">实体类的类型</param>
        /// <param name="list">要转换的IList</param>
        /// <returns>转换得到的DataTable</returns>
        public static DataTable ToDataTable(Type type, IList list)
        {
            PropertyInfo[] pis = type.GetProperties();
            DataTable dt = new DataTable();
            if (pis.Length > 0)
            {
                foreach (PropertyInfo pi in pis)
                {
                    dt.Columns.Add(pi.Name, pi.PropertyType);
                }
            }
            else
            {
                return null;
            }
            for (int i = 0; i < list.Count; i++)
            {
                DataRow dr = dt.NewRow();
                foreach (PropertyInfo pi in pis)
                {
                    dr[pi.Name] = type.InvokeMember(pi.Name, BindingFlags.GetProperty, null, list[i], null);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// 将IList转换成XML
        /// </summary>
        /// <param name="type">实体类的类型</param>
        /// <param name="list">要转换的IList</param>
        /// <returns>转换得到的 XML String</returns>
        public static String ToXML(Type type, IList list)
        {
            return ConvertDataTable.ToXML(ToDataTable(type, list));
        }


        /// <summary>
        /// 将IList转换成JSON
        /// </summary>
        /// <param name="list">要转换的IList</param>
        /// <returns>转换得到的 JSON String</returns>
        public static String ToJSON(IList list)
        {
            return JSONHelper.ToJSON(list);
        }

        /// <summary>
        /// 将IList转换成JSON
        /// </summary>
        /// <param name="type">实体类的类型</param>
        /// <param name="list">要转换的IList</param>
        /// <returns>转换得到的 JSON String</returns>
        /// <remarks>废弃的，尽量不要使用</remarks>
        /// <remarks>可以使用一个参数的方法（不含类型的那个）</remarks>
        public static String ToJSON(Type type, IList list)
        {
            return ConvertDataTable.ToJSON(ToDataTable(type, list));
        }
    }
}
