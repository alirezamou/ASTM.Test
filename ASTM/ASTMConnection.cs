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
			0x02, 0x31, 0x54, 0x65, 0x73, 0x74, 0x03, 0x44, 0x34, 0x0D, 0x0A
		};

	private const int MinFrameLength = 7;

	public event EventHandler<MessageReceivedEventArgs>? OnMessageReceived;

	public void DataReceived()
	{
		string data = Encoding.ASCII.GetString(buffer);

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

				if (!isFrameValid) break;

				bool isValid = IsCheckSumValid(frame);

				IsETB = frame[frame.Length - 5] == Codes.ETB;

				var endTextIndex = frame.Length - 7;

				var record = frame.Substring(2, endTextIndex);

				Records.Add(record);

				if (!IsETB)
					OnMessageReceived?.Invoke(this, new MessageReceivedEventArgs(record));

				TempBuffer.Clear();
				IsETB = false;
			}
		}
	}

	private bool IsCheckSumValid(string frame)
	{
		/* from Frame number to ETX or ETB */
		var chunk = frame.Substring(1, frame.Length - 5);
		int sum = 0;

		foreach (var ch in chunk)
			sum += ch;

		var hexStr = ((byte)(sum % 256)).ToString("X2", CultureInfo.InvariantCulture);

		return hexStr == frame.Substring(frame.Length - 4, 2);
	}
}
