namespace ASTM.Layers.Record.MessageHeader;

public class SenderNameOrId : CompositeField
{
    public string SendersDeviceName { get; set; } = string.Empty;

    public int CommunicationProgramVersion { get; set; }
}
