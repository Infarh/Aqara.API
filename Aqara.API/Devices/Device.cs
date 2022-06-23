using Aqara.API.Models;

namespace Aqara.API.Devices;

public class Device : IEquatable<Device>, IEquatable<DeviceInfo>, IDisposable
{
    public static Device Create(IDeviceManager Manager, DeviceInfo info) => info.ModelId switch
    {
        DeviceLumiWeatherV1.DeviceModelId => new DeviceLumiWeatherV1(Manager, info),
        DeviceLumiSwitchL1aeu1.DeviceModelId => new DeviceLumiSwitchL1aeu1(Manager, info),
        DeviceLumiSwitchL2aeu1.DeviceModelId => new DeviceLumiSwitchL2aeu1(Manager, info),
        DeviceLumiAirmonitorAcn01.DeviceModelId => new DeviceLumiAirmonitorAcn01(Manager, info),
        DeviceLumiPlugMaeu01.DeviceModelId => new DeviceLumiPlugMaeu01(Manager, info),
        DeviceLumiSensorMotionAq2.DeviceModelId => new DeviceLumiSensorMotionAq2(Manager, info),
        DeviceLumiSensorMagnetAq2.DeviceModelId => new DeviceLumiSensorMagnetAq2(Manager, info),
        DeviceLumiRemoteB1acn01.DeviceModelId => new DeviceLumiRemoteB1acn01(Manager, info),
        DeviceLumiSensorWleakAq1.DeviceModelId => new DeviceLumiSensorWleakAq1(Manager, info),
        DeviceLumiGatewayIragl7.DeviceModelId => new DeviceLumiGatewayIragl7(Manager, info),
        _ => new(Manager, info)
    };

    private IDeviceManager? _Manager;

    private readonly DeviceInfo _Info;

    public string Id => _Info.Id;

    public string Name => _Info.Name;

    public string ModelId => _Info.ModelId;

    protected IDeviceManager Manager => _Manager 
        ?? throw new ObjectDisposedException(GetType().ToString(), "Устройство было уничтожено (отключено от менеджера устройств)");

    protected Device(IDeviceManager Manager, DeviceInfo Info)
    {
        _Manager = Manager;
        _Info = Info;
    }

    public bool Equals(Device? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (!ReferenceEquals(_Manager, other._Manager)) return false;
        return _Info.Id.Equals(other._Info.Id, StringComparison.OrdinalIgnoreCase);
    }

    public bool Equals(DeviceInfo? other) => other is { Id: var id } && _Info.Id.Equals(id, StringComparison.OrdinalIgnoreCase);

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Device)obj);
    }

    public async ValueTask<IReadOnlySet<Feature>> GetFeatures(CancellationToken Cancel = default)
    {
        var model_id = ModelId;
        if (Feature.GetModelFeatures(model_id) is not { } features)
        {
            var api_features = await Manager.API.GetDeviceModelFeatures(model_id, Cancel: Cancel).ConfigureAwait(false);
            features = new HashSet<Feature>(api_features.Select(f => new Feature(f)));
            Feature.SetModelFeatures(model_id, features);
        }

        return features;
    }

    public override int GetHashCode() => HashCode.Combine(Manager, _Info);

    public static bool operator ==(Device? left, Device? right) => Equals(left, right);
    public static bool operator !=(Device? left, Device? right) => !Equals(left, right);

    void IDisposable.Dispose() => _Manager = null!;
}