namespace ASTM.Layers.Record.Result;

public static class ResultStatus {
    public static ResultStatusEnum InitialResult { get; } = ResultStatusEnum.F;
    public static ResultStatusEnum RerunResult { get; } = ResultStatusEnum.C;
}

public enum ResultStatusEnum
{
    None,
    /// <summary>
    /// Initial Result
    /// </summary>
    F,
    /// <summary>
    /// Rerun result
    /// </summary>
    C
}
