using System.ComponentModel;

namespace ASTM.Layers.Record.TestOrder;

public enum OrderPriorityEnum
{
    [Description("R")]
    RoutineSample,

    [Description("S")]
    StatSample,
}