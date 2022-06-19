using System.ComponentModel.DataAnnotations;
using EnumFastToStringGenerated;

namespace Aqara.API.Models;

public class DeviceInfo
{
    public string Id { get; init; } = null!;

    public string ParentId { get; init; } = null!;

    public string PositionId { get; init; } = null!;

    public DateTime CreateTime { get; init; }

    public string TimeZone { get; init; } = null!;

    public string Model { get; init; } = null!;

    public DateTime UpdateTime { get; init; }

    public DeviceModelType ModelType { get; init; }

    public bool OnlineState { get; init; }

    public string FirmwareVersion { get; init; } = null!;

    public string DeviceName { get; init; } = null!;

    public override string ToString() => $"{Id}:{DeviceName}(model:{Model})";
}

/// <summary>Вид устройства</summary>
[EnumGenerator]
public enum DeviceModelType
{
    /// <summary>Шлюз с дочерними устройствами</summary>
    [Display(Name = "Шлюз с дочерними устройствами")]
    GatewayWithChilds = 1,

    /// <summary>Шлюз без дочерних устройств</summary>
    [Display(Name = "Шлюз без дочерних устройств")]
    GatewayWithoutChilds = 2,

    /// <summary>Обычное устройство</summary>
    [Display(Name = "Обычное устройство")]
    SubDevice = 3,
}
