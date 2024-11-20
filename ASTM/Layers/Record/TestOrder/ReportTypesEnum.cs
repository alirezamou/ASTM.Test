using System.ComponentModel;

namespace ASTM.Layers.Record.TestOrder;

public enum ReportTypesEnum {

    [Description("Q")]
    ResponseToInquiryDownload,

    [Description("Z")]
    NoResponseRequestToInquiry,

    [Description("O")]
    Upload,
}
