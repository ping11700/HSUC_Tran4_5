using System.Windows;
using System.Windows.Controls;

namespace HSUC_Tran4_5._View
{
    public class MessageWinTemplateSelector : DataTemplateSelector
    {

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            //if (item is ClassA)
            //    return (container as FrameworkElement).FindResource("uc_Login") as DataTemplate;
            //else if (item is ClassB)
            //    return (container as FrameworkElement).FindResource("uc_TaskConfig") as DataTemplate;
            return null;
        }
    }
}
