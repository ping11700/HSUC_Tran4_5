using Common;
using HSUC_Tran4_5._Model;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace HSUC_Tran4_5._ViewModel
{
    public partial class ViewModel_MainWindow : ViewModelBase
    {

        #region 属性

        private ObservableCollection<DemoDataModel> dataList;
        public ObservableCollection<DemoDataModel> DataList
        {
            get => dataList;
            set { dataList = value; NotifyPropertyChanged(nameof(DataList)); }
        }

        private ObservableCollection<LogDataModel> logDataList;
        public ObservableCollection<LogDataModel> LogDataList
        {
            get => logDataList;
            set { logDataList = value; NotifyPropertyChanged(nameof(LogDataList)); }
        }
        #endregion



        #region 命令

        /// <summary>
        ///     切换例子命令
        /// </summary>

        public UCCommand LoadDataCmd => new Lazy<UCCommand>(() =>
            new UCCommand(LoadData)).Value;


        #endregion



        #region 方法


        public void Start_VM01()
        {
            LoadData();
        }





        private void LoadData()
        {

#if NET40
            Task.Factory.StartNew(() =>
#else
            Task.Run(() =>
#endif
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    DataList = new ObservableCollection<DemoDataModel>(DataSer.GetDemoDataList());

                    LogDataList = new ObservableCollection<LogDataModel>(DataSer.GetLogDataList());

                }), DispatcherPriority.ApplicationIdle);
            });


        }

        #endregion
    }
}
