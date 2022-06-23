using System.Diagnostics.Contracts;
using Aqara.API.Models;

namespace Aqara.API.Devices;

/// <summary>Датчик открытия дверей/окон на герконе</summary>
public class DeviceLumiSensorMagnetAq2 : Device
{
    public const string DeviceModelId = "lumi.sensor_magnet.aq2";

    internal DeviceLumiSensorMagnetAq2(IDeviceManager Manager, DeviceInfo Info) : base(Manager, Info)
    {
        if (Info.ModelId != DeviceModelId)
            throw new InvalidOperationException($"Указан некорректный идентификатор модели в конфигурации устройства. Идентификатор модели может быть только {DeviceModelId}, а указан {Info.ModelId}")
            {
                Data = { { "DeviceInfo", Info } }
            };
        Contract.EndContractBlock();
    }

    public override string ToString() => $"{Name}[{Id}]:Датчик открытия дверей/окон на герконе";

    [ContractInvariantMethod]
    private void Invariant() => Contract.Invariant(ModelId == DeviceModelId);
}