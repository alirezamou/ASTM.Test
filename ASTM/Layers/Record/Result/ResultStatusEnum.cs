using System.ComponentModel;

namespace ASTM.Layers.Record.Result;

public enum ResultStatusEnum
{
    None,

    [Description("F")]
    LastResult,

    [Description("X")]
    ResultsCannotBeDone,

    [Description("R")]
    ResultCommunicated,

    [Description("V")]
    ReleasedResultByUser,

    [Description("Y")]
    BlockedBySystem,

    [Description("+")]
    BlockedByUser,
}
