using System.Text;

namespace Aqara.API.Models;

public class PositionInfo
{
    public string PositionId { get; init; } = null!;

    public string? ParentPositionId { get; init; }

    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;

    public DateTime CreationTime { get; init; }

    public override string ToString() => (Description is { Length: > 0 } description
            ? new StringBuilder(PositionId).Append(':').Append(Name).Append(':').Append(description)
            : new StringBuilder(PositionId).Append(':').Append(Name))
       .ToString();
}