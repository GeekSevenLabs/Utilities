namespace GeekSevenLabs.Utilities.Documents;

public readonly struct IndividualRegistrationInfo(
    string value,
    bool isValid,
    int firstVerificationDigit,
    int secondVerificationDigit,
    int region)
{
    public string Value { get; } = value;
    public bool IsValid { get; } = isValid;

    public int FirstVerificationDigit { get; } = firstVerificationDigit;
    public int SecondVerificationDigit { get; } = secondVerificationDigit;
    public int Region { get; } = region;
}