namespace ASTM.Layers.Record.Result;

public static class ResultAbnormalFlags
{
    public static ResultAbnormalFlagsEnum LessThanNormalRange { get; set; } = ResultAbnormalFlagsEnum.L;
    public static ResultAbnormalFlagsEnum MoreThanNormalRange { get; set; } = ResultAbnormalFlagsEnum.H;
    public static ResultAbnormalFlagsEnum LessThanMeasuredRange { get; set; } = ResultAbnormalFlagsEnum.LL;
    public static ResultAbnormalFlagsEnum MoreThanMeasuredRange { get; set; } = ResultAbnormalFlagsEnum.HH;
    public static ResultAbnormalFlagsEnum Normal { get; set; } = ResultAbnormalFlagsEnum.N;
    public static ResultAbnormalFlagsEnum Abnormal { get; set; } = ResultAbnormalFlagsEnum.A;
}

public enum ResultAbnormalFlagsEnum
{
    None,
    /// <summary>
    /// Less than Normal Range
    /// </summary>
    L,
    /// <summary>
    /// More than normal range
    /// </summary>
    H,
    /// <summary>
    /// Less than measured range
    /// </summary>
    LL,
    /// <summary>
    /// More than measured range
    /// </summary>
    HH,
    /// <summary>
    /// Normal
    /// </summary>
    N,
    /// <summary>
    /// Abnormal
    /// </summary>
    A
}
