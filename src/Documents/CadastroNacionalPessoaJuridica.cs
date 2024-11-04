using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace GeekSevenLabs.Utilities.Documents;

/// <summary>
/// Cadastro Nacional de Pessoa Jurídica (CNPJ) é um documento usado para identificar uma pessoa jurídica no Brasil.
/// </summary>
public static partial class CadastroNacionalPessoaJuridica
{
    private static readonly int[] Multiplier = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
    private const int MaxLengthWithoutMask = 14;
    private const int MaxLengthWithMask = 18;
    private static readonly char[] NumbersChars = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];

    private static readonly char[] LettersChars =
    [
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V',
        'W', 'X', 'Y', 'Z'
    ];

    /// <summary>
    /// Verifica se o Cadastro Nacional de Pessoa Jurídica (CNPJ) é válido.
    /// </summary>
    /// <param name="cnpj"> CNPJ: ex 00.000.000/0000-00 ou 00000000000000 </param>
    /// <returns> True se o Cadastro Nacional de Pessoa Jurídica (CNPJ) é válido </returns>
    public static bool IsValid(string cnpj)
    {
        if (!TryCleanAndPreValidation(cnpj, out var cleanedCnpj))
        {
            return false;
        }

        if (cleanedCnpj.AllCharactersAreEqual())
        {
            return false;
        }

        var firstVerificationDigit = CalculateFirstVerificationDigit(cleanedCnpj);
        var secondVerificationDigit = CalculateSecondVerificationDigit(cleanedCnpj);

        return cleanedCnpj.EndsWith($"{firstVerificationDigit}{secondVerificationDigit}");
    }

    /// <summary>
    /// Gera um Cadastro Nacional de Pessoa Jurídica (CNPJ) aleatório.
    /// </summary>
    /// <param name="formatted"> Formata o Cadastro Nacional de Pessoa Jurídica (CNPJ) Ex: 00.000.000/0000-00. </param>
    /// <param name="useLetters"> Usa letras no Cadastro Nacional de Pessoa Jurídica (CNPJ) Ex: AB.C7E.F3H/I7KL-00. </param>
    /// <returns></returns>
    public static string Generate(bool formatted = false, bool useLetters = false)
    {
        var cnpj = string.Empty;

        if (useLetters)
        {
            var validTokens = NumbersChars.Concat(LettersChars).ToArray();
            for (var i = 1; i <= 12; i++)
            {
                cnpj += validTokens[Random.Shared.Next(0, validTokens.Length)];
            }
        }
        else
        {
            cnpj += Random.Shared.NextInt64(100000000000, 999999999999);
        }

        cnpj += CalculateFirstVerificationDigit(cnpj);
        cnpj += CalculateSecondVerificationDigit(cnpj);

        return formatted ? Format(cnpj) : cnpj;
    }

    /// <summary>
    /// Formata o Cadastro Nacional de Pessoa Jurídica (CNPJ) Ex: 00.000.000/0000-00.
    /// </summary>
    /// <param name="cnpj"> Cadastro Nacional de Pessoa Jurídica (CNPJ) Ex: 00000000000000. </param>
    /// <returns> CNPJ formatado Ex: 00.000.000/0000-00. </returns>
    public static string Format(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj))
        {
            return cnpj;
        }

        var cleanedCnpj = cnpj
            .ToUpper()
            .RemoveNonAlphabeticOrNumericCharacters();

        if (cleanedCnpj.Length is not MaxLengthWithoutMask)
        {
            return cnpj;
        }

        return cleanedCnpj
            .Insert(2, ".")
            .Insert(6, ".")
            .Insert(10, "/")
            .Insert(15, "-");
    }

    /// <summary>
    /// Verifica se o CNPJ está no padrão correto.
    /// </summary>
    /// <param name="cnpj"> CNPJ: ex 00.000.000/0000-00 ou 00000000000000 </param>
    /// <returns> CnpjPatternMatchResult informa se o CNPJ está no padrão correto e se está com mascára. </returns>
    public static CnpjPatternMatchResult IsValidPattern(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj)) return CnpjPatternMatchResult.Invalid;

        if (CreateCnpjRegex().IsMatch(cnpj)) return CnpjPatternMatchResult.ValidUnmasked;
        if (CreateCnpjWithLettersRegex().IsMatch(cnpj)) return CnpjPatternMatchResult.ValidUnmaskedWithLetters;

        if (CreateMaskedCnpjRegex().IsMatch(cnpj)) return CnpjPatternMatchResult.ValidMasked;
        
        return CreateMaskedCnpjWithLettersRegex().IsMatch(cnpj) ? 
            CnpjPatternMatchResult.ValidMaskedWithLetters : 
            CnpjPatternMatchResult.Invalid;
    }

    private static bool TryCleanAndPreValidation(string cnpj, [NotNullWhen(true)] out string? cleanedCnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj) ||
            cnpj.Length > MaxLengthWithMask ||
            cnpj.Length < MaxLengthWithoutMask)
        {
            cleanedCnpj = null;
            return false;
        }

        cnpj = cnpj.RemoveTokens(".", "/", "-");

        if (cnpj.Length is not MaxLengthWithoutMask)
        {
            cleanedCnpj = null;
            return false;
        }

        cnpj = cnpj
            .ToUpper()
            .RemoveNonAlphabeticOrNumericCharacters();

        if (cnpj.Length is not MaxLengthWithoutMask)
        {
            cleanedCnpj = null;
            return false;
        }

        if (cnpj[12].IsNotNumber() || cnpj[13].IsNotNumber())
        {
            cleanedCnpj = null;
            return false;
        }

        cleanedCnpj = cnpj;
        return true;
    }

    private static int CalculateFirstVerificationDigit(string cleanedCnpj)
    {
        var sum = 0;
        for (var i = 1; i < Multiplier.Length; i++)
            sum += cleanedCnpj[i - 1].ToDigitRepresentation(offset: 48) * Multiplier[i];
        return CalculateDigit(sum);
    }

    private static int CalculateSecondVerificationDigit(string cleanedCnpj)
    {
        var sum = 0;
        for (var i = 0; i < Multiplier.Length; i++)
            sum += cleanedCnpj[i].ToDigitRepresentation(offset: 48) * Multiplier[i];
        return CalculateDigit(sum);
    }

    private static int CalculateDigit(int sum)
    {
        var rest = sum % 11;
        return rest < 2 ? 0 : 11 - rest;
    }

    [GeneratedRegex(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$")]
    public static partial Regex CreateMaskedCnpjRegex();

    [GeneratedRegex(@"^[0-9A-Z]{2}\.[0-9A-Z]{3}\.[0-9A-Z]{3}/[0-9A-Z]{4}-\d{2}$")]
    public static partial Regex CreateMaskedCnpjWithLettersRegex();

    [GeneratedRegex(@"^\d{14}$")]
    public static partial Regex CreateCnpjRegex();

    [GeneratedRegex(@"^[0-9A-Z]{14}$")]
    public static partial Regex CreateCnpjWithLettersRegex();
}