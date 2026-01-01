namespace Aqara.API;

/// <summary>Идентификаторы моделей устройств</summary>
public static class DeviceModels
{
    /// <summary>Комнатный термометр (<see cref="Features.Temperature"/>, давление, влажность)</summary>
    public const string Thermometer = "lumi.weather.v1";

    /// <summary>Настенный выключатель с двумя клавишами (<see cref="Features.SwitchState"/>)</summary>
    public const string WallSwitch2 = "lumi.switch.l1aeu1";
}
