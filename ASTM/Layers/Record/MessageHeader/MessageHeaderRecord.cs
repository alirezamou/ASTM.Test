namespace ASTM.Layers.Record.MessageHeader;

public class MessageHeaderRecord : IRecord
{
    public char RecordTypeId { get; } = RecordType.MessageHeader;

    public static readonly string DelimeterDefinition = $"{Delimeters.Field}{Delimeters.Repeat}{Delimeters.Component}{Delimeters.Escape}";

    public string SenderNameOrId { get; set; } = string.Empty;

    public string ReceiverId { get; set; } = string.Empty;

    public string CommentOrSpecialInstructions { get; set; } = string.Empty;

    public ProcessingIDEnum ProcessingId { get; set; }

    public int VersionNo { get; set; }

    public string MessageDateTime { get; set; } = string.Empty;

    public MessageHeaderRecord(string record) { }

    // don't know what type of record
    public void parse(string record)
    {
        var fields = record.Split();

        for (int i = 0; i < fields.Length; i++) {
            // if field is empty continue

            // if not then:
                // grab all instance properties (DONT KNOW WHICH INSTANCE)
                // find a property that its field index === i
                // convert field to property type (ST, CM, NM, TS, TX... etc.)
                // assign converted field string to propery value

            if (string.IsNullOrEmpty(fields[i])) continue;

            var properties = this.GetType().GetProperties();

            properties.FirstOrDefault(p =>
            {
                var fieldIndexAttribute = p
                .GetCustomAttributes(false)
                .FirstOrDefault(a =>
                {

                    return true;
                });

                return true;
            });
        }
    }

    public Dictionary<string, int> PropertyIndexMapping { get; set; }
}
