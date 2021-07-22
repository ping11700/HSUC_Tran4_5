using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Common.Controls
{
    /// <summary>
    /// Button_FIcon.xaml 的交互逻辑
    /// </summary>
    public partial class Button_FIcon : Button
    {




        public Orientation Orien
        {
            get { return (Orientation)GetValue(OrienProperty); }
            set { SetValue(OrienProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orien.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrienProperty =
            DependencyProperty.Register("Orien", typeof(Orientation), typeof(Button_FIcon), new PropertyMetadata(Orientation.Horizontal));




        public Brush TextForeground
        {
            get { return (Brush)GetValue(TextForegroundProperty); }
            set { SetValue(TextForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextForegroundProperty =
            DependencyProperty.Register("TextForeground", typeof(Brush), typeof(Button_FIcon), new PropertyMetadata(default(Brush)));









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
