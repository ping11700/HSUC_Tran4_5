namespace Common.Utils
{
    public sealed class RegexHelper
    {
        /// <summary>
        /// 检测正则表达式
        /// </summary>
        /// <param name="DetectionNum"></param>
        /// <returns></returns>
        public static string DetectionRegex(TextDetectRule DetectionNum)
        {
            switch (DetectionNum)
            {
                case TextDetectRule.NumOnly:
                    return "^[0-9]+$";
                case TextDetectRule.LetterOnly:
                    return "^[a-zA-Z]+$";
                case TextDetectRule.ChineseOnly:
                    return "^[\u4e00-\u9fa5]+$";
                case TextDetectRule.LetterAndNum:
                    return "^[a-zA-Z0-9]+$";
                case TextDetectRule.LetterAndChinese:
                    return "^[\u4e00-\u9fa5a-zA-Z]+$";
                case TextDetectRule.EscapeSpecChars:
                    return "[`~!@#$%^&*()_\\-+=|{}':;',\\[\\].<>/?~！@#￥%……&*（）——+|{}【】‘；：”“’。，、？]";
                case TextDetectRule.IsPassword:
                    return "^[a-zA-Z0-9_.]{6,17}$";
                case TextDetectRule.WithoutChinese:
                    return "[^\u4e00-\u9fa5]+$";
                case TextDetectRule.NonnegativeFloat:
                    return "^\\d+(\\.\\d+)?$";
                case TextDetectRule.SignedNum:
                    return "^-?[0-9]+$";
                case TextDetectRule.SignedFloat:
                    return "^-?\\d+(\\.\\d+)?$";
                case TextDetectRule.DigitalCeiling:
                    return "^([1-9][0-9]{0,2})$";
                case TextDetectRule.FourDigitalFloat:
                    return "^[0-9]{1,4}(\\.\\d+)?$";
                case TextDetectRule.ZeroToFify:
                    return "(^[1-4][0-9]$)|(^[5][0]$)|(^[0-9]$)";
                case TextDetectRule.StandbyTime:
                    return "(^[1-9][0-9]$)|(^[1][0-1][0-9]$)|(^[1][2][0]$)|(^[5-9]$)";
                case TextDetectRule.PhoneOrFax:
                    return "^[\\d\\+\\-\u0020]+$";
                case TextDetectRule.CellPhone:
                    return "^1[3456789]\\d{9}$";
                case TextDetectRule.YearCtrl:
                    return "(^[1-9][0-9]$)|(^[1][0-9][0-9]$)|(^[2][0][0]$)|(^[0-9]$)";
                case TextDetectRule.MonthCtrl:
                    return "(^[1-9][0-9]$)|(^[1-9][0-9][0-9]$)|(^[0-9]$)|(^[1][0-9][0-9][0-9]$)|(^[2][4][0][0]$)|(^[2][0-3][0-9][0-9]$)";
                case TextDetectRule.DayCtrl:
                    return "(^[1-9][0-9]$)|(^[1-9][0-9][0-9]$)|(^[0-9]$)|(^[1-9][0-9][0-9][0-9]$)|(^[1-6][1-9][0-9][0-9][0-9]$)|(^[7][1-2][0-9][0-9][0-9]$)|(^[7][3][0][0][0]$)";
                case TextDetectRule.PositiveInteger:
                    return "(^[1-9][0-9]*$)";
                case TextDetectRule.PositiveFloat:
                    return "(^0\\.\\d*[1-9]\\d?$)|(^[1-9]\\d*(.\\d*[1-9]\\d?)?$)|(^[1-9]\\d*(.0)?$)";
                default:
                    return null;

            }

        }

        /// <summary>
        /// 错误提示信息
        /// </summary>
        /// <param name="ErrorReason"></param>
        /// <param name="culture">en-us/ zh-cn</param>
        /// <returns></returns>
        public static string ErrorString(TextDetectRule ErrorReason, System.Globalization.CultureInfo culture)
        {
            switch (culture.Name)
            {
                case "zh-CN":
                    switch (ErrorReason)
                    {
                        case TextDetectRule.IsNuLL:
                            return ("输入为空");
                        case TextDetectRule.NumOnly:
                            return ("仅数字");
                        case TextDetectRule.LetterOnly:
                            return ("ToolTip_LetterOnly");
                        case TextDetectRule.ChineseOnly:
                            return ("ToolTip_ChineseOnly");
                        case TextDetectRule.LetterAndChinese:
                            return ("ToolTip_LetterAndChinese");
                        case TextDetectRule.LetterAndNum:
                            return ("ToolTip_LetterAndNum");
                        case TextDetectRule.EscapeSpecChars:
                            return ("不能包含特殊字符");
                        case TextDetectRule.IsPassword:
                            return ("ToolTip_IsPassword");
                        case TextDetectRule.WithoutChinese:
                            return ("ToolTip_WithoutChinese");
                        case TextDetectRule.NonnegativeFloat:
                            return ("ToolTip_NonnegativeFloat");
                        case TextDetectRule.SignedFloat:
                            return ("ToolTip_SignedFloat");
                        case TextDetectRule.SignedNum:
                            return ("ToolTip_SignedNum");
                        case TextDetectRule.ZeroToFify:
                        case TextDetectRule.FourDigitalFloat:
                        case TextDetectRule.DigitalCeiling:
                            return ("ToolTip_DigitalCeiling");
                        case TextDetectRule.StandbyTime:
                            return ("ToolTip_StandbyTime");
                        case TextDetectRule.PhoneOrFax:
                            return ("ToolTip_DigitalCeiling");
                        case TextDetectRule.CellPhone:
                            return ("ToolTip_CellPhone");
                        case TextDetectRule.YearCtrl:
                            return ("ToolTip_YearCtrl");
                        case TextDetectRule.MonthCtrl:
                            return ("ToolTip_MonthCtrl");
                        case TextDetectRule.DayCtrl:
                            return ("ToolTip_DayCtrl");
                        case TextDetectRule.PositiveInteger:
                            return ("ToolTip_PositiveInteger");
                        case TextDetectRule.PositiveFloat:
                            return ("ToolTip_PositiveFloat");
                        default:
                            return null;
                    }
                //英文
                case "en-US":
                    switch (ErrorReason)
                    {
                        case TextDetectRule.IsNuLL:
                            return ("ToolTip_IsNull");
                        case TextDetectRule.NumOnly:
                            return ("ToolTip_NumOnly");
                        case TextDetectRule.LetterOnly:
                            return ("ToolTip_LetterOnly");
                        case TextDetectRule.ChineseOnly:
                            return ("ToolTip_ChineseOnly");
                        case TextDetectRule.LetterAndChinese:
                            return ("ToolTip_LetterAndChinese");
                        case TextDetectRule.LetterAndNum:
                            return ("ToolTip_LetterAndNum");
                        case TextDetectRule.EscapeSpecChars:
                            return ("ToolTip_EscapeSpecChars");
                        case TextDetectRule.IsPassword:
                            return ("ToolTip_IsPassword");
                        case TextDetectRule.WithoutChinese:
                            return ("ToolTip_WithoutChinese");
                        case TextDetectRule.NonnegativeFloat:
                            return ("ToolTip_NonnegativeFloat");
                        case TextDetectRule.SignedFloat:
                            return ("ToolTip_SignedFloat");
                        case TextDetectRule.SignedNum:
                            return ("ToolTip_SignedNum");
                        case TextDetectRule.ZeroToFify:
                        case TextDetectRule.FourDigitalFloat:
                        case TextDetectRule.DigitalCeiling:
                            return ("ToolTip_DigitalCeiling");
                        case TextDetectRule.StandbyTime:
                            return ("ToolTip_StandbyTime");
                        case TextDetectRule.PhoneOrFax:
                            return ("ToolTip_DigitalCeiling");
                        case TextDetectRule.CellPhone:
                            return ("ToolTip_CellPhone");
                        case TextDetectRule.YearCtrl:
                            return ("ToolTip_YearCtrl");
                        case TextDetectRule.MonthCtrl:
                            return ("ToolTip_MonthCtrl");
                        case TextDetectRule.DayCtrl:
                            return ("ToolTip_DayCtrl");
                        case TextDetectRule.PositiveInteger:
                            return ("ToolTip_PositiveInteger");
                        case TextDetectRule.PositiveFloat:
                            return ("ToolTip_PositiveFloat");
                        default:
                            return null;
                    }
                default:
                    return null;
            }
        }


    }

    /// <summary>
    /// 正则表达式
    /// </summary>
    public enum TextDetectRule
    {
        NonDetection,         //不检查字符    默认
        NumOnly,              //只有数字                      ^[0-9]+$          
        LetterOnly,           //只有英文字母                  ^[a-zA-Z]+$
        ChineseOnly,          //只有中文字符                  ^[\u4e00-\u9fa5]+$
        EscapeSpecChars,      //特殊字符屏蔽                  [`~!@#$%^&*()_\\-+=|{}':;',\\[\\].<>/?~！@#￥%……&*（）——+|{}【】‘；：”“’。，、？]$
        WithoutChinese,       //没有中文字符 = 英文+数字+符号 ^[0-9a-zA-Z]+$"
        LetterAndNum,         //英文字符和数字                ^[a-zA-Z0-9]+$
        LetterAndChinese,     //中英文字符                    ^[\u4e00-\u9fa5a-zA-Z]+$
        IsPassword,           //密码格式                      ^[a-zA-Z0-9_.]{6,17}$
        NonnegativeFloat,     //非负浮点型                    "^\\d+(\\.\\d+)?$"
        SignedFloat,          //有符号浮点数                  "^-?\\d+(\\.\\d+)?$"
        SignedNum,            //有符号整数                    ^-?[0-9]+$
        DigitalCeiling,       //数字限制（1-999）               ^([1-9][0-9]{0,2})$
        FourDigitalFloat,     // 数字限制(0.0-9999.999..)
        ZeroToFify,           //0-50                          (^[1-4][0-9]$)|(^[5][0]$)|(^[0-9]$)
        StandbyTime,          //5-120                         (^[1-9][0-9]$)|(^[1][0-1][0-9]$)|(^[1][2][0]$)|(^[5-9]$)
        IsNuLL,               //空
        PhoneOrFax,           //电话或者传真                   "[\\d\\+\\-\u0020]+$"
        CellPhone,            //手机号                         "^1[34578]\\d{9}$"
        YearCtrl,             //0-200   
        MonthCtrl,            //0-2400
        DayCtrl,              //0-73000
        PositiveInteger,      //正整数                        (^[1-9][0-9]*$)
        PositiveFloat,        //正浮点数                       (^0\.\d*[1-9]\d?$)|(^[1-9]\d*(.\d*[1-9]\d?)?$)|(^[1-9]\d*(.0)?$) 
    }


    /// <summary>
    ///     包含一些正则验证所需要的字符串
    /// </summary>
    public sealed class RegexPatterns
    {
        /// <summary>
        ///     邮件正则匹配表达式
        /// </summary>
        public const string MailPattern =
            @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        /// <summary>
        ///     手机号正则匹配表达式
        /// </summary>
        public const string PhonePattern = @"^((13[0-9])|(15[^4,\d])|(18[0,5-9]))\d{8}$";

        /// <summary>
        /// 密码 无特殊符号
        /// </summary>
        public const string IsPasswordPattern = "^[a-zA-Z0-9_.]{6,12}$";

        /// <summary>
        ///     IP正则匹配
        /// </summary>
        public const string IpPattern =
            @"^(25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)\."
            + @"(25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)\."
            + @"(25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)\."
            + @"(25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)$";

        /// <summary>
        ///     A类IP正则匹配
        /// </summary>
        public const string IpAPattern =
            @"^(12[0-6]|1[0-1]\d|[1-9]?\d)\."
            + @"(25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)\."
            + @"(25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)\."
            + @"(25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)$";

        /// <summary>
        ///     B类IP正则匹配
        /// </summary>
        public const string IpBPattern =
            @"^(19[0-1]|12[8-9]|1[3-8]\d)\."
            + @"(25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)\."
            + @"(25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)\."
            + @"(25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)$";

        /// <summary>
        ///     C类IP正则匹配
        /// </summary>
        public const string IpCPattern =
            @"^(19[2-9]|22[0-3]|2[0-1]\d)\."
            + @"(25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)\."
            + @"(25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)\."
            + @"(25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)$";

        /// <summary>
        ///     D类IP正则匹配
        /// </summary>
        public const string IpDPattern =
            @"^(22[4-9]|23\d\."
            + @"(25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)\."
            + @"(25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)\."
            + @"(25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)$";

        /// <summary>
        ///     E类IP正则匹配
        /// </summary>
        public const string IpEPattern =
            @"^(25[0-5]|24\d\."
            + @"(25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)\."
            + @"(25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)\."
            + @"(25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)$";

        /// <summary>
        ///     汉字正则匹配
        /// </summary>
        public const string ChinesePattern = @"^[\u4e00-\u9fa5]$";

        /// <summary>
        ///     Url正则匹配
        /// </summary>
        public const string UrlPattern =
            @"((http|ftp|https)://)(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,4})*(/[a-zA-Z0-9\&%_\./-~-]*)?";

        /// <summary>
        ///     数字正则匹配
        /// </summary>
        public const string NumberPattern = @"^\d$";

        /// <summary>
        ///     计算性质数字正则匹配
        /// </summary>
        public const string DigitsPattern = @"[+-]?\d*\.?\d+(?:\.\d+)?(?:[eE][+-]?\d+)?";

        /// <summary>
        ///     正整数正则匹配
        /// </summary>
        public const string PIntPattern = @"^[1-9]\d*$";

        /// <summary>
        ///     负整数正则匹配
        /// </summary>
        public const string NIntPattern = @"^-[1-9]\d*$ ";

        /// <summary>
        ///     整数正则匹配
        /// </summary>
        public const string IntPattern = @"^-?[1-9]\d*$";

        /// <summary>
        ///     非负整数正则匹配
        /// </summary>
        public const string NnIntPattern = @"^[1-9]\d*|0$";

        /// <summary>
        ///     非正整数正则匹配
        /// </summary>
        public const string NpIntPattern = @"^-[1-9]\d*|0$";

        /// <summary>
        ///     正浮点数正则匹配
        /// </summary>
        public const string PDoublePattern = @"^[1-9]\d*\.\d*|0\.\d*[1-9]\d*$";

        /// <summary>
        ///     负浮点数正则匹配
        /// </summary>
        public const string NDoublePattern = @"^-([1-9]\d*\.\d*|0\.\d*[1-9]\d*)$";

        /// <summary>
        ///     浮点数正则匹配
        /// </summary>
        public const string DoublePattern = @"^-?([1-9]\d*\.\d*|0\.\d*[1-9]\d*|0?\.0+|0)$";

        /// <summary>
        ///     非负浮点数正则匹配
        /// </summary>
        public const string NnDoublePattern = @"^[1-9]\d*\.\d*|0\.\d*[1-9]\d*|0?\.0+|0$";

        /// <summary>
        ///     非正浮点数正则匹配
        /// </summary>
        public const string NpDoublePattern = @"^(-([1-9]\d*\.\d*|0\.\d*[1-9]\d*))|0?\.0+|0$";

        /// <summary>
        ///     根据属性名称使用反射来获取值
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public object GetValue(string propertyName) => GetType().GetField(propertyName).GetValue(null);
    }
}
