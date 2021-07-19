using System;

namespace Common
{
    public class OSVersion
    {

        /// <summary>
        /// win7操作系统
        /// </summary>
        public static bool IsWin7 => Environment.OSVersion.Version.Major == 6
                                        && Environment.OSVersion.Version.Minor == 1;
        /// <summary>
        /// win8操作系统
        /// </summary>
        public static bool IsWin8 => Environment.OSVersion.Version.Major == 6
                                        && Environment.OSVersion.Version.Minor == 2;

        /// <summary>
        /// win8.1操作系统
        /// </summary>
        public static bool IsWin8_1 => Environment.OSVersion.Version.Major == 6
                                        && Environment.OSVersion.Version.Minor == 3;

        /// <summary>
        /// win10操作系统
        /// </summary>
        public static bool IsWin10 => Environment.OSVersion.Version.Major == 10;


        /// <summary>
        /// 64位操作系统
        /// </summary>
        public static bool Is64Sys => Environment.Is64BitOperatingSystem;


    }
}
