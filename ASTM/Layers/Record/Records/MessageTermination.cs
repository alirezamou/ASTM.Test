namespace ASTM.Layers.Record.Records;

internal class MessageTermination
{
    public static readonly char RecordTypeId = 'L';
    public string SequenceNumber { get; set; } = "1";
    public string TerminationCode { get; set; } = string.Empty; // N or E
}
