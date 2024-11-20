using System.ComponentModel;

namespace ASTM.Layers.Record.TestOrder;

public enum SampleTypeEnum
{
    [Description("SAMPLE")]
    PatientSample,

    [Description("CONTROL")]
    ControlSample,
}
