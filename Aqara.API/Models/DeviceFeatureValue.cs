namespace Aqara.API.Models;

public class DeviceFeatureValue
{
    public string DeviceId { get; init; } = null!;

    public string FeatureId { get; init; } = null!;

    public DateTime Time { get; init; }

    public double Value { get; init; }

    public override string ToString() => $"{DeviceId}({FeatureId})[{Time:dd.MM.yy HH:mm:ss}]:{Value}";
}
