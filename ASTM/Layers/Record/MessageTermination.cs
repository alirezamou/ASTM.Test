namespace ASTM.Layers.Record;

internal class MessageTermination: IRecord
{
    [Field(FieldPosition = 1, OrderNo = 1, Type = FieldType.ST, Max = 1, EV = FieldEffectivity.X, CV = FieldEffectivity.R, FieldName = "Record Type ID")]
    public static readonly char RecordTypeId = RecordType.MessageTermination;

    [Field(FieldPosition = 2, OrderNo = 2, Type = FieldType.NM, Max = 6, EV = FieldEffectivity.X, CV = FieldEffectivity.R, FieldName = "Sequence Number")]
    public int SequenceNumber { get; set; } = 1;

    [Field(FieldPosition = 3, OrderNo = 3, Type = FieldType.ST, Max = 1, EV = FieldEffectivity.X, CV = FieldEffectivity.R, FieldName = "Termination Code")]
    public TerminationCode TerminationCode { get; set; }
}
