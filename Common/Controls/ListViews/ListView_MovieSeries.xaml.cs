using Common.Utils;
using System.Windows;
using System.Windows.Controls;

namespace Common.Controls
{
    /// <summary>
    /// ListView_MovieSeries.xaml 的交互逻辑
    /// </summary>
    public partial class ListView_MovieSeries : ListView
    {
        public Thickness ItemPadding
        {
            get { return (Thickness)GetValue(ItemPaddingProperty); }
            set { SetValue(ItemPaddingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemPadding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemPaddingProperty =
            DependencyProperty.Register("ItemPadding", typeof(Thickness), typeof(ListView_MovieSeries), new PropertyMetadata(new Thickness(0)));



        static ListView_MovieSeries()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ListView_MovieSeries), new FrameworkPropertyMetadata(typeof(ListView_MovieSeries)));
        }


        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            ScrollViewer sc = VisualHelper<ScrollViewer>.FindChild(this);

            if (sc == null) return;

            sc.ScrollToBottom();

            Items.Refresh();


            if (SelectedIndex == Items.Count - 1)
                sc.ScrollToBottom();
            else
                ScrollIntoView(SelectedItem);
        }

        //protected override Size ArrangeOverride(Size finalSize)
        //{
        //    if (SelectedIndex >= 0)
        //    {
        //        var item = ItemContainerGenerator.ContainerFromIndex(SelectedIndex) as ListViewItem;

        //        finalSize.Height += item.Height;
        //    }


        //    return finalSize;
        //}

    }
}


