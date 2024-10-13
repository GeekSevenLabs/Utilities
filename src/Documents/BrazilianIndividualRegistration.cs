namespace GeekSevenLabs.Utilities.Documents;

/// <summary>
/// Individual Registration (CPF) is a document used to identify a citizen in Brazil.
/// </summary>
public static class BrazilianIndividualRegistration
{
    private static readonly int[] Multiplier = [10, 9, 8, 7, 6, 5, 4, 3, 2];
    private const int MaxLength = 11;

    /// <summary>
    /// Check if the individual registration (CPF) is valid.
    /// </summary>
    /// <param name="individualRegistration">CPF: ex 000.000.000-00 or 00000000000</param>
    /// <returns>True when individual registration (CPF) is valid</returns>
    public static bool IsValid(string individualRegistration)
    {
        if (string.IsNullOrWhiteSpace(individualRegistration))
        {
            return false;
        }

        individualRegistration = individualRegistration.RemoveNonNumericCharacters();

        if (individualRegistration.Length is not MaxLength)
        {
            return false;
        }

        var firstVerificationDigit = CalculateFirstVerificationDigit(individualRegistration);
        var secondVerificationDigit = CalculateSecondVerificationDigit(individualRegistration);

        return firstVerificationDigit == ParseDigit(individualRegistration[9]) &&
               secondVerificationDigit == ParseDigit(individualRegistration[10]);
    }

    /// <summary>
    /// Generate a random individual registration (CPF).
    /// </summary>
    /// <param name="region"> CPF region code (one digit) Ex: 4. </param>
    /// <param name="formatted"> Format the individual registration (CPF) Ex: 000.000.000-00. </param>
    /// <returns> A random individual registration (CPF). </returns>
    public static string Generate(int? region = null, bool formatted = false)
    {
        string individualRegistration;

        if (region.HasValue)
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(region.Value, 9);
            individualRegistration = Random.Shared.Next(10000000, 99999999).ToString() + region;
        }
        else
        {
            individualRegistration = Random.Shared.Next(100000000, 999999999).ToString();
        }

        individualRegistration += CalculateFirstVerificationDigit(individualRegistration);
        individualRegistration += CalculateSecondVerificationDigit(individualRegistration);

        return formatted ? Format(individualRegistration) : individualRegistration;
    }

    /// <summary>
    /// Format the individual registration (CPF) Ex: 000.000.000-00.
    /// </summary>
    /// <param name="individualRegistration"> Individual registration (CPF) Ex: 00000000000. </param>
    /// <returns> Formatted individual registration (CPF) Ex: 000.000.000-00. </returns>
    public static string Format(string individualRegistration)
    {
        return individualRegistration.Length is MaxLength
            ? individualRegistration.Insert(3, ".").Insert(7, ".").Insert(11, "-")
            : individualRegistration;
    }

    /// <summary>
    /// Get information about the individual registration (CPF).
    /// </summary>
    /// <param name="individualRegistration"> Individual registration (CPF) Ex: 00000000000 or 000.000.000-00. </param>
    /// <returns> Information about the individual registration (CPF). </returns>
    public static BrazilianIndividualRegistrationInfo GetInfo(string individualRegistration)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(individualRegistration);

        individualRegistration = individualRegistration.RemoveNonNumericCharacters();
        ArgumentOutOfRangeException.ThrowIfNotEqual(individualRegistration.Length, MaxLength);

        var firstVerificationDigit = CalculateFirstVerificationDigit(individualRegistration);
        var secondVerificationDigit = CalculateSecondVerificationDigit(individualRegistration);

        var isValid = firstVerificationDigit == ParseDigit(individualRegistration[9]) &&
                      secondVerificationDigit == ParseDigit(individualRegistration[10]);

        return new BrazilianIndividualRegistrationInfo
        {
            Value = individualRegistration,
            IsValid = isValid,
            FirstVerificationDigit = firstVerificationDigit,
            SecondVerificationDigit = secondVerificationDigit,
            Region = ParseDigit(individualRegistration[8])
        };
    }

    private static int CalculateFirstVerificationDigit(string individualRegistration)
    {
        var sum = Multiplier.Select((mult, index) => ParseDigit(individualRegistration[index]) * mult).Sum();

        var rest = sum % 11;
        return rest < 2 ? 0 : 11 - rest;
    }

    private static int CalculateSecondVerificationDigit(string individualRegistration)
    {
        var sum = Multiplier.Select((mult, index) => ParseDigit(individualRegistration[index + 1]) * mult).Sum();

        var rest = sum % 11;
        return rest < 2 ? 0 : 11 - rest;
    }

    private static int ParseDigit(char digit) => int.Parse(digit.ToString());
}