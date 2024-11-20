namespace ASTM.Layers.Record.TestOrder;

public class TestOrderRecord : IRecord
{
    public char RecordTypeId { get; } = RecordType.TestOrder;

    public int SequenceNumber { get; set; }

    public string? SpecimenID { get; set; }

    public InstrumentSpecimenID? InstrumentSpecimenID { get; set; }

    public UniversalTestID? UniversalTestID { get; set; }

    public OrderPriorityEnum Priority { get; set; }

    public string? SpecimenCollectionDateTime { get; set; }

    public ActionCodeEnum ActionCode { get; set; }

    public ReportTypesEnum ReportTypes { get; set; }
}
