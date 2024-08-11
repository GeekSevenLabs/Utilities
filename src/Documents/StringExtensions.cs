namespace GeekSevenLabs.Utilities.Documents;

internal static class StringExtensions
{
    public static string RemoveNonNumericCharacters(this string value)
    {
        return string.IsNullOrWhiteSpace(value) ? string.Empty : new string(value.Where(char.IsDigit).ToArray());
    }
}