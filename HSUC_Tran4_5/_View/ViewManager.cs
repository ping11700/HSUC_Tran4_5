using HSUC_Tran4_5._ViewModel;
using System;

namespace HSUC_Tran4_5._View
{
    class ViewManager
    {
        private static readonly Lazy<ViewManager> lazy = new Lazy<ViewManager>(() => new ViewManager());

        public static ViewManager Instance => lazy.Value;

        private ViewManager() { }

        public MainWindow MainWin { get; private set; }


        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            CreateView();

            MainWin.Show();
        }


        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            DisposeView();
        }


        /// <summary>
        /// 创建View
        /// </summary>
        private void CreateView()
        {
            MainWin = new MainWindow() { DataContext = ViewModelManager.Instance.VM_MainWindow };
        }



        /// <summary>
        /// 释放View
        /// </summary>
        private void DisposeView()
        {
            MainWin.Close();
        }
    }
}
