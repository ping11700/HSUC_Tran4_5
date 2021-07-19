using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Common.Controls
{


    /*
     * 播放音量Slider 
     */

    /// <summary>
    /// Slider_PlayerSound.xaml 的交互逻辑
    /// </summary>
    public partial class Slider_PlayerSound : Slider
    {


        public double RepeatBtnHeight
        {
            get { return (double)GetValue(RepeatBtnHeightProperty); }
            set { SetValue(RepeatBtnHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RepeatBtnHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RepeatBtnHeightProperty =
            DependencyProperty.Register("RepeatBtnHeight", typeof(double), typeof(Slider_PlayerSound), new PropertyMetadata(0.0));



        public double RepeatBtnWidth
        {
            get { return (double)GetValue(RepeatBtnWidthProperty); }
            set { SetValue(RepeatBtnWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RepeatBtnWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RepeatBtnWidthProperty =
            DependencyProperty.Register("RepeatBtnWidth", typeof(double), typeof(Slider_PlayerSound), new PropertyMetadata(0.0));


        public Brush IncreaseRepeatBtnBackground
        {
            get { return (Brush)GetValue(IncreaseRepeatBtnBackgroundProperty); }
            set { SetValue(IncreaseRepeatBtnBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IncreaseRepeatBtnBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IncreaseRepeatBtnBackgroundProperty =
            DependencyProperty.Register("IncreaseRepeatBtnBackground", typeof(Brush), typeof(Slider_PlayerSound), new PropertyMetadata(null));


        /// <summary>
        /// 未播放进度条透明度
        /// </summary>
        public double IncreaseRepeatBtnOpacity
        {
            get { return (double)GetValue(IncreaseRepeatBtnOpacityProperty); }
            set { SetValue(IncreaseRepeatBtnOpacityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IncreaseRepeatBtnOpacity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IncreaseRepeatBtnOpacityProperty =
            DependencyProperty.Register("IncreaseRepeatBtnOpacity", typeof(double), typeof(Slider_PlayerSound), new PropertyMetadata(1.0));




        public Brush DecreaseRepeatBtnBackground
        {
            get { return (Brush)GetValue(DecreaseRepeatBtnBackgroundProperty); }
            set { SetValue(DecreaseRepeatBtnBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DecreaseRepeatBtnBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DecreaseRepeatBtnBackgroundProperty =
            DependencyProperty.Register("DecreaseRepeatBtnBackground", typeof(Brush), typeof(Slider_PlayerSound), new PropertyMetadata(null));



        public double ThumbHeight
        {
            get { return (double)GetValue(ThumbHeightProperty); }
            set { SetValue(ThumbHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ThumbHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThumbHeightProperty =
            DependencyProperty.Register("ThumbHeight", typeof(double), typeof(Slider_PlayerSound), new PropertyMetadata(0.0));



        public double ThumbWidth
        {
            get { return (double)GetValue(ThumbWidthProperty); }
            set { SetValue(ThumbWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ThumbWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThumbWidthProperty =
            DependencyProperty.Register("ThumbWidth", typeof(double), typeof(Slider_PlayerSound), new PropertyMetadata(0.0));



        static Slider_PlayerSound()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Slider_PlayerSound), new FrameworkPropertyMetadata(typeof(Slider_PlayerSound)));
        }

    }
}
