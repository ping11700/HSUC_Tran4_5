using System;
using System.Linq;
using System.Windows;
using System.Windows.Interop;

namespace Common.SystemAPI
{
    public class WindowAPI
    {


        public static Window GetActiveWindow() => Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);





        /*hWndInsertAfter，[输入]：
 存放要将hWnd参数指定的窗口定位在哪个窗口句柄的前面，不能为NULL，也可以为（选一至一个）：
 HWND_BOTTOM（1）：
         如果hWnd参数指定的窗口是当前活动窗口，将此窗口定位在Z轴顺序的底部，也就是所有窗口的后面，且如果此窗口是置顶窗口，就变成非置顶窗口。
         如果hWnd参数指定的窗口不是当前活动窗口，则不做任何定位，无论此窗口是置顶窗口，还是非置顶窗口。
 HWND_NOTOPMOST（-2）：
         如果hWnd参数指定的窗口是置顶窗口，且是当前活动窗口，就变成非置顶窗口，并定位在所有非置顶窗口的前面，及在所有置顶窗口的后面。
         如果hWnd参数指定的窗口已经是一个非置顶窗口，或不是当前活动窗口，则不做任何定位。
 HWND_TOP（0）：
         如果hWnd参数指定的窗口是非置顶窗口，且是当前活动窗口，将窗口定位在所有非置顶窗口的前面，及在所有置顶窗口的后面。
         如果hWnd参数指定的窗口已经是一个置顶窗口，或不是当前活动窗口，则不做任何定位。
 HWND_TOPMOST（-1）：
         将hWnd参数指定的窗口定位在所有非置顶窗口和置顶窗口的前面，并将窗口变成置顶窗口,无论此窗口是不是当前活动窗口。
         如果hWnd参数指定的窗口在置顶后，又有其他窗口被置顶，则此窗口将被定位在其他置顶窗口的后面。
         如果要一直保持某个窗口的置顶位置，需要每隔一段时间就设置一次置顶，才能保证不被其他窗口盖住。
  */
        /// <summary>
        /// 窗体是否置顶
        /// </summary>
        /// <param name="IsTop"></param>
        /// <param name="win"></param>
        public static void SetWinTopMost(int hWndInsertAfter, UIElement ue, int wFlags = 0)
        {

            var ps = PresentationSource.FromVisual(ue);
            if (ps == null) return;

            var hwnd = ((HwndSource)PresentationSource.FromVisual(ue)).Handle;
            RECT rect;

            if (Win32API.GetWindowRect(hwnd, out rect))
            {
                Win32API.SetWindowPos(hwnd, hWndInsertAfter, rect.Left, rect.Top, Math.Abs(rect.Right - rect.Left), Math.Abs(rect.Bottom - rect.Top), wFlags);
            }
        }
    }
}
