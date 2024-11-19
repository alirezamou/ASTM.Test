namespace ASTM.Layers.Record.TestOrder;

public static class OrderPriority
{
    public static OrderPriorityEnum RoutineSample { get; } = OrderPriorityEnum.R;
    public static OrderPriorityEnum StatSample { get; } = OrderPriorityEnum.S;
}

public enum OrderPriorityEnum
{
    None,
    /// <summary>
    /// Routine Sample
    /// </summary>
    R,
    /// <summary>
    /// Stat sample
    /// </summary>
    S,
}