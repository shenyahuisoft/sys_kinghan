using System;
using System.Collections;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.IO;

namespace CommonUtil
{
    /// <summary>
    /// Excel导入的类
    /// </summary>
    public sealed class ImportExcel
    {

        /// <summary>
        /// 从Excel获取数据DataTable
        /// </summary>
        /// <param name="path"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable GetDataTableFromExcel(string path, string tableName)
        {
            //string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source = " + path + ";Extended Properties ='Excel 8.0;HDR=Yes;IMEX=1'";
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source = " + path + ";Extended Properties ='Excel 12.0;HDR=Yes;IMEX=1'";
            OleDbConnection conn = new OleDbConnection(strConn);
            OleDbDataAdapter myCommand = null;
            string strTable = "[" + tableName + "$]";

            string strExcel = "SELECT * FROM " + strTable;//[Sheet1$]";
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                myCommand = new OleDbDataAdapter(strExcel, strConn);
                myCommand.Fill(ds, strTable);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                return null; //throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}