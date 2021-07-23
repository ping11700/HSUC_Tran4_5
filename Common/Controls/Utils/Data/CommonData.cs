namespace Common.Controls.Utils
{
    /// <summary>
    ///     装箱后的值类型（用于提高效率）
    /// </summary>
    internal static class ValueBoxes
    {
        internal static object TrueBox = true;

        internal static object FalseBox = false;

        internal static object Double0Box = .0;

        internal static object Double01Box = .1;

        internal static object Double1Box = 1.0;

        internal static object Double10Box = 10.0;

        internal static object Double20Box = 20.0;

        internal static object Double100Box = 100.0;

        internal static object Double200Box = 200.0;

        internal static object Double300Box = 300.0;

        internal static object DoubleNeg1Box = -1.0;

        internal static object Int0Box = 0;

        internal static object Int1Box = 1;

        internal static object Int2Box = 2;

        internal static object Int5Box = 5;

        internal static object Int99Box = 99;

        internal static object BooleanBox(bool value) => value ? TrueBox : FalseBox;
    }


    public enum InfoType
    {
        Success = 0,
        Info,
        Warning,
        Error,
        Fatal,
        Ask
    }

    public enum SideMenuItemRole
    {
        Header,
        Item
    }

    public enum ExpandMode
    {
        /// <summary>
        ///     最多只能显示一项，且不可折叠
        /// </summary>
        ShowOne,

        /// <summary>
        ///     显示所有项，且不可折叠
        /// </summary>
        ShowAll,

        /// <summary>
        ///     类似ShowOne，但是控件的尺寸不随项的数量而改变
        /// </summary>
        Accordion,

        /// <summary>
        ///     没有任何限制
        /// </summary>
        Freedom
    }



     public enum DrawerShowMode
    {
        Cover,
        Push,
        Press
    }
}
