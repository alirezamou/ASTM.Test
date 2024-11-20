namespace ASTM.Layers.Record.RequestInformation;

public class StartingRangeIdNumber : CompositeField
{

    public string SampleID { get; set; } = string.Empty;

    public int SequenceNo { get; set; }

    public string CarrierNo { get; set; } = string.Empty;

    public int PositionNo { get; set; }

    public SampleTypeEnum SampleType { get; set; }

    public ContainerTypeEnum ContainerType { get; set; }
}
