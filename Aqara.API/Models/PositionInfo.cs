using System.Text;

namespace Aqara.API.Models;

/// <summary>Описание места расположения устройств</summary>
public class PositionInfo
{
    /// <summary>Идентификатор</summary>
    public string PositionId { get; init; } = null!;

    /// <summary>Идентификатор родительского места расположения</summary>
    public string? ParentPositionId { get; init; }

    /// <summary>Название</summary>
    public string Name { get; init; } = null!;

    /// <summary>Описание</summary>
    public string Description { get; init; } = null!;

    /// <summary>Время создания</summary>
    public DateTime CreateTime { get; init; }

    public override string ToString() => (Description is { Length: > 0 } description
            ? new StringBuilder(PositionId).Append(':').Append(Name).Append(':').Append(description)
            : new StringBuilder(PositionId).Append(':').Append(Name))
       .ToString();
}