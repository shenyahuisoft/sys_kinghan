using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServices.Common
{
    /// <summary>
    /// DatabaseMap 数据源实例信息
    /// </summary>
    internal sealed class DatabaseMap
    {

        #region 变量

        /// <summary>
        /// 数据库名
        /// </summary>
        private string m_Name;

        #endregion


        #region 构造函数

        public DatabaseMap(string Name)
        {
            m_Name = Name;
        }

        #endregion


        #region 属性

        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        #endregion

    }
}
