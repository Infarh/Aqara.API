namespace Aqara.API.Models;

public class StatisticValueInfo
{
    public string DeviceId { get; init; } = null!;

    public string FeatureId { get; init; } = null!;

    public double Value { get; init; }

    public DateTime? Time { get; init; }

    public DateTime StartTime { get; init; }

    public DateTime EndTime { get; init; }

    public StatisticValueType ValueType { get; init; }

    public override string ToString() => $"{DeviceId}({FeatureId}:{ValueType})[{Time:dd.MM.yy HH:mm:ss}] {Value}";
}

public enum StatisticValueType
{
    Difference, 
    Min,
    Max,
    Average,
    Frequency
}
