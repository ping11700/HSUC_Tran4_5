using System.Windows;
using System.Windows.Controls;

namespace Common.Controls
{
    public class TabItem_FIcon : TabItem
    {



        public string TIFIcon
        {
            get { return (string)GetValue(TIFIconProperty); }
            set { SetValue(TIFIconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TIFIcon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TIFIconProperty =
            DependencyProperty.Register("TIFIcon", typeof(string), typeof(TabItem_FIcon), new PropertyMetadata(string.Empty));





        static TabItem_FIcon()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TabItem_FIcon), new FrameworkPropertyMetadata(typeof(TabItem_FIcon)));
        }


    }
}
