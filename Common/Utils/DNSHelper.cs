using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Common.Utils
{
    public class DNSHelper
    {

        private static string host = Environment.OSVersion.Platform == PlatformID.Win32NT ?
                                     "system32\\drivers\\etc\\hosts" :
                                     "hosts";

        private static string hostFile => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), host);

        private const string id = "39.102.34.50 t-api.vcinema.cn";
        private const string domain = "t-api.vcinema.cn";

        /// <summary>
        /// 根据ip替换
        /// </summary>
        public static void ReplaceByIP()
        {

            if (!((IList)File.ReadAllLines(hostFile)).Contains(id))
            {
                File.AppendAllLines(hostFile, new String[] { id });
            }
        }


        /// <summary>
        /// 根据域名替换
        /// </summary>
        public static void ReplaceByDomain()
        {
            string[] lines = File.ReadAllLines(hostFile);

            if (lines.Any(s => s.Contains(domain)))
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains(domain))
                        lines[i] = id;
                }
                File.WriteAllLines(hostFile, lines);
            }
            else if (!lines.Contains(id))
            {
                File.AppendAllLines(hostFile, new String[] { id });
            }
        }


        /// <summary>
        /// 直接追加
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public static bool ModifyHostsFile(string entry = id)
        {
            try
            {
                using (StreamWriter w = File.AppendText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), @"drivers\etc\hosts")))
                {
                    w.WriteLine(entry);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        /// <summary>
        /// 根据url获取主机ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string GetDNSIP(string url)
        {
            try
            {
                Uri host = new Uri(url);

                foreach (var hostAddress in Dns.GetHostAddresses(host.Host))
                {
                    if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
                        return hostAddress.ToString();
                }
                return string.Empty;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
