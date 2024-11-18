using System.Net;
using System.Text;
using System.Net.Sockets;

namespace Connection;

public class TcpConnection
{
    // establish connection using port and ip
    // connect
    // disconnect

    // send data
    // receive data

    private TcpClient _client { get; set; }
    private NetworkStream _stream {  get; set; }
    private StreamReader _streamReader { get; set; }
    private StreamWriter _streamWriter { get; set; }
    private readonly string host = "127.0.0.1";
    private readonly int port = 9000;

    public void Connect()
    {
        _client = new TcpClient(host, port);
        _stream = _client.GetStream();
        _streamReader = new StreamReader(_stream, Encoding.ASCII);
        _streamWriter = new StreamWriter(_stream, Encoding.ASCII);
    }

    public void SendMessage(string message)
    {
        try
        {
            _streamWriter.Write(message);
            _streamWriter.Flush();
        } catch(Exception e) 
        {
            Console.WriteLine("Error in sending message: ", e.Message);
        }
    }

    public string ReceiveMessage()
    {
        try
        {
            return _streamReader.ReadLine();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error in Receiving Message: ", e.Message);
            return null;
        }
    }

    public void Close()
    {
        _streamReader.Close();
        _streamWriter.Close();
        _stream.Close();
        _client.Close();
    }
}
