using System.Windows;
using System.Windows.Controls;

namespace HSUC_Tran4_5._View
{
    /// <summary>
    /// UserControl_ProcessMonitor.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl_ProcessMonitor : UserControl
    {
        public UserControl_ProcessMonitor()
        {
            InitializeComponent();
            NameScope.SetNameScope(this.contextMenu, NameScope.GetNameScope(this));
        }

    }
}
