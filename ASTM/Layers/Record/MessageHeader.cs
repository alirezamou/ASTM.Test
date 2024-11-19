namespace ASTM.Layers.Record;

public class MessageHeader : IRecord
{
    [Field(FieldPosition = 1, OrderNo = 1, Type = FieldType.ST, Max = 1, EV = FieldEffectivity.X, CV = FieldEffectivity.R, FieldName = "Record Type ID")]
    public char RecordTypeId = RecordType.MessageHeader;

    [Field(FieldPosition = 2, OrderNo = 2, Type = FieldType.ST, Max = 4, EV = FieldEffectivity.X, CV = FieldEffectivity.R, FieldName = "Delimiter Definition")]
    public static readonly string DelimeterDefinition = $"{Delimeters.Field}{Delimeters.Repeat}{Delimeters.Component}{Delimeters.Escape}";

    [Field(FieldPosition = 3, FieldName = "Message Control ID")]
    public string MessageControlId { get; set; } = string.Empty;

    [Field(FieldPosition = 4, FieldName = "Access Password")]
    public string AccessPassword { get; set; } = string.Empty;

    [Field(FieldPosition = 5, OrderNo = 3, Type = FieldType.CM, Max = 36, EV = FieldEffectivity.X, CV = FieldEffectivity.R, FieldName = "Sender Name or ID")]
    [SubField(Type = FieldType.TX, Max = 30, Comment = "Sender's device name")]
    [SubField(Type = FieldType.NM, Max = 5, Comment = "Communication program version")]
    public string SenderNameOrId { get; set; } = string.Empty;

    [Field(FieldPosition = 6, FieldName = "Sender Street Address")]
    public string SenderStreetAddress { get; set; } = string.Empty;

    [Field(FieldPosition = 7, FieldName = "Reserved Field")]
    public string ReservedField { get; set; } = string.Empty;

    [Field(FieldPosition = 8, FieldName = "Sender Telephone Number")]
    public string SenderTelephoneNumber { get; set; } = string.Empty;

    [Field(FieldPosition = 9, FieldName = "Characteritics of Sender")]
    public string CharacteristicsOfSender { get; set; } = string.Empty;

    [Field(FieldPosition = 10, Type = FieldType.ST, Max = 30, CV = FieldEffectivity.X, FieldName = "Receiver ID")]
    public string ReceiverId { get; set; } = "host";

    [Field(FieldPosition = 11, OrderNo = 5, FieldName = "Comment or Special Instructions")]
    [SubField(Type = FieldType.ST, Max = 5, Comment = "Meaning of message")]
    [SubField(Type = FieldType.ST, Max = 5, Comment = "Cause of message")]
    public string CommentOrSpecialInstructions { get; set; } = string.Empty;

    [Field(FieldPosition = 12, OrderNo = 6, Type = FieldType.ST, Max = 1, EV = FieldEffectivity.X, CV = FieldEffectivity.R, FieldName = "Processing ID")]
    public string ProcessingId { get; set; } = "p";

    [Field(FieldPosition = 13, OrderNo = 7, Type = FieldType.NM, Max = 1, CV = FieldEffectivity.R, FieldName = "Version No.")]
    public string VersionNo { get; set; } = "1";

    [Field(FieldPosition = 14, FieldName = "Date and Time of Message")]
    public string DateAndTimeOfMessage { get; set; } = string.Empty;
}
