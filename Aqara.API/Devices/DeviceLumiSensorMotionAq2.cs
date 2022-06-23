using System.Diagnostics.Contracts;
using Aqara.API.Models;

namespace Aqara.API.Devices;

/// <summary>Датчик движения и освещённости</summary>
public class DeviceLumiSensorMotionAq2 : Device
{
    public const string DeviceModelId = "lumi.sensor_motion.aq2";

    internal DeviceLumiSensorMotionAq2(IDeviceManager Manager, DeviceInfo Info) : base(Manager, Info)
    {
        if (Info.ModelId != DeviceModelId)
            throw new InvalidOperationException($"Указан некорректный идентификатор модели в конфигурации устройства. Идентификатор модели может быть только {DeviceModelId}, а указан {Info.ModelId}")
            {
                Data = { { "DeviceInfo", Info } }
            };
        Contract.EndContractBlock();
    }

    public override string ToString() => $"{Name}[{Id}]:Датчик движения и освещённости";

    [ContractInvariantMethod]
    private void Invariant() => Contract.Invariant(ModelId == DeviceModelId);
}
