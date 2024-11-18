using Connection;
using System.Net;
using System.Net.Sockets;
using System.Text;

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
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            if(socket.Connected)
            {
                throw new Exception("socket is already connected");
            }
            socket.Connect("192.168.1.100", 80);

            if (socket.Connected) {
                byte[] buffer = new byte[1024];
                while(socket != null && socket.Connected)
                {
                    if(socket.Available > 0)
                    {
                        int count = socket.Receive(buffer);
                        var data = Encoding.ASCII.GetString(buffer, 0, 1024);
                        Console.WriteLine(data);
                        continue;
                    }

                    Task.Delay(5);
                }
            }

        } catch(Exception ex) { 
        }
    }
}