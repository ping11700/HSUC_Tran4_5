using Common.Controls.Utils;
using Common.Utils;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace Common.Controls
{
    [TemplatePart(Name = ElementPasswordBox, Type = typeof(PasswordBox_Eye))]
    [TemplatePart(Name = ElementTextBox, Type = typeof(TextBox))]
    [TemplatePart(Name = ElementTextBlock, Type = typeof(TextBlock))]
    public class PasswordBox_Eye : Control
    {

        private const string ElementTextBlock = "PART_TextBlock";

        private const string ElementTextBox = "PART_TextBox";

        private const string ElementPasswordBox = "PART_PasswordBox";

        private SecureString _password;

        private TextBlock _textBlock;

        private TextBox _textBox;

        /// <summary>
        ///     掩码字符
        /// </summary>
        public static readonly DependencyProperty PasswordCharProperty = PasswordBox.PasswordCharProperty.AddOwner(typeof(PasswordBox_Eye),
                new FrameworkPropertyMetadata('●'));

        public char PasswordChar
        {
            get => (char)GetValue(PasswordCharProperty);
            set => SetValue(PasswordCharProperty, value);
        }

        /// <summary>
        ///     数据是否错误
        /// </summary>
        public static readonly DependencyProperty IsErrorProperty = DependencyProperty.Register(
            "IsError", typeof(bool), typeof(PasswordBox_Eye), new PropertyMetadata(ValueBoxes.FalseBox));

        public bool IsError
        {
            get => (bool)GetValue(IsErrorProperty);
            set => SetValue(IsErrorProperty, ValueBoxes.BooleanBox(value));
        }

        /// <summary>
        ///     错误提示
        /// </summary>
        public static readonly DependencyProperty ErrorStrProperty = DependencyProperty.Register(
            "ErrorStr", typeof(string), typeof(PasswordBox_Eye), new PropertyMetadata(default(string)));

        public string ErrorStr
        {
            get => (string)GetValue(ErrorStrProperty);
            set => SetValue(ErrorStrProperty, value);
        }




        public RegexType RegexType
        {
            get { return (RegexType)GetValue(RegexTypeProperty); }
            set { SetValue(RegexTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RegexType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RegexTypeProperty =
            DependencyProperty.Register("RegexType", typeof(RegexType), typeof(PasswordBox_Eye), new PropertyMetadata(default(RegexType)));



        public static readonly DependencyProperty ShowEyeButtonProperty = DependencyProperty.Register(
            "ShowEyeButton", typeof(bool), typeof(PasswordBox_Eye), new PropertyMetadata(ValueBoxes.FalseBox));

        public bool ShowEyeButton
        {
            get => (bool)GetValue(ShowEyeButtonProperty);
            set => SetValue(ShowEyeButtonProperty, ValueBoxes.BooleanBox(value));
        }

        public static readonly DependencyProperty ShowPasswordProperty = DependencyProperty.Register(
            "ShowPassword", typeof(bool), typeof(PasswordBox_Eye),
            new PropertyMetadata(ValueBoxes.FalseBox, OnShowPasswordChanged));

        public bool ShowPassword
        {
            get => (bool)GetValue(ShowPasswordProperty);
            set => SetValue(ShowPasswordProperty, ValueBoxes.BooleanBox(value));
        }

        public static readonly DependencyProperty IsSafeEnabledProperty = DependencyProperty.Register(
            "IsSafeEnabled", typeof(bool), typeof(PasswordBox_Eye), new PropertyMetadata(ValueBoxes.TrueBox, OnIsSafeEnabledChanged));

        private static void OnIsSafeEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var p = (PasswordBox_Eye)d;
            p.SetCurrentValue(UnsafePasswordProperty, !(bool)e.NewValue ? p.Password : string.Empty);
        }

        public bool IsSafeEnabled
        {
            get => (bool)GetValue(IsSafeEnabledProperty);
            set => SetValue(IsSafeEnabledProperty, ValueBoxes.BooleanBox(value));
        }

        public static readonly DependencyProperty UnsafePasswordProperty = DependencyProperty.Register(
            "UnsafePassword", typeof(string), typeof(PasswordBox_Eye), new FrameworkPropertyMetadata(default(string),
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnUnsafePasswordChanged));

        private static void OnUnsafePasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var p = (PasswordBox_Eye)d;
            if (!p.IsSafeEnabled)
            {
                p.Password = e.NewValue != null ? e.NewValue.ToString() : string.Empty;
            }
        }

        public string UnsafePassword
        {
            get => (string)GetValue(UnsafePasswordProperty);
            set => SetValue(UnsafePasswordProperty, value);
        }

        public static readonly DependencyProperty MaxLengthProperty =
            TextBox.MaxLengthProperty.AddOwner(typeof(PasswordBox_Eye));

        public int MaxLength
        {
            get => (int)GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }

        public static readonly DependencyProperty SelectionBrushProperty =
            TextBoxBase.SelectionBrushProperty.AddOwner(typeof(PasswordBox_Eye));

        public Brush SelectionBrush
        {
            get => (Brush)GetValue(SelectionBrushProperty);
            set => SetValue(SelectionBrushProperty, value);
        }

        public static readonly DependencyProperty SelectionOpacityProperty =
            TextBoxBase.SelectionOpacityProperty.AddOwner(typeof(PasswordBox_Eye));

        public double SelectionOpacity
        {
            get => (double)GetValue(SelectionOpacityProperty);
            set => SetValue(SelectionOpacityProperty, value);
        }

        public static readonly DependencyProperty CaretBrushProperty =
            TextBoxBase.CaretBrushProperty.AddOwner(typeof(PasswordBox_Eye));

        public Brush CaretBrush
        {
            get => (Brush)GetValue(CaretBrushProperty);
            set => SetValue(CaretBrushProperty, value);
        }

#if !NET40

        public static readonly DependencyProperty IsSelectionActiveProperty =
            TextBoxBase.IsSelectionActiveProperty.AddOwner(typeof(PasswordBox_Eye));

        public bool IsSelectionActive => ActualPasswordBox != null && (bool)ActualPasswordBox.GetValue(IsSelectionActiveProperty);

#endif


        public PasswordBox ActualPasswordBox { get; set; }

        [DefaultValue("")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Password
        {
            get
            {
                if (ShowEyeButton && ShowPassword)
                {
                    return _textBox.Text;
                }
                return ActualPasswordBox?.Password;
            }
            set
            {
                if (ActualPasswordBox == null)
                {
                    _password = new SecureString();
                    value ??= string.Empty;
                    foreach (var item in value)
                        _password.AppendChar(item);

                    return;
                }

                if (Equals(ActualPasswordBox.Password, value)) return;
                ActualPasswordBox.Password = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SecureString SecurePassword => ActualPasswordBox?.SecurePassword;


        public virtual bool VerifyData(bool isPassword)
        {

            _textBlock.Visibility = (string.IsNullOrEmpty(_textBox.Text) && string.IsNullOrEmpty(ActualPasswordBox.Password)) ?
                                     Visibility.Visible : Visibility.Collapsed;


            if (RegexType != RegexType.Common)
            {
                if (isPassword)
                {
                    IsError = string.IsNullOrEmpty(ActualPasswordBox.Password) ? true :
                   !ActualPasswordBox.Password.IsKindOf(RegexType);
                }
                else
                {
                    IsError = string.IsNullOrEmpty(_textBox.Text) ? true :
                  !_textBox.Text.IsKindOf(RegexType);
                }
                
            }

            else
                IsError = false;
         

            return IsError;

        }

        private static void OnShowPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctl = (PasswordBox_Eye)d;
            if (!ctl.ShowEyeButton) return;
            if ((bool)e.NewValue)
            {
                ctl._textBox.Text = ctl.ActualPasswordBox.Password;
                ctl._textBox.Select(string.IsNullOrEmpty(ctl._textBox.Text) ? 0 : ctl._textBox.Text.Length, 0);
            }
            else
            {
                ctl.ActualPasswordBox.Password = ctl._textBox.Text;
                ctl._textBox.Clear();
            }
        }

        public override void OnApplyTemplate()
        {
            if (ActualPasswordBox != null)
                ActualPasswordBox.PasswordChanged -= PasswordBox_PasswordChanged;

            if (_textBox != null)
                _textBox.TextChanged -= TextBox_TextChanged;

            base.OnApplyTemplate();

            ActualPasswordBox = GetTemplateChild(ElementPasswordBox) as PasswordBox;
            _textBox = GetTemplateChild(ElementTextBox) as TextBox;
            _textBlock = GetTemplateChild(ElementTextBlock) as TextBlock;


            if (ActualPasswordBox != null)
            {
                ActualPasswordBox.PasswordChanged += PasswordBox_PasswordChanged;
                ActualPasswordBox.SetBinding(PasswordBox.MaxLengthProperty, new Binding(MaxLengthProperty.Name) { Source = this });
                ActualPasswordBox.SetBinding(PasswordBox.SelectionBrushProperty, new Binding(SelectionBrushProperty.Name) { Source = this });

                ActualPasswordBox.SetBinding(PasswordBox.SelectionOpacityProperty, new Binding(SelectionOpacityProperty.Name) { Source = this });
                ActualPasswordBox.SetBinding(PasswordBox.CaretBrushProperty, new Binding(CaretBrushProperty.Name) { Source = this });

                if (_password != null)
                {
                    if (_password.Length > 0)
                    {
                        var valuePtr = IntPtr.Zero;
                        try
                        {
                            valuePtr = Marshal.SecureStringToGlobalAllocUnicode(_password);
                            ActualPasswordBox.Password = Marshal.PtrToStringUni(valuePtr) ?? throw new InvalidOperationException();
                        }
                        finally
                        {
                            Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
                            _password.Clear();
                        }
                    }
                }
            }

            if (_textBox != null)
            {
                _textBox.TextChanged += TextBox_TextChanged;
            }
        }

        public void Paste()
        {
            ActualPasswordBox.Paste();
            if (ShowEyeButton && ShowPassword)
            {
                _textBox.Text = ActualPasswordBox.Password;
            }
        }

        public void SelectAll()
        {
            ActualPasswordBox.SelectAll();
            if (ShowEyeButton && ShowPassword)
            {
                _textBox.SelectAll();
            }
        }

        public void Clear()
        {
            ActualPasswordBox.Clear();
            _textBox.Clear();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (VerifyData(true) && !IsSafeEnabled)
            {
                SetCurrentValue(UnsafePasswordProperty, ActualPasswordBox.Password);

                if (ShowPassword)
                {
                    _textBox.Text = ActualPasswordBox.Password;
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            VerifyData(false);

            if (!IsSafeEnabled && ShowPassword)
            {
                Password = _textBox.Text;
                SetCurrentValue(UnsafePasswordProperty, Password);
            }
        }
    }
}
