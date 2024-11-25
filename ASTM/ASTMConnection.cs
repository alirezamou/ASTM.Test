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

	public System.Timers.Timer NextFrameTimer { get; set; }
	public long NextFrameTimeoutCounter { get; set; } = 0;

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

		NextFrameTimer = new System.Timers.Timer
		{
			Enabled = false,
			Interval = 10000,
		};
		NextFrameTimer.Elapsed += NextFrameTimer_Elapsed;
	}

	private void NextFrameTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
	{
		_logger.Log("Timeout");

		if (NextFrameTimeoutCounter > 6)
		{
			_logger.Log("Counter Limit hit 6");
			_logger.Log("Set State to Idle");

			Status = ConnectionStatus.Idle;
			NextFrameTimeoutCounter = 0;
			NextFrameTimer.Stop();
			return;
		}
		NextFrameTimeoutCounter++;

		_logger.Log("Sending <ACK>");
		_connection?.Write(ControlCharactersHex.ACK.ToString());
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
					if (ch == ControlCharactersChar.ENQ)
					{
						try
						{
							_logger.Log("Received <ENQ>");
							_connection?.Write(ControlCharactersHex.NAK.ToString());
							_logger.Log("Sending <NAK>");
							Status = ConnectionStatus.Idle;
							NextFrameTimer.Stop();
						}
						catch { }
						return;
					}

					if (ch == ControlCharactersChar.EOT)
					{
						_logger.Log("Received <EOT>");
						Status = ConnectionStatus.Idle;
						NextFrameTimer.Stop();
						return;
					}

					if (ch != 0) TempBuffer.Append(ch);

					if (ch == ControlCharactersChar.LF)
					{
						handleReceiveFrame(TempBuffer.ToString());
					}

					break;
				case ConnectionStatus.Idle:
					if (ch == ControlCharactersChar.ENQ)
					{
						_logger.Log("Received <ENQ>");

						Status = ConnectionStatus.Receiving;
						TempBuffer.Clear();
						Records.Clear();
						IsETB = false;
						NextFrameTimeoutCounter = 0;
						NextFrameTimer.Start();

						_logger.Log("Sending <ACK>");
						_connection?.Write(ControlCharactersHex.ACK.ToString());

						return;
					}
					_logger.Log("Sending <NAK>");
					_connection?.Write(ControlCharactersHex.NAK.ToString());

					break;
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
		if (frame[0] != ControlCharactersChar.STX) isFrameValid = false;
		if (frame[frame.Length - 1] != ControlCharactersHex.LF) isFrameValid = false;
		if (frame[frame.Length - 2] != ControlCharactersHex.CR) isFrameValid = false;

		if (!isFrameValid)
		{
			_logger.Error("Frame is invalid");
			_connection?.Write(ControlCharactersHex.NAK.ToString());
			NextFrameTimeoutCounter = 0;
			NextFrameTimer.Stop();
			NextFrameTimer.Start();
			return;
		}

		if (!IsCheckSumValid(frame))
		{
			_logger.Error("Check sum is invalid");
			_connection?.Write(ControlCharactersHex.NAK.ToString());
			NextFrameTimer.Stop();
			NextFrameTimer.Start();
			return;
		}

		_connection?.Write(ControlCharactersHex.ACK.ToString());
		NextFrameTimer.Stop();
		NextFrameTimer.Start();
		NextFrameTimeoutCounter = 0;

		IsETB = frame[frame.Length - 5] == ControlCharactersChar.ETB;

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
