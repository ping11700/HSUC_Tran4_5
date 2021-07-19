using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Common.Controls
{


    /*
     * 播放选集 按钮
     */

    /// <summary>
    /// Expander_MovieSeries.xaml 的交互逻辑
    /// </summary>
    public partial class Expander_MovieSeries : Expander
    {
        public ICommand ExpandCommand
        {
            get { return (ICommand)GetValue(ExpandCommandProperty); }
            set { SetValue(ExpandCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ExpandCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExpandCommandProperty =
            DependencyProperty.Register("ExpandCommand", typeof(ICommand), typeof(Expander_MovieSeries), new PropertyMetadata(null));



        static Expander_MovieSeries()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Expander_MovieSeries), new FrameworkPropertyMetadata(typeof(Expander_MovieSeries)));
        }

    }
}
