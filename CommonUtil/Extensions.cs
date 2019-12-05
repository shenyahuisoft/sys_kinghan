using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtil
{
    public static class Extensions
    {
        /// <summary>
        /// string转DateTime
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this string context)
        {
            try
            {
                var time = Convert.ToDateTime(context);
                return time;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static DateTime ToDateTimeWithOutNull(this string context)
        {
            var time = DateTime.Now;
            try
            {
                time = Convert.ToDateTime(context);
            }
            catch (Exception)
            {
                // ignored
            }
            return time;
        }
    }
}
