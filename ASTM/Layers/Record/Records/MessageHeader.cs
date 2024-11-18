namespace ASTM.Layers.Record.Records;

internal class MessageHeader
{
    public static readonly char RecordTypeId = 'H';
    public string DelimeterDefinition { get; set; } = $"{Delimeters.Field}{Delimeters.Repeat}{Delimeters.Component}{Delimeters.Escape}";
    public string MessageControlId { get; set; } = string.Empty;
    public string AccessPassword { get; set; } = string.Empty;
    public string SenderNameOrId { get; set; } = string.Empty;
    public string SenderStreetAddress { get; set; } = string.Empty;
    public string ReservedField { get; set; } = string.Empty;
    public string SenderTelephoneNumber { get; set; } = string.Empty;
    public string CharacteristicsOfSender { get; set; } = string.Empty;
    public string ReceiverId { get; set; } = "host";
    public string CommentOrSpecialInstructions { get; set; } = string.Empty;
    public string ProcessingId { get; set; } = "p";
    public string VersionNo { get; set; } = "1";
    public string DateAndTimeOfMessage { get; set; } = string.Empty;
}
