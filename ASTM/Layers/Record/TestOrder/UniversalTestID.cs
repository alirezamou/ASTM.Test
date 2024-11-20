namespace ASTM.Layers.Record.TestOrder;

public class UniversalTestID : CompositeField
{
    public string ApplicationCode { get; set; } = string.Empty;

    public int Dilution { get; set; }
}
