using ASTM;
using Connection;

internal class Program
{
    private async static Task Main(string[] args)
    {
        var host = "192.168.10.100";
        var port = 9000;
        var tcpConnection = new TcpConnection(host, port);
        var astmConnection = new ASTMConnection(tcpConnection);
        await astmConnection.Connect();
    }
}