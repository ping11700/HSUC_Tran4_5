using System.Windows;
using System.Windows.Controls;

namespace Common.Controls
{


    /*
     * 视频解码按钮
     */

    /// <summary>
    /// Button_AVDecode.xaml 的交互逻辑
    /// </summary>
    public partial class Button_AVDecode : Button
    {

        /// <summary>
        /// 解码描述
        /// </summary>
        public string DecodeDesc
        {
            get { return (string)GetValue(DecodeDescProperty); }
            set { SetValue(DecodeDescProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DecodeDesc.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DecodeDescProperty =
            DependencyProperty.Register("DecodeDesc", typeof(string), typeof(Button_AVDecode), new PropertyMetadata(string.Empty));




        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(Button_AVDecode), new PropertyMetadata(false));


        static Button_AVDecode()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Button_AVDecode), new FrameworkPropertyMetadata(typeof(Button_AVDecode)));
        }


    }
}
