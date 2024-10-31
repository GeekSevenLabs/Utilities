using System.Diagnostics.CodeAnalysis;

namespace GeekSevenLabs.Utilities.Documents;

/// <summary>
/// Cadastro de Pessoa Física (CPF) é um documento usado para identificar um cidadão no Brasil.
/// </summary>
public static class CadastroPessoaFisica
{
    private static readonly int[] Multiplier = [10, 9, 8, 7, 6, 5, 4, 3, 2];
    private const int MaxLengthWithoutMask = 11;
    private const int MaxLengthWithMask = 14;

    /// <summary>
    /// Verifica se o Cadastro de Pessoa Física (CPF) é válido.
    /// </summary>
    /// <param name="cpf">CPF: ex 000.000.000-00 ou 00000000000</param>
    /// <returns>True se o CPF for válido</returns>
    public static bool IsValid(string cpf)
    {
        if(!TryCleanAndPreValidation(cpf, out var cleanedCpf))
        {
            return false;
        }

        if (cleanedCpf.AllCharactersAreEqual())
        {
            return false;
        }

        var firstVerificationDigit = CalculateFirstVerificationDigit(cleanedCpf);
        var secondVerificationDigit = CalculateSecondVerificationDigit(cleanedCpf);

        return cleanedCpf.EndsWith($"{firstVerificationDigit}{secondVerificationDigit}");
    }

    /// <summary>
    /// Gera um CPF aleatório.
    /// </summary>
    /// <param name="region"> Código da região do CPF (um dígito) Ex: 4. </param>
    /// <param name="formatted"> Formatar o CPF Ex: 000.000.000-00. </param>
    /// <returns> Um CPF aleatório. </returns>
    public static string Generate(int? region = null, bool formatted = false)
    {
        string cpf;

        if (region.HasValue)
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(region.Value, 9);
            cpf = Random.Shared.Next(10000000, 99999999).ToString() + region;
        }
        else
        {
            cpf = Random.Shared.Next(100000000, 999999999).ToString();
        }

        cpf += CalculateFirstVerificationDigit(cpf);
        cpf += CalculateSecondVerificationDigit(cpf);

        return formatted ? Format(cpf) : cpf;
    }

    /// <summary>
    /// Formata o CPF Ex: 000.000.000-00.
    /// </summary>
    /// <param name="cpf">CPF Ex: 00000000000. </param>
    /// <returns>CPF formatado Ex: 000.000.000-00. </returns>
    public static string Format(string cpf)
    {
        if(TryCleanAndPreValidation(cpf, out var cleanedCpf))
        {
            return cleanedCpf
                .Insert(3, ".")
                .Insert(7, ".")
                .Insert(11, "-");
        }

        return cpf;
    }

    private static bool TryCleanAndPreValidation(string cpf, [NotNullWhen(true)] out string? cleanedCpf)
    {
        if (string.IsNullOrWhiteSpace(cpf) || 
            cpf.Length > MaxLengthWithMask ||
            cpf.Length < MaxLengthWithoutMask)
        {
            cleanedCpf = null;
            return false;
        }
        
        cpf = cpf.RemoveTokens(".", "-");

        if (cpf.Length is not MaxLengthWithoutMask)
        {
            cleanedCpf = null;
            return false;
        }
        
        cpf = cpf.RemoveNonNumericCharacters();

        if (cpf.Length is not MaxLengthWithoutMask)
        {
            cleanedCpf = null;
            return false;
        }
        
        cleanedCpf = cpf;
        return true;
    } 

    private static int CalculateFirstVerificationDigit(string cpf)
    {
        var sum = 0;
        for(var i = 0; i < Multiplier.Length; i++) sum += cpf[i].ToDigit() * Multiplier[i];
        return CalculateDigit(sum);
    }

    private static int CalculateSecondVerificationDigit(string cpf)
    {
        var sum = 0;
        for(var i = 0; i < Multiplier.Length; i++) sum += cpf[i + 1].ToDigit() * Multiplier[i];
        return CalculateDigit(sum);
    }

    private static int CalculateDigit(int sum)
    {
        var rest = sum % 11;
        return rest < 2 ? 0 : 11 - rest;
    }
}
