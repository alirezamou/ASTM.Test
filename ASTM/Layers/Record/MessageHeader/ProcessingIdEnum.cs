namespace ASTM.Layers.Record.MessageHeader;

public static class ProcessingID {
    public static ProcessingIDEnum Production = ProcessingIDEnum.P;
}

public enum ProcessingIDEnum
{
    None,
    /// <summary>
    /// Production
    /// </summary>
    P
}