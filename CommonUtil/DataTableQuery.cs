using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace CommonUtil
{
    public class DataTableQuery
    {
        /// <summary>
        /// DataTable 查询
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        public static DataTable Select(DataTable dt, string filterExpression)
        {
            DataRow[] drs = dt.Select(filterExpression);
            if (drs.Length == 0)
            {
                return dt.Clone();
            }
            return drs.CopyToDataTable();
        }

        /// <summary>
        /// DataTable 查询
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        public static DataTable Sort(DataTable dt, string sortExpression)
        {
            DataView dv = dt.DefaultView;
            dv.Sort = sortExpression;
            return dv.ToTable();
        }
    }
}