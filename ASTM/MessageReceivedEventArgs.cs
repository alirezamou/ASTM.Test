namespace ASTM;

public class MessageReceivedEventArgs: EventArgs
{
    public string ReceivedMessage { get; set; } = string.Empty;

    public MessageReceivedEventArgs(string message)
    {
        ReceivedMessage = message;
    }
}