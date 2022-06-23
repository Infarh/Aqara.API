using System.Collections.Immutable;
using Aqara.API.Models;

namespace Aqara.API.Devices;

public class Feature : IEquatable<Feature>
{
    private static ImmutableDictionary<string, IReadOnlySet<Feature>> __Features = ImmutableDictionary<string, IReadOnlySet<Feature>>.Empty;

    public static IReadOnlySet<Feature>? GetModelFeatures(string ModelId) => __Features.TryGetValue(ModelId, out var features) ? features : null;

    public static void SetModelFeatures(string ModelId, IReadOnlySet<Feature> ModelFeatures)
    {
        ImmutableInterlocked.AddOrUpdate(ref __Features, ModelId, ModelFeatures, (_, set) => set);
    }

    private readonly DeviceFeatureInfo _Info;

    public string Id => _Info.FeatureId;

    public string Name => _Info.Name;

    public string Description => _Info.Description;

    public Feature(DeviceFeatureInfo Info) => _Info = Info;

    public override int GetHashCode() => Id.GetHashCode();

    public bool Equals(Feature? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Feature)obj);
    }

    public override string ToString() => $"{Id}:{Description}";

    public static bool operator ==(Feature? left, Feature? right) => Equals(left, right);
    public static bool operator !=(Feature? left, Feature? right) => !Equals(left, right);
}