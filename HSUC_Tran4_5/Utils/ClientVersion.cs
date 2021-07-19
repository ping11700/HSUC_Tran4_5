using System.Windows;

namespace HSUC_Tran4_5.Utils
{
    internal class ClientVersion
    {


        /// <summary>
        /// 客户端版本
        /// </summary>
        public static string SoftwareVersion => Application.ResourceAssembly.GetName().Version.ToString();


        /// <summary>
        /// Build版本
        /// </summary>
        public static string BuildVersion => Application.ResourceAssembly.GetName().Version.Major.ToString() + "." +
                                             Application.ResourceAssembly.GetName().Version.Minor.ToString() + "." +
                                             Application.ResourceAssembly.GetName().Version.Build.ToString();

        /// <summary>
        /// Revision版本
        /// </summary>
        public static string Revision => Application.ResourceAssembly.GetName().Version.Revision.ToString();


    }
}
