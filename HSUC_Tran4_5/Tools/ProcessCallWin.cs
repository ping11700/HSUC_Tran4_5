using Common;
using Common.Log4;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace HSUC_Tran4_5.Tools
{
    public class ProcessCallWin
    {

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int WS_SHOWNORMAL = 1;

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        private const uint WM_SHOWWINDOW = 0x0018;
        private const int SW_PARENTOPENING = 3;

        public static void CallWin()
        {
            try
            {
                //var process = RunningInstance();
                //if (process == null || process.MainWindowHandle == IntPtr.Zero) return;

                ////确保窗口没有被最小化或最大化
                //ShowWindowAsync(process.MainWindowHandle, WS_SHOWNORMAL);

                ////设置真实进程为foreground window
                //SetForegroundWindow(process.MainWindowHandle);


                IntPtr handle = Win32API.FindWindow(null, "MainWindow");

                if (handle == IntPtr.Zero) return;

                //确保窗口没有被最小化或最大化
                ShowWindowAsync(handle, WS_SHOWNORMAL);

                SendMessage(handle, WM_SHOWWINDOW, IntPtr.Zero, new IntPtr(SW_PARENTOPENING));

            }
            catch (Exception ex)
            {
                LogService.ILogger.Warn($"CallWin Failed: { ex.Message }");
            }

        }


        /// <summary>
        /// 得到正在运行的进程
        /// </summary>
        /// <returns></returns>
        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);

            //遍历正在有相同名字运行的进程
            foreach (Process process in processes)
            {
                //忽略现有的进程
                if (process.Id != current.Id)
                {
                    //确保进程从EXE文件运行
                    if (process.MainModule.FileName == current.MainModule.FileName)
                    {
                        // 返回另一个进程实例
                        return process;
                    }
                }
            }
            //没有其它的进程，返回Null
            return null;
        }

    }
}
