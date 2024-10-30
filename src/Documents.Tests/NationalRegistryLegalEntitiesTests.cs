namespace GeekSevenLabs.Utilities.Documents.Tests;

public class NationalRegistryLegalEntitiesTests
{
    [Fact]
    public void IsValid_WhenNationalRegistryLegalEntitiesIsNull_ReturnsFalse()
    {
        // Arrange
        string nationalRegistryOfLegalEntities = null!;

        // Act
        var result = NationalRegistryLegalEntities.IsValid(nationalRegistryOfLegalEntities);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void IsValid_WhenNationalRegistryLegalEntitiesIsEmpty_ReturnsFalse()
    {
        // Arrange
        var nationalRegistryOfLegalEntities = string.Empty;

        // Act
        var result = NationalRegistryLegalEntities.IsValid(nationalRegistryOfLegalEntities);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void IsValid_WhenNationalRegistryLegalEntitiesIsWhitespace_ReturnsFalse()
    {
        // Arrange
        const string nationalRegistryOfLegalEntities = " ";

        // Act
        var result = NationalRegistryLegalEntities.IsValid(nationalRegistryOfLegalEntities);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void IsValid_WhenNationalRegistryLegalEntitiesIsInvalid_ReturnsFalse()
    {
        // Arrange
        const string nationalRegistryOfLegalEntities = "12.345.678/9123-12";

        // Act
        var result = NationalRegistryLegalEntities.IsValid(nationalRegistryOfLegalEntities);

        // Assert
        result.Should().BeFalse();
    }
    
    [Theory]
    [InlineData("79.594.544/0001-89")]
    [InlineData("74.141.298/0001-96")]
    [InlineData("47.848.197/0001-87")]
    [InlineData("55.361.889/0001-24")]
    [InlineData("73.508.530/0001-19")]
    [InlineData("0F.QU3.WLW/6SCR-69")]
    public void IsValid_WhenNationalRegistryLegalEntitiesIsValid_ReturnsTrue(string nationalRegistryOfLegalEntities)
    {
        // Act
        var result = NationalRegistryLegalEntities.IsValid(nationalRegistryOfLegalEntities);

        // Assert
        result.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("88059534000134")]
    [InlineData("27298254000194")]
    [InlineData("99526994000141")]
    [InlineData("94993378000187")]
    [InlineData("0FQU3WLW6SCR69")]
    public void IsValid_WhenNationalRegistryLegalEntitiesIsValidWithoutSpecialCharacters_ReturnsTrue(string nationalRegistryOfLegalEntities)
    {
        // Act
        var result = NationalRegistryLegalEntities.IsValid(nationalRegistryOfLegalEntities);

        // Assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public void Generate_WhenFormattedIsTrue_ReturnsNationalRegistryLegalEntitiesWithSpecialCharacters()
    {
        // Act
        var result = NationalRegistryLegalEntities.Generate(formatted: true);

        // Assert
        result.Should().MatchRegex(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$");
    }
    
    [Fact]
    public void Generate_WhenFormattedIsFalse_ReturnsNationalRegistryLegalEntitiesWithoutSpecialCharacters()
    {
        // Act
        var result = NationalRegistryLegalEntities.Generate(formatted: false);

        // Assert
        result.Should().MatchRegex(@"^\d{14}$");
    }
    
    [Fact]
    public void Generate_WhenUseLettersIsTrue_ReturnsNationalRegistryLegalEntitiesWithLetters()
    {
        // Act
        var result = NationalRegistryLegalEntities.Generate(useLetters: true);

        // Assert
        result.Should().MatchRegex("^[A-Z0-9]{14}$");
    }
    
    [Fact]
    public void Generate_WhenUseLettersIsFalse_ReturnsNationalRegistryLegalEntitiesWithoutLetters()
    {
        // Act
        var result = NationalRegistryLegalEntities.Generate(useLetters: false);

        // Assert
        result.Should().MatchRegex(@"^\d{14}$");
    }
    
    [Fact]
    public void Generate_WhenUseLettersIsTrueAndFormattedIsTrue_ReturnsNationalRegistryLegalEntitiesWithLettersAndSpecialCharacters()
    {
        // Act
        var result = NationalRegistryLegalEntities.Generate(useLetters: true, formatted: true);

        // Assert
        result.Should().MatchRegex(@"^[A-Z0-9]{2}\.[A-Z0-9]{3}\.[A-Z0-9]{3}/[A-Z0-9]{4}-[A-Z0-9]{2}$");
    }
    
    [Fact]
    public void Format_WhenNationalRegistryLegalEntitiesIsValid_ReturnsNationalRegistryLegalEntitiesWithSpecialCharacters()
    {
        // Arrange
        const string nationalRegistryOfLegalEntities = "79594544000189";

        // Act
        var result = NationalRegistryLegalEntities.Format(nationalRegistryOfLegalEntities);

        // Assert
        result.Should().Be("79.594.544/0001-89");
    }
    
    [Fact]
    public void Format_WhenNationalRegistryLegalEntitiesIsInvalid_ReturnsNationalRegistryLegalEntitiesInformed()
    {
        // Arrange
        const string nationalRegistryOfLegalEntities = "7959454400018";

        // Act
        var result = NationalRegistryLegalEntities.Format(nationalRegistryOfLegalEntities);

        // Assert
        result.Should().Be(nationalRegistryOfLegalEntities);
    }
    
    [Fact]
    public void Format_WhenNationalRegistryLegalEntitiesIsEmpty_ReturnsNationalRegistryLegalEntitiesInformed()
    {
        // Arrange
        const string nationalRegistryOfLegalEntities = "";

        // Act
        var result = NationalRegistryLegalEntities.Format(nationalRegistryOfLegalEntities);

        // Assert
        result.Should().Be(nationalRegistryOfLegalEntities);
    }
    
    [Fact]
    public void Format_WhenNationalRegistryLegalEntitiesIsWhitespace_ReturnsNationalRegistryLegalEntitiesInformed()
    {
        // Arrange
        const string nationalRegistryOfLegalEntities = " ";

        // Act
        var result = NationalRegistryLegalEntities.Format(nationalRegistryOfLegalEntities);

        // Assert
        result.Should().Be(nationalRegistryOfLegalEntities);
    }
    
    [Fact]
    public void Format_WhenNationalRegistryLegalEntitiesIsNull_ReturnsNationalRegistryLegalEntitiesInformed()
    {
        // Arrange
        string nationalRegistryOfLegalEntities = null!;

        // Act
        var result = NationalRegistryLegalEntities.Format(nationalRegistryOfLegalEntities);

        // Assert
        result.Should().BeNull();
    }
    
    [Fact]
    public void Format_WhenNationalRegistryLegalEntitiesHasSpecialCharacters_ReturnsNationalRegistryLegalEntitiesWithSpecialCharacters()
    {
        // Arrange
        const string nationalRegistryOfLegalEntities = "79.594.544/0001-89";

        // Act
        var result = NationalRegistryLegalEntities.Format(nationalRegistryOfLegalEntities);

        // Assert
        result.Should().Be(nationalRegistryOfLegalEntities);
    }
    
}