using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Common.Controls
{
    [TemplateVisualState(Name = "Large", GroupName = "SizeStates")]
    [TemplateVisualState(Name = "Small", GroupName = "SizeStates")]
    [TemplateVisualState(Name = "InActive", GroupName = "ActiveStates")]
    [TemplateVisualState(Name = "Active", GroupName = "ActiveStates")]
    public class ProgressRing : Control
    {
        private List<Action> _deferredActions = new List<Action>();

        static ProgressRing()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressRing), new FrameworkPropertyMetadata(typeof(ProgressRing)));

            VisibilityProperty.OverrideMetadata(typeof(ProgressRing),
                                                new FrameworkPropertyMetadata(
                                                    new PropertyChangedCallback(
                                                        (ringObject, e) =>
                                                        {
                                                            if (e.NewValue != e.OldValue)
                                                            {
                                                                var ring = (ProgressRing)ringObject;
                                                                //auto set IsActive to false if we're hiding it.
                                                                if ((Visibility)e.NewValue != Visibility.Visible)
                                                                {
                                                                    //sets the value without overriding it's binding (if any).
                                                                    ring.SetCurrentValue(ProgressRing.IsActiveProperty, false);
                                                                }
                                                                else
                                                                {
                                                                    // #1105 don't forget to re-activate
                                                                    ring.IsActive = true;
                                                                }
                                                            }
                                                        })));
        }

        public ProgressRing()
        {
            SizeChanged += OnSizeChanged;
        }

        public double MaxSideLength
        {
            get { return (double)GetValue(MaxSideLengthProperty); }
            private set { SetValue(MaxSideLengthProperty, value); }
        }
        public static readonly DependencyProperty MaxSideLengthProperty =
            DependencyProperty.Register("MaxSideLength", typeof(double), typeof(ProgressRing), new PropertyMetadata(default(double)));

        public double EllipseDiameter
        {
            get { return (double)GetValue(EllipseDiameterProperty); }
            private set { SetValue(EllipseDiameterProperty, value); }
        }
        public static readonly DependencyProperty EllipseDiameterProperty =
            DependencyProperty.Register("EllipseDiameter", typeof(double), typeof(ProgressRing), new PropertyMetadata(default(double)));

        public Thickness EllipseOffset
        {
            get { return (Thickness)GetValue(EllipseOffsetProperty); }
            private set { SetValue(EllipseOffsetProperty, value); }
        }
        public static readonly DependencyProperty EllipseOffsetProperty =
            DependencyProperty.Register("EllipseOffset", typeof(Thickness), typeof(ProgressRing), new PropertyMetadata(default(Thickness)));

        public double BindableWidth
        {
            get { return (double)GetValue(BindableWidthProperty); }
            private set { SetValue(BindableWidthProperty, value); }
        }
        public static readonly DependencyProperty BindableWidthProperty =
            DependencyProperty.Register("BindableWidth", typeof(double), typeof(ProgressRing), new PropertyMetadata(default(double), BindableWidthCallback));

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(ProgressRing), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, IsActiveChanged));

        public bool IsLarge
        {
            get { return (bool)GetValue(IsLargeProperty); }
            set { SetValue(IsLargeProperty, value); }
        }
        public static readonly DependencyProperty IsLargeProperty =
            DependencyProperty.Register("IsLarge", typeof(bool), typeof(ProgressRing), new PropertyMetadata(true, IsLargeChangedCallback));



        private static void BindableWidthCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var ring = dependencyObject as ProgressRing;
            if (ring == null)
                return;

            var action = new Action(() =>
            {
                ring.SetEllipseDiameter(
                    (double)dependencyPropertyChangedEventArgs.NewValue);
                ring.SetEllipseOffset(
                    (double)dependencyPropertyChangedEventArgs.NewValue);
                ring.SetMaxSideLength(
                    (double)dependencyPropertyChangedEventArgs.NewValue);
            });

            if (ring._deferredActions != null)
                ring._deferredActions.Add(action);
            else
                action();
        }

        private void SetMaxSideLength(double width)
        {
            MaxSideLength = width <= 20 ? 20 : width;
        }

        private void SetEllipseDiameter(double width)
        {
            EllipseDiameter = width / 8;
        }

        private void SetEllipseOffset(double width)
        {
            EllipseOffset = new Thickness(0, width / 2, 0, 0);
        }

        private static void IsLargeChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var ring = dependencyObject as ProgressRing;
            if (ring == null)
                return;

            ring.UpdateLargeState();
        }

        private void UpdateLargeState()
        {
            Action action;

            if (IsLarge)
                action = () => VisualStateManager.GoToState(this, "Large", true);
            else
                action = () => VisualStateManager.GoToState(this, "Small", true);

            if (_deferredActions != null)
                _deferredActions.Add(action);

            else
                action();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
        {
            BindableWidth = ActualWidth;
        }

        private static void IsActiveChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var ring = dependencyObject as ProgressRing;
            if (ring == null)
                return;

            ring.UpdateActiveState();
        }

        private void UpdateActiveState()
        {
            Action action;

            if (IsActive)
                action = () => VisualStateManager.GoToState(this, "Active", true);
            else
                action = () => VisualStateManager.GoToState(this, "InActive", true);

            if (_deferredActions != null)
                _deferredActions.Add(action);

            else
                action();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UpdateLargeState();
            UpdateActiveState();
            if (_deferredActions != null)
                foreach (var action in _deferredActions)
                    action();
            _deferredActions = null;
        }
    }
}
