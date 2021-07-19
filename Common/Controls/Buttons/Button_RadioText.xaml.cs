using System.Windows;
using System.Windows.Controls;

namespace Common.Controls
{

    /// <summary>
    /// Button_RadioText.xaml 的交互逻辑
    /// </summary>
    public partial class Button_RadioText : RadioButton
    {


        public string RText
        {
            get { return (string)GetValue(RTextProperty); }
            set { SetValue(RTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RTextProperty =
            DependencyProperty.Register("RText", typeof(string), typeof(Button_RadioText), new PropertyMetadata(string.Empty));



        static Button_RadioText()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Button_RadioText), new FrameworkPropertyMetadata(typeof(Button_RadioText)));
        }


    }
}
