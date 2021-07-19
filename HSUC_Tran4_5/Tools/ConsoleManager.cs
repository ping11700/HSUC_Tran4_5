using Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace HSUC_Tran4_5.Tools
{


    //参考: https://www.cnblogs.com/Chary/p/No0000B8.html

    [SuppressUnmanagedCodeSecurity]
    public class ConsoleManager
    {
        /// <summary>
        ///     添加或删除从调用进程处理函数列表中的应用definedhandlerroutinefunction。如果没有指定的事件处理函数，函数集的可继承的属性，确定是否调用过程忽略了Ctrl + C信号。
        /// </summary>
        /// <param name="handlerRoutine">指向要添加或删除的程序定义HandlerRoutine函数的指针。 此参数可以为NULL。</param>
        /// <param name="add">
        ///     如果这个参数为TRUE，处理程序被添加; 如果是FALSE，则处理程序被删除。如果HandlerRoutine参数为NULL，一个TRUE值会导致调用进程忽略CTRL +
        ///     C输入，以及一个FALSE值恢复CTRL + C输入的正常处理。忽略或处理CTRL + C的此属性由子进程继承。
        /// </param>
        /// <returns>如果函数成功，返回值为非零。如果函数失败，返回值为零。</returns>
        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate handlerRoutine, bool add);

        //当关闭Console时，系统会发送下面的消息
        private const int CtrlCEvent = 0; //无论是从键盘输入或由GenerateConsoleCtrlEvent功能信号产生的一个CTRL + C接收信号
        private const int CtrlBreakEvent = 1; //无论是从键盘输入或由GenerateConsoleCtrlEvent信号产生的一个CTRL + BREAK信号接收。
        private const int CtrlCloseEvent = 2; //信号系统，当用户关闭控制台（通过单击控制台窗口菜单上的关闭按钮，或通过从任务管理器结束任务）
        private const int CtrlLogoffEvent = 5; //用户注销时系统发送到所有控制台进程的信号。此信号不指示哪个用户正在注销，因此不能进行任何假设。请注意，此信号仅由服务接收。交互式应用程序在注销时终止，因此当系统发送此信号时，它们不存在。
        private const int CtrlShutdownEvent = 6; //系统关闭时系统发送的信号。在系统发送此信号时，交互式应用程序不存在，因此在这种情况下它只能被服务接收。服务还有自己的关闭事件的通知机制。这个信号还可以通过使用应用程序生成的GenerateConsoleCtrlEvent。

        private static ConsoleCtrlDelegate ConsoleCtrlDelegateHandlerRoutine = HandlerRoutine;

        public delegate bool ConsoleCtrlDelegate(int dwCtrlType); //定义处理程序委托 

        //是否已经开启控制台
        public static bool HasConsole => Win32API.GetConsoleWindow() != IntPtr.Zero;



        public delegate void CtrlCPressedHandler(object sender, ConsoleCancelEventArgs e);

        public static event CtrlCPressedHandler CtrlCPressed;

        private static void OnCtrlCPressed(object sender, ConsoleCancelEventArgs e)
        {
            CtrlCPressed?.Invoke(sender, e);
        }


        /// <summary>
        /// Creates a new console instance if the process is not attached to a console already.
        /// </summary>
        public static void Show()
        {
            //#if DEBUG
            if (!HasConsole)
            {
                Win32API.AllocConsole();
                InvalidateOutAndError();

                //与控制台标题名一样的路径,根据控制台标题找控制台
                var windowHandler = Win32API.FindWindow(null, Process.GetCurrentProcess().MainModule.FileName);
                //找关闭按钮
                var closeMenu = Win32API.GetSystemMenu(windowHandler, IntPtr.Zero);
                var scClose = 0xF060;
                //关闭按钮禁用
                Win32API.RemoveMenu(closeMenu, scClose, 0x0);

                if (SetConsoleCtrlHandler(ConsoleCtrlDelegateHandlerRoutine, true))
                    Console.Write("成功阻止窗口关闭-");

            }
            //#endif
        }

        /// <summary>
        /// If the process has a console attached to it, it will be detached and no longer visible. Writing to the System.Console is still possible, but no output will be shown.
        /// </summary>
        public static void Hide()
        {
            //#if DEBUG
            if (HasConsole)
            {
                SetOutAndErrorNull();
                Win32API.FreeConsole();
            }
            //#endif
        }

        public static void Toggle()
        {
            if (HasConsole)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        private static void InvalidateOutAndError()
        {
            Type type = typeof(System.Console);

            System.Reflection.FieldInfo _out = type.GetField("_out",
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

            System.Reflection.FieldInfo _error = type.GetField("_error",
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

            System.Reflection.MethodInfo _InitializeStdOutError = type.GetMethod("InitializeStdOutError",
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);


            UAssert.Assert(_out != null);
            UAssert.Assert(_error != null);
            UAssert.Assert(_InitializeStdOutError != null);

            _out.SetValue(null, null);
            _error.SetValue(null, null);
            _InitializeStdOutError.Invoke(null, new object[] { true });
        }

        private static void SetOutAndErrorNull()
        {
            Console.SetOut(TextWriter.Null);
            Console.SetError(TextWriter.Null);
        }

        private static bool HandlerRoutine(int ctrlType)
        {
            switch (ctrlType)
            {
                case CtrlCEvent:
                    OnCtrlCPressed(null, null);
                    Console.WriteLine("Ctrl+C按下，阻止");
                    return true; //这里返回true，表示阻止响应系统对该程序的操作成功
                case CtrlBreakEvent:
                    Console.WriteLine("Ctrl+BREAK按下，阻止");
                    return true;
                case CtrlCloseEvent:
                    Console.WriteLine("CLOSE");
                    break;
                case CtrlLogoffEvent:
                    Console.WriteLine("LOGOFF");
                    break;
                case CtrlShutdownEvent:
                    Console.WriteLine("SHUTDOWN");
                    break;
            }
            return true; //true 表示阻止响应系统对该程序的操作 //false 忽略处理，让系统进行默认操作  
        }

    }
}
