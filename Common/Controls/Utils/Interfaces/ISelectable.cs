using System.Windows;

namespace Common.Controls.Utils
{
    public interface ISelectable
    {
        event RoutedEventHandler Selected;

        bool IsSelected { get; set; }
    }
}
