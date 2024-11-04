namespace GeekSevenLabs.Utilities.Documents;

public record CnpjPatternMatchResult(bool IsValid, bool IsMasked)
{
    internal static CnpjPatternMatchResult Invalid => new(false, false);
    internal static CnpjPatternMatchResult ValidUnmasked => new(true, false);
    internal static CnpjPatternMatchResult ValidMasked => new(true, true);
    
    public bool IsValidUnmasked => IsValid && !IsMasked;
    public bool IsValidMasked => IsValid && IsMasked;
}