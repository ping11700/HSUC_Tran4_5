using Common;
using HSUC_Tran4_5._Model;
using HSUC_Tran4_5._View;
using HSUC_Tran4_5.Utils;
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

        public UCCommand OpenLoginCmd => new Lazy<UCCommand>(() =>
            new UCCommand(OpenLogin)).Value;


        public UCCommand OpenTaskCongifCmd => new Lazy<UCCommand>(() =>
            new UCCommand(OpenTaskConfig)).Value;


        public UCCommand OpenUpdataCmd => new Lazy<UCCommand>(() =>
            new UCCommand(OpenUpdata)).Value;


        


        public Action<MessWinTypes> OpenMessageWindowAction;


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
        private void OpenLogin()
        {
            OpenMessageWindowAction?.Invoke(MessWinTypes.Login);
        }


        /// <summary>
        /// 更新
        /// </summary>
        private void OpenUpdata()
        {
            OpenMessageWindowAction?.Invoke(MessWinTypes.Update);
        }


        #region ContextMenu 右键菜单

        /// <summary>
        /// 任务配置
        /// </summary>
        private void OpenTaskConfig()
        {
            OpenMessageWindowAction?.Invoke(MessWinTypes.TaskConfig);
        }





        #endregion
    }
}
