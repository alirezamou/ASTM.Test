namespace ASTM.Layers.Record.MessageHeader;

public class CommentOrSpecialInstruction : CompositeField
{
    public string MeaningOfMessage { get; set; } = string.Empty;

    public string CauseOfMessage { get; set; } = string.Empty;
}