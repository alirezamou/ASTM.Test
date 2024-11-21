namespace ASTM.Layers.Record.Result;

public class ResultRecord: IRecord
{
    public char RecordTypeId { get; } = RecordType.Result;

    public int SequenceNumber { get; set; }

    public UniversalTestID? UniversalTestID { get; set; }

    public DataOrMeasurmentValue? DataOrMeasurmentValue { get; set; }

    public string Units { get; set; } = string.Empty;

    public ReferenceRange? ReferenceRanges { get; set; }

    public ResultAbnormalFlagsEnum ResultAbnormalFlags { get; set; }

    public ResultStatusEnum ResultStatus { get; set; }

    public string OperatorIdentification { get; set; } = string.Empty;

    public string DateTimeTestStarted { get; set; } = string.Empty;

    public string DateTimeTestCompleted { get; set; } = string.Empty;

    public string InstrumentID { get; set; } = string.Empty;
}
