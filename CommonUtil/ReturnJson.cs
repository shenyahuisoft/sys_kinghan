using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Script.Serialization;

namespace CommonUtil
{
    /// <summary>
    /// JSONHelper 的摘要说明
    /// </summary>
    public class ReturnJson
    {
        public static string GetReturnJson(JSONHelper jsonHelp)
        {
            return jsonHelp.ToString();
        }

        public static string GetReturnJson(DataTable dt)
        {
            JSONHelper jsonHelp = new JSONHelper();
            try
            {
                jsonHelp.totalCount = dt.Rows.Count;
                for (int i = 0; i < jsonHelp.totalCount; i++)
                {
                    DataRow dr = dt.Rows[i];
                    foreach (DataColumn dc in dt.Columns)
                    {
                        jsonHelp.AddItem(dc.ColumnName, dr[dc.ColumnName].ToString());
                    }
                    jsonHelp.ItemOk();
                }
                jsonHelp.success = true;
            }
            catch
            {
                jsonHelp.success = false;
            }
            return jsonHelp.ToString();
        }

        public static string GetReturnJson(DataTable dt, int totalCount)
        {
            JSONHelper jsonHelp = new JSONHelper();
            try
            {
                jsonHelp.totalCount = totalCount < 0 ? dt.Rows.Count : totalCount;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    foreach (DataColumn dc in dt.Columns)
                    {
                        jsonHelp.AddItem(dc.ColumnName, dr[dc.ColumnName].ToString());
                    }
                    jsonHelp.ItemOk();
                }
                jsonHelp.success = true;
            }
            catch
            {
                jsonHelp.success = false;
            }
            return jsonHelp.ToString();
        }
        /// <summary>
        ///  只得到表对应的json格式
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static string GetReturnJsonRaw(DataTable dt)
        {
            JSONHelper jsonHelp = new JSONHelper();
            try
            {
             
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    foreach (DataColumn dc in dt.Columns)
                    {
                        jsonHelp.AddItem(dc.ColumnName, dr[dc.ColumnName].ToString());
                    }
                  
                }
              
            }
            catch
            {
                jsonHelp.success = false;
            }
            return jsonHelp.ToString();
        }


        public static string GetReturnJson(bool success)
        {
            //JSONHelper jsonHelp = new JSONHelper();
            //jsonHelp.success = success;
            //return jsonHelp.ToString();



            JSONHelper jsonHelp = new JSONHelper();
            jsonHelp.success = success;

            jsonHelp.totalCount = 0;


            return jsonHelp.ToString();
        }

    }
}