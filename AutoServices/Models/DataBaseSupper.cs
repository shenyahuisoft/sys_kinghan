using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServices.Models
{
    public class DataBaseSupper
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
