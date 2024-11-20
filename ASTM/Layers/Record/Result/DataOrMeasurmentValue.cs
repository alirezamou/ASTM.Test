namespace ASTM.Layers.Record.Result;

public class DataOrMeasurmentValue : CompositeField
{
    public string MeasurmentValue { get; set; } = string.Empty;

    public string CutOffIndex { get; set; } = string.Empty;
}