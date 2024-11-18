using Connection;
using System.Net;
using System.Net.Sockets;

internal class Program
{
    private static void Main(string[] args)
    {
        // create ASTM instance
        // send ENQ frame
        // wait for ACK

        var port = 9000;
        var address = IPAddress.Parse("127.0.0.1");

        try
        {
            var listener = new TcpListener(address, port);
            listener.Start();

            while (true)
            {
                Console.Write("Waiting for a connection... ");

                using TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Connected!");
            }
        } catch(Exception ex) { 
        }
        
    }
}