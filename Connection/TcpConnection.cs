using System.Net;
using System.Text;
using System.Net.Sockets;
using Shared;

namespace Connection;

public class TcpConnection
{
    private readonly ILogger _logger;

    public string Host { get; private set; } = string.Empty;
    public int Port { get; private set; }

    public TcpListener? _server { get; private set; }
    public TcpClient? _client { get; private set; }
    public Socket? _socket { get; private set; }

    public event EventHandler<ConnectionHeartBeatEventArgs>? OnReceiveHeartBeat;
    public event EventHandler<ConnectionDataReceivedEventArgs>? OnReceiveData;

    public TcpConnection(string host, int port, ILogger logger)
    {
        Host = host;
        Port = port;

        _logger = logger;
    }

    public async Task StartServer()
    {
        try
        {
            var ipAddress = IPAddress.Parse(Host);
            _server = new TcpListener(ipAddress, Port);
            _server.Start();

            var buffer = new byte[1024];
            var readBytes = 0;

            while (true)
            {
                try
                {
                    _logger.Log("Waiting for client...");
                    _client = await _server.AcceptTcpClientAsync();
                    _socket = _client.Client;

                    var stream = _client.GetStream();
                    while ((readBytes = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        var data = Encoding.UTF8.GetString(buffer, 0, readBytes);

                        if (data.Contains("alive"))
                        {
                            if (data.Length == 5)
                            {
                                this.OnReceiveHeartBeat?.Invoke(this, new ConnectionHeartBeatEventArgs(DateTime.Now.ToString()));
                                if (_socket == null) break;
                            }
                            else if (data.Length > 5)
                            {
                                data = data.Replace("alive", string.Empty);
                                this.OnReceiveData?.Invoke(this, new ConnectionDataReceivedEventArgs(data));
                            }
                        }
                        else
                        {
                            this.OnReceiveData?.Invoke(this, new ConnectionDataReceivedEventArgs(data));
                        }
                    }
                }
                catch (SocketException ex)
                {
                    _logger.Error("Socket Error: " + ex.ToString());
                }
                catch { }
                finally
                {
                    _socket?.Dispose();
                }
            }
        }
        catch (SocketException ex)
        {
            _logger.Error("Socket Error: " + ex.ToString());
        }
        catch (Exception ex)
        {
            _logger.Error("Unknown Error");
        }
    }
    public void Write(string data)
    {
        var buffer = Encoding.UTF8.GetBytes(data);
        _socket?.Send(buffer);
    }
}
