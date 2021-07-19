using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace Common.Controls
{


    /*
     * 播放视频清晰度 按钮
     */

    /// <summary>
    /// Button_Resolution.xaml 的交互逻辑
    /// </summary>
    public partial class Button_Resolution : Button
    {

        /// <summary>
        /// 数据源
        /// </summary>
        public IEnumerable ItemS
        {
            get { return (IEnumerable)GetValue(ItemSProperty); }
            set { SetValue(ItemSProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItmeS.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemSProperty =
            DependencyProperty.Register("ItemS", typeof(IEnumerable), typeof(Button_Resolution), new PropertyMetadata(null));



        public string SelectedValue
        {
            get { return (string)GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register("SelectedValue", typeof(string), typeof(Button_Resolution), new PropertyMetadata(string.Empty));



        public bool IsCloseS
        {
            get { return (bool)GetValue(IsCloseSProperty); }
            set { SetValue(IsCloseSProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsCloseS.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCloseSProperty =
            DependencyProperty.Register("IsCloseS", typeof(bool), typeof(Button_Resolution), new PropertyMetadata(false));



        /// <summary>
        /// 音量改变事件
        /// </summary>
        public static readonly RoutedEvent RSelectChangedEvent = EventManager.RegisterRoutedEvent("RSelectChangedEvent", RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventArgs<object>), typeof(Button_Resolution));

        public event RoutedEventHandler RSelectChanged
        {
            //将路由事件添加路由事件处理程序
            add { AddHandler(RSelectChangedEvent, value); }
            //从路由事件处理程序中移除路由事件
            remove { RemoveHandler(RSelectChangedEvent, value); }
        }


        private ListView _lv;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _lv = this.GetTemplateChild("PART_LV") as ListView;

            if (_lv == null) return;

            _lv.SelectionChanged += (s, e) =>
            {
                RoutedEventArgs args = new RoutedEventArgs(RSelectChangedEvent, this);
                RaiseEvent(args);
            };
        }


        static Button_Resolution()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Button_Resolution), new FrameworkPropertyMetadata(typeof(Button_Resolution)));
        }

    }
}
