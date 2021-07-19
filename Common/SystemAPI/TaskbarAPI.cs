using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace Common.SystemAPI
{
    public class TaskbarAPI
    {


        #region 全屏时显示任务栏
        private const int MONITOR_DEFAULTTONEAREST = 0x00000002;
        private static Window win;

        private static MINMAXINFO AdjustWorkingAreaForAutoHide(IntPtr monitorContainingApplication, MINMAXINFO mmi)
        {
            IntPtr hand = Win32API.FindWindow("Shell_TrayWnd", null);
            if (hand == IntPtr.Zero) return mmi;
            IntPtr monitorWithTaskbarOnIt = Win32API.MonitorFromWindow(hand, MONITOR_DEFAULTTONEAREST);
            if (!monitorContainingApplication.Equals(monitorWithTaskbarOnIt)) return mmi;
            APPBARDATA abd = new APPBARDATA();
            abd.cbSize = Marshal.SizeOf(abd);
            abd.hWnd = hand;
            Win32API.SHAppBarMessage((int)ABMsg.ABM_GETTASKBARPOS, ref abd);
            int uEdge = GetEdge(abd.rc);
            bool autoHide = Convert.ToBoolean(Win32API.SHAppBarMessage((int)ABMsg.ABM_GETSTATE, ref abd));

            if (!autoHide) return mmi;

            switch (uEdge)
            {
                case (int)ABEdge.ABE_LEFT:
                    mmi.ptMaxPosition.x += 2;
                    mmi.ptMaxTrackSize.x -= 2;
                    mmi.ptMaxSize.x -= 2;
                    break;
                case (int)ABEdge.ABE_RIGHT:
                    mmi.ptMaxSize.x -= 2;
                    mmi.ptMaxTrackSize.x -= 2;
                    break;
                case (int)ABEdge.ABE_TOP:
                    mmi.ptMaxPosition.y += 2;
                    mmi.ptMaxTrackSize.y -= 2;
                    mmi.ptMaxSize.y -= 2;
                    break;
                case (int)ABEdge.ABE_BOTTOM:
                    mmi.ptMaxSize.y -= 2;
                    mmi.ptMaxTrackSize.y -= 2;
                    break;
                default:
                    return mmi;
            }
            return mmi;
        }

        private static int GetEdge(RECT rc)
        {
            int uEdge = -1;
            if (rc.Top == rc.Left && rc.Bottom > rc.Right)
                uEdge = (int)ABEdge.ABE_LEFT;
            else if (rc.Top == rc.Left && rc.Bottom < rc.Right)
                uEdge = (int)ABEdge.ABE_TOP;
            else if (rc.Top > rc.Left)
                uEdge = (int)ABEdge.ABE_BOTTOM;
            else
                uEdge = (int)ABEdge.ABE_RIGHT;
            return uEdge;
        }

        /// <summary>
        /// 自动显示任务栏
        /// </summary>
        /// <param name="window"></param>
        public static void AutoShowTaksBar(Window window)
        {
            IntPtr handle = (new System.Windows.Interop.WindowInteropHelper(window)).Handle;
            win = window;

            if (handle != IntPtr.Zero)
                System.Windows.Interop.HwndSource.FromHwnd(handle).AddHook(new System.Windows.Interop.HwndSourceHook(WindowProc));
        }

        /// <summary>
        /// 去除自动显示任务栏
        /// </summary>
        /// <param name="window"></param>
        public static void RemoveAutoShowTaksBar(Window window)
        {
            IntPtr handle = (new System.Windows.Interop.WindowInteropHelper(window)).Handle;
            win = window;

            if (handle != IntPtr.Zero)
                System.Windows.Interop.HwndSource.FromHwnd(handle).RemoveHook(new System.Windows.Interop.HwndSourceHook(WindowProc));
        }

        private static IntPtr WindowProc(System.IntPtr hwnd, int msg, System.IntPtr wParam, System.IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0x0024:
                    WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;
            }

            return (IntPtr)0;
        }

        private static void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        {

            MINMAXINFO mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));

            // Adjust the maximized size and position to fit the work area of the correct monitor
            int MONITOR_DEFAULTTONEAREST = 0x00000002;
            IntPtr monitor = Win32API.MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);

            if (monitor != IntPtr.Zero)
            {
                MONITORINFO monitorInfo = new MONITORINFO();
                Win32API.GetMonitorInfo(monitor, monitorInfo);
                RECT rcWorkArea = monitorInfo.rcWork;
                RECT rcMonitorArea = monitorInfo.rcMonitor;
                mmi.ptMaxPosition.x = Math.Abs(rcWorkArea.Left - rcMonitorArea.Left);
                mmi.ptMaxPosition.y = Math.Abs(rcWorkArea.Top - rcMonitorArea.Top);
                mmi.ptMaxSize.x = Math.Abs(rcWorkArea.Right - rcWorkArea.Left);
                mmi.ptMaxSize.y = Math.Abs(rcWorkArea.Bottom - rcWorkArea.Top);
                mmi.ptMaxTrackSize.x = mmi.ptMaxSize.x;
                mmi.ptMaxTrackSize.y = mmi.ptMaxSize.y;


                mmi.ptMinTrackSize.x = (int)(win.MinWidth);
                mmi.ptMinTrackSize.y = (int)(win.MinHeight);

                mmi = AdjustWorkingAreaForAutoHide(monitor, mmi);
            }

            Marshal.StructureToPtr(mmi, lParam, true);
        }


        #endregion



        #region 全屏时隐藏任务栏
        private const int AutoHide = 0x01;

        private const int AlwaysOnTop = 0x02;

        /// <summary>
        /// Set the Taskbar State option
        /// </summary>
        /// <param name="option">AppBarState to activate</param>
        public static void SetTaskbarState(bool auto)
        {
            APPBARDATA msgData = new APPBARDATA();
            msgData.cbSize = Marshal.SizeOf(msgData);
            msgData.hWnd = Win32API.FindWindow("System_TrayWnd", null);
            msgData.lParam = auto;
            Win32API.SHAppBarMessage((int)ABMsg.ABM_SETSTATE, ref msgData);
        }



        #endregion




    }
}
