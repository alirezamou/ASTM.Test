namespace ASTM.Layers.Record.MessageTermination;

internal class MessageTerminationRecord : IRecord
{
    public char RecordTypeId { get; } = RecordType.MessageTermination;

    public int SequenceNumber { get; set; } = 1;

    public TerminationCodeEnum TerminationCode { get; set; }
}
