using Common.Controls.Utils;
using Common.Utils;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Common.Controls
{

    /// <summary>
    ///     数值选择控件
    /// </summary>
    [TemplatePart(Name = ElementTextBox, Type = typeof(TextBox))]
    [TemplatePart(Name = ElementBorder, Type = typeof(Border))]
    public class NumericUpDown : Control
    {
        #region Constants

        private const string ElementTextBox = "PART_TextBox";
        private const string ElementBorder = "ROOT_Border";

        #endregion Constants

        #region Data

        private TextBox _textBox;
        private Border _border;

        private bool _updateText;

        #endregion Data

        public NumericUpDown()
        {
            UpCommand = new UCCommand(_ =>
            {
                if (IsReadOnly) return;

                Value += Increment;
            });

            DownCommand = new UCCommand(_ =>
            {
                if (IsReadOnly) return;

                Value -= Increment;
            });


            Loaded += (s, e) => OnApplyTemplate();
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);

            if (_textBox != null)
            {
                _textBox?.Focus();
                _textBox.Select(_textBox.Text.Length, 0);
            }
        }

        public override void OnApplyTemplate()
        {
            if (_textBox != null)
            {
                TextCompositionManager.RemovePreviewTextInputHandler(_textBox, PreviewTextInputHandler);
                _textBox.TextChanged -= TextBox_TextChanged;
                _textBox.PreviewKeyDown -= TextBox_PreviewKeyDown;
                _textBox.LostFocus -= TextBox_LostFocus;
            }

            base.OnApplyTemplate();

            _textBox = this.GetTemplateChild("PART_TextBox") as TextBox;
            _border = this.GetTemplateChild("ROOT_Border") as Border;

            if (_textBox != null)
            {
                _textBox.SetBinding(SelectionBrushProperty, new Binding(SelectionBrushProperty.Name) { Source = this });

                _textBox.SetBinding(SelectionOpacityProperty, new Binding(SelectionOpacityProperty.Name) { Source = this });
                _textBox.SetBinding(CaretBrushProperty, new Binding(CaretBrushProperty.Name) { Source = this });

                TextCompositionManager.AddPreviewTextInputHandler(_textBox, PreviewTextInputHandler);
                _textBox.TextChanged += TextBox_TextChanged;
                _textBox.PreviewKeyDown += TextBox_PreviewKeyDown;
                _textBox.LostFocus += TextBox_LostFocus;
                _textBox.Text = CurrentText;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) => UpdateData();

        private void UpdateData()
        {
            if (!VerifyData())
            {
                _updateText = true;
                return;
            }
            if (double.TryParse(_textBox.Text, out var value))
            {
                _updateText = false;
                Value = value;
                _updateText = true;
            }
        }

        private void PreviewTextInputHandler(object sender, TextCompositionEventArgs e) => UpdateData();

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IsError) return;
            _textBox.Text = CurrentText;
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (IsReadOnly) return;

            if (e.Key == Key.Up)
            {
                Value += Increment;
            }
            else if (e.Key == Key.Down)
            {
                Value -= Increment;
            }
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            if (_textBox.IsFocused && !IsReadOnly)
            {
                Value += e.Delta > 0 ? Increment : -Increment;
                e.Handled = true;
            }
        }

        private string CurrentText => string.IsNullOrWhiteSpace(ValueFormat)
            ? DecimalPlaces.HasValue
                ? Value.ToString($"#0.{new string('0', DecimalPlaces.Value)}")
                : Value.ToString()
            : Value.ToString(ValueFormat);



        /// <summary>
        ///     当前值
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(double), typeof(NumericUpDown),
            new FrameworkPropertyMetadata(ValueBoxes.Double0Box, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnValueChanged, CoerceValue), ValidateDataHelper.IsInRangeOfDouble);

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctl = (NumericUpDown)d;
            var v = (double)e.NewValue;
            ctl.SetText();

        }

        private void SetText()
        {
            if (_updateText && _textBox != null)
            {
                _textBox.Text = CurrentText;
                _textBox.Select(_textBox.Text.Length, 0);
            }

        }

        private static object CoerceValue(DependencyObject d, object basevalue)
        {
            var ctl = (NumericUpDown)d;
            var minimum = ctl.Minimum;
            var num = (double)basevalue;
            if (num < minimum)
            {
                ctl.Value = minimum;
                return minimum;
            }
            var maximum = ctl.Maximum;
            if (num > maximum)
            {
                ctl.Value = maximum;
            }
            ctl.SetText();
            return num > maximum ? maximum : num;
        }

        /// <summary>
        ///     当前值
        /// </summary>
        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        /// <summary>
        ///     最大值
        /// </summary>
        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(
            "Maximum", typeof(double), typeof(NumericUpDown), new PropertyMetadata(double.MaxValue, OnMaximumChanged, CoerceMaximum), ValidateDataHelper.IsInRangeOfDouble);

        private static void OnMaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctl = (NumericUpDown)d;
            ctl.CoerceValue(MinimumProperty);
            ctl.CoerceValue(ValueProperty);
        }

        private static object CoerceMaximum(DependencyObject d, object basevalue)
        {
            var minimum = ((NumericUpDown)d).Minimum;
            return (double)basevalue < minimum ? minimum : basevalue;
        }

        /// <summary>
        ///     最大值
        /// </summary>
        public double Maximum
        {
            get => (double)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        /// <summary>
        ///     最小值
        /// </summary>
        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(
            "Minimum", typeof(double), typeof(NumericUpDown), new PropertyMetadata(double.MinValue, OnMinimumChanged, CoerceMinimum), ValidateDataHelper.IsInRangeOfDouble);

        private static void OnMinimumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctl = (NumericUpDown)d;
            ctl.CoerceValue(MaximumProperty);
            ctl.CoerceValue(ValueProperty);
        }

        private static object CoerceMinimum(DependencyObject d, object basevalue)
        {
            var maximum = ((NumericUpDown)d).Maximum;
            return (double)basevalue > maximum ? maximum : basevalue;
        }

        /// <summary>
        ///     最小值
        /// </summary>
        public double Minimum
        {
            get => (double)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        /// <summary>
        ///     指示每单击一下按钮时增加或减少的数量
        /// </summary>
        public static readonly DependencyProperty IncrementProperty = DependencyProperty.Register(
            "Increment", typeof(double), typeof(NumericUpDown), new PropertyMetadata(ValueBoxes.Double1Box));

        /// <summary>
        ///     指示每单击一下按钮时增加或减少的数量
        /// </summary>
        public double Increment
        {
            get => (double)GetValue(IncrementProperty);
            set => SetValue(IncrementProperty, value);
        }

        /// <summary>
        ///     指示要显示的小数位数
        /// </summary>
        public static readonly DependencyProperty DecimalPlacesProperty = DependencyProperty.Register(
            "DecimalPlaces", typeof(int?), typeof(NumericUpDown), new PropertyMetadata(default(int?)));

        /// <summary>
        ///     指示要显示的小数位数
        /// </summary>
        public int? DecimalPlaces
        {
            get => (int?)GetValue(DecimalPlacesProperty);
            set => SetValue(DecimalPlacesProperty, value);
        }

        /// <summary>
        ///     指示要显示的数字的格式
        /// </summary>
        public static readonly DependencyProperty ValueFormatProperty = DependencyProperty.Register(
            "ValueFormat", typeof(string), typeof(NumericUpDown), new PropertyMetadata(default(string)));

        /// <summary>
        ///     指示要显示的数字的格式，这将会覆盖 <see cref="DecimalPlaces"/> 属性
        /// </summary>
        public string ValueFormat
        {
            get => (string)GetValue(ValueFormatProperty);
            set => SetValue(ValueFormatProperty, value);
        }

        /// <summary>
        ///     是否显示上下调值按钮
        /// </summary>
        public static readonly DependencyProperty ShowUpDownButtonProperty = DependencyProperty.Register(
            "ShowUpDownButton", typeof(bool), typeof(NumericUpDown), new PropertyMetadata(ValueBoxes.TrueBox));

        /// <summary>
        ///     是否显示上下调值按钮
        /// </summary>
        public bool ShowUpDownButton
        {
            get => (bool)GetValue(ShowUpDownButtonProperty);
            set => SetValue(ShowUpDownButtonProperty, ValueBoxes.BooleanBox(value));
        }

        /// <summary>
        ///     数据是否错误
        /// </summary>
        public static readonly DependencyProperty IsErrorProperty = DependencyProperty.Register(
            "IsError", typeof(bool), typeof(NumericUpDown), new PropertyMetadata(ValueBoxes.FalseBox));

        public bool IsError
        {
            get => (bool)GetValue(IsErrorProperty);
            set => SetValue(IsErrorProperty, ValueBoxes.BooleanBox(value));
        }

        /// <summary>
        ///     错误提示
        /// </summary>
        public static readonly DependencyProperty ErrorStrProperty = DependencyProperty.Register(
            "ErrorStr", typeof(string), typeof(NumericUpDown), new PropertyMetadata(default(string)));

        public string ErrorStr
        {
            get => (string)GetValue(ErrorStrProperty);
            set => SetValue(ErrorStrProperty, value);
        }


        public ICommand UpCommand
        {
            get { return (ICommand)GetValue(UpCommandProperty); }
            set { SetValue(UpCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UpCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UpCommandProperty =
            DependencyProperty.Register("UpCommand", typeof(ICommand), typeof(NumericUpDown), new PropertyMetadata(null));





        public ICommand DownCommand
        {
            get { return (ICommand)GetValue(DownCommandProperty); }
            set { SetValue(DownCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DownCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DownCommandProperty =
            DependencyProperty.Register("DownCommand", typeof(ICommand), typeof(NumericUpDown), new PropertyMetadata(null));





        public static readonly DependencyPropertyKey RegexTypePropertyKey =
            DependencyProperty.RegisterReadOnly("RegexType", typeof(RegexType), typeof(NumericUpDown),
                new PropertyMetadata(default(RegexType)));



        /// <summary>
        ///     文本类型
        /// </summary>
        public static readonly DependencyProperty RegexTypeProperty = RegexTypePropertyKey.DependencyProperty;

        public RegexType RegexType
        {
            get => (RegexType)GetValue(RegexTypeProperty);
            set => SetValue(RegexTypeProperty, value);
        }



        /// <summary>
        ///     标识 IsReadOnly 依赖属性。
        /// </summary>
        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register(
            "IsReadOnly", typeof(bool), typeof(NumericUpDown), new PropertyMetadata(ValueBoxes.FalseBox));

        /// <summary>
        ///     获取或设置一个值，该值指示NumericUpDown是否只读。
        /// </summary>
        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, ValueBoxes.BooleanBox(value));
        }

        public static readonly DependencyProperty SelectionBrushProperty =
            TextBoxBase.SelectionBrushProperty.AddOwner(typeof(NumericUpDown));

        public Brush SelectionBrush
        {
            get => (Brush)GetValue(SelectionBrushProperty);
            set => SetValue(SelectionBrushProperty, value);
        }

        public static readonly DependencyProperty SelectionOpacityProperty =
            TextBoxBase.SelectionOpacityProperty.AddOwner(typeof(NumericUpDown));

        public double SelectionOpacity
        {
            get => (double)GetValue(SelectionOpacityProperty);
            set => SetValue(SelectionOpacityProperty, value);
        }

        public static readonly DependencyProperty CaretBrushProperty =
            TextBoxBase.CaretBrushProperty.AddOwner(typeof(NumericUpDown));

        public Brush CaretBrush
        {
            get => (Brush)GetValue(CaretBrushProperty);
            set => SetValue(CaretBrushProperty, value);
        }


        public virtual bool VerifyData()
        {
            if (!string.IsNullOrEmpty(_textBox.Text))
            {
                if (double.TryParse(_textBox.Text, out var value))
                {
                    if (value < Minimum || value > Maximum)
                        IsError = true;
                    else
                        IsError = false;
                }
                else
                    IsError = true;
            }
            else
                IsError = false;

            return IsError;
        }

    }
}
