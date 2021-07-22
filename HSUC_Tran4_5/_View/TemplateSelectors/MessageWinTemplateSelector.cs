using HSUC_Tran4_5._ViewModel;
using HSUC_Tran4_5.Utils;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HSUC_Tran4_5._View
{
    public class MessageWinTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            try
            {
                switch ((MessWinTypes)item)
                {
                    case MessWinTypes.TaskConfig:
                        return (container as FrameworkElement).FindResource("uc_TaskConfigDataTemplate") as DataTemplate;
                    case MessWinTypes.Login:
                        return (container as FrameworkElement).FindResource("uc_LoginDataTemplate") as DataTemplate;
                    case MessWinTypes.Update:
                        return (container as FrameworkElement).FindResource("uc_UpdataDataTemplate") as DataTemplate;
                    default:
                        return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
