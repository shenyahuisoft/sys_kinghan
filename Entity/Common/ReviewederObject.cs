using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ReviewederObject
    {
        /// <summary>
        /// 罗俊岗位
        /// </summary>
        public StaffObj LuoJun { get; set; }

        /// <summary>
        /// 李龙岗位
        /// </summary>
        public StaffObj LiLong { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class StaffObj
    {
        public string StaffID { get; set; }

        public string StaffName { get; set; }
    }
}
