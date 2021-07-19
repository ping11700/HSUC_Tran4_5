using System;
using System.Text.RegularExpressions;

namespace Common.Utils
{
    /// <summary>
    ///     包含一些正则验证操作
    /// </summary>
    public static class RegexJudgment
    {
        private static readonly RegexPatterns RegexPatterns = new RegexPatterns();

        /// <summary>
        ///     判断字符串是否满足指定的格式
        /// </summary>
        /// <param name="text">需要判断的字符串</param>
        /// <param name="textType">指定格式的文本</param>
        /// <returns></returns>
        public static bool IsKindOf(this string text, RegexType regexType)
        {
            if (regexType == RegexType.Common) return true;
            return Regex.IsMatch(text,
                RegexPatterns.GetValue(Enum.GetName(typeof(RegexType), regexType) + "Pattern").ToString());
        }



    }
}
