namespace Aqara.API.Models;

/// <summary>Значение параметра устройства</summary>
public class DeviceFeatureValue
{
    /// <summary>Идентификатор устройства</summary>
    public string DeviceId { get; init; } = null!;

    /// <summary>Идентификатор параметра устройства</summary>
    public string FeatureId { get; init; } = null!;

    /// <summary>Время измерения</summary>
    public DateTime Time { get; init; }

    /// <summary>Значение параметра</summary>
    public double Value { get; init; }

    public override string ToString() => $"{DeviceId}({FeatureId})[{Time:dd.MM.yy HH:mm:ss}]:{Value}";
}
