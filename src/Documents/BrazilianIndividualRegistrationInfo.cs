namespace GeekSevenLabs.Utilities.Documents;

public readonly struct BrazilianIndividualRegistrationInfo
{
    public required string Value { get; init; }
    public required bool IsValid { get; init; }

    public required int FirstVerificationDigit { get; init; }
    public required int SecondVerificationDigit { get; init; }
    public required int Region { get; init; }
}