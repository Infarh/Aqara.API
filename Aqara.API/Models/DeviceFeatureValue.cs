namespace Aqara.API.Models;

public class DeviceFeatureValue
{
    public string DeviceId { get; init; }

    public string FeatureId { get; init; }

    public DateTime Time { get; init; }

    public double Value { get; init; }

    public override string ToString() => $"{DeviceId}({FeatureId})[{Time:dd.MM.yy HH:mm:ss}]:{Value}";
}
