namespace ASTM.Layers.Record.TestOrder;

public static class ReportTypes
{
    public static ReportTypesEnum TestOrderDownload { get; } = ReportTypesEnum.O;
    public static ReportTypesEnum CommunicationOfResultUpload { get; } = ReportTypesEnum.F;
}

public enum ReportTypesEnum { 
    None,
    /// <summary>
    /// Download
    /// </summary>
    O,
    /// <summary>
    /// Upload
    /// </summary>
    F
}
