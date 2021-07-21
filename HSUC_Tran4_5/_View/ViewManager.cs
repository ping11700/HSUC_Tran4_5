﻿using HSUC_Tran4_5._ViewModel;
using System;

namespace HSUC_Tran4_5._View
{
    class ViewManager
    {
        private static readonly Lazy<ViewManager> lazy = new Lazy<ViewManager>(() => new ViewManager());

        public static ViewManager Instance => lazy.Value;

        public MainWindow MainWin { get; init; }

        private ViewManager() {
        
            MainWin = new MainWindow() { DataContext = ViewModelManager.Instance.VM_MainWindow };

        }



        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            MainWin.Show();
        }


        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            MainWin.Close();

        }


        
    }
}
