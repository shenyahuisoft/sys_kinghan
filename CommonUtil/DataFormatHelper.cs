using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtil
{
    public class DataFormatHelper
    {
        /// <summary>
        /// 参数加工
        /// </summary>
        /// <param name="pairs"></param>
        /// <returns></returns>
        public static string GetSaveYCJCData(Dictionary<string, string> pairs)
        {
            StringBuilder sbStr = new StringBuilder();
            int i = 0;
            foreach (var item in pairs)
            {
                sbStr.AppendFormat("{0}:|:{1}", item.Key, string.IsNullOrEmpty(item.Value) ? "-1" : item.Value);
                if (i < pairs.Count - 1)
                {
                    sbStr.Append("#|#");
                }
                i++;
            }
            return sbStr.ToString();
        }
    }
}
