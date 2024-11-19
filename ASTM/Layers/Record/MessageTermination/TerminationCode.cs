namespace ASTM.Layers.Record.MessageTermination;

public static class TerminationCode
{
    public static TerminationCodeEnum Normal { get; } = TerminationCodeEnum.N;
    public static TerminationCodeEnum SystemError { get; } = TerminationCodeEnum.E;
}

public enum TerminationCodeEnum
{
    /// <summary>
    /// Normal End
    /// </summary>
    N,
    /// <summary>
    /// System Error: Receiving Error, hardware error, application error
    /// </summary>
    E
}
