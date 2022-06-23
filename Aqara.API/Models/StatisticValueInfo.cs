namespace Aqara.API.Models;

/// <summary>Статистическое значение</summary>
public class StatisticValueInfo
{
    /// <summary>Идентификатор устройства</summary>
    public string DeviceId { get; init; } = null!;

    /// <summary>Идентификатор параметра</summary>
    public string FeatureId { get; init; } = null!;

    /// <summary>Значение параметра</summary>
    public double Value { get; init; }

    /// <summary>Время измерения параметра</summary>
    public DateTime? Time { get; init; }

    /// <summary>Время начала интервала времени измерения статистики</summary>
    public DateTime StartTime { get; init; }

    /// <summary>Время конца интервала времени измерения статистики</summary>
    public DateTime EndTime { get; init; }

    /// <summary>Тип значения статистики параметра</summary>
    public StatisticValueType ValueType { get; init; }

    public override string ToString() => $"{DeviceId}({FeatureId}:{ValueType})[{Time:dd.MM.yy HH:mm:ss}] {Value}";
}