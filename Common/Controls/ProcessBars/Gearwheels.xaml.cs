using System.Windows;
using System.Windows.Controls;

namespace Common.Controls
{
    /// <summary>
    /// Gearwheels.xaml 的交互逻辑
    /// </summary>
    public partial class Gearwheels : UserControl
    {
        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(Gearwheels), new PropertyMetadata(false));



        static Gearwheels()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Gearwheels), new FrameworkPropertyMetadata(typeof(Gearwheels)));
        }
    }
}
