using Aqara.API.Devices;

namespace Aqara.API.Models;

/// <summary>Информация об устройстве</summary>
public class DeviceInfo : IEquatable<DeviceInfo>, IEquatable<Device>
{
    /// <summary>Идентификатор</summary>
    public string Id { get; init; } = null!;

    /// <summary>Идентификатор родительского</summary>
    public string ParentId { get; init; } = null!;

    /// <summary>Идентификатор места расположения</summary>
    public string PositionId { get; init; } = null!;

    /// <summary>Время создания</summary>
    public DateTime CreateTime { get; init; }

    /// <summary>Временная зона</summary>
    public string TimeZone { get; init; } = null!;

    /// <summary>Идентификатор модели</summary>
    public string ModelId { get; init; } = null!;

    /// <summary>Время обновления</summary>
    public DateTime UpdateTime { get; init; }

    /// <summary>Тип модели</summary>
    public DeviceModelType ModelType { get; init; }

    /// <summary>Состояние активности</summary>
    public bool OnlineState { get; init; }

    /// <summary>Версия прошивки</summary>
    public string FirmwareVersion { get; init; } = null!;

    /// <summary>Название устройства</summary>
    public string Name { get; init; } = null!;

    public override string ToString() => $"{Id}:{Name}(model:{ModelId})";

    public bool Equals(DeviceInfo? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id.Equals(other.Id, StringComparison.OrdinalIgnoreCase);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DeviceInfo)obj);
    }

    public override int GetHashCode() => Id.GetHashCode();

    //public override int GetHashCode()
    //{
    //    var hash = 0x18d;
    //    foreach (var c in Id)
    //        unchecked
    //        {
    //            hash = (hash * 0x18d) ^ (byte)c;
    //        }

    //    return hash;
    //}

    public bool Equals(Device? other) => other is { Id: var id } && Id.Equals(id, StringComparison.OrdinalIgnoreCase);

    public static bool operator ==(DeviceInfo? left, DeviceInfo? right) => Equals(left, right);

    public static bool operator !=(DeviceInfo? left, DeviceInfo? right) => !Equals(left, right);
}