namespace ASTM.Layers.Record.Result;

public class UniversalTestID : CompositeField
{
    public string ApplicationCode { get; set; } = string.Empty;

    public int Dilution { get; set; }

    public PreDilutionEnum PreDilution { get; set; }
}
