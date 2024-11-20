using System.ComponentModel;

namespace ASTM.Layers.Record.RequestInformation;

public enum ContainerTypeEnum
{
    [Description("NORMAL")]
    TestTubeOrSampleCup,

    [Description("REDUCED")]
    SampleCupOnly
}