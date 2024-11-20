using System.ComponentModel;

namespace ASTM.Layers.Record.Result;

public enum ResultAbnormalFlagsEnum
{
    None,

    [Description("L")]
    LessThanNormalRange,

    [Description("H")]
    MoreThanNormalRange,

    [Description("<")]
    LessThanMeasuredRange,

    [Description(">")]
    MoreThanMeasuredRange,

    [Description("N")]
    Normal,

    [Description("A")]
    Abnormal
}