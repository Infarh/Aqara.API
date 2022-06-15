using System.Text.Json.Serialization;

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

public enum DeviceModelType
{
    GatewayWithChilds = 1,
    GatewayWithoutChilds = 2,
    SubDevice = 3,
}
