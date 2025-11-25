namespace Store.Shared;

public static class StringExtensions
{
    public static bool IsEmpty(this string? value)
        => string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);

    public static bool IsEqualTo(this string? actual, string? expected)
        => actual?.ToLower() == expected?.ToLower();

    public static bool IsNotEqualTo(this string? actual, string? expected)
        => !actual.IsEqualTo(expected);
}