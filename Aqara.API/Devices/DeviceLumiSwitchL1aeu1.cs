using System.Diagnostics.Contracts;
using Aqara.API.Models;

namespace Aqara.API.Devices;

/// <summary>Настенный выключатель с одной клавишей</summary>
public class DeviceLumiSwitchL1aeu1 : Device
{
    public const string DeviceModelId = "lumi.switch.l1aeu1";

    internal DeviceLumiSwitchL1aeu1(IDeviceManager Manager, DeviceInfo Info) : base(Manager, Info)
    {
        if (Info.ModelId != DeviceModelId)
            throw new InvalidOperationException($"Указан некорректный идентификатор модели в конфигурации устройства. Идентификатор модели может быть только {DeviceModelId}, а указан {Info.ModelId}")
            {
                Data = { { "DeviceInfo", Info } }
            };
        Contract.EndContractBlock();
    }

    public override string ToString() => $"{Name}[{Id}]:Настенный выключатель с одной клавишей";

    [ContractInvariantMethod]
    private void Invariant() => Contract.Invariant(ModelId == DeviceModelId);
}