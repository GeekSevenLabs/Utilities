namespace GeekSevenLabs.Utilities.Documents;

public record CnpjPatternMatchResult(bool IsValid, bool IsMasked, bool WithLetters)
{
    internal static CnpjPatternMatchResult Invalid => new(false, false, false);
    internal static CnpjPatternMatchResult ValidUnmasked => new(true, false, false);
    internal static CnpjPatternMatchResult ValidUnmaskedWithLetters => new(true, false, true);
    internal static CnpjPatternMatchResult ValidMasked => new(true, true, false);
    internal static CnpjPatternMatchResult ValidMaskedWithLetters => new(true, true, true);
    
    public bool IsValidUnmasked => IsValid && !IsMasked && !WithLetters;
    public bool IsValidUnmaskedWithLetters => IsValid && !IsMasked && WithLetters;
    public bool IsValidMasked => IsValid && IsMasked && !WithLetters;
    public bool IsValidMaskedWithLetters => IsValid && IsMasked && WithLetters;
}