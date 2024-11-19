namespace ASTM.Layers.Record.TestOrder;

public class TestOrderRecord: IRecord
{
    public char RecordTypeId { get; } = RecordType.TestOrder;

    public int SequenceNumber { get; set; }

    public string? SpecimenID { get; set; }

    public CompositeField? InstrumentSpecimenID { get; set; }

    public CompositeField? UniversalTestID { get; set; }

    public OrderPriorityEnum Priority { get; set; }

    public string? RequestedOrderedDateTime { get; set; }

    public string? SpecimenCollectionDateTime { get; set; }

    public ActionCode ActionCode { get; set; }

    public SpecimenDescriptorEnum SpecimenDescriptor { get; set; }

    public string? DateTimeResultsReportedOrLastModified { get; set; }

    public ReportTypesEnum ReportTypes { get; set; }
}
