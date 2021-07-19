using Common.Resources.CommonResources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Common.Controls
{


    /*
     * 置顶/取消置顶 按钮
     */

    /// <summary>
    /// Button_Fix.xaml 的交互逻辑
    /// </summary>
    public partial class Button_Fix : Button
    {
        /// <summary>
        /// 初始Path
        /// </summary>
        public StreamGeometry BPath
        {
            get { return (StreamGeometry)GetValue(BPathProperty); }
            set { SetValue(BPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Path.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BPathProperty =
            DependencyProperty.Register("BPath", typeof(StreamGeometry), typeof(Button_Fix), new PropertyMetadata(null, BPthCallBack));

        private static void BPthCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bf = d as Button_Fix;

            if (bf._path == null) return;

            bf._path.Fill = (StreamGeometry)e.NewValue == CommonResources.FixedGeometry ?
                                                          CommonResources.Common_Blue01 : CommonResources.Common_White;
        }


        private Path _path;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _path = this.GetTemplateChild("PART_Path") as Path;
            if (_path == null) return;
        }


        static Button_Fix()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Button_Fix), new FrameworkPropertyMetadata(typeof(Button_Fix)));
        }


        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);

            _path.Fill = CommonResources.Common_Blue01;
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);

            _path.Fill = BPath == CommonResources.FixedGeometry ?
                                   CommonResources.Common_Blue01 : CommonResources.Common_White;

        }

    }
}
