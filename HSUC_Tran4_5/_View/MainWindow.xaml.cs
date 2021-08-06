using Common.Log4;
using Common.SystemAPI;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HSUC_Tran4_5._View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IBaseWin
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;

            string a = System.AppDomain.CurrentDomain.BaseDirectory;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            TaskbarAPI.AutoShowTaksBar(this);
        }





        #region 页面交互
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                try
                {
                    this.DragMove();
                }
                catch (Exception ex)
                {
                    LogService.ILogger.Warn($"MainWindow MouseMove_DragMove failed : {ex.Message } ");
                }
            }
        }
        #endregion



        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            TaskbarAPI.RemoveAutoShowTaksBar(this);
        }

        private void TabControl_MouseMove(object sender, MouseEventArgs e)
        {
            e.Handled = true;
        }
    }
}
