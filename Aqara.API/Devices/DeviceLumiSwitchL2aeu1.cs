using System.Diagnostics.Contracts;
using Aqara.API.Models;

namespace Aqara.API.Devices;

/// <summary>Настенный выключатель с двумя клавишами</summary>
public class DeviceLumiSwitchL2aeu1 : Device
{
    public const string DeviceModelId = "lumi.switch.l2aeu1";

    internal DeviceLumiSwitchL2aeu1(IDeviceManager Manager, DeviceInfo Info) : base(Manager, Info)
    {
        if (Info.ModelId != DeviceModelId)
            throw new InvalidOperationException($"Указан некорректный идентификатор модели в конфигурации устройства. Идентификатор модели может быть только {DeviceModelId}, а указан {Info.ModelId}")
            {
                Data = { { "DeviceInfo", Info } }
            };
        Contract.EndContractBlock();
    }

    public override string ToString() => $"{Name}[{Id}]:Настенный выключатель с двумя клавишами";

    [ContractInvariantMethod]
    private void Invariant() => Contract.Invariant(ModelId == DeviceModelId);
}