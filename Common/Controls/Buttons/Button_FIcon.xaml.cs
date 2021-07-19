using System.Windows;
using System.Windows.Controls;

namespace Common.Controls
{
    /// <summary>
    /// Button_FIcon.xaml 的交互逻辑
    /// </summary>
    public partial class Button_FIcon : Button
    {




        public string BFIcon
        {
            get { return (string)GetValue(BFIconProperty); }
            set { SetValue(BFIconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BFIcon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BFIconProperty =
            DependencyProperty.Register("BFIcon", typeof(string), typeof(Button_FIcon), new PropertyMetadata(string.Empty));




        static Button_FIcon()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Button_FIcon), new FrameworkPropertyMetadata(typeof(Button_FIcon)));
        }
    }
}
