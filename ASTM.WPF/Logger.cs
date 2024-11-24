using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ASTM.WPF;
public class Logger
{
    private readonly StackPanel? _logsContainer;
    public Logger(StackPanel? logsContainer)
    {
        _logsContainer = logsContainer;
    }

    public void Log(string message)
    {
        var log = new TextBox
        {
            Text = message,
            IsReadOnly = true,
            BorderBrush = Brushes.Transparent,
            Background = Brushes.Transparent,
            FontSize = 16,
        };

        _logsContainer?.Children.Add(log);

        _logsContainer?.UpdateLayout();
    }
}
