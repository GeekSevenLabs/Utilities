

namespace GeekSevenLabs.Utilities.Documents.Tests;

public class CadastroPessoaFisicaTests
{
    [Fact]
    public void IsValid_WhenCadastroPessoaFisicaIsNull_ReturnsFalse()
    {
        // Arrange
        string cpf = null!;

        // Act
        var result = CadastroPessoaFisica.IsValid(cpf);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void IsValid_WhenCadastroPessoaFisicaIsEmpty_ReturnsFalse()
    {
        // Arrange
        var cpf = string.Empty;

        // Act
        var result = CadastroPessoaFisica.IsValid(cpf);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void IsValid_WhenCadastroPessoaFisicaIsWhiteSpace_ReturnsFalse()
    {
        // Arrange
        const string cpf = " ";

        // Act
        var result = CadastroPessoaFisica.IsValid(cpf);

        // Assert
        result.Should().BeFalse();
    }
    
    [Theory]
    [InlineData("144.442.344-57")]
    [InlineData("543.434.321-76")]
    [InlineData("A822.420.106-62")]
    [InlineData("822.420.106-62a")]
    [InlineData("000.000.000-00")]
    [InlineData("111.111.111-11")]
    [InlineData("222.222.222-22")]
    public void IsValid_WhenCadastroPessoaFisicaIsInvalid_ReturnsFalse(string cpf)
    {
        // Act
        var result = CadastroPessoaFisica.IsValid(cpf);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void IsValid_WhenCadastroPessoaFisicaIsValid_ReturnsTrue()
    {
        // Arrange
        const string cpf = "34701370053";

        // Act
        var result = CadastroPessoaFisica.IsValid(cpf);

        // Assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public void IsValid_WhenCadastroPessoaFisicaHasNonNumericCharacters_ReturnsTrue()
    {
        // Arrange
        const string cpf = "347.013.700-53";

        // Act
        var result = CadastroPessoaFisica.IsValid(cpf);

        // Assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public void IsValid_WhenCadastroPessoaFisicaHasLessThanElevenCharacters_ReturnsFalse()
    {
        // Arrange
        const string cpf = "1249218543";

        // Act
        var result = CadastroPessoaFisica.IsValid(cpf);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void IsValid_WhenCadastroPessoaFisicaHasMoreThanElevenCharacters_ReturnsFalse()
    {
        // Arrange
        const string cpf = "347013700536";

        // Act
        var result = CadastroPessoaFisica.IsValid(cpf);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void IsValid_WhenCadastroPessoaFisicaHasFirstVerificationDigitInvalid_ReturnsFalse()
    {
        // Arrange
        const string cpf = "12492185455";

        // Act
        var result = CadastroPessoaFisica.IsValid(cpf);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void IsValid_WhenCadastroPessoaFisicaHasSecondVerificationDigitInvalid_ReturnsFalse()
    {
        // Arrange
        const string cpf = "12492185436";

        // Act
        var result = CadastroPessoaFisica.IsValid(cpf);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void Generate_WhenRegionIsNullAndFormattedIsFalse_ReturnsValidCadastroPessoaFisica()
    {
        // Arrange and Act
        var cpf = CadastroPessoaFisica.Generate();

        // Assert
        cpf.Should().MatchRegex(@"^\d{11}$");
        CadastroPessoaFisica.IsValid(cpf).Should().BeTrue();
    }
    
    [Fact]
    public void Generate_WhenRegionIsNullAndFormattedIsTrue_ReturnsValidFormattedCadastroPessoaFisica()
    {
        // Arrange and Act
        var cpf = CadastroPessoaFisica.Generate(formatted: true);

        // Assert
        cpf.Should().MatchRegex(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$");
        CadastroPessoaFisica.IsValid(cpf).Should().BeTrue();
    }
    
    [Fact]
    public void Generate_WhenRegionIsNotNullAndFormattedIsFalse_ReturnsValidCadastroPessoaFisica()
    {
        // Arrange and Act
        var cpf = CadastroPessoaFisica.Generate(region: 1);

        // Assert
        cpf.Should().MatchRegex(@"^\d{8}1\d{2}$");
        CadastroPessoaFisica.IsValid(cpf).Should().BeTrue();
    }
    
    [Fact]
    public void Generate_WhenRegionIsNotNullAndFormattedIsTrue_ReturnsValidFormattedCadastroPessoaFisica()
    {
        // Arrange and Act
        var cpf = CadastroPessoaFisica.Generate(region: 1, formatted: true);

        // Assert
        cpf.Should().MatchRegex(@"^\d{3}.\d{3}.\d{2}1-\d{2}$");
        CadastroPessoaFisica.IsValid(cpf).Should().BeTrue();
    }
    
    [Fact]
    public void Format_WhenCadastroPessoaFisicaHasElevenCharacters_ReturnsFormattedCadastroPessoaFisica()
    {
        // Arrange
        const string cpf = "34701370053";

        // Act
        var result = CadastroPessoaFisica.Format(cpf);

        // Assert
        result.Should().Be("347.013.700-53");
    }
    
    [Fact]
    public void Format_WhenCadastroPessoaFisicaHasLessThanElevenCharacters_ReturnsCadastroPessoaFisica()
    {
        // Arrange
        const string cpf = "1249218543";

        // Act
        var result = CadastroPessoaFisica.Format(cpf);

        // Assert
        cpf.Should().Be(result);
    }
    
    [Fact]
    public void Format_WhenCadastroPessoaFisicaHasMoreThanElevenCharacters_ReturnsCadastroPessoaFisica()
    {
        // Arrange
        const string cpf = "347013700536";

        // Act
        var result = CadastroPessoaFisica.Format(cpf);

        // Assert
        cpf.Should().Be(result);
    }
}
