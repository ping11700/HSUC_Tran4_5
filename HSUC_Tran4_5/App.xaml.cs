using Common;
using Common.Log4;
using HSUC_Tran4_5.Tools;
using HSUC_Tran4_5.Utils;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace HSUC_Tran4_5
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        /// <summary>
        /// 检测多个进程同时运行的互斥体
        /// </summary>
        private Mutex _procInstanceMutex;

        /// <summary>
        /// 程序是否已启动
        /// </summary>
        private bool isApplicationStarted;


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ////捕获主线程（UI线程的异常）
            //DispatcherUnhandledException += App_DispatcherUnhandledException;
            ////捕获非UI线程的异常（线程池的异常不能捕获）
            //AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            ////捕获Task异常
            //TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            //检查是否同时运行了多个进程实例
            bool createdNew = false;
            _procInstanceMutex = new Mutex(true, "HSUC_Tran_App_Mutex", out createdNew);


            //防止多个进程实例
            if (!createdNew)
            {
                ProcessCallWin.CallWin();

                LogService.ILogger.Fatal($"HSUC_Tran_App_Mutex: { createdNew }");

                //Shutdown();
                Environment.Exit(0);

                return;
            }

            OpenOrCloseSplashScreen(true);

            //判断操作系统
            JudgeOSVersion();


#if DEBUG



            if (true/*Keyboard.IsKeyDown(Key.LeftShift)*/)
                //配置APP
                //ConfigureAPP();
#endif
                //客户端初始化
                Controller.Instance.Init();

            isApplicationStarted = true;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            //关闭控制台
            if (ConsoleManager.HasConsole)
                ConsoleManager.Hide();

            if (_procInstanceMutex != null)
                _procInstanceMutex.ReleaseMutex();

            LogService.ILogger.Info("Application is OnExit");

            //捕获主线程（UI线程的异常）
            this.DispatcherUnhandledException -= App_DispatcherUnhandledException;
            //捕获非UI线程的异常（线程池的异常不能捕获）
            AppDomain.CurrentDomain.UnhandledException -= CurrentDomain_UnhandledException;
            //捕获Task异常
            TaskScheduler.UnobservedTaskException -= TaskScheduler_UnobservedTaskException;

            ////xml 保存配置文件
            //XmlManager.Instance.SaveConfigs();
            //LogService.ILogger.Info($"XmlManager save congfigs success");

            //不管线程有没有工作都强制关闭所有线程进而正常结束进程。
            System.Environment.Exit(0);

        }

        /// <summary>
        /// 捕获非UI线程的异常（线程池的异常不能捕获）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            OnException(e.Exception);
        }

        /// <summary>
        /// 捕获主线程（UI线程的异常）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            OnException(e.ExceptionObject as Exception);
        }

        /// <summary>
        /// //捕获Task异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            OnException(e.Exception);
        }

        /// <summary>
        /// 系统异常 重启
        /// </summary>
        /// <param name="e"></param>
        private void OnException(Exception ex)
        {
            RecordUnhandledException(ex);

#if DEBUG

            if (ConsoleManager.HasConsole)
                Console.ReadKey();
#endif
            if (isApplicationStarted)
                RestartApplication();
        }

        /// <summary>
        /// 记录不能处理的系统异常
        /// </summary>
        /// <param name="e"></param>
        private void RecordUnhandledException(Exception ex)
        {
            LogService.ILogger.Fatal($"Unhandled Exception: {ex}");
        }

        /// <summary>
        /// 开启/关闭启动页
        /// </summary>
        /// <param name="isOpen">开启</param>
        public static void OpenOrCloseSplashScreen(bool isOpen)
        {
            //if (isOpen)
            //{
            //    splashScreen = new Tools.SplashScreen();

            //    splashScreen.Show();
            //}
            //else
            //{
            //    splashScreen?.Close();
            //}
        }

        /// <summary>
        /// 判定操作系统
        /// </summary>
        private void JudgeOSVersion()
        {

            string sysPath = OSVersion.Is64Sys ? "Win64" : "Win86";
            string dllPath = AppDomain.CurrentDomain.BaseDirectory + "Dll" + "\\" + sysPath;

            //记录版本号
            LogService.ILogger.Info(ClientVersion.SoftwareVersion);

            //判断操作系统
            if (OSVersion.IsWin7)
            {
                LogService.ILogger.Info("当前操作系统为Win7");
                //设置第三方dll注入路径
                //Win32API.SetDllDirectory(dllPath);

            }
            else if (OSVersion.IsWin8)
            {
                LogService.ILogger.Info("当前操作系统为Win8");
                //设置第三方dll注入路径
                //Win32API.SetDllDirectory(dllPath);

            }
            else if (OSVersion.IsWin8_1)
            {
                LogService.ILogger.Info("当前操作系统为Win8.1");
                //设置第三方dll注入路径
                //Win32API.SetDllDirectory(dllPath);

            }
            else if (OSVersion.IsWin10)
            {
                LogService.ILogger.Info("当前操作系统为Win10");
            }


        }

        /// <summary>
        /// 配置应用
        /// </summary>
        private void ConfigureAPP()
        {
            //Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            var win = new AppConfigWin();
            win.ShowDialog();

            //Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

            //是否打开控制台
            if (win.IsOpenConsole)
                ConsoleManager.Show();
        }

        /// <summary>
        /// 重启当前进程
        /// </summary>
        private void RestartApplication()
        {
            //程序位置
            string strAppFileName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;

            System.Diagnostics.Process myNewProcess = new System.Diagnostics.Process();
            //要启动的应用程序
            myNewProcess.StartInfo.FileName = strAppFileName;
            // 设置要启动的进程的初始目录
            myNewProcess.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            //启动程序
            myNewProcess.Start();
            //结束该程序
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            //结束该进程所有线程
            Environment.Exit(0);
        }

    }
}
