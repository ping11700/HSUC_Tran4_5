using System.Windows;
using System.Windows.Controls;

namespace Common.Controls
{


    /*
     * 播放选集 按钮
     */

    /// <summary>
    /// Button_Text.xaml 的交互逻辑
    /// </summary>
    public partial class Button_Text : Button
    {
        public string BText
        {
            get { return (string)GetValue(BTextProperty); }
            set { SetValue(BTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BTextProperty =
            DependencyProperty.Register("BText", typeof(string), typeof(Button_Text), new PropertyMetadata(string.Empty));




        public double BFontSize
        {
            get { return (double)GetValue(BFontSizeProperty); }
            set { SetValue(BFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BFontSizeProperty =
            DependencyProperty.Register("BFontSize", typeof(double), typeof(Button_Text), new PropertyMetadata(0.0));




        static Button_Text()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Button_Text), new FrameworkPropertyMetadata(typeof(Button_Text)));
        }

    }
}
