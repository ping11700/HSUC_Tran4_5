using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Common.Controls
{

    public class Clock_Animation : Control
    {
        private readonly DispatcherTimer _dispatcherTimer;

        private bool _isDisposed;

        public static readonly DependencyProperty NumberListProperty = DependencyProperty.Register(
            "NumberList", typeof(List<int>), typeof(Clock_Animation), new PropertyMetadata(new List<int> { 0, 0, 0, 0, 0, 0 }));

        public List<int> NumberList
        {
            get => (List<int>)GetValue(NumberListProperty);
            set => SetValue(NumberListProperty, value);
        }

        public static readonly DependencyProperty DisplayTimeProperty = DependencyProperty.Register(
            "DisplayTime", typeof(DateTime), typeof(Clock_Animation), new PropertyMetadata(default(DateTime), OnDisplayTimeChanged));

        private static void OnDisplayTimeChanged(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var ctl = (Clock_Animation)s;
            var v = (DateTime)e.NewValue;

            ctl.NumberList = new List<int>
            {
                v.Hour / 10,
                v.Hour % 10,
                v.Minute / 10,
                v.Minute % 10,
                v.Second / 10,
                v.Second % 10,
            };
        }

        public DateTime DisplayTime
        {
            get => (DateTime)GetValue(DisplayTimeProperty);
            set => SetValue(DisplayTimeProperty, value);
        }

        public Clock_Animation()
        {
            _dispatcherTimer = new DispatcherTimer(DispatcherPriority.Render)
            {
                Interval = TimeSpan.FromMilliseconds(200)
            };

            IsVisibleChanged += Clock_IsVisibleChanged;
        }

        ~Clock_Animation() => Dispose();

        public void Dispose()
        {
            if (_isDisposed) return;

            IsVisibleChanged -= Clock_IsVisibleChanged;
            _dispatcherTimer.Stop();
            _isDisposed = true;

            GC.SuppressFinalize(this);
        }

        private void Clock_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
            {
                _dispatcherTimer.Tick += DispatcherTimer_Tick;
                _dispatcherTimer.Start();
            }
            else
            {
                _dispatcherTimer.Stop();
                _dispatcherTimer.Tick -= DispatcherTimer_Tick;
            }
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e) => DisplayTime = DateTime.Now;
    }
}
