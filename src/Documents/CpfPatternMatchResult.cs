namespace GeekSevenLabs.Utilities.Documents;

public record CpfPatternMatchResult(bool IsValid, bool IsMasked)
{
    internal static CpfPatternMatchResult Invalid => new(false, false);
    internal static CpfPatternMatchResult ValidUnmasked => new(true, false);
    internal static CpfPatternMatchResult ValidMasked => new(true, true);
    
    public bool IsValidUnmasked => IsValid && !IsMasked;
    public bool IsValidMasked => IsValid && IsMasked;
}