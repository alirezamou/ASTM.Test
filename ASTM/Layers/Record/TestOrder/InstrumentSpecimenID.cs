namespace ASTM.Layers.Record.TestOrder;

public class InstrumentSpecimenID : CompositeField
{
    public int SequenceNo { get; set; }

    public string CarrierNo { get; set; } = string.Empty;

    public int PositionNo { get; set; }

    public SampleTypeEnum SampleType { get; set; }

    public ContainerTypeEnum ContainerType { get; set; }
}
