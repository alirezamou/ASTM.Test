using System.ComponentModel;

namespace ASTM.Layers.Record.RequestInformation;

public enum RequestInformationStatusCodeEnum
{
    [Description("O")]
    OrderQuery,

    [Description("A")]
    CancelLastRequest,
}