using Common.Log4;
using Microsoft.Win32;
using System;
using System.Reflection;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Common.Controls
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class WebBrowserUC : UserControl
    {

        private const int URLMON_OPTION_USERAGENT = 0x10000001;
        private const string InternetExplorerRootKey = @"Software\Microsoft\Internet Explorer";
        private const string BrowserEmulationKey = InternetExplorerRootKey + @"\Main\FeatureControl\FEATURE_BROWSER_EMULATION";

        #region 定义IE版本的枚举
        /// <summary>
        /// 定义IE版本的枚举
        /// </summary>
        private enum IeVersion
        {
            Edge,//11001(0x2af9)
            标准ie11,//11000(0x2af8)× 
            强制ie10,//10001 (0x2711) Internet Explorer 10。网页以IE 10的标准模式展现，页面!DOCTYPE无效 
            标准ie10,//10000 (0x02710) Internet Explorer 10。在IE 10标准模式中按照网页上!DOCTYPE指令来显示网页。Internet Explorer 10 默认值。
            强制ie9,//9999 (0x270F) Windows Internet Explorer 9. 强制IE9显示，忽略!DOCTYPE指令 
            标准ie9,//9000 (0x2328) Internet Explorer 9. Internet Explorer 9默认值，在IE9标准模式中按照网页上!DOCTYPE指令来显示网页。
            强制ie8,//8888 (0x22B8) Internet Explorer 8，强制IE8标准模式显示，忽略!DOCTYPE指令 
            标准ie8,//8000 (0x1F40) Internet Explorer 8默认设置，在IE8标准模式中按照网页上!DOCTYPE指令展示网页
            标准ie7,//7000 (0x1B58) 使用WebBrowser Control控件的应用程序所使用的默认值，在IE7标准模式中按照网页上!DOCTYPE指令来展示网页
            Default,
        }
        #endregion

        #region 设置WebBrowser的默认版本
        /// <summary>
        /// 设置WebBrowser的默认版本
        /// </summary>
        /// <param name="ver">IE版本</param>
        private void SetIE(IeVersion ieVersion)
        {
            string productName = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;//获取程序名称

            object version;
            switch (ieVersion)
            {
                case IeVersion.Default:
                    version = 0x000;
                    break;
                case IeVersion.标准ie7:
                    version = 0x1B58;
                    break;
                case IeVersion.标准ie8:
                    version = 0x1F40;
                    break;
                case IeVersion.强制ie8:
                    version = 0x22B8;
                    break;
                case IeVersion.标准ie9:
                    version = 0x2328;
                    break;
                case IeVersion.强制ie9:
                    version = 0x270F;
                    break;
                case IeVersion.标准ie10:
                    version = 0x02710;
                    break;
                case IeVersion.强制ie10:
                    version = 0x2711;
                    break;
                case IeVersion.标准ie11:
                    version = 0x2af8;
                    break;
                case IeVersion.Edge:
                    version = 0x2af9;
                    break;
                default:
                    version = 0x1F40;
                    break;
            }

            try
            {

                RegistryKey key = Registry.CurrentUser.OpenSubKey(BrowserEmulationKey, true);

                if (key != null)
                {
                    if (ieVersion != IeVersion.Default)
                    {
                        // if it's a valid value, update or create the value
                        key.SetValue(productName, (int)ieVersion, RegistryValueKind.DWord);
                    }
                    else
                    {
                        // otherwise, remove the existing value
                        key.DeleteValue(productName, false);
                    }
                }
            }
            catch (SecurityException)
            {
                LogService.ILogger.Warn($" The user does not have the permissions required to read from the registry key");
            }
            catch (UnauthorizedAccessException)
            {
                LogService.ILogger.Warn($" The user does not have the necessary registry rights");
            }
            finally
            {
                RegistryKey wuui = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", true);
                //该项必须已存在
                if (wuui != null)
                    wuui.SetValue(productName, version, RegistryValueKind.DWord);

            }

        }
        #endregion

        #region 获取本机IE版本
        /// <summary>
        /// 获取本机IE版本
        /// </summary>
        public static int GetInternetExplorerMajorVersion()
        {
            int result;

            result = 0;

            try
            {
                RegistryKey key;

                key = Registry.LocalMachine.OpenSubKey(InternetExplorerRootKey);

                if (key != null)
                {
                    object value;

                    value = key.GetValue("svcVersion", null) ?? key.GetValue("Version", null);

                    if (value != null)
                    {
                        string version;
                        int separator;

                        version = value.ToString();
                        separator = version.IndexOf('.');
                        if (separator != -1)
                        {
                            int.TryParse(version.Substring(0, separator), out result);
                        }
                    }
                }
            }
            catch (SecurityException)
            {
                LogService.ILogger.Warn($" The user does not have the permissions required to read from the registry key");
            }
            catch (UnauthorizedAccessException)
            {
                LogService.ILogger.Warn($" The user does not have the necessary registry rights");
            }
            return result;
        }
        #endregion

        #region 制定webbrowser内核版本 
        public void DraftEdition()
        {
            int ieResult = GetInternetExplorerMajorVersion();
            if (ieResult == 11)
            {
                SetIE(IeVersion.标准ie11);
            }
            else if (ieResult == 10)
            {
                SetIE(IeVersion.强制ie10);
            }
            else if (ieResult == 9)
            {
                SetIE(IeVersion.强制ie9);
            }
            else
            {
                SetIE(IeVersion.强制ie8);
            }
        }
        #endregion


        #region 在浏览器上放置控件元素
        public static void SetPanel(DependencyObject d, Panel value) => d.SetValue(PanelProperty, value);
        public static Panel GetPanel(DependencyObject d) => (Panel)d.GetValue(PanelProperty);

        public static readonly DependencyProperty PanelProperty =
            DependencyProperty.RegisterAttached("Panel", typeof(Panel), typeof(WebBrowserUC), new PropertyMetadata(null));



        public static void SetTopElement(DependencyObject d, FrameworkElement value) => d.SetValue(TopElementProperty, value);
        public static FrameworkElement GetTopElement(DependencyObject d) => (FrameworkElement)d.GetValue(TopElementProperty);

        public static readonly DependencyProperty TopElementProperty =
            DependencyProperty.RegisterAttached("TopElement", typeof(FrameworkElement), typeof(WebBrowserUC), new PropertyMetadata(null, new PropertyChangedCallback(OnTopElementPropertyChanged)));

        private static void OnTopElementPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var b = d.GetValue(PanelProperty);
            if (b is null || !(b is Panel) || e.NewValue is null)
                return;
            var panel = b as Panel;
            var web = (d as WebBrowserUC).browser;
            var ui = e.NewValue as FrameworkElement;
            SetRect(panel, web, ui);
            panel.SizeChanged += (sender, args) =>
            {
                SetRect(panel, web, ui);
            };

        }

        private static IntPtr C1;
        private static void SetRect(Panel panel, WebBrowser web, FrameworkElement ui)
        {
            //IntPtr handle = web.Handle;
            //Win32API.DeleteObject(C1);
            //Win32API.SetWindowRgn(handle, IntPtr.Zero, true);

            //Rect PanelRect = new Rect(new Size(panel.ActualWidth * ScreenAPI.AbsoluteDPIXRatio,
            //    panel.ActualHeight * ScreenAPI.AbsoluteDPIYRatio));

            //C1 = Win32API.CreateRectRgn(
            //    0,
            //    0,
            //    (int)(PanelRect.BottomRight.X * ScreenAPI.AbsoluteDPIXRatio),
            //    (int)(PanelRect.BottomRight.Y * ScreenAPI.AbsoluteDPIYRatio));

            //Rect UIRect = new Rect(new Size(
            //    ui.ActualWidth * ScreenAPI.AbsoluteDPIXRatio,
            //    ui.ActualHeight * ScreenAPI.AbsoluteDPIXRatio));

            //var D1 = (int)(ui.TransformToAncestor(panel).Transform(new Point(0, 0)).X * ScreenAPI.AbsoluteDPIXRatio);

            //var D2 = (int)(ui.TransformToAncestor(panel).Transform(new Point(0, 0)).Y * ScreenAPI.AbsoluteDPIXRatio);

            //var D3 = (int)(D1 + UIRect.Width);

            //var D4 = (int)(D2 + UIRect.Height);

            //var C2 = Win32API.CreateRectRgn(D1, D2, D3, D4);

            //Win32API.CombineRgn(C1, C1, C2, 4);

            //Win32API.SetWindowRgn(handle, C1, true);
        }

        #endregion


        /// <summary>
        /// js绑定类
        /// </summary>
        public object JsObject
        {
            set
            {
                //js绑定
                this.browser.ObjectForScripting = value;
            }


        }

        /// <summary>
        /// 判断是否可以后退
        /// </summary>
        public bool CanGoBack
        {
            get
            {
                return this.browser.CanGoBack;
            }
        }

        /// <summary>
        /// 网页title
        /// </summary>
        public string HtmlTitle
        {
            get
            {
                return ((dynamic)this.browser.Document).Title;
            }
        }


        /// <summary>
        /// UserAgent
        /// </summary>
        public string UserAgent { get; set; }


        /// <summary>
        /// 网址URI
        /// </summary>
        public string UriSource
        {
            get { return (string)GetValue(UriSourceProperty); }
            set { SetValue(UriSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UriSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UriSourceProperty =
            DependencyProperty.Register("UriSource", typeof(string), typeof(WebBrowserUC), new PropertyMetadata(SourcePropertyChangedCallBakc));
        private static void SourcePropertyChangedCallBakc(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cusWebBrowser = d as WebBrowserUC;

            if (cusWebBrowser != null)
            {
                string address = e.NewValue as string;
                if (String.IsNullOrEmpty(address)) return;
                if (address.Equals("about:blank")) return;

                cusWebBrowser.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                {
                    cusWebBrowser.browser.Navigate(address);
                }));
            }
        }

        public WebBrowserUC()
        {
            InitializeComponent();
            DraftEdition();
        }


        /// <summary>
        /// 导航完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void browser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            try
            {
                IntPtr pHandle = Win32API.GetCurrentProcess();
                Win32API.SetProcessWorkingSetSize(pHandle, -1, -1);
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

        }

        private void browser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            WebBrowser wb = (WebBrowser)sender;

            //'注意：必须在网页完全显示之后才可以运行
            //不显示滚动条

            string script = "document.documentElement.style.overflow ='auto'";
            wb.InvokeScript("execScript", new Object[] { script, "JavaScript" });


            //double zoon = ScreenAPI.AbsoluteDPIXRatio * ScreenAPI.AbsoluteDPIYRatio * 100;

            //SetBrowser(wb, (int)(zoon), true);


        }




        /// <summary>
        /// The flags are used to zoom web browser's content.
        /// </summary>
        private readonly int OLECMDEXECOPT_DODEFAULT = 0;
        private readonly int OLECMDID_OPTICAL_ZOOM = 63;


        /// <summary>
        /// 设置浏览器
        /// </summary>
        /// <param name="webbrowser"></param>
        /// <param name="zoom"> 放大/缩小倍数</param>
        /// <param name="isQuiet">是否禁止脚本错误提示</param>
        private void SetBrowser(WebBrowser webbrowser, int zoom, bool isQuiet)
        {
            try
            {
                if (null == webbrowser)
                {
                    return;
                }

                FieldInfo fiComWebBrowser = webbrowser.GetType().GetField(
                    "_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
                if (null != fiComWebBrowser)
                {
                    Object objComWebBrowser = fiComWebBrowser.GetValue(webbrowser);



                    if (null != objComWebBrowser)
                    {
                        //zoom 放大/缩小
                        object[] args = new object[]
                        {
                            OLECMDID_OPTICAL_ZOOM,
                            OLECMDEXECOPT_DODEFAULT,
                            zoom,
                            IntPtr.Zero
                        };
                        objComWebBrowser.GetType().InvokeMember(
                            "ExecWB",
                            BindingFlags.InvokeMethod,
                            null, objComWebBrowser,
                            args);

                        //禁止ScriptErrors 脚本错误提示
                        objComWebBrowser.GetType().InvokeMember(
                            "Silent",
                            BindingFlags.SetProperty,
                            null, objComWebBrowser,
                            new object[] { isQuiet });


                    }
                }
            }
            catch (System.Exception ex)
            {
                LogService.ILogger.Warn($"设置WebBrowser异常:{ex.Message}");
            }
        }

        /// <summary>
        /// 修改UA
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void browser_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            //修改UA
            Win32API.UrlMkSetSessionOption(URLMON_OPTION_USERAGENT, UserAgent, UserAgent.Length, 0);
        }

        /// <summary>
        /// 浏览器返回
        /// </summary>
        public void GoBack()
        {
            if (this.browser.CanGoBack)
            {
                this.browser.GoBack();
            }
        }
    }
}
