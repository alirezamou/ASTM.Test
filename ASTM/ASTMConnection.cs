using Connection;
using ASTM.Layers.Frame;
using System.Globalization;
using System.Text;

namespace ASTM;

public class ASTMConnection
{
    public StringBuilder TempBuffer = new StringBuilder();
    public List<string> Records = new List<string>();
    public bool IsETB { get; set; } = false;

    private const int MinFrameLength = 7;

    public event EventHandler<MessageReceivedEventArgs>? OnMessageReceived;
    public event EventHandler<ConnectionHeartBeatEventArgs>? OnReceiveHeartBeat;

    public ConnectionStatus Status { get; set; } = ConnectionStatus.Idle;

    public TcpConnection? _connection { get; private set; }

    public ASTMConnection(TcpConnection connection)
    {
        _connection = connection;
        _connection.OnReceiveData += HandleReceiveData;
        _connection.OnReceiveHeartBeat += OnReceiveHeartBeat;
    }

    public void HandleReceiveHeartBeat(object? sender, ConnectionHeartBeatEventArgs arg)
    {
        if (arg is not null) OnReceiveHeartBeat?.Invoke(this, arg);
    }

    public void HandleReceiveData(object? sender, ConnectionDataReceivedEventArgs arg)
    {
        var data = arg.data;

        if (data.Contains("alive") || string.IsNullOrWhiteSpace(data)) return;

        foreach (var ch in data)
        {
            switch (Status)
            {
                case ConnectionStatus.Receiving:
                    {
                        if (ch == CodesChar.ENQ)
                        {
                            Console.WriteLine("Received <ENQ>");
                            _connection.Write(Codes.NAK.ToString());
                            Console.WriteLine("Sending <NAK>");
                            continue;
                        }

                        if (ch == CodesChar.EOT)
                        {
                            Console.WriteLine("Received <EOT>");
                            Status = ConnectionStatus.Idle;
                            TempBuffer.Clear();
                            Records.Clear();
                            IsETB = false;

                            return;
                        }

                        if (ch != 0) TempBuffer.Append(ch);

                        if (ch == CodesChar.LF)
                        {
                            handleReceiveFrame(TempBuffer.ToString());
                        }

                        break;
                    }
                case ConnectionStatus.Idle:
                    {
                        if (ch == CodesChar.ENQ)
                        {
                            Console.WriteLine("Received <ENQ>");
                            Status = ConnectionStatus.Receiving;
                            _connection.Write(Codes.ACK.ToString());
                            Console.WriteLine("Sending <ACK>");
                            continue;
                        }
                        Console.WriteLine("Sending <NAK>");
                        _connection?.Write(Codes.NAK.ToString());

                        break;
                    }
                case ConnectionStatus.Establishing:
                    {
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Default case");
                        break;
                    }
            }
        }
    }

    public void handleReceiveFrame(string frame)
    {
        var isFrameValid = true;

        if (string.IsNullOrWhiteSpace(frame)) isFrameValid = false;
        if (frame.Length < MinFrameLength) isFrameValid = false;
        if (frame[0] != CodesChar.STX) isFrameValid = false;
        if (frame[frame.Length - 1] != Codes.LF) isFrameValid = false;
        if (frame[frame.Length - 2] != Codes.CR) isFrameValid = false;

        if (!isFrameValid)
        {
            Console.Error.WriteLine("Frame is invalid");
            _connection.Write(Codes.NAK.ToString());
            return;
        }

        if (!IsCheckSumValid(frame))
        {
            Console.Error.WriteLine("Check sum is invalid");
            _connection.Write(Codes.NAK.ToString());
            return;
        }

        _connection.Write(Codes.ACK.ToString());

        IsETB = frame[frame.Length - 5] == CodesChar.ETB;

        Console.WriteLine(">>>> frame: " + frame + " - " + (IsETB ? "ETB" : "ETX"));

        var endTextIndex = frame.Length - 7;

        var record = frame.Substring(2, endTextIndex);

        Records.Add(record);

        if (!IsETB)
        {
            string text = "";
            foreach (var r in Records) text += r;

            OnMessageReceived?.Invoke(this, new MessageReceivedEventArgs(text));
            Records.Clear();
        }

        TempBuffer.Clear();
        IsETB = false;
    }

    private bool IsCheckSumValid(string frame)
    {
        var chunk = frame.Substring(1, frame.Length - 5);
        int sum = 0;

        foreach (var ch in chunk)
            sum += ch;

        var hexStr = ((byte)(sum % 256)).ToString("X2", CultureInfo.InvariantCulture);

        return hexStr == frame.Substring(frame.Length - 4, 2);
    }

    public async Task Connect()
    {
        await _connection?.StartServer();
    }
}
