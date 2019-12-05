using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml;

namespace CommonUtil
{
    public class ConvertDataTable
    {
        /// <summary>
        /// 将DataTable转换成XML
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>转换后得到的 XML String</returns>
        public static string ToXML(DataTable dt)
        {
            dt.TableName = "Default";
            MemoryStream stream = null;
            XmlTextWriter writer = null;
            try
            {
                stream = new MemoryStream();
                writer = new XmlTextWriter(stream, Encoding.UTF8);
                dt.WriteXml(writer);
                int count = (int)stream.Length;
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(arr, 0, count);

                UTF8Encoding utf = new UTF8Encoding();
                return utf.GetString(arr).Trim();
            }
            catch (Exception ex)
            {
                return String.Empty;
            }
            finally
            {
                if (writer != null) writer.Close();
            }
        }

        public static string ToJSON(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            return jsonBuilder.ToString();
        }


        /// <summary>
        /// 将DataTable转换成JSON
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="startIndex">开始行号</param>
        /// <param name="length">获取行数</param>
        /// <returns>转换后得到的 JSON String</returns>
        public static string ToJSON(DataTable dt, int startIndex, int length)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (startIndex + i < dt.Rows.Count)
                {
                    DataRow dr = dt.Rows[startIndex + i];
                    foreach (DataColumn dc in dt.Columns)
                    {
                        dic.Add(dc.ColumnName, dr[dc.ColumnName]);
                    }
                }
            }
            return JSONHelper.ToJSON(dic);
        }

        /// <summary>
        /// 将DataTable转换成IList
        /// </summary>
        /// <param name="type">实体类的类型</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IList ToIList(Type type, DataTable dt)
        {
            if (dt.Columns.Count == 0)
            {
                return null;
            }

            string[] colNames = new string[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                colNames[i] = dt.Columns[i].Caption;
            }

            IList mList = new ArrayList();
            PropertyInfo[] pis = type.GetProperties();
            if (pis.Length > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
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
                                    //Error.Log("DataTableToIList:" + ex.Message);
                                }
                            }
                        }
                    }
                    mList.Add(mObject);
                }
            }
            return mList;
        }

    }
}
