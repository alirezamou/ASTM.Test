using System.ComponentModel;

namespace ASTM.Layers.Record.MessageTermination;

public enum TerminationCodeEnum
{
    [Description("F")]
    InquiryNormalWithResponseData,

    [Description("I")]
    InquiryNormalWithoutResponseData,

    [Description("Q")]
    InquiryAbnormalAllDataInRecordIsNotDefined,

    [Description("E")]
    InquiryAbnormalSystemError,

    [Description("E")]
    ResponseUploadDownloadAbnormal,

    [Description("E")]
    InvalidRecord,
}
