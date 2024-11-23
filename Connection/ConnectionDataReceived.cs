namespace Connection;

public class ConnectionDataReceivedEventArgs : EventArgs
{
	public string data { get; set; } = string.Empty;
	public ConnectionDataReceivedEventArgs(string message)
	{
		data = message;
	}
}
