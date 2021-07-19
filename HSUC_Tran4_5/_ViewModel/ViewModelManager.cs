using System;

namespace HSUC_Tran4_5._ViewModel
{
    public class ViewModelManager
    {
        private static readonly Lazy<ViewModelManager> lazy = new Lazy<ViewModelManager>(() => new ViewModelManager());

        public static ViewModelManager Instance => lazy.Value;

        private ViewModelManager() { }


        /// <summary>
        /// MainWindow的ViewModel
        /// </summary>
        public ViewModel_MainWindow VM_MainWindow { get; private set; }



        public void Init()
        {
            CreateMVVM();


        }


        public void Dispose()
        {

            DisposeMVVM();

        }


        /// <summary>
        /// 创建VM
        /// </summary>
        public void CreateMVVM()
        {

            //Main
            VM_MainWindow = new ViewModel_MainWindow();
            VM_MainWindow.Init();

            // ViewModel间通信
            ViewModelCommunicate();
        }



        /// <summary>
        /// 释放VM
        /// </summary>
        public void DisposeMVVM()
        {

            //ViewModel间断开通信
            ViewModelDisConnecte();


            VM_MainWindow.Dispose();


            VM_MainWindow = null;
        }




        /// <summary>
        /// ViewModel间通信
        /// </summary>
        private void ViewModelCommunicate()
        {

        }

        /// <summary>
        /// ViewModel间断开通信
        /// </summary>
        private void ViewModelDisConnecte()
        {

        }

    }
}
