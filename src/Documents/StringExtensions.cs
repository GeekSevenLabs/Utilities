namespace GeekSevenLabs.Utilities.Documents;

internal static class StringExtensions
{
    public static string RemoveNonNumericCharacters(this string value)
    {
        return string.IsNullOrWhiteSpace(value) ? string.Empty : new string(value.Where(char.IsDigit).ToArray());
    }
    
    public static string RemoveNonAlphabeticOrNumericCharacters(this string value)
    {
        return string.IsNullOrWhiteSpace(value) ? string.Empty : new string(value.Where(char.IsLetterOrDigit).ToArray());
    }
    
    public static string RemoveTokens(this string value, params string[] tokens)
    {
        return string.IsNullOrWhiteSpace(value) ? string.Empty : tokens.Aggregate(value, (current, token) => current.Replace(token, string.Empty));
    }
    
    public static bool AllCharactersAreEqual(this string value)
    {
        return value.Distinct().Count() == 1;
    }
}