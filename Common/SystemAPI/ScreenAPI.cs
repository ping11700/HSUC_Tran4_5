using System;
using System.Windows;

namespace Common.SystemAPI
{
    public class ScreenAPI
    {
        public static void FindMonitorRectsFromPoint(Point point, out Rect monitorRect, out Rect workAreaRect)
        {
            var intPtr = Win32API.MonitorFromPoint(new POINT
            {
                x = (int)point.X,
                y = (int)point.Y
            }, 2);

            monitorRect = new Rect(0.0, 0.0, 0.0, 0.0);
            workAreaRect = new Rect(0.0, 0.0, 0.0, 0.0);

            if (intPtr != IntPtr.Zero)
            {
                MONITORINFO monitorInfo = default;
                Win32API.GetMonitorInfo(intPtr, monitorInfo);
                monitorRect = new Rect(monitorInfo.rcMonitor.Position, monitorInfo.rcMonitor.Size);
                workAreaRect = new Rect(monitorInfo.rcWork.Position, monitorInfo.rcWork.Size);
            }
        }
    }

}
