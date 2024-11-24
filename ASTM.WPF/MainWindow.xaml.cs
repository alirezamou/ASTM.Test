using Connection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ASTM.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Logger Logger { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            Start();
        }

        public async void Start()
        {
            var host = "192.168.10.100";
            var port = 9000;
            var tcpConnection = new TcpConnection(host, port, new Logger(LogsContainer));
            var astmConnection = new ASTMConnection(tcpConnection, new Logger(LogsContainer));

            await astmConnection.Connect();
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            LogsContainer.Children.Clear();
            LogsContainer.UpdateLayout();
        }
    }
}