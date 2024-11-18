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


        var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            Console.WriteLine("Connecting...");
            socket.Connect("192.168.10.199", 23);

            if (socket.Connected) { 

                var buffer = new byte[1024];
                var readBytes = 0;

                while (socket != null && socket.Connected) {
                    if(socket.Available > 0)
                    {
                        readBytes = socket.Receive(buffer);
                        var data = Encoding.ASCII.GetString(buffer, 0, readBytes);
                        Console.WriteLine("received data: " + data);
                    }
                }
            }
        }
        catch (Exception ex) { 
            Console.WriteLine("Error: " + ex.Message);
        }


        /* server listening  */

        //var port = 9000;
        //var address = IPAddress.Parse("192.168.10.100");

        //try
        //{
        //    var listener = new TcpListener(address, port);
        //    listener.Start();

        //    byte[] buffer = new byte[1024];

        //    while (true)
        //    {
        //        try
        //        {
        //            var client = listener.AcceptTcpClient();
        //            Console.WriteLine("new client: ", (client.Client.RemoteEndPoint as IPEndPoint).Address);
        //            var socket = client.Client;
        //            var stream = client.GetStream();
        //            int readBytes = 0;

        //            while ((readBytes = stream.Read(buffer, 0, 1024)) > 0)
        //            {
        //                var data = Encoding.ASCII.GetString(buffer, 0, readBytes);
        //                if (data.Contains("alive") && data.Length > 5)
        //                {
        //                    Console.WriteLine("Heartbeat");
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("error in accepting client: ", ex.ToString());
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine("Error in starting listener: ", ex.ToString());
        //}
    }
}