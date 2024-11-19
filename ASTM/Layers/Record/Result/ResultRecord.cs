namespace ASTM.Layers.Record.Result;

public class ResultRecord
{
    public char RecordTypeId { get; } = RecordType.Result;

    public int SequenceNumber { get; set; }

    public CompositeField? UniversalTestID { get; set; }

    public CompositeField? DataOrMeasurmentValue { get; set; }

    public string Units { get; set; } = string.Empty;

    public CompositeField? ReferenceRanges { get; set; }

    public ResultAbnormalFlagsEnum ResultAbnormalFlags { get; set; }

    public ResultStatusEnum ResultStatus { get; set; }

    public string OperatorIdentification { get; set; } = string.Empty;

    public string DateTimeTestStarted { get; set; } = string.Empty;

    public string DateTimeTestCompleted { get; set; } = string.Empty;

    public string InstrumentID { get; set; } = string.Empty;
}
