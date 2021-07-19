using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Common.Controls
{
    //圆角窗体 控件

    /// <summary>
    /// Button_Elipse.xaml 的交互逻辑
    /// </summary>
    public partial class Window_Corner : Window
    {

        public bool IsShowMin
        {
            get { return (bool)GetValue(IsShowMinProperty); }
            set { SetValue(IsShowMinProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsShowMin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowMinProperty =
            DependencyProperty.Register("IsShowMin", typeof(bool), typeof(Window_Corner), new PropertyMetadata(false));


        public bool IsShowClose
        {
            get { return (bool)GetValue(IsShowCloseProperty); }
            set { SetValue(IsShowCloseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsShowClose.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowCloseProperty =
            DependencyProperty.Register("IsShowClose", typeof(bool), typeof(Window_Corner), new PropertyMetadata(false));




        /// <summary>
        /// 能否移动 
        /// </summary>
        public virtual bool IsCanMove { get; set; }


        //01
        static Window_Corner()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Window_Corner), new FrameworkPropertyMetadata(typeof(Window_Corner)));
        }

        //02 -> 外部 InitializeComponent 03
        public Window_Corner()
        {
            IsCanMove = true;
        }

        //04
        public override void OnApplyTemplate()
        {

            Button minBtn = this.GetTemplateChild("Btn_Min") as Button;
            Button closeBtn = this.GetTemplateChild("Btn_Close") as Button;

            if (minBtn == null || closeBtn == null) { return; }

            //minBtn.Click += delegate
            //{
            //    this.WindowState = WindowState.Minimized;
            //};

            //closeBtn.Click += delegate
            //{
            //    this.Close(); 
            //};

            minBtn.Click += WinMin;
            closeBtn.Click += WinClose;



        }

        protected virtual void WinMin(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        protected virtual void WinClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (IsCanMove)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            }
        }
    }
}
