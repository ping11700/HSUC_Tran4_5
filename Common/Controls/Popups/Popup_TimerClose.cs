using Common.SystemAPI;
using Common.Utils;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Common.Controls
{
    /// <summary>
    /// 定时关闭Popup  用于播放器中Button
    /// </summary>
    public class Popup_TimerClose : Popup
    {

        private Window _owner;

        public Popup_TimerClose()
        {
            this.Loaded += Popup_TimerClose_Loaded;
            this.AllowsTransparency = true;
            this.SnapsToDevicePixels = true;
            this.PopupAnimation = PopupAnimation.Slide;

            //this.MouseEnter += (s, e) => IsOpen = true;
            //this.MouseLeave += (s, e) => IsOpen = false;


        }

        static Popup_TimerClose()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Popup_TimerClose), new FrameworkPropertyMetadata(typeof(Popup_TimerClose)));
        }


        private void Popup_TimerClose_Loaded(object sender, RoutedEventArgs e)
        {
            _owner = VisualHelper<Window>.FindParent(this);

            if (_owner != null)
            {
                _owner.LocationChanged += (s, ea) => UpdateLocation();
                _owner.SizeChanged += (s, ea) => UpdateLocation();
            }

        }



        /// <summary>
        /// Popup随window一起移动
        /// </summary>
        private void UpdateLocation()
        {
            if (this.IsOpen)
            {
                MethodInfo mi = typeof(Popup).GetMethod("UpdatePosition",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                mi?.Invoke(this, null);
            }
        }

        protected override void OnOpened(EventArgs e)
        {
            base.OnOpened(e);

            WindowAPI.SetWinTopMost(-2, this, 0x0010);

        }
    }
}
