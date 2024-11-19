namespace ASTM.Layers.Record.RequestInformation;

public static class UniversalTestID
{
    public static UniversalTestIDEnum All { get; }  = UniversalTestIDEnum.All;
}

public enum UniversalTestIDEnum
{
    None,
    /// <summary>
    /// All
    /// </summary>
    All
}