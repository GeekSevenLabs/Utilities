namespace GeekSevenLabs.Utilities.Documents;

/// <summary>
/// National Registry of Legal Entities (CNPJ) is a document used to identify a legal entity in Brazil.
/// </summary>
public static class NationalRegistryLegalEntities
{
    private static readonly int[] Multiplier = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
    private const int MaxLength = 14;
    private static readonly char[] NumbersChars = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];
    private static readonly char[] LettersChars = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];

    /// <summary>
    /// Check if the national registry of legal entities (CNPJ) is valid.
    /// </summary>
    /// <param name="nationalRegistryOfLegalEntities"> CNPJ: ex 00.000.000/0000-00 or 00000000000000 </param>
    /// <returns> True when national registry of legal entities (CNPJ) is valid </returns>
    public static bool IsValid(string nationalRegistryOfLegalEntities)
    {
        if (string.IsNullOrWhiteSpace(nationalRegistryOfLegalEntities))
        {
            return false;
        }
        
        var cleanedNationalRegistryOfLegalEntities = nationalRegistryOfLegalEntities
            .ToUpper()
            .RemoveNonAlphabeticOrNumericCharacters();

        if (cleanedNationalRegistryOfLegalEntities.Length is not MaxLength)
        {
            return false;
        }
        
        if (cleanedNationalRegistryOfLegalEntities[12].IsNotNumber() || 
            cleanedNationalRegistryOfLegalEntities[13].IsNotNumber())
        {
            return false;
        }
        
        var firstVerificationDigit = CalculateFirstVerificationDigit(cleanedNationalRegistryOfLegalEntities);
        var secondVerificationDigit = CalculateSecondVerificationDigit(cleanedNationalRegistryOfLegalEntities);
        
        return firstVerificationDigit == cleanedNationalRegistryOfLegalEntities[12].ToDigit() && 
               secondVerificationDigit == cleanedNationalRegistryOfLegalEntities[13].ToDigit();
    }
    
    /// <summary>
    /// Generate a random national registry of legal entities (CNPJ).
    /// </summary>
    /// <param name="formatted"> Format the national registry of legal entities (CNPJ) Ex: 00.000.000/0000-00. </param>
    /// <param name="useLetters"> Use letters in the national registry of legal entities (CNPJ) Ex: AB.CDE.FGH/IJKL-MN. </param>
    /// <returns></returns>
    public static string Generate(bool formatted = false, bool useLetters = false)
    {
        var nationalRegistryOfLegalEntities = string.Empty;

        if (useLetters)
        {
            var validTokens = NumbersChars.Concat(LettersChars).ToArray();
            for (var i = 1; i <= 12; i++)
            {
                nationalRegistryOfLegalEntities += validTokens[Random.Shared.Next(0, validTokens.Length)];
            }
        }
        else
        {
            nationalRegistryOfLegalEntities += Random.Shared.NextInt64(100000000000, 999999999999);
        }

        nationalRegistryOfLegalEntities += CalculateFirstVerificationDigit(nationalRegistryOfLegalEntities);
        nationalRegistryOfLegalEntities += CalculateSecondVerificationDigit(nationalRegistryOfLegalEntities);

        return formatted ? Format(nationalRegistryOfLegalEntities) : nationalRegistryOfLegalEntities;
    }
    
    /// <summary>
    /// Format the national registry of legal entities (CNPJ) Ex: 00.000.000/0000-00.
    /// </summary>
    /// <param name="nationalRegistryOfLegalEntities"> National registry of legal entities (CNPJ) Ex: 00000000000000. </param>
    /// <returns> Formatted national registry of legal entities (CNPJ) Ex: 00.000.000/0000-00. </returns>
    public static string Format(string nationalRegistryOfLegalEntities)
    {
        if (string.IsNullOrWhiteSpace(nationalRegistryOfLegalEntities))
        {
            return nationalRegistryOfLegalEntities;
        }
        
        var cleanedNationalRegistryOfLegalEntities = nationalRegistryOfLegalEntities
            .ToUpper()
            .RemoveNonAlphabeticOrNumericCharacters();
        
        if (cleanedNationalRegistryOfLegalEntities.Length is not MaxLength)
        {
            return nationalRegistryOfLegalEntities;
        }
        
        return cleanedNationalRegistryOfLegalEntities
            .Insert(2, ".")
            .Insert(6, ".")
            .Insert(10, "/")
            .Insert(15, "-");
    }

    private static int CalculateFirstVerificationDigit(string cleanedNationalRegistryOfLegalEntities)
    {
        var sum = 0;
        for (var i = 1; i < Multiplier.Length; i++)
        {
            sum += cleanedNationalRegistryOfLegalEntities[i - 1].ToDigitRepresentation(offset: 48) * Multiplier[i];
        }

        var rest = sum % 11;
        return rest < 2 ? 0 : 11 - rest;
    }

    
    private static int CalculateSecondVerificationDigit(string cleanedNationalRegistryOfLegalEntities)
    {
        var sum = 0;
        for (var i = 0; i < Multiplier.Length; i++)
        {
            sum += cleanedNationalRegistryOfLegalEntities[i].ToDigitRepresentation(offset: 48) * Multiplier[i];
        }

        var rest = sum % 11;
        return rest < 2 ? 0 : 11 - rest;
    }
    
}
