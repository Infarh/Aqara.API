namespace Aqara.API;

public static class Features
{
    /// <summary>Значение температуры термометра (значения t*1000)</summary>
    public const string Temperature = "0.1.85";

    /// <summary>Состояние выключателя (Значения 0/1)</summary>
    public const string SwitchState = "4.1.85";
}

public static class DeviceModels
{
    /// <summary>Комнатный термометр (<see cref="Features.Temperature"/>, давление, влажность)</summary>
    public const string Thermometer = "lumi.weather.v1";

    /// <summary>Настенный выключатель с двумя клавишами (<see cref="Features.SwitchState"/>)</summary>
    public const string WallSwitch2 = "lumi.switch.l1aeu1";
}
