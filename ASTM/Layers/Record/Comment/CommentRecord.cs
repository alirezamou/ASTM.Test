namespace ASTM.Layers.Record.Comment;

public class CommentRecord
{
    public char RecordTypeId { get; } = RecordType.Comment;

    public int SequenceNumber { get; set; }

    public string CommentSource { get; set; } = string.Empty;

    public CommentText? CommentText { get; set; }

    public string CommentType { get; set; } = string.Empty;
}
