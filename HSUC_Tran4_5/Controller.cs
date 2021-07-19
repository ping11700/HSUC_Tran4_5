using Common.Log4;
using HSUC_Tran4_5._View;
using HSUC_Tran4_5._ViewModel;
using System;
using System.Windows;

namespace HSUC_Tran4_5
{
    public sealed class Controller
    {
        private static readonly Lazy<Controller> lazy = new Lazy<Controller>(() => new Controller());

        public static Controller Instance => lazy.Value;

        private Controller() { }







        /// <summary>
        /// 初始化模块
        /// </summary>
        public void Init()
        {
            LogService.ILogger.Info("Startup");

            ////加载配置文件
            //XmlManager.Instance.LoadConfig();
            //LogService.ILogger.Info("XmlManager Load success");

            ////UIManager 初始化
            //UIManager.Instance.Init();
            //LogService.ILogger.Info("UIManager Init success");

            ////ModelManager 初始化
            //ModelManager.Instance.Init();
            //LogService.ILogger.Info("ModelManager Init success");

            ////网络初始化
            //NetworkManager.Instance.Init();
            //LogService.ILogger.Info("NetworkManager Init success");

            ////WebManager 初始化
            //WebManager.Instance.Init();
            //LogService.ILogger.Info("WebClient Init success");


            //ViewModelManager 初始化
            ViewModelManager.Instance.Init();
            LogService.ILogger.Info($"ViewModelManager Init success");


            //ViewManager 初始化
            ViewManager.Instance.Init();
            LogService.ILogger.Info($"ViewManager Init success");

        }












        /// <summary>
        /// 注册属性通知
        /// </summary>
        private void RegisterPropertyNotification()
        {
        }

        /// <summary>
        /// 注销属性通知
        /// </summary>
        private void DeregisterPropertyNotification()
        {
        }




        /// <summary>
        /// 程序关闭
        /// </summary>
        public void ShutDown()
        {
            LogService.ILogger.Info($"(程序关闭 -> ShutDown start)");

            //ViewManager 初始化
            ViewManager.Instance.Dispose();
            LogService.ILogger.Info($"ViewManager Dispose success");


            //ViewModelManager释放
            ViewModelManager.Instance.Dispose();
            LogService.ILogger.Info($"ViewModelManager Dispose success");


            ////XmlManager释放
            //XmlManager.Instance.Dispose();
            //LogService.ILogger.Info($"XmlManager save congfigs success");

            ////WebManager 释放
            //WebManager.Instance.Dispose();
            //LogService.ILogger.Info($"WebManager Dispose success");

            ////UIManager 释放
            //UIManager.Instance.Dispose();
            //LogService.ILogger.Info($"UIManager Dispose success");

            ////ModelManager 释放
            //ModelManager.Instance.Dispose();
            //LogService.ILogger.Info($"ModelManager Dispose success");

            ////NetworkManager 释放
            //NetworkManager.Instance.Dispose();
            //LogService.ILogger.Info($"NetworkManager Dispose success");


            //程序关闭
            Application.Current.Shutdown();
            LogService.ILogger.Info($"Application Shutdown success");
        }
    }
}
