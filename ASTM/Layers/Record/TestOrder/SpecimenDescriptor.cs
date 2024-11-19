namespace ASTM.Layers.Record.TestOrder;

public static class SpecimenDescriptor {
    public static SpecimenDescriptorEnum BloodSerum { get; }  = SpecimenDescriptorEnum.BloodSerum;
    public static SpecimenDescriptorEnum Urine { get; }  = SpecimenDescriptorEnum.Urine;
    public static SpecimenDescriptorEnum Others { get; }  = SpecimenDescriptorEnum.Others;
}

public enum SpecimenDescriptorEnum
{
    None,
    BloodSerum = 1,
    Urine = 2,
    Others = 5
}