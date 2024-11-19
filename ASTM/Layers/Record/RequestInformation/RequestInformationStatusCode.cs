namespace ASTM.Layers.Record.RequestInformation;

public static class RequestInformationStatusCode
{
    public static readonly RequestInformationStatusCodeEnum CancelRequest = RequestInformationStatusCodeEnum.A;
    public static readonly RequestInformationStatusCodeEnum OrderQuery = RequestInformationStatusCodeEnum.A;
}

public enum RequestInformationStatusCodeEnum
{
    /// <summary>
    /// Cancel the last request (to host)
    /// </summary>
    A,
    /// <summary>
    /// Order Query (to host)
    /// </summary>
    O
}