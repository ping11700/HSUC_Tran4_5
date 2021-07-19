using Common.Resources.CommonResources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Common.Controls
{


    /*
     * Path按钮
     */

    /// <summary>
    /// Button_Path.xaml 的交互逻辑
    /// </summary>
    public partial class Button_Path : Button
    {
        /// <summary>
        /// 初始Path
        /// </summary>
        public StreamGeometry BPath
        {
            get { return (StreamGeometry)GetValue(BPathProperty); }
            set { SetValue(BPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Path.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BPathProperty =
            DependencyProperty.Register("BPath", typeof(StreamGeometry), typeof(Button_Path), new PropertyMetadata(null));




        /// <summary>
        /// Color
        /// </summary>
        public Brush BColor
        {
            get { return (Brush)GetValue(BColorProperty); }
            set { SetValue(BColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BColorProperty =
            DependencyProperty.Register("BColor", typeof(Brush), typeof(Button_Path), new PropertyMetadata(CommonResources.Common_Blue01));


        static Button_Path()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Button_Path), new FrameworkPropertyMetadata(typeof(Button_Path)));
        }

    }
}
