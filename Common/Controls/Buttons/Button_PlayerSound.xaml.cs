using Common.Resources.CommonResources;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Common.Controls
{


    /*
     * 播放器音量 按钮
     */

    /// <summary>
    /// Button_PlayerSound.xaml 的交互逻辑
    /// </summary>
    public partial class Button_PlayerSound : Button
    {


        public bool IsCloseS
        {
            get { return (bool)GetValue(IsCloseSProperty); }
            set { SetValue(IsCloseSProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsCloseS.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCloseSProperty =
            DependencyProperty.Register("IsCloseS", typeof(bool), typeof(Button_PlayerSound), new PropertyMetadata(false));


        /// <summary>
        /// 音量
        /// </summary>
        public int SoundVolume
        {
            get { return (int)GetValue(SoundVolumeProperty); }
            set { SetValue(SoundVolumeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SoundVolume.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SoundVolumeProperty =
            DependencyProperty.Register("SoundVolume", typeof(int), typeof(Button_PlayerSound), new PropertyMetadata(0, SoundVolumeCallBack));

        private static void SoundVolumeCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ower = d as Button_PlayerSound;
            if (ower._pathBtn != null)
            {
                ower._pathBtn.BPath = (int)e.NewValue <= 0 ?
                                      CommonResources.NoVolumeGeometry : (int)e.NewValue <= 30 ?
                                      CommonResources.MinVolumeGeometry : (int)e.NewValue < 70 ?
                                      CommonResources.NormalVolumeGeometry : CommonResources.MaxVolumeGeometry;
            }

        }




        /// <summary>
        /// 音量改变事件
        /// </summary>
        public static readonly RoutedEvent SoundVolumeChangedEvent = EventManager.RegisterRoutedEvent("SoundVolumeChangedEvent", RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventArgs<object>), typeof(Button_PlayerSound));

        public event RoutedEventHandler SoundVolumeChanged
        {
            //将路由事件添加路由事件处理程序
            add { AddHandler(SoundVolumeChangedEvent, value); }
            //从路由事件处理程序中移除路由事件
            remove { RemoveHandler(SoundVolumeChangedEvent, value); }
        }


        private Button_Path _pathBtn;
        private Slider _slider;


        private bool isRecordSound = true;
        private int sounRecord;
        private int preSoundVolume;

        private readonly int MaxSoundVolume = 100;


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _pathBtn = this.GetTemplateChild("PART_PathBtn") as Button_Path;
            _slider = this.GetTemplateChild("PART_sound_slider") as Slider;

            if (_pathBtn == null || _slider == null) return;

            _pathBtn.Click += _pathBtn_Click;

            _pathBtn.BPath = SoundVolume <= 0 ?
                                            CommonResources.NoVolumeGeometry : SoundVolume <= 30 ?
                                            CommonResources.MinVolumeGeometry : SoundVolume < 70 ?
                                            CommonResources.NormalVolumeGeometry : CommonResources.MaxVolumeGeometry;


            _slider.ValueChanged += _slider_ValueChanged;


            preSoundVolume = sounRecord = SoundVolume;
        }



        static Button_PlayerSound()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Button_PlayerSound), new FrameworkPropertyMetadata(typeof(Button_PlayerSound)));
        }


        public Button_PlayerSound()
        {

            this.MouseWheel += SoundPopupButton_MouseWheel;
        }


        private void _slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (isRecordSound)
                sounRecord = SoundVolume;

            isRecordSound = true;

            RoutedEventArgs args = new RoutedEventArgs(SoundVolumeChangedEvent, this);
            RaiseEvent(args);
        }



        private void SoundPopupButton_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            preSoundVolume += (int)Math.Floor(e.Delta * 0.01);

            SoundVolume = preSoundVolume <= 0 ?
                                         0 : preSoundVolume >= MaxSoundVolume ?
                                         MaxSoundVolume : preSoundVolume;

            preSoundVolume = SoundVolume;
        }


        private void _pathBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SoundVolume > 0)
            {
                isRecordSound = false;

                SoundVolume = 0;
            }
            else
            {
                SoundVolume = sounRecord <= 0 ? 30 : sounRecord;
            }

            preSoundVolume = SoundVolume;
        }
    }
}
