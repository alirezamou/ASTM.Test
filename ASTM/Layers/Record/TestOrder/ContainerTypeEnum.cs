using System.ComponentModel;

namespace ASTM.Layers.Record.TestOrder;

public enum ContainerTypeEnum
{
    [Description("NORMAL")]
    TestTubeOrSampleCup,

    [Description("REDUCED")]
    SampleCupOnly,
}
