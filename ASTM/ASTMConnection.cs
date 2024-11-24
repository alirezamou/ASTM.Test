using Connection;
using ASTM.Layers.Frame;
using System.Globalization;
using System.Text;
using Shared;

namespace ASTM;

public class ASTMConnection
{
    private readonly ILogger _logger;

    public StringBuilder TempBuffer = new StringBuilder();
    public List<string> Records = new List<string>();
    public bool IsETB { get; set; } = false;

    private const int MinFrameLength = 7;

    public event EventHandler<MessageReceivedEventArgs>? OnMessageReceived;
    public event EventHandler<ConnectionHeartBeatEventArgs>? OnReceiveHeartBeat;

    public ConnectionStatus Status { get; set; } = ConnectionStatus.Idle;

    public TcpConnection? _connection { get; private set; }

    public ASTMConnection(TcpConnection connection, ILogger logger)
    {
        _logger = logger;

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
                            _logger.Log("Received <ENQ>");
                            _connection?.Write(Codes.NAK.ToString());
                            _logger.Log("Sending <NAK>");
                            continue;
                        }

                        if (ch == CodesChar.EOT)
                        {
                            _logger.Log("Received <EOT>");
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
                            _logger.Log("Received <ENQ>");
                            Status = ConnectionStatus.Receiving;
                            _connection?.Write(Codes.ACK.ToString());
                            _logger.Log("Sending <ACK>");
                            continue;
                        }
                        _logger.Log("Sending <NAK>");
                        _connection?.Write(Codes.NAK.ToString());

                        break;
                    }
                case ConnectionStatus.Establishing:
                    {
                        break;
                    }
                default:
                    {
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
            _logger.Error("Frame is invalid");
            _connection?.Write(Codes.NAK.ToString());
            return;
        }

        if (!IsCheckSumValid(frame))
        {
            _logger.Error("Check sum is invalid");
            _connection?.Write(Codes.NAK.ToString());
            return;
        }

        _connection?.Write(Codes.ACK.ToString());

        IsETB = frame[frame.Length - 5] == CodesChar.ETB;

        _logger.Log(">>>> frame: " + frame + " - " + (IsETB ? "ETB" : "ETX"));

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
