using Common;
using HSUC_Tran4_5._Model;
using HSUC_Tran4_5._View;
using HSUC_Tran4_5.Utils;
using System;

namespace HSUC_Tran4_5._ViewModel
{
    public class ViewModel_MessageWindow : ViewModelBase
    {

        private MessWinTypes messageWinType;
        public MessWinTypes MessageWinType
        {
            get => messageWinType;
            set 
            {
                messageWinType = value;
                NotifyPropertyChanged(nameof(MessageWinType));
            }


        }


        #region 命令

       
       

        #endregion









        internal ViewModel_MessageWindow()
        {
            
        }


        public override void Init()
        {
        }



        public override void Dispose()
        {

          
            
        }





        internal void OpenMessageWindow(MessWinTypes winType) 
        {
            switch (winType)
            {
                case MessWinTypes.TaskConfig:
                    MessageWinType = MessWinTypes.TaskConfig;
                    break;
                case MessWinTypes.Login:
                    MessageWinType = MessWinTypes.Login;
                    break;
                case MessWinTypes.Update:
                    MessageWinType = MessWinTypes.Update;
                    break;
                default:
                    break;
            }

            ViewManager.Instance.MesgWin.ShowDialog();
        }
    }
}
