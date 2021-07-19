using Common.Resources.CommonResources;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Common.Controls
{

    /// <summary>
    /// Head 可以滚动的TabControl
    /// </summary>
    public class TabControl_Scrollable : TabControl
    {
        private const int ScrollStep = 25;

        private Button tabLeftButton;
        private Button tabRightButton;
        private ScrollViewer tabScrollViewer;
        private Panel tabPanelTop;


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.tabLeftButton = GetTemplateChild("Forward_Btn") as Button;
            this.tabRightButton = GetTemplateChild("Back_Btn") as Button;
            this.tabScrollViewer = GetTemplateChild("TabScrollViewerTop") as ScrollViewer;
            this.tabPanelTop = GetTemplateChild("HeaderPanel") as Panel;

            if (tabLeftButton == null || tabRightButton == null || tabScrollViewer == null || tabPanelTop == null) return;

            if (this.tabLeftButton != null)
                this.tabLeftButton.Click += tabLeftButton_Click;

            if (this.tabRightButton != null)
                this.tabRightButton.Click += tabRightButton_Click;

            this.tabScrollViewer.Loaded += (s, e) => this.UpdateScrollButtonsAvailability();
            this.tabScrollViewer.ScrollChanged += (s, e) => this.UpdateScrollButtonsAvailability();

            this.SelectionChanged += (s, e) => this.ScrollToSelectedItem();
            this.Loaded += (s, e) => this.ScrollToSelectedItem();

        }



        static TabControl_Scrollable()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TabControl_Scrollable), new FrameworkPropertyMetadata(typeof(TabControl_Scrollable)));
        }

        #region Add item functionality



        #endregion

        #region Scrollable tabs


        /// <summary>
        /// 右翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabRightButton_Click(object sender, RoutedEventArgs e)
        {
            if (null != this.tabScrollViewer && null != this.tabPanelTop)
            {
                tabScrollViewer.LineRight();
            }

        }


        /// <summary>
        /// 左翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabLeftButton_Click(object sender, RoutedEventArgs e)
        {
            if (null != this.tabScrollViewer)
            {
                tabScrollViewer.LineLeft();
            }
        }

        /// <summary>
        /// Change visibility and avalability of buttons if it is necessary
        /// </summary>
        /// <param name="horizontalOffset">the real offset instead of outdated one from the scroll viewer</param>
        private void UpdateScrollButtonsAvailability(double? horizontalOffset = null)
        {
            if (this.tabScrollViewer == null) return;

            var hOffset = horizontalOffset ?? this.tabScrollViewer.HorizontalOffset;
            hOffset = Math.Max(hOffset, 0);

            var scrWidth = this.tabScrollViewer.ScrollableWidth;
            scrWidth = Math.Max(scrWidth, 0);

            if (this.tabLeftButton != null)
            {
                this.tabLeftButton.Visibility = scrWidth == 0 ? Visibility.Collapsed : Visibility.Visible;

                this.tabLeftButton.IsEnabled = hOffset > 0;

                if (!this.tabLeftButton.IsEnabled)
                {
                    this.tabLeftButton.Opacity = 0.5;
                }
                else
                {
                    this.tabLeftButton.Opacity = 1;
                }

            }
            if (this.tabRightButton != null)
            {
                this.tabRightButton.Visibility = scrWidth == 0 ? Visibility.Collapsed : Visibility.Visible;

                this.tabRightButton.IsEnabled = hOffset < scrWidth;

                if (!this.tabRightButton.IsEnabled)
                {
                    this.tabRightButton.Opacity = 0.5;
                }
                else
                {
                    this.tabRightButton.Opacity = 1;

                }
            }
        }

        /// <summary>
        /// Scrolls to a selected tab
        /// </summary>
        private void ScrollToSelectedItem()
        {
            var model = base.SelectedItem;
            var si = this.ItemContainerGenerator.ContainerFromItem(model) as TabItem;
            if (si == null || this.tabScrollViewer == null)
                return;
            if (si.ActualWidth == 0 && !si.IsLoaded)
            {
                si.Loaded += (s, e) => ScrollToSelectedItem();
                return;
            }

            this.ScrollToItem(si);

            if (this.Items.Count <= 1)
            {
                si.Foreground = CommonResources.Common_White;
            }
        }

        /// <summary>
        /// Scrolls to the specified tab
        /// </summary>
        private void ScrollToItem(TabItem si)
        {
            var tabItems = this.Items.Cast<object>()
                .Select(item => this.ItemContainerGenerator.ContainerFromItem(item) as TabItem);

            var leftItems = tabItems
                .Where(ti => ti != null)
                .TakeWhile(ti => ti != si).ToList();

            var leftItemsWidth = leftItems.Sum(ti => ti.ActualWidth);

            //If the selected item is situated somewhere at the right area
            if (leftItemsWidth + si.ActualWidth > this.tabScrollViewer.HorizontalOffset + this.tabScrollViewer.ViewportWidth)
            {
                var currentHorizontalOffset = (leftItemsWidth + si.ActualWidth) - this.tabScrollViewer.ViewportWidth;
                // the selected item has extra width, so I add it to the offset
                var hMargin = !leftItems.Any(ti => ti.IsSelected) && !si.IsSelected ? this.tabPanelTop.Margin.Left + this.tabPanelTop.Margin.Right : 0;
                currentHorizontalOffset += hMargin;

                this.tabScrollViewer.ScrollToHorizontalOffset(currentHorizontalOffset);
            }
            //if the selected item somewhere at the left
            else if (leftItemsWidth < this.tabScrollViewer.HorizontalOffset)
            {
                var currentHorizontalOffset = leftItemsWidth;
                // the selected item has extra width, so I remove it from the offset
                var hMargin = leftItems.Any(ti => ti.IsSelected) ? this.tabPanelTop.Margin.Left + this.tabPanelTop.Margin.Right : 0;
                currentHorizontalOffset -= hMargin;

                this.tabScrollViewer.ScrollToHorizontalOffset(currentHorizontalOffset);
            }
        }

        /// <summary>
        /// Returns the tab item by using some kind of a hit-test
        /// </summary>
        /// <param name="offset">the absolute coordinate in pixels starting from the left</param>
        private TabItem GetItemByOffset(double offset)
        {
            var tabItems = this.Items.Cast<object>()
                .Select(item => this.ItemContainerGenerator.ContainerFromItem(item) as TabItem)
                .ToList();

            double currentItemsWidth = 0;
            // get tabs one by one and calculate their aggregated width until the offset value is reached
            foreach (var ti in tabItems)
            {
                if (currentItemsWidth + ti.ActualWidth >= offset)
                    return ti;

                currentItemsWidth += ti.ActualWidth;
            }

            return tabItems.LastOrDefault();
        }


        #endregion
    }
}
