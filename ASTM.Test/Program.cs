using Connection;

internal class Program
{
    private static void Main(string[] args)
    {
        // create ASTM instance
        // send ENQ frame
        // wait for ACK
        var tcp = new TcpConnection();
        tcp.Connect();
        tcp.SendMessage("5");
        while(true)
        {
            Thread.Sleep(1000);
            var str = tcp.ReceiveMessage();
            Console.WriteLine("Received Message: ", str);
        }
    }
}