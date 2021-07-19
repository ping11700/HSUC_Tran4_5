using HSUC_Tran4_5.Utils;
using System;
using System.Windows;

namespace HSUC_Tran4_5.Tools
{
    /// <summary>
    /// AppConfigWin.xaml 的交互逻辑
    /// </summary>
    public partial class AppConfigWin : Window
    {
        /// <summary>
        /// 是否开启命令窗口
        /// </summary>
        public bool IsOpenConsole { get; private set; }

        /// <summary>
        /// 程序集版本号
        /// </summary>
        public string AssemblyNum { get; private set; }

        /// <summary>
        /// 0 bate , 1 测试 , 2 线上
        /// </summary>
        public int EnvironmentIndex { get; private set; }

        /// <summary>
        /// false=> win100 / true=> win0
        /// </summary>
        public bool IsWin0Channel { get; private set; }

        public AppConfigWin()
        {
            InitializeComponent();
            Loaded += AppConfigWin_Loaded;
        }

        private void AppConfigWin_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentAssemblyNum_Tb.Text = ClientVersion.SoftwareVersion;
        }

        private void OK_Clicked(object sender, RoutedEventArgs e)
        {
            IsOpenConsole = this.openConsole.IsChecked.Value;
            EnvironmentIndex = this.Environment_Combo.SelectedIndex;
            IsWin0Channel = this.win0_RB.IsChecked == true;

            this.DialogResult = true;

            //SetAssemblyVersion();
        }



        private void SetAssemblyVersion()
        {
            //var exe = AppDomain.CurrentDomain.BaseDirectory + "HSUC_Tran4_5.exe";

            //AssemblyDefinition ass = AssemblyDefinition.ReadAssembly(exe);
            ////var module = ass.Modules.First();
            ////var modAssVersion = module.AssemblyReferences.First(ar => ar.Name == "HSUC_Tran4_5");
            ////module.AssemblyReferences.Remove(modAssVersion);
            ////modAssVersion.Version = new Version(4, 0, 3, 0);
            ////module.AssemblyReferences.Add(modAssVersion);
            //ass.Name.Version = new Version("1.0.3.120");

            //this.Dispatcher.Invoke(() => { ass.Write(exe); });
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            IsOpenConsole = this.openConsole.IsChecked.Value;
            EnvironmentIndex = this.Environment_Combo.SelectedIndex;
            IsWin0Channel = this.win0_RB.IsChecked == true;

        }
    }
}
