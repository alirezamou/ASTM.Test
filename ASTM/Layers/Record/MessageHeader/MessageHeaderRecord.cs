namespace ASTM.Layers.Record.MessageHeader;

public class MessageHeaderRecord : IRecord
{
    public char RecordTypeId { get; } = RecordType.MessageHeader;

    public static readonly string DelimeterDefinition = $"{Delimeters.Field}{Delimeters.Repeat}{Delimeters.Component}{Delimeters.Escape}";

    public string MessageControlId { get; set; } = string.Empty;

    public string AccessPassword { get; set; } = string.Empty;

    public CompositeField? SenderNameOrId { get; set; }

    public string SenderStreetAddress { get; set; } = string.Empty;

    public string ReservedField { get; set; } = string.Empty;

    public string SenderTelephoneNumber { get; set; } = string.Empty;

    public string CharacteristicsOfSender { get; set; } = string.Empty;

    public string ReceiverId { get; set; } = string.Empty;

    public string CommentOrSpecialInstructions { get; set; } = string.Empty;

    public ProcessingIDEnum ProcessingId { get; set; }

    public int VersionNo { get; set; }

    public string MessageDateTime { get; set; } = string.Empty;
}
