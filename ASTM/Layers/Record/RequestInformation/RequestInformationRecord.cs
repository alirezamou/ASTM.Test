namespace ASTM.Layers.Record.RequestInformation;

public class RequestInformationRecord : IRecord
{
    public char RecordTypeId { get; } = RecordType.RequestInformation;

    public int SequenceNumber { get; set; }

    public CompositeField? StartingRangeIDNumber { get; set; }

    public UniversalTestIDEnum UniversalTestID { get; set; }

    public RequestInformationStatusCodeEnum RequestInformationStatusCode { get; set; }

}
