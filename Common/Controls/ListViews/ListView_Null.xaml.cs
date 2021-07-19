using System.Windows;
using System.Windows.Controls;

namespace Common.Controls
{
    /// <summary>
    /// ListView_Null.xaml 的交互逻辑
    /// </summary>
    public partial class ListView_Null : ListView
    {

        public Orientation Orientation_LV
        {
            get { return (Orientation)GetValue(Orientation_LVProperty); }
            set { SetValue(Orientation_LVProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientation_LV.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Orientation_LVProperty =
            DependencyProperty.Register("Orientation_LV", typeof(Orientation), typeof(ListView_Null), new PropertyMetadata(Orientation.Horizontal));


        public Thickness ItemPadding
        {
            get { return (Thickness)GetValue(ItemPaddingProperty); }
            set { SetValue(ItemPaddingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemPadding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemPaddingProperty =
            DependencyProperty.Register("ItemPadding", typeof(Thickness), typeof(ListView_Null), new PropertyMetadata(new Thickness(0)));


        static ListView_Null()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ListView_Null), new FrameworkPropertyMetadata(typeof(ListView_Null)));
        }


    }
}
