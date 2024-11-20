using System.ComponentModel;

namespace ASTM.Layers.Record.TestOrder;

public enum ActionCodeEnum
{
    [Description("X")]
    MeasuredUpload,

    [Description("N")]
    NewSampleOrderDownload,

    [Description ("X\\Q")]
    ControlSampleUpload,

    [Description("Q")]
    ControlSampleDownload,

    [Description("A")]
    AdditionalTestOrderUpload
}
