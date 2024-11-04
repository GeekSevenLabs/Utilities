namespace GeekSevenLabs.Utilities.Documents.Tests;

public class CadastroNacionalPessoaJuridicaTests
{
    [Fact]
    public void IsValid_WhenCnpjIsNull_ReturnsFalse()
    {
        // Arrange
        string cnpj = null!;

        // Act
        var isValid = CadastroNacionalPessoaJuridica.IsValid(cnpj);

        // Assert
        isValid.Should().BeFalse();
    }
    
    [Fact]
    public void IsValid_WhenCnpjIsEmpty_ReturnsFalse()
    {
        // Arrange
        var cnpj = string.Empty;

        // Act
        var isValid = CadastroNacionalPessoaJuridica.IsValid(cnpj);

        // Assert
        isValid.Should().BeFalse();
    }
    
    [Fact]
    public void IsValid_WhenCnpjIsWhitespace_ReturnsFalse()
    {
        // Arrange
        const string cnpj = " ";

        // Act
        var isValid = CadastroNacionalPessoaJuridica.IsValid(cnpj);

        // Assert
        isValid.Should().BeFalse();
    }
    
    [Fact]
    public void IsValid_WhenCnpjIsInvalid_ReturnsFalse()
    {
        // Arrange
        const string cnpj = "12.345.678/9123-12";

        // Act
        var isValid = CadastroNacionalPessoaJuridica.IsValid(cnpj);

        // Assert
        isValid.Should().BeFalse();
    }
    
    [Theory]
    [InlineData("79.594.544/0001-89")]
    [InlineData("74.141.298/0001-96")]
    [InlineData("47.848.197/0001-87")]
    [InlineData("55.361.889/0001-24")]
    [InlineData("73.508.530/0001-19")]
    [InlineData("0F.QU3.WLW/6SCR-69")]
    public void IsValid_WhenCnpjIsValid_ReturnsTrue(string cnpj)
    {
        // Act
        var isValid = CadastroNacionalPessoaJuridica.IsValid(cnpj);

        // Assert
        isValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("88059534000134")]
    [InlineData("27298254000194")]
    [InlineData("99526994000141")]
    [InlineData("94993378000187")]
    [InlineData("0FQU3WLW6SCR69")]
    public void IsValid_WhenCnpjIsValidWithoutSpecialCharacters_ReturnsTrue(string cnpj)
    {
        // Act
        var isValid = CadastroNacionalPessoaJuridica.IsValid(cnpj);

        // Assert
        isValid.Should().BeTrue();
    }
    
    [Fact]
    public void Generate_WhenFormattedIsTrue_ReturnsCnpjWithSpecialCharacters()
    {
        // Act
        var cnpj = CadastroNacionalPessoaJuridica.Generate(formatted: true);

        // Assert
        cnpj.Should().MatchRegex(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$");
    }
    
    [Fact]
    public void Generate_WhenFormattedIsFalse_ReturnsCnpjWithoutSpecialCharacters()
    {
        // Act
        var cnpj = CadastroNacionalPessoaJuridica.Generate(formatted: false);

        // Assert
        cnpj.Should().MatchRegex(@"^\d{14}$");
    }
    
    [Fact]
    public void Generate_WhenUseLettersIsTrue_ReturnsCnpjWithLetters()
    {
        // Act
        var cnpj = CadastroNacionalPessoaJuridica.Generate(useLetters: true);

        // Assert
        cnpj.Should().MatchRegex("^[A-Z0-9]{14}$");
    }
    
    [Fact]
    public void Generate_WhenUseLettersIsFalse_ReturnsCnpjWithoutLetters()
    {
        // Act
        var cnpj = CadastroNacionalPessoaJuridica.Generate(useLetters: false);

        // Assert
        cnpj.Should().MatchRegex(@"^\d{14}$");
    }
    
    [Fact]
    public void Generate_WhenUseLettersIsTrueAndFormattedIsTrue_ReturnsCnpjWithLettersAndSpecialCharacters()
    {
        // Act
        var cnpj = CadastroNacionalPessoaJuridica.Generate(useLetters: true, formatted: true);

        // Assert
        cnpj.Should().MatchRegex(@"^[A-Z0-9]{2}\.[A-Z0-9]{3}\.[A-Z0-9]{3}/[A-Z0-9]{4}-[A-Z0-9]{2}$");
    }
    
    [Fact]
    public void Format_WhenCnpjIsValid_ReturnsCnpjWithSpecialCharacters()
    {
        // Arrange
        const string cnpj = "79594544000189";

        // Act
        var cnpjFormatted = CadastroNacionalPessoaJuridica.Format(cnpj);

        // Assert
        cnpjFormatted.Should().Be("79.594.544/0001-89");
    }
    
    [Fact]
    public void Format_WhenCnpjIsInvalid_ReturnsCnpjInformed()
    {
        // Arrange
        const string cnpj = "7959454400018";

        // Act
        var cnpjFormatted = CadastroNacionalPessoaJuridica.Format(cnpj);

        // Assert
        cnpjFormatted.Should().Be(cnpj);
    }
    
    [Fact]
    public void Format_WhenCnpjIsEmpty_ReturnsCnpjInformed()
    {
        // Arrange
        const string cnpj = "";

        // Act
        var cnpjFormatted = CadastroNacionalPessoaJuridica.Format(cnpj);

        // Assert
        cnpjFormatted.Should().Be(cnpj);
    }
    
    [Fact]
    public void Format_WhenCnpjIsWhitespace_ReturnsCnpjInformed()
    {
        // Arrange
        const string cnpj = " ";

        // Act
        var cnpjFormatted = CadastroNacionalPessoaJuridica.Format(cnpj);

        // Assert
        cnpjFormatted.Should().Be(cnpj);
    }
    
    [Fact]
    public void Format_WhenCnpjIsNull_ReturnsCnpjInformed()
    {
        // Arrange
        string cnpj = null!;

        // Act
        var cnpjFormatted = CadastroNacionalPessoaJuridica.Format(cnpj);

        // Assert
        cnpjFormatted.Should().BeNull();
    }
    
    [Fact]
    public void Format_WhenCnpjHasSpecialCharacters_ReturnsCnpjWithSpecialCharacters()
    {
        // Arrange
        const string cnpj = "79.594.544/0001-89";

        // Act
        var cnpjFormatted = CadastroNacionalPessoaJuridica.Format(cnpj);

        // Assert
        cnpjFormatted.Should().Be(cnpj);
    }
    
    [Fact]
    public void IsValidPattern_WhenCnpjIsValid_ReturnsTrue()
    {
        // Arrange
        const string cnpj = "79594544000189";

        // Act
        var result = CadastroNacionalPessoaJuridica.IsValidPattern(cnpj);

        // Assert
        result.IsValidUnmasked.Should().BeTrue();
    }
    
    [Fact]
    public void IsValidPattern_WhenCnpjIsValidWithLetters_ReturnsTrue()
    {
        // Arrange
        const string cnpj = "0FQU3WLW6SCR69";

        // Act
        var result = CadastroNacionalPessoaJuridica.IsValidPattern(cnpj);

        // Assert
        result.IsValidUnmasked.Should().BeTrue();
    }
    
    [Fact]
    public void IsValidPattern_WhenCnpjIsValidWithMask_ReturnsTrue()
    {
        // Arrange
        const string cnpj = "79.594.544/0001-89";

        // Act
        var result = CadastroNacionalPessoaJuridica.IsValidPattern(cnpj);

        // Assert
        result.IsValidMasked.Should().BeTrue();
    }
    
    [Fact]
    public void IsValidPattern_WhenCnpjIsValidWithMaskAndLetters_ReturnsTrue()
    {
        // Arrange
        const string cnpj = "0F.QU3.WLW/6SCR-69";

        // Act
        var result = CadastroNacionalPessoaJuridica.IsValidPattern(cnpj);

        // Assert
        result.IsValidMasked.Should().BeTrue();
    }
    
    [Fact]
    public void IsValidPattern_WhenCnpjIsInvalid_ReturnsFalse()
    {
        // Arrange
        const string cnpj = "7959454400018";

        // Act
        var result = CadastroNacionalPessoaJuridica.IsValidPattern(cnpj);

        // Assert
        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void IsValidPattern_WhenCnpjIsInvalidWithLetters_ReturnsFalse()
    {
        // Arrange
        const string cnpj = "0FQU3WLW6SCR6";

        // Act
        var result = CadastroNacionalPessoaJuridica.IsValidPattern(cnpj);

        // Assert
        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void IsValidPattern_WhenCnpjIsInvalidWithMask_ReturnsFalse()
    {
        // Arrange
        const string cnpj = "79.594.544/0001-8";

        // Act
        var result = CadastroNacionalPessoaJuridica.IsValidPattern(cnpj);

        // Assert
        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void IsValidPattern_WhenCnpjIsInvalidWithMaskAndLetters_ReturnsFalse()
    {
        // Arrange
        const string cnpj = "0F.QU3.WLW/6SCR-6";

        // Act
        var result = CadastroNacionalPessoaJuridica.IsValidPattern(cnpj);

        // Assert
        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void IsValidPattern_WhenCnpjIsEmpty_ReturnsInvalid()
    {
        // Arrange
        const string cnpj = "";

        // Act
        var result = CadastroNacionalPessoaJuridica.IsValidPattern(cnpj);

        // Assert
        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void IsValidPattern_WhenCnpjIsWhitespace_ReturnsInvalid()
    {
        // Arrange
        const string cnpj = " ";

        // Act
        var result = CadastroNacionalPessoaJuridica.IsValidPattern(cnpj);

        // Assert
        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void IsValidPattern_WhenCnpjIsNull_ReturnsInvalid()
    {
        // Arrange
        string cnpj = null!;

        // Act
        var result = CadastroNacionalPessoaJuridica.IsValidPattern(cnpj);

        // Assert
        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void CreateCnpjUnmaskedRegex_ReturnsRegex()
    {
        // Act
        var regex = CadastroNacionalPessoaJuridica.CreateUnmaskedCnpjRegex();

        // Assert
        regex.Should().NotBeNull();
    }
    
    [Fact]
    public void CreateCnpjUnmaskedRegex_ReturnsRegexWithPattern()
    {
        // Act
        var regex = CadastroNacionalPessoaJuridica.CreateUnmaskedCnpjRegex();

        // Assert
        regex.ToString().Should().Be(@"^[0-9A-Z]{12}\d{2}$");
    }
    
    [Fact]
    public void CreateMaskedCnpjRegex_RegexShouldMatchCnpjWithLettersNoMasked()
    {
        // Arrange
        var cnpj = CadastroNacionalPessoaJuridica.Generate(formatted: false, useLetters: true);

        // Act
        var regex = CadastroNacionalPessoaJuridica.CreateUnmaskedCnpjRegex();

        // Assert
        regex.IsMatch(cnpj).Should().BeTrue();
    }
    
    [Fact]
    public void CreateMaskedCnpjRegex_RegexShouldNotMatchCnpj()
    {
        // Arrange
        var cnpj = CadastroNacionalPessoaJuridica.Generate(formatted: false, useLetters: true);
        cnpj = cnpj[..^1] + "A";

        // Act
        var regex = CadastroNacionalPessoaJuridica.CreateMaskedCnpjRegex();

        // Assert
        regex.IsMatch(cnpj).Should().BeFalse();
    }
    
    [Fact]
    public void CreateMaskedCnpjRegex_ReturnsRegex()
    {
        // Act
        var regex = CadastroNacionalPessoaJuridica.CreateMaskedCnpjRegex();

        // Assert
        regex.Should().NotBeNull();
    }
    
    [Fact]
    public void CreateMaskedCnpjRegex_RegexShouldMatchCnpj()
    {
        // Arrange
        var cnpj = CadastroNacionalPessoaJuridica.Generate(formatted: true);

        // Act
        var regex = CadastroNacionalPessoaJuridica.CreateMaskedCnpjRegex();

        // Assert
        regex.IsMatch(cnpj).Should().BeTrue();
    }
    
    [Fact]
    public void CreateMaskedCnpjWithLettersRegex_ReturnsRegexWithPattern()
    {
        // Act
        var regex = CadastroNacionalPessoaJuridica.CreateMaskedCnpjRegex();

        // Assert
        regex.ToString().Should().Be(@"^[0-9A-Z]{2}\.[0-9A-Z]{3}\.[0-9A-Z]{3}/[0-9A-Z]{4}-\d{2}$");
    }
    
    [Fact]
    public void CreateMaskedCnpjRegex_RegexShouldMatchCnpjWithLetters()
    {
        // Arrange
        var cnpj = CadastroNacionalPessoaJuridica.Generate(formatted: true, useLetters: true);

        // Act
        var regex = CadastroNacionalPessoaJuridica.CreateMaskedCnpjRegex();

        // Assert
        regex.IsMatch(cnpj).Should().BeTrue();
    }
}