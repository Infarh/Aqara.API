namespace Aqara.API.Models;

public class DeviceFeatureInfo
{
    public string ResourceId { get; init; } = null!;

    public string Name { get; init; } = null!;

    public string NameEn { get; init; } = null!;

    public string Description { get; init; } = null!;

    public string DescriptionEn { get; init; } = null!;

    public long MinValue { get; init; }

    public long MaxValue { get; init; }

    public int? Unit { get; init; }

    public string DefaultValue { get; init; } = null!;

    public string SubjectModel { get; init; } = null!;

    /// <summary>Permissions (0-readable, 1-writable, 2-readable/writable)</summary>
    public DeviceFeatureAccess Access { get; init; }

    public string Server { get; init; } = null!;

    public string Enums { get; init; } = null!;

    public override string ToString() => $"{ResourceId}:{Name}[{MinValue}:{MaxValue}]:{Access}";
}

public enum DeviceFeatureAccess
{
    Read,
    Write,
    ReadWrite,
}