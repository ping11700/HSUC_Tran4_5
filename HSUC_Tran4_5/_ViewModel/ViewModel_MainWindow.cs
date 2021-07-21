﻿using Common;
using HSUC_Tran4_5._Model;
using HSUC_Tran4_5._View;
using System;

namespace HSUC_Tran4_5._ViewModel
{
    public partial class ViewModel_MainWindow : ViewModelBase
    {

        public DataService DataSer { get; init; }


        #region 命令

        /// <summary>
        ///     切换例子命令
        /// </summary>

        public UCCommand LoginCmd => new Lazy<UCCommand>(() =>
            new UCCommand(Login)).Value;

       

        #endregion









        internal ViewModel_MainWindow()
        {
            DataSer = new DataService();
        }


        public override void Init()
        {
            Start_VM01();
        }



        public override void Dispose()
        {

        }



        /// <summary>
        /// 登录
        /// </summary>
        private void Login()
        {
            MessageWindow messageWin = new MessageWindow();

            messageWin.Show();

        }



    }
}
