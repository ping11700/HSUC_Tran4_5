using Common.Resources.CommonResources;
using Common.SystemAPI;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HSUC_Tran4_5._View
{
    /// <summary>
    /// UserControl_WinHeader.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl_WinHeader : UserControl
    {


        public Window _owner;


        public UserControl_WinHeader()
        {
            InitializeComponent();

            this.Loaded += UserControl_WinHeader_Loaded;


        }

        private void UserControl_WinHeader_Loaded(object sender, RoutedEventArgs e)
        {
            _owner = Window.GetWindow(this);

            _owner.StateChanged += _owner_StateChanged;
        }

        private void _owner_StateChanged(object sender, EventArgs e)
        {
            this.Max_Btn.BPath = _owner.WindowState == WindowState.Maximized ?
                                 CommonResources.NormalGeometry : CommonResources.MaxGeometry;
        }



        /// <summary>
        /// 置顶/取消置顶
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Fix_Btn_Click(object sender, RoutedEventArgs e)
        {
            //取消置顶
            if (_owner.Topmost)
            {
                WindowAPI.SetWinTopMost(-2, this);
                _owner.Topmost = false;
            }
            //置顶
            else
            {
                WindowAPI.SetWinTopMost(-1, this);
                _owner.Topmost = true;
            }
            this.Fix_Btn.BPath = _owner.Topmost ? CommonResources.FixedGeometry : CommonResources.FixGeometry;
        }



        /// <summary>
        /// 最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Min_Click(object sender, RoutedEventArgs e)
        {
            _owner.WindowState = WindowState.Minimized;
        }


        /// <summary>
        /// 最大化/Normal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Max_Click(object sender, RoutedEventArgs e)
        {
            //最大化/非最大化
            if (_owner.WindowState == WindowState.Maximized)
            {
                _owner.ResizeMode = ResizeMode.CanResize;

                _owner.WindowState = WindowState.Normal;
            }
            else
            {
                _owner.ResizeMode = ResizeMode.NoResize;

                _owner.WindowState = WindowState.Maximized;
            }

        }


        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            _owner.Close();
        }


    }
}
