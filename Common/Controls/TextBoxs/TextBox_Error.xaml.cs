using Common.Controls.Utils;
using Common.Utils;
using System.Windows;
using System.Windows.Controls;

namespace Common.Controls
{
    public class TextBox_Error : TextBox
    {

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            VerifyData();
        }

        /// <summary>
        ///     数据是否错误
        /// </summary>
        public static readonly DependencyProperty IsErrorProperty = DependencyProperty.Register(
            "IsError", typeof(bool), typeof(TextBox_Error), new PropertyMetadata(ValueBoxes.FalseBox));

        public bool IsError
        {
            get => (bool)GetValue(IsErrorProperty);
            set => SetValue(IsErrorProperty, ValueBoxes.BooleanBox(value));
        }

        /// <summary>
        ///     错误提示
        /// </summary>
        public static readonly DependencyProperty ErrorStrProperty = DependencyProperty.Register(
            "ErrorStr", typeof(string), typeof(TextBox_Error), new PropertyMetadata(default(string)));

        public string ErrorStr
        {
            get => (string)GetValue(ErrorStrProperty);
            set => SetValue(ErrorStrProperty, value);
        }

        /// <summary>
        ///     文本类型
        /// </summary>
        public static readonly DependencyProperty RegexTypeProperty = DependencyProperty.Register(
            "RegexType", typeof(RegexType), typeof(TextBox_Error), new PropertyMetadata(default(RegexType)));

        public RegexType RegexType
        {
            get => (RegexType)GetValue(RegexTypeProperty);
            set => SetValue(RegexTypeProperty, value);
        }

        /// <summary>
        ///     是否显示清除按钮
        /// </summary>
        public static readonly DependencyProperty ShowClearButtonProperty = DependencyProperty.Register(
            "ShowClearButton", typeof(bool), typeof(TextBox_Error), new PropertyMetadata(ValueBoxes.FalseBox));

        public bool ShowClearButton
        {
            get => (bool)GetValue(ShowClearButtonProperty);
            set => SetValue(ShowClearButtonProperty, ValueBoxes.BooleanBox(value));
        }


        private Border _border;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _border = this.GetTemplateChild("ROOT_Border") as Border;
            if (_border == null) return;
        }


        static TextBox_Error()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBox_Error), new FrameworkPropertyMetadata(typeof(TextBox_Error)));
        }



        public virtual void VerifyData()
        {

            if (!string.IsNullOrEmpty(Text))
            {
                if (RegexType != RegexType.Common)
                    IsError = !Text.IsKindOf(RegexType);
                else
                    IsError = false;
            }
            else
                IsError = false;
        }


    }
}
