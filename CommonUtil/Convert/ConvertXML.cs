using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.IO;
using System.Xml;

namespace CommonUtil
{
    public class ConvertXML
    {
        /// <summary>
        /// 将XML转换成DataTable
        /// </summary>
        /// <param name="xml"></param>
        /// <returns>转换后得到的DataTable</returns>
        public static DataTable ToDataTable(string xml)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xml);
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                if (xmlDS.Tables.Count > 0)
                {
                    return xmlDS.Tables[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                string strTest = ex.Message;
                return null;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
    }
}
