using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Shared;

namespace ASTM.WPF;
public class Logger: ILogger
{
    private readonly StackPanel? _logsContainer;
    public Logger(StackPanel? logsContainer)
    {
        _logsContainer = logsContainer;
    }

	public void Error(string message)
	{
		var log = new TextBox
		{
			Text = "- Error : " + message,
			IsReadOnly = true,
			BorderBrush = Brushes.Transparent,
			Background = Brushes.Transparent,
            Foreground = Brushes.Red,
			FontSize = 16,
            Margin = new Thickness(0, 10, 0, 0),
		};

		_logsContainer?.Children.Add(log);

		_logsContainer?.UpdateLayout();
	}

	public void Log(string message)
    {
        var log = new TextBox
        {
            Text = "- Log: " + message,
            IsReadOnly = true,
            BorderBrush = Brushes.Transparent,
            Background = Brushes.Transparent,
            FontSize = 16,
			Margin = new Thickness(0, 10, 0, 0),
		};

        _logsContainer?.Children.Add(log);

        _logsContainer?.UpdateLayout();
    }
}
