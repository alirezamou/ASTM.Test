using ASTM.Layers.Frame;
using System.Globalization;
using System.Text;

namespace ASTM;

public class ASTMConnection
{
	public StringBuilder TempBuffer = new StringBuilder();
	public List<string> Records = new List<string>();
	public bool IsETB { get; set; } = false;

	byte[] buffer = new byte[]
		{
			0x02, 0x01, 0x48, 0x65, 0x6C, 0x6C, 0x6F, 0x17, 0xA5, 0xBC, 0x0D, 0x0A,
			0x02, 0x02, 0x57, 0x6F, 0x72, 0x6C, 0x64, 0x17, 0xB6, 0xD7, 0x0D, 0x0A,
			0x02, 0x03, 0x4F, 0x72, 0x74, 0x2D, 0x33, 0x03, 0xC5, 0xD1, 0x0D, 0x0A
		};

	private const int MinFrameLength = 7;

	public event EventHandler<MessageReceivedEventArgs>? OnMessageReceived;

	public ConnectionStatus Status { get; set; } = ConnectionStatus.Receiving;

	private readonly IConnection _connection = null;

	public ASTMConnection(IConnection connection)
	{
		_connection = connection;
	}

	public void DataReceived()
	{
		string data = Encoding.ASCII.GetString(buffer);

		if (data.Contains("alive") || string.IsNullOrWhiteSpace(data)) return;

		switch (Status)
		{
			case ConnectionStatus.Receiving:
				{
					if (data[0] == Codes.ENQ)
					{
						Console.WriteLine("Received <ENQ>");
						_connection.Write(Codes.NAK.ToString());
						Console.WriteLine("Sending <NAK>");
					}

					if (data[0] == Codes.EOT)
						Status = ConnectionStatus.Idle;
					else
					{
						foreach (var ch in data)
						{
							if (ch != 0) TempBuffer.Append(ch);

							if (ch == Codes.LF)
							{
								var frame = TempBuffer.ToString();

								var isFrameValid = true;

								if (frame.Length < MinFrameLength) isFrameValid = false;
								if (frame[0] != Codes.STX) isFrameValid = false;
								if (frame[frame.Length - 1] != Codes.LF) isFrameValid = false;
								if (frame[frame.Length - 2] != Codes.CR) isFrameValid = false;

								if (!isFrameValid)
								{
									Console.Error.WriteLine("Frame is not valid");
									_connection.Write(Codes.NAK.ToString());
								}

								bool isValid = IsCheckSumValid(frame);

								if (!isValid)
								{
									Console.Error.WriteLine("Check sum is invalid");
									_connection.Write(Codes.NAK.ToString());
								}

								_connection.Write(Codes.ACK.ToString());

								IsETB = frame[frame.Length - 5] == Codes.ETB;

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
						}
					}
					break;
				}
			case ConnectionStatus.Idle:
				{
					if (data[0] == Codes.ENQ)
					{
						Console.WriteLine("Received <ENQ>");
						Status = ConnectionStatus.Receiving;
						_connection.Write(Codes.ACK.ToString());
						Console.WriteLine("Sending <ACK>");
					}
					break;
				}
			default:
				{
					Console.WriteLine("Default case");
					break;
				}
		}

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
}
