namespace Connection;

public class ConnectionHeartBeatEventArgs: EventArgs
{
	public string data { get; set; }
    public ConnectionHeartBeatEventArgs(string timeStamp)
    {
		data = timeStamp;
    }
}
