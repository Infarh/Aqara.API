using System.Diagnostics.Contracts;
using Aqara.API.Models;

namespace Aqara.API.Devices;

/// <summary>Розетка</summary>
public class DeviceLumiPlugMaeu01 : Device
{
    public const string DeviceModelId = "lumi.plug.maeu01";

    internal DeviceLumiPlugMaeu01(IDeviceManager Manager, DeviceInfo Info) : base(Manager, Info)
    {
        if (Info.ModelId != DeviceModelId)
            throw new InvalidOperationException($"Указан некорректный идентификатор модели в конфигурации устройства. Идентификатор модели может быть только {DeviceModelId}, а указан {Info.ModelId}")
            {
                Data = { { "DeviceInfo", Info } }
            };
        Contract.EndContractBlock();
    }

    public override string ToString() => $"{Name}[{Id}]:Розетка";

    [ContractInvariantMethod]
    private void Invariant() => Contract.Invariant(ModelId == DeviceModelId);
}