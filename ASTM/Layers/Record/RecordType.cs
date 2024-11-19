namespace ASTM.Layers.Record;

public static class RecordType
{
    public static readonly char MessageHeader = 'H';
    public static readonly char MessageTermination = 'L';
    public static readonly char RequestInformation = 'Q';
    public static readonly char PatientInformation = 'P';
    public static readonly char TestOrder = 'O';
    public static readonly char Result = 'R';
    public static readonly char Comment = 'C';
}
