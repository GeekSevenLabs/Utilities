namespace GeekSevenLabs.Utilities.Documents.Tests;

public class CadastroNacionalPessoaJuridicaTests
{
    [Fact]
    public void IsValid_WhenCadastroNacionalPessoaJuridicaIsNull_ReturnsFalse()
    {
        // Arrange
        string cnpj = null!;

        // Act
        var isValid = CadastroNacionalPessoaJuridica.IsValid(cnpj);

        // Assert
        isValid.Should().BeFalse();
    }
    
    [Fact]
    public void IsValid_WhenCadastroNacionalPessoaJuridicaIsEmpty_ReturnsFalse()
    {
        // Arrange
        var cnpj = string.Empty;

        // Act
        var isValid = CadastroNacionalPessoaJuridica.IsValid(cnpj);

        // Assert
        isValid.Should().BeFalse();
    }
    
    [Fact]
    public void IsValid_WhenCadastroNacionalPessoaJuridicaIsWhitespace_ReturnsFalse()
    {
        // Arrange
        const string cnpj = " ";

        // Act
        var isValid = CadastroNacionalPessoaJuridica.IsValid(cnpj);

        // Assert
        isValid.Should().BeFalse();
    }
    
    [Fact]
    public void IsValid_WhenCadastroNacionalPessoaJuridicaIsInvalid_ReturnsFalse()
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
    public void IsValid_WhenCadastroNacionalPessoaJuridicaIsValid_ReturnsTrue(string cnpj)
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
    public void IsValid_WhenCadastroNacionalPessoaJuridicaIsValidWithoutSpecialCharacters_ReturnsTrue(string cnpj)
    {
        // Act
        var isValid = CadastroNacionalPessoaJuridica.IsValid(cnpj);

        // Assert
        isValid.Should().BeTrue();
    }
    
    [Fact]
    public void Generate_WhenFormattedIsTrue_ReturnsCadastroNacionalPessoaJuridicaWithSpecialCharacters()
    {
        // Act
        var cnpj = CadastroNacionalPessoaJuridica.Generate(formatted: true);

        // Assert
        cnpj.Should().MatchRegex(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$");
    }
    
    [Fact]
    public void Generate_WhenFormattedIsFalse_ReturnsCadastroNacionalPessoaJuridicaWithoutSpecialCharacters()
    {
        // Act
        var cnpj = CadastroNacionalPessoaJuridica.Generate(formatted: false);

        // Assert
        cnpj.Should().MatchRegex(@"^\d{14}$");
    }
    
    [Fact]
    public void Generate_WhenUseLettersIsTrue_ReturnsCadastroNacionalPessoaJuridicaWithLetters()
    {
        // Act
        var cnpj = CadastroNacionalPessoaJuridica.Generate(useLetters: true);

        // Assert
        cnpj.Should().MatchRegex("^[A-Z0-9]{14}$");
    }
    
    [Fact]
    public void Generate_WhenUseLettersIsFalse_ReturnsCadastroNacionalPessoaJuridicaWithoutLetters()
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
    public void Format_WhenCadastroNacionalPessoaJuridicaIsValid_ReturnsCnpjWithSpecialCharacters()
    {
        // Arrange
        const string cnpj = "79594544000189";

        // Act
        var cnpjFormatted = CadastroNacionalPessoaJuridica.Format(cnpj);

        // Assert
        cnpjFormatted.Should().Be("79.594.544/0001-89");
    }
    
    [Fact]
    public void Format_WhenCadastroNacionalPessoaJuridicaIsInvalid_ReturnsCnpjInformed()
    {
        // Arrange
        const string cnpj = "7959454400018";

        // Act
        var cnpjFormatted = CadastroNacionalPessoaJuridica.Format(cnpj);

        // Assert
        cnpjFormatted.Should().Be(cnpj);
    }
    
    [Fact]
    public void Format_WhenCadastroNacionalPessoaJuridicaIsEmpty_ReturnsCnpjInformed()
    {
        // Arrange
        const string cnpj = "";

        // Act
        var cnpjFormatted = CadastroNacionalPessoaJuridica.Format(cnpj);

        // Assert
        cnpjFormatted.Should().Be(cnpj);
    }
    
    [Fact]
    public void Format_WhenCadastroNacionalPessoaJuridicaIsWhitespace_ReturnsCnpjInformed()
    {
        // Arrange
        const string cnpj = " ";

        // Act
        var cnpjFormatted = CadastroNacionalPessoaJuridica.Format(cnpj);

        // Assert
        cnpjFormatted.Should().Be(cnpj);
    }
    
    [Fact]
    public void Format_WhenCadastroNacionalPessoaJuridicaIsNull_ReturnsCnpjInformed()
    {
        // Arrange
        string cnpj = null!;

        // Act
        var cnpjFormatted = CadastroNacionalPessoaJuridica.Format(cnpj);

        // Assert
        cnpjFormatted.Should().BeNull();
    }
    
    [Fact]
    public void Format_WhenCadastroNacionalPessoaJuridicaHasSpecialCharacters_ReturnsCnpjWithSpecialCharacters()
    {
        // Arrange
        const string cnpj = "79.594.544/0001-89";

        // Act
        var cnpjFormatted = CadastroNacionalPessoaJuridica.Format(cnpj);

        // Assert
        cnpjFormatted.Should().Be(cnpj);
    }
    
}