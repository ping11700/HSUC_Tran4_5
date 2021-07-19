using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Common.Controls
{


    /*
     * 播放下一集 按钮
     */

    /// <summary>
    /// Button_PlayNext.xaml 的交互逻辑
    /// </summary>
    public partial class Button_PlayNext : Button
    {
        /// <summary>
        /// 图片源
        /// </summary>
        public ImageSource ImageS
        {
            get { return (ImageSource)GetValue(ImageSProperty); }
            set { SetValue(ImageSProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageS.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSProperty =
            DependencyProperty.Register("ImageS", typeof(ImageSource), typeof(Button_PlayNext), new PropertyMetadata(null));


        /// <summary>
        /// 集数
        /// </summary>
        public string Text1S
        {
            get { return (string)GetValue(Text1SProperty); }
            set { SetValue(Text1SProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextS.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Text1SProperty =
            DependencyProperty.Register("Text1S", typeof(string), typeof(Button_PlayNext), new PropertyMetadata(string.Empty));

        /// <summary>
        /// 描述
        /// </summary>
        public string Text2S
        {
            get { return (string)GetValue(Text2SProperty); }
            set { SetValue(Text2SProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextS.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Text2SProperty =
            DependencyProperty.Register("Text2S", typeof(string), typeof(Button_PlayNext), new PropertyMetadata(string.Empty));



        public bool IsCloseS
        {
            get { return (bool)GetValue(IsCloseSProperty); }
            set { SetValue(IsCloseSProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsCloseS.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCloseSProperty =
            DependencyProperty.Register("IsCloseS", typeof(bool), typeof(Button_PlayNext), new PropertyMetadata(false));




        public bool IsOpenS
        {
            get { return (bool)GetValue(IsOpenSProperty); }
            set { SetValue(IsOpenSProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOpenS.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenSProperty =
            DependencyProperty.Register("IsOpenS", typeof(bool), typeof(Button_PlayNext), new PropertyMetadata(false));





        /// <summary>
        /// 播放下一集
        /// </summary>
        public ICommand PlayCommand
        {
            get { return (ICommand)GetValue(PlayCommandProperty); }
            set { SetValue(PlayCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlayCommandProperty =
            DependencyProperty.Register("PlayCommand", typeof(ICommand), typeof(Button_PlayNext), new PropertyMetadata(null));


        static Button_PlayNext()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Button_PlayNext), new FrameworkPropertyMetadata(typeof(Button_PlayNext)));
        }

    }
}
