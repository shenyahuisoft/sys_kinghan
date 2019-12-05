using System;
using System.Linq;

namespace CommonUtil
{
    public static class RandomLetters
    {
        /// <summary>
        /// 获取随机字母字符串
        /// </summary>
        /// <param name="existing">现有的</param>
        /// <param name="codePrefix">编号前缀</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static string GetLetters(string[] existing,string codePrefix, int length = 4)
        {
            var letters = string.IsNullOrEmpty(codePrefix) ? string.Empty : codePrefix + "-";
            var constant = new[]
            {
                "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U",
                "V", "W", "X", "Y", "Z"
            };
            var rand = new Random();
            while (true)
            {
                for (var i = 0; i < length; i++)
                {
                    letters += constant[rand.Next(0, 23)];
                }
                if (existing.All(n => n != letters))
                {
                    break;
                }
            }
            return letters;
        }
    }
}
