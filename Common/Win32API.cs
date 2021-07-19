using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Windows;

namespace Common
{
    public class Win32API
    {

        #region 注入DLL

        #region 窗体Dll

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        //参数1:指的是类名。参数2，指的是窗口的标题名。两者至少要知道1个
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hwnd, uint wMsg, int wParam, string lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hwnd, uint wMsg, IntPtr wParam, IntPtr lParam);

        // 用于设置窗口
        [DllImport("user32.dll", EntryPoint = "SetWindowPos", CallingConvention = CallingConvention.StdCall)]
        public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);


        /*不用UI渲染完成即可获得句柄
		  获得焦点窗口的句柄
		*/
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetCursorPos(out POINT pt);

        #endregion 窗体Dll

        #region 控制台DLL


        /// <summary>
        ///     为当前进程分配一个新控制台
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();

        /// <summary>
        ///     使调用进程从其控制台分离
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool FreeConsole();

        /// <summary>
        ///     检索与调用进程相关联的控制台窗口句柄
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetConsoleWindow();

        /// <summary>
        ///     检取与调用进程有关的控制台所用的输出代码页的等价内容，以便将输出函数所写入的内容转换成显示图象
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern int GetConsoleOutputCP();


        //AttachConsole是把输出附加到指定控制台上
        //当dwProcessId = -1，表示使用当前进程的父进程控制台；当dwProcessId = pid，表示使用指定进程的控制台
        [DllImport("Kernel32.dll", EntryPoint = "AttachConsole", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern void AttachConsole(int dwProcessId);

        #endregion

        #region 菜单DLL

        /// <summary>
        ///     该函数允许应用程序为复制或修改而访问窗口菜单（系统菜单或控制菜单）。
        ///     任何没有用GetSystemMenu函数来生成自己的窗口菜单拷贝的窗口将接受标准窗口菜单。
        ///     窗口菜单最初包含的菜单项有多种标识符值，如SC_CLOSE，SC_MOVE和SC_SIZE。
        ///     窗口菜单上的菜单项发送WM_SYSCOMMAND消息。
        /// </summary>
        /// <param name="hWnd">拥有窗口菜单拷贝的窗口的句柄。</param>
        /// <param name="bRevert">指定将执行的操作。如果此参数为FALSE，GetSystemMenu返回当前使用窗口菜单的拷贝的句柄。该拷贝初始时与窗口菜单相同，但可以被修改。如果此参数为TRUE，GetSystemMenu重置窗口菜单到缺省状态。如果存在先前的窗口菜单，将被销毁。</param>
        /// <returns>如果参数bRevert为FALSE，返回值是窗口菜单的拷贝的句柄：如果参数bRevert为TRUE，返回值是NULL。</returns>
        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, IntPtr bRevert);


        /// <summary>
        ///     删除指定的菜单项或弹出式菜单
        /// </summary>
        /// <param name="hMenu"></param>
        /// <param name="nPos"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        public static extern int RemoveMenu(IntPtr hMenu, int nPos, int flags);
        #endregion

        #region 屏幕DLL

        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromWindow(IntPtr hwnd, MonitorFromWindowFlags dwFlags);

        [DllImport("SHCore.dll")]
        public static extern int SetProcessDpiAwareness(PROCESS_DPI_AWARENESS awareness);

        [DllImport("SHCore.dll")]
        public static extern int GetProcessDpiAwareness(IntPtr hprocess, out PROCESS_DPI_AWARENESS awareness);

        [DllImport("SHCore.dll")]
        public static extern int GetDpiForMonitor(IntPtr hmonitor, MonitorDpiTypes dpiType, out int dpiX, out int dpiY);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hdc);

        [DllImport("gdi32.dll")]
        public static extern int GetDeviceCaps(IntPtr hdc, DeviceCaps nIndex);

        #endregion 屏幕DLL

        #region 任务栏DLL
        [DllImport("shell32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int SHAppBarMessage(int dwMessage, ref APPBARDATA pData);

        [DllImport("user32.dll")]
        public static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);

        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromPoint(POINT pt, int flags);


        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);
        #endregion 任务栏DLL

        #region 浏览器DLL
        [DllImport("KERNEL32.DLL", EntryPoint = "SetProcessWorkingSetSize", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool SetProcessWorkingSetSize(IntPtr pProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);

        [DllImport("KERNEL32.DLL", EntryPoint = "GetCurrentProcess", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetCurrentProcess();


        [DllImport("urlmon.dll", CharSet = CharSet.Ansi)]
        public static extern int UrlMkSetSessionOption(int dwOption, string pBuffer, int dwBufferLength, int dwReserved);


        #region 绘制矩形
        [DllImport("user32.dll")]
        public static extern bool SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool redraw);

        /// <summary>
        /// 创建一个矩形，本来四个参数均为x1 y1 x2 y2 意思为左上角X1，Y1坐标，右下角X2,Y2坐标
        /// left意味矩形和左边的距离
        /// top意味着矩形和顶边距离
        /// rectrightbottom_x意味着矩形右下角的X坐标
        /// rectrightbottom_y意味着矩形右下角的Y坐标
        /// </summary>
        /// <param name="Left"></param>
        /// <param name="Top"></param>
        /// <param name="RectRightBottom_X"></param>
        /// <param name="RectRightBottom_Y"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateRectRgn(int Left, int Top, int RectRightBottom_X, int RectRightBottom_Y);

        //合并选项:
        //RGN_AND  = 1;
        //RGN_OR   = 2;
        //RGN_XOR  = 3;
        //RGN_DIFF = 4;
        //RGN_COPY = 5; {复制第一个区域}
        [DllImport("gdi32.dll")]
        public static extern int CombineRgn(IntPtr hrgnDst, IntPtr hrgnSrc1, IntPtr hrgnSrc2, int iMode);
        #endregion

        #endregion

        #region 系统休眠DLL
        [DllImport("kernel32.dll")]
        public static extern uint SetThreadExecutionState(ExecutionFlag flags);

        #endregion 系统休眠DLL

        #region 刷新资源管理DLL

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern void SHChangeNotify(SHChangeNotifyEvents eventID, SHChangeNotifyFlags flags, string path, string path2);

        #endregion

        #region 启动一个外部程序DLL

        //https://blog.csdn.net/jinhuicao/article/details/83573246
        [DllImport("kernel32.dll")]
        public static extern uint WinExec(string lpCmdLine, WinExecFlag uCmdShow);

        #endregion

        #region 网络DLL


        [DllImport("winInet.dll ")]
        public static extern bool InternetGetConnectedState(ref int flag, int dwReserved);


        // The GetExtendedTcpTable function retrieves a table that contains a list of
        // TCP endpoints available to the application. Decorating the function with
        // DllImport attribute indicates that the attributed method is exposed by an
        // unmanaged dynamic-link library 'iphlpapi.dll' as a static entry point.
        [DllImport("iphlpapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern uint GetExtendedTcpTable(IntPtr pTcpTable, ref int pdwSize,
            bool bOrder, int ulAf, TcpTableClass tableClass, uint reserved = 0);

        // The GetExtendedUdpTable function retrieves a table that contains a list of
        // UDP endpoints available to the application. Decorating the function with
        // DllImport attribute indicates that the attributed method is exposed by an
        // unmanaged dynamic-link library 'iphlpapi.dll' as a static entry point.
        [DllImport("iphlpapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern uint GetExtendedUdpTable(IntPtr pUdpTable, ref int pdwSize,
            bool bOrder, int ulAf, UdpTableClass tableClass, uint reserved = 0);


        #endregion

        #region 壁纸引擎DLL
        [DllImport("user32", ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.I4)]
        public static extern int MapWindowPoints(IntPtr hWndFrom, IntPtr hWndTo, [In, Out] ref RECT rect, [MarshalAs(UnmanagedType.U4)] int cPoints);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetParent(IntPtr child, IntPtr parent);

        [DllImport("user32.dll")]
        public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        [DllImport("user32.dll")]
        public static extern IntPtr GetMenu(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int GetMenuItemCount(IntPtr hMenu);

        [DllImport("user32.dll")]
        public static extern bool RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);

        [DllImport("user32.dll")]
        public static extern bool DrawMenuBar(IntPtr hWnd);


        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
        private static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);


        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        private static extern IntPtr GetWindowLongPtr32(IntPtr hWnd, int nIndex);

        // This static method is required because Win32 does not support
        // GetWindowLongPtr directly
        public static IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size == 8)
                return GetWindowLongPtr64(hWnd, nIndex);
            else
                return GetWindowLongPtr32(hWnd, nIndex);
        }


        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        public static extern int SetWindowLong32(HandleRef hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]

        public static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, IntPtr dwNewLong);
        // This helper static method is required because the 32-bit version of user32.dll does not contain this API
        // (on any versions of Windows), so linking the method will fail at run-time. The bridge dispatches the request
        // to the correct function (GetWindowLong in 32-bit mode and GetWindowLongPtr in 64-bit mode)
        public static IntPtr SetWindowLongPtr(HandleRef hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 8)
                return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
            else
                return new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
        }

        #endregion

        #endregion 注入DLL


        /// <summary>
        /// 设置默认Dll路径
        /// </summary>
        /// <param name="lpPathName"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDllDirectory(string dllPath);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, BestFitMapping = false, SetLastError = true, ExactSpelling = true)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, String methodName);


        [DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode, SetLastError = true)]
        [ResourceExposure(ResourceScope.Process)]
        public static extern IntPtr LoadLibrary(string libFilename);

        [DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode, SetLastError = true)]
        [ResourceExposure(ResourceScope.Process)]
        public static extern bool FreeLibrary(IntPtr hModule);


        /// <summary>
        /// 删除句柄
        /// </summary>
        /// <param name="hObject"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);



    }


    #region Dll 数据结构 

    /// <summary>
    /// 设置窗体显示形式
    /// </summary>
    public enum nCmdShow : uint
    {
        SW_NONE,//初始值
        SW_FORCEMINIMIZE,//：在WindowNT5.0中最小化窗口，即使拥有窗口的线程被挂起也会最小化。在从其他线程最小化窗口时才使用这个参数。
        SW_MIOE,//：隐藏窗口并激活其他窗口。
        SW_MAXIMIZE,//：最大化指定的窗口。
        SW_MINIMIZE,//：最小化指定的窗口并且激活在Z序中的下一个顶层窗口。
        SW_RESTORE,//：激活并显示窗口。如果窗口最小化或最大化，则系统将窗口恢复到原来的尺寸和位置。在恢复最小化窗口时，应用程序应该指定这个标志。
        SW_SHOW,//：在窗口原来的位置以原来的尺寸激活和显示窗口。
        SW_SHOWDEFAULT,//：依据在STARTUPINFO结构中指定的SW_FLAG标志设定显示状态，STARTUPINFO 结构是由启动应用程序的程序传递给CreateProcess函数的。
        SW_SHOWMAXIMIZED,//：激活窗口并将其最大化。
        SW_SHOWMINIMIZED,//：激活窗口并将其最小化。
        SW_SHOWMINNOACTIVATE,//：窗口最小化，激活窗口仍然维持激活状态。
        SW_SHOWNA,//：以窗口原来的状态显示窗口。激活窗口仍然维持激活状态。
        SW_SHOWNOACTIVATE,//：以窗口最近一次的大小和状态显示窗口。激活窗口仍然维持激活状态。
        SW_SHOWNOMAL,//：激活并显示一个窗口。如果窗口被最小化或最大化，系统将其恢复到原来的尺寸和大小。应用程序在第一次显示窗口的时候应该指定此标志。
    }

    #endregion Dll 数据结构 

    #region 屏幕Dll 数据结构

    [Flags]
    public enum MonitorFromWindowFlags
    {
        MONITOR_DEFAULTTONULL = 0x00000000,
        MONITOR_DEFAULTTOPRIMARY = 0x00000001,
        MONITOR_DEFAULTTONEAREST = 0x00000002,
    }


    public enum MonitorDpiTypes
    {
        EffectiveDPI = 0,
        AngularDPI = 1,
        RawDPI = 2,
    }

    public enum PROCESS_DPI_AWARENESS
    {
        PROCESS_DPI_UNAWARE = 0,
        PROCESS_SYSTEM_DPI_AWARE = 1,
        PROCESS_PER_MONITOR_DPI_AWARE = 2,
    }

    public enum DeviceCaps
    {
        /// <summary>
        /// Logical pixels inch in X
        /// </summary>
        LOGPIXELSX = 88,

        /// <summary>
        /// Horizontal width in pixels
        /// </summary>
        HORZRES = 8,

        /// <summary>
        /// Horizontal width of entire desktop in pixels
        /// </summary>
        DESKTOPHORZRES = 118
    }

    #endregion

    #region 任务栏Dll 数据结构

    [StructLayout(LayoutKind.Sequential)]
    public struct APPBARDATA
    {
        public int cbSize;
        public IntPtr hWnd;
        public int uCallbackMessage;
        public int uEdge;
        public RECT rc;
        public bool lParam;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class MONITORINFO
    {
        public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));
        public RECT rcMonitor = new RECT();
        public RECT rcWork = new RECT();
        public int dwFlags = 0;
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;                             //最左坐标
        public int Top;                             //最上坐标
        public int Right;                           //最右坐标
        public int Bottom;                        //最下坐标


        public Point Position => new Point(Left, Top);
        public Size Size => new Size(Width, Height);

        public int Height
        {
            get => Bottom - Top;
            set => Bottom = Top + value;
        }

        public int Width
        {
            get => Right - Left;
            set => Right = Left + value;
        }
    }

    public enum ABEdge
    {
        ABE_LEFT = 0,
        ABE_TOP = 1,
        ABE_RIGHT = 2,
        ABE_BOTTOM = 3
    }

    public enum ABMsg
    {
        ABM_NEW = 0,
        ABM_REMOVE = 1,
        ABM_QUERYPOS = 2,
        ABM_SETPOS = 3,
        ABM_GETSTATE = 4,
        ABM_GETTASKBARPOS = 5,
        ABM_ACTIVATE = 6,
        ABM_GETAUTOHIDEBAR = 7,
        ABM_SETAUTOHIDEBAR = 8,
        ABM_WINDOWPOSCHANGED = 9,
        ABM_SETSTATE = 10
    }



    [StructLayout(LayoutKind.Sequential)]
    public struct MINMAXINFO
    {
        public POINT ptReserved;
        public POINT ptMaxSize;
        public POINT ptMaxPosition;
        public POINT ptMinTrackSize;
        public POINT ptMaxTrackSize;
    };



    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int x;
        public int y;

        public POINT(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    #endregion

    #region 系统休眠Dll 数据结构

    /*
	*只使用Continus参数时，则是恢复系统休眠策略。
	*不使用Continus参数时，实现阻止系统休眠或显示器关闭一次
	*组合使用Continus参数时，实现阻止系统休眠或显示器关闭至线程终止
	*/
    [Flags]
    public enum ExecutionFlag : uint
    {
        System = 0x00000001,
        Display = 0x00000002,
        Continus = 0x80000000,
    }
    #endregion 系统休眠Dll 数据结构

    #region 刷新资源管理Dll 数据结构

    [Flags]
    public enum SHChangeNotifyEvents : uint
    {
        RenameItem = 0x00000001,
        Create = 0x00000002,
        Delete = 0x00000004,
        MkDir = 0x00000008,
        RmDir = 0x00000010,
        MediaInserted = 0x00000020,
        MediaRemoved = 0x00000040,
        DriveRemoved = 0x00000080,
        DriveAdd = 0x00000100,
        NetShare = 0x00000200,
        NetUnshare = 0x00000400,
        Attributes = 0x00000800,
        UpdateDir = 0x00001000,
        UpdateItem = 0x00002000,
        ServerDisconnect = 0x00004000,
        UpdateImage = 0x00008000,
        DriveAddGui = 0x00010000,
        RenameFolder = 0x00020000,
        FreeSpace = 0x00040000,
        ExtendedEvent = 0x04000000,
        AssocChanged = 0x08000000,
        DiskEvents = 0x0002381F,
        GlobalEvents = 0x0C0581E0,
        AllEvents = 0x7FFFFFFF,
        Interrupt = 0x80000000
    }

    public enum SHChangeNotifyFlags : uint
    {
        IdList = 0x0000,
        PathA = 0x0001,
        PrinterA = 0x0002,
        Dword = 0x0003,
        PathW = 0x0005,
        PrinterW = 0x0006,
        Type = 0x00FF,
        Flush = 0x1000,
        FlushNoWait = 0x3000,
        NotifyRecursive = 0x10000
    }
    #endregion


    #region 启动一个外部程序Dll 数据结构

    [Flags]
    public enum WinExecFlag : uint
    {
        SW_HIDE = 0,//隐藏窗口，活动状态给令一个窗口
        SW_SHOWNORMAL = 1,//用原来的大小和位置显示一个窗口，同时令其进入活动状态
        SW_NORMAL = 1,
        SW_SHOWMINIMIZED = 2,
        SW_SHOWMAXIMIZED = 3,
        SW_MAXIMIZE = 3,
        SW_SHOWNOACTIVATE = 4, //用最近的大小和位置显示一个窗口，同时不改变活动窗口
        SW_SHOW = 5, //用当前的大小和位置显示一个窗口，同时令其进入活动状态
        SW_MINIMIZE = 6, //最小化窗口，活动状态给令一个窗口
        SW_SHOWMINNOACTIVE = 7,//最小化一个窗口，同时不改变活动窗口
        SW_SHOWNA = 8, //用当前的大小和位置显示一个窗口，不改变活动窗口
        SW_RESTORE = 9, //与 SW_SHOWNORMAL  1 相同
        SW_SHOWDEFAULT = 10,
        SW_FORCEMINIMIZE = 11,
        SW_MAX = 11,
    }
    #endregion

    #region 网络Dll 数据结构
    // Enum to define the set of values used to indicate the type of table returned by 
    // calls made to the function 'GetExtendedTcpTable'.
    public enum TcpTableClass
    {
        TCP_TABLE_BASIC_LISTENER,
        TCP_TABLE_BASIC_CONNECTIONS,
        TCP_TABLE_BASIC_ALL,
        TCP_TABLE_OWNER_PID_LISTENER,
        TCP_TABLE_OWNER_PID_CONNECTIONS,
        TCP_TABLE_OWNER_PID_ALL,
        TCP_TABLE_OWNER_MODULE_LISTENER,
        TCP_TABLE_OWNER_MODULE_CONNECTIONS,
        TCP_TABLE_OWNER_MODULE_ALL
    }


    // Enum to define the set of values used to indicate the type of table returned by calls
    // made to the function GetExtendedUdpTable.
    public enum UdpTableClass
    {
        UDP_TABLE_BASIC,
        UDP_TABLE_OWNER_PID,
        UDP_TABLE_OWNER_MODULE
    }
    #endregion


    #region 壁纸引擎DLL 数据结构



    public abstract class WindowStyles
    {
        public const uint WS_OVERLAPPED = 0x00000000;
        public const uint WS_POPUP = 0x80000000;
        public const uint WS_CHILD = 0x40000000;
        public const uint WS_MINIMIZE = 0x20000000;
        public const uint WS_VISIBLE = 0x10000000;
        public const uint WS_DISABLED = 0x08000000;
        public const uint WS_CLIPSIBLINGS = 0x04000000;
        public const uint WS_CLIPCHILDREN = 0x02000000;
        public const uint WS_MAXIMIZE = 0x01000000;
        public const uint WS_CAPTION = 0x00C00000;     /* WS_BORDER | WS_DLGFRAME  */
        public const uint WS_BORDER = 0x00800000;
        public const uint WS_DLGFRAME = 0x00400000;
        public const uint WS_VSCROLL = 0x00200000;
        public const uint WS_HSCROLL = 0x00100000;
        public const uint WS_SYSMENU = 0x00080000;
        public const uint WS_THICKFRAME = 0x00040000;
        public const uint WS_GROUP = 0x00020000;
        public const uint WS_TABSTOP = 0x00010000;

        public const uint WS_MINIMIZEBOX = 0x00020000;
        public const uint WS_MAXIMIZEBOX = 0x00010000;

        public const uint WS_TILED = WS_OVERLAPPED;
        public const uint WS_ICONIC = WS_MINIMIZE;
        public const uint WS_SIZEBOX = WS_THICKFRAME;
        public const uint WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW;

        // Common Window Styles

        public const uint WS_OVERLAPPEDWINDOW =
            (WS_OVERLAPPED |
              WS_CAPTION |
              WS_SYSMENU |
              WS_THICKFRAME |
              WS_MINIMIZEBOX |
              WS_MAXIMIZEBOX);

        public const uint WS_POPUPWINDOW =
            (WS_POPUP |
              WS_BORDER |
              WS_SYSMENU);

        public const uint WS_CHILDWINDOW = WS_CHILD;

        //Extended Window Styles

        public const uint WS_EX_DLGMODALFRAME = 0x00000001;
        public const uint WS_EX_NOPARENTNOTIFY = 0x00000004;
        public const uint WS_EX_TOPMOST = 0x00000008;
        public const uint WS_EX_ACCEPTFILES = 0x00000010;
        public const uint WS_EX_TRANSPARENT = 0x00000020;

        //#if(WINVER >= 0x0400)
        public const uint WS_EX_MDICHILD = 0x00000040;
        public const uint WS_EX_TOOLWINDOW = 0x00000080;
        public const uint WS_EX_WINDOWEDGE = 0x00000100;
        public const uint WS_EX_CLIENTEDGE = 0x00000200;
        public const uint WS_EX_CONTEXTHELP = 0x00000400;

        public const uint WS_EX_RIGHT = 0x00001000;
        public const uint WS_EX_LEFT = 0x00000000;
        public const uint WS_EX_RTLREADING = 0x00002000;
        public const uint WS_EX_LTRREADING = 0x00000000;
        public const uint WS_EX_LEFTSCROLLBAR = 0x00004000;
        public const uint WS_EX_RIGHTSCROLLBAR = 0x00000000;

        public const uint WS_EX_CONTROLPARENT = 0x00010000;
        public const uint WS_EX_STATICEDGE = 0x00020000;
        public const uint WS_EX_APPWINDOW = 0x00040000;

        public const uint WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE);
        public const uint WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST);
        //#endif /* WINVER >= 0x0400 */

        //#if(_WIN32_WINNT >= 0x0500)
        public const uint WS_EX_LAYERED = 0x00080000;
        //#endif /* _WIN32_WINNT >= 0x0500 */

        //#if(WINVER >= 0x0500)
        public const uint WS_EX_NOINHERITLAYOUT = 0x00100000; // Disable inheritence of mirroring by children
        public const uint WS_EX_LAYOUTRTL = 0x00400000; // Right to left mirroring
                                                        //#endif /* WINVER >= 0x0500 */

        //#if(_WIN32_WINNT >= 0x0500)
        public const uint WS_EX_COMPOSITED = 0x02000000;
        public const uint WS_EX_NOACTIVATE = 0x08000000;
        //#endif /* _WIN32_WINNT >= 0x0500 */
    }


    #endregion

}
