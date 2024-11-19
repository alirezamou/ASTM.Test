namespace ASTM.Layers.Record.TestOrder;

public static class ActionCode
{
    public static ActionCodeEnum PatientSample { get; } = ActionCodeEnum.N;
    public static ActionCodeEnum ControlSample { get; } = ActionCodeEnum.Q;
    public static ActionCodeEnum AdditionalTestOrder { get; } = ActionCodeEnum.A;
}

public enum ActionCodeEnum
{
    None,
    /// <summary>
    /// Communication of patient sample result from analyzer. 
    /// </summary>
    N,
    /// <summary>
    /// Communication of control sample result from analyzer.
    /// </summary>
    Q,
    /// <summary>
    /// Test order from host
    /// </summary>
    A
}
