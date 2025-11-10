namespace com.github.zehsteam.LocalMultiplayer.Extensions;

internal static class StringExtensions
{
    public static ulong ToUlong(this string value, ulong defaultValue = 0)
    {
        return ulong.TryParse(value, out ulong result) ? result : defaultValue;
    }
}
