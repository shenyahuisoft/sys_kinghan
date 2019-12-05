using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using System.Data;

namespace CommonUtil
{
    /// <summary>
    /// JSONHelper 的摘要说明
    /// </summary>
    public class JSONHelper
    {
        //对应JSON的singleInfo成员
        public string singleInfo = string.Empty;

        protected string _status = string.Empty;
        protected string _message = string.Empty;
        protected string _token = string.Empty;
        protected string _error = string.Empty;
        protected bool _success = true;
        protected long _totalCount = 0;
        protected System.Collections.ArrayList arrData = new ArrayList();
        protected System.Collections.ArrayList arrDataItem = new ArrayList();
        private Dictionary<string, object> customProperties = new Dictionary<string, object>();

        public JSONHelper()
        {

        }
        public JSONHelper(DataTable dt)
        {
            this.AddData(dt);
        }


        /// <summary>
        /// 将DataTable转换成JSON
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>转换后得到的 JSON String</returns>
        public static string ToHelperJSON(DataTable dt)
        {
            JSONHelper jsonHelp = new JSONHelper();
            try
            {
                jsonHelp.totalCount = dt.Rows.Count;
                jsonHelp.message = dt.TableName;
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

        public static string ToJSON(DataRow dr)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {
                dic.Add(dr.Table.Columns[i].ColumnName, dr[i].ToString());
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(dic);
        }

        public static string ToJSON(object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        public static string ToJSON(object obj, int recursionDepth)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.RecursionLimit = recursionDepth;
            return serializer.Serialize(obj);
        }

        public void AddData(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (DataColumn dc in dt.Columns)
                    {
                        this.AddItem(dc.ColumnName, dr[dc].ToString());
                    }
                    this.ItemOk();
                }
                this.totalCount = dt.Rows.Count;
            }
        }

        public static ArrayList DeToArrayList(string jsonstring)
        {
            ArrayList list = new ArrayList();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            object[] json = (object[])(serializer.DeserializeObject(jsonstring));

            foreach (object item in json)
            {
                list.Add(item);
            }
            return list;
        }

        public static Dictionary<string, object> DeToDictionary(string jsonstring)
        {
            ArrayList list = new ArrayList();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Dictionary<string, object> json = (Dictionary<string, object>)(serializer.DeserializeObject(jsonstring));
            return json;
        }

        #region 解析Dictionary

        public static int GetDictionaryIntValue(Dictionary<string, object> json, string key)
        {
            return GetDictionaryIntValue(json, key, 0);
        }

        public static int GetDictionaryIntValue(Dictionary<string, object> json, string key, int defaultValue)
        {
            return ConvertObject.ToInt32(GetDictionaryStringValue(json, key), defaultValue);
        }

        public static decimal GetDictionaryDecimalValue(Dictionary<string, object> json, string key)
        {
            return GetDictionaryDecimalValue(json, key, 0);
        }

        public static decimal GetDictionaryDecimalValue(Dictionary<string, object> json, string key, decimal defaultValue)
        {
            return ConvertObject.ToDecimal(GetDictionaryStringValue(json, key), defaultValue);
        }

        public static bool GetDictionaryBoolValue(Dictionary<string, object> json, string key)
        {
            return GetDictionaryBoolValue(json, key, false);
        }

        public static bool GetDictionaryBoolValue(Dictionary<string, object> json, string key, bool defaultValue)
        {
            return ConvertObject.ToBoolean(GetDictionaryStringValue(json, key), defaultValue);
        }

        public static string GetDictionaryStringValue(Dictionary<string, object> json, string key)
        {
            return GetDictionaryStringValue(json, key, "");
        }

        public static string GetDictionaryStringValue(Dictionary<string, object> json, string key, string defaultValue)
        {
            object val = GetDictionaryValue(json, key);
            if (val == null)
            {
                return defaultValue;
            }
            else
            {
                return val.ToString();
            }
        }

        public static object GetDictionaryValue(Dictionary<string, object> json, string key)
        {
            object outValue;
            if (json.TryGetValue(key, out outValue))
            {
                return outValue;
            }
            else
            {
                return null;
            }
        }

        #endregion

        //对应于JSON的success成员
        public bool success
        {
            get
            {
                return _success;
            }
            set
            {
                //如设置为true则清空error
                if (success) _error = string.Empty;
                _success = value;
            }
        }

        //对应于JSON的error成员
        public string error
        {
            get
            {
                return _error;
            }
            set
            {
                //如设置error，则自动设置success为false
                if (value != "") _success = false;
                _error = value;
            }
        }

        //对应于JSON的status成员
        public string status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }

        //对应于JSON的message成员
        public string message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
            }
        }

        //对应于JSON的token成员
        public string token
        {
            get
            {
                return _token;
            }
            set
            {
                _token = value;
            }
        }

        public long totalCount
        {
            get { return _totalCount; }
            set { _totalCount = value; }
        }


        //重置，每次新生成一个json对象时必须执行该方法
        public void Reset()
        {
            _success = true;
            _error = string.Empty;
            singleInfo = string.Empty;
            arrData.Clear();
            arrDataItem.Clear();
        }



        public void AddItem(string name, string value)
        {
            arrData.Add("\"" + name + "\":" + "\"" + Regex.Replace(value, "\\n", "", RegexOptions.IgnoreCase) + "\"");
        }
        public void ItemOk()
        {
            arrData.Add("<BR>");
        }

        /// <summary>
        /// 添加自定义属性
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="val">值,可以是string,int,double,Dictionary&lt;t, t>,object对象</param>
        public void AddProperty(string key, object val)
        {
            customProperties.Add(key, val);
        }

        public static string GetData(DataTable dt)
        {
            JSONHelper json = new JSONHelper();
            json.AddData(dt);
            return json.GetData();

        }
        public string GetData()
        {
            StringBuilder sb = new StringBuilder("[");
            int index = 0;
            if (arrData.Count <= 0)
            {
                sb.Append("]");
            }
            else
            {
                sb.Append("{");
                foreach (string val in arrData)
                {
                    index++;

                    if (val != "<BR>")
                    {
                        sb.Append(val + ",");
                    }
                    else
                    {
                        sb = sb.Replace(",", "", sb.Length - 1, 1);
                        sb.Append("},");
                        if (index < arrData.Count)
                        {
                            sb.Append("{");
                        }
                    }

                }
                sb = sb.Replace(",", "", sb.Length - 1, 1);
                sb.Append("]");
            }
            return sb.ToString();
        }

        //序列化JSON对象，得到返回的JSON代码
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.Append("\"totalCount\":" + totalCount.ToString() + ",");
            sb.Append("\"success\":" + _success.ToString().ToLower() + ",");
            sb.Append("\"error\":\"" + _error.Replace("\"", "\\\"") + "\",");
            sb.Append("\"status\":\"" + _status.Replace("\"", "\\\"") + "\",");
            sb.Append("\"message\":\"" + _message.Replace("\"", "\\\"") + "\",");
            sb.Append("\"token\":\"" + _token.Replace("\"", "\\\"") + "\",");
            sb.Append("\"data\":" + GetData());

            //开始添加自定义属性
            string val = null;
            Type valType = null;
            foreach (KeyValuePair<string, object> item in customProperties)
            {
                val = null;
                if (item.Value == null)
                {
                    continue;
                }
                valType = item.Value.GetType();
                if (valType == typeof(string))
                {
                    val = string.Format(",\"{0}\":\"{1}\"", item.Key, item.Value.ToString());
                }
                else if (valType == typeof(int) || valType == typeof(double))
                {
                    val = string.Format(",\"{0}\":{1}", item.Key, item.Value.ToString());
                }
                else if (valType == typeof(bool))
                {
                    val = string.Format(",\"{0}\":{1}", item.Key, item.Value.ToString().ToLower());
                }
                else if (valType == typeof(DataTable))
                {
                    val = string.Format(",\"{0}\":{1}", item.Key, JSONHelper.GetData((DataTable)item.Value));
                }
                else if (valType.IsSerializable)
                {
                    val = string.Format(",\"{0}\":{1}", item.Key, JSONHelper.ToJSON(item.Value));
                }
                if (val != null)
                {
                    sb.Append(val);
                }
            }

            sb.Append("}");
            return sb.ToString();
        }

    }
}