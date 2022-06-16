namespace Aqara.API.Exceptions;

public static class TimeEx
{
    public const long UnixTimeDelta = 621355968000000000;

    public static long ToUnixTicks(long Ticks) => (Ticks - UnixTimeDelta) / 10000;

    public static long FromUnixTicks(long UnixTicks) => UnixTicks * 10000 + UnixTimeDelta;

    public static DateTime UnixTimeFromTicks(long UnixTicks) => new(FromUnixTicks(UnixTicks));

    public static long ToUnixTimeTicks(this DateTime time) => ToUnixTicks(time.Ticks);
}