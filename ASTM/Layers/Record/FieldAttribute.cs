namespace ASTM.Layers.Record;

internal class FieldAttribute : Attribute
{
    public int FieldPosition { get; set; }
    public int OrderNo { get; set; }
    public string FieldName { get; set; } = string.Empty;
    public FieldType Type { get; set; }
    public int Max { get; set; }
    public FieldEffectivity EV { get; set; }
    public FieldEffectivity CV { get; set; }
    public string Comment { get; set; } = string.Empty;
}

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
internal class SubFieldAttribute : FieldAttribute { }
