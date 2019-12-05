using System;
using System.Collections;
using System.Data;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml;

namespace CommonUtil
{
    public class ConvertDataRow
    {

        /// <summary>
        /// 将DataRow转换为实体类
        /// </summary>
        /// <param name="type">实体类的类型</param>
        /// <param name="dr">要转换的DataRow</param>
        /// <returns>转换得到的实体类</returns>
        public static object ToObject(Type type, DataRow dr)
        {
            PropertyInfo[] pis = type.GetProperties();
            if (pis.Length == 0)
            {
                return null;
            }

            int cellCount = dr.Table.Columns.Count;
            if (cellCount == 0)
            {
                return null;
            }

            string[] colNames = new string[cellCount];
            for (int i = 0; i < cellCount; i++)
            {
                colNames[i] = dr.Table.Columns[i].Caption;
            }

            object mObject = type.Assembly.CreateInstance(type.ToString());

            foreach (PropertyInfo pi in pis)
            {
                for (int colIndex = 0; colIndex < colNames.Length; colIndex++)
                {
                    if (pi.Name == colNames[colIndex])
                    {
                        try
                        {
                            type.InvokeMember(pi.Name, BindingFlags.SetProperty, null, mObject, new object[] { dr[pi.Name] });
                            continue;
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
            return mObject;
        }

    }
}
