namespace ASTM.Layers.Record.PatientInformation;

public class PatientInformationRecord: IRecord
{
    public char RecordTypeId { get; } = RecordType.PatientInformation;
    public int SequenceNumber { get; set; }
}
