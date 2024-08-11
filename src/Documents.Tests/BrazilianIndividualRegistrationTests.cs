

namespace GeekSevenLabs.Utilities.Documents.Tests;

public class BrazilianIndividualRegistrationTests
{
    [Fact]
    public void IsValid_WhenIndividualRegistrationIsNull_ReturnsFalse()
    {
        // Arrange
        string individualRegistration = null!;

        // Act
        var result = BrazilianIndividualRegistration.IsValid(individualRegistration);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void IsValid_WhenIndividualRegistrationIsEmpty_ReturnsFalse()
    {
        // Arrange
        var individualRegistration = string.Empty;

        // Act
        var result = BrazilianIndividualRegistration.IsValid(individualRegistration);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void IsValid_WhenIndividualRegistrationIsWhiteSpace_ReturnsFalse()
    {
        // Arrange
        const string individualRegistration = " ";

        // Act
        var result = BrazilianIndividualRegistration.IsValid(individualRegistration);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void IsValid_WhenIndividualRegistrationIsInvalid_ReturnsFalse()
    {
        // Arrange
        const string individualRegistration = "12345678901";

        // Act
        var result = BrazilianIndividualRegistration.IsValid(individualRegistration);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void IsValid_WhenIndividualRegistrationIsValid_ReturnsTrue()
    {
        // Arrange
        const string individualRegistration = "34701370053";

        // Act
        var result = BrazilianIndividualRegistration.IsValid(individualRegistration);

        // Assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public void IsValid_WhenIndividualRegistrationHasNonNumericCharacters_ReturnsTrue()
    {
        // Arrange
        const string individualRegistration = "347.013.700-53";

        // Act
        var result = BrazilianIndividualRegistration.IsValid(individualRegistration);

        // Assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public void IsValid_WhenIndividualRegistrationHasLessThanElevenCharacters_ReturnsFalse()
    {
        // Arrange
        const string individualRegistration = "1249218543";

        // Act
        var result = BrazilianIndividualRegistration.IsValid(individualRegistration);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void IsValid_WhenIndividualRegistrationHasMoreThanElevenCharacters_ReturnsFalse()
    {
        // Arrange
        const string individualRegistration = "347013700536";

        // Act
        var result = BrazilianIndividualRegistration.IsValid(individualRegistration);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void IsValid_WhenIndividualRegistrationHasFirstVerificationDigitInvalid_ReturnsFalse()
    {
        // Arrange
        const string individualRegistration = "12492185455";

        // Act
        var result = BrazilianIndividualRegistration.IsValid(individualRegistration);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void IsValid_WhenIndividualRegistrationHasSecondVerificationDigitInvalid_ReturnsFalse()
    {
        // Arrange
        const string individualRegistration = "12492185436";

        // Act
        var result = BrazilianIndividualRegistration.IsValid(individualRegistration);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void Generate_WhenRegionIsNullAndFormattedIsFalse_ReturnsValidIndividualRegistration()
    {
        // Arrange and Act
        var individualRegistration = BrazilianIndividualRegistration.Generate();

        // Assert
        individualRegistration.Should().MatchRegex(@"^\d{11}$");
        BrazilianIndividualRegistration.IsValid(individualRegistration).Should().BeTrue();
    }
    
    [Fact]
    public void Generate_WhenRegionIsNullAndFormattedIsTrue_ReturnsValidFormattedIndividualRegistration()
    {
        // Arrange and Act
        var individualRegistration = BrazilianIndividualRegistration.Generate(formatted: true);

        // Assert
        individualRegistration.Should().MatchRegex(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$");
        BrazilianIndividualRegistration.IsValid(individualRegistration).Should().BeTrue();
    }
    
    [Fact]
    public void Generate_WhenRegionIsNotNullAndFormattedIsFalse_ReturnsValidIndividualRegistration()
    {
        // Arrange and Act
        var individualRegistration = BrazilianIndividualRegistration.Generate(1);

        // Assert
        individualRegistration.Should().MatchRegex(@"^\d{8}1\d{2}$");
        BrazilianIndividualRegistration.IsValid(individualRegistration).Should().BeTrue();
    }
    
    [Fact]
    public void Generate_WhenRegionIsNotNullAndFormattedIsTrue_ReturnsValidFormattedIndividualRegistration()
    {
        // Arrange and Act
        var individualRegistration = BrazilianIndividualRegistration.Generate(1, true);

        // Assert
        individualRegistration.Should().MatchRegex(@"^\d{3}.\d{3}.\d{2}1-\d{2}$");
        BrazilianIndividualRegistration.IsValid(individualRegistration).Should().BeTrue();
    }
    
    [Fact]
    public void Format_WhenIndividualRegistrationHasElevenCharacters_ReturnsFormattedIndividualRegistration()
    {
        // Arrange
        const string individualRegistration = "34701370053";

        // Act
        var result = BrazilianIndividualRegistration.Format(individualRegistration);

        // Assert
        result.Should().Be("347.013.700-53");
    }
    
    [Fact]
    public void Format_WhenIndividualRegistrationHasLessThanElevenCharacters_ReturnsIndividualRegistration()
    {
        // Arrange
        const string individualRegistration = "1249218543";

        // Act
        var result = BrazilianIndividualRegistration.Format(individualRegistration);

        // Assert
        individualRegistration.Should().Be(result);
    }
    
    [Fact]
    public void Format_WhenIndividualRegistrationHasMoreThanElevenCharacters_ReturnsIndividualRegistration()
    {
        // Arrange
        const string individualRegistration = "347013700536";

        // Act
        var result = BrazilianIndividualRegistration.Format(individualRegistration);

        // Assert
        individualRegistration.Should().Be(result);
    }
    
    [Fact]
    public void GetInfo_WhenIndividualRegistrationIsNull_ThrowsArgumentException()
    {
        // Arrange
        string individualRegistration = null!;

        // Act
        var execution = () => BrazilianIndividualRegistration.GetInfo(individualRegistration);

        // Assert
        execution.Should().Throw<ArgumentException>().WithMessage("Value cannot be null. (Parameter 'individualRegistration')");
    }
    
    [Fact]
    public void GetInfo_WhenIndividualRegistrationIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var individualRegistration = string.Empty;

        // Act
        var execution = () => BrazilianIndividualRegistration.GetInfo(individualRegistration);

        // Assert
        execution.Should().Throw<ArgumentException>().WithMessage("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'individualRegistration')");
    }
    
    [Fact]
    public void GetInfo_WhenIndividualRegistrationHasLessThanElevenCharacters_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        const string individualRegistration = "1249218543";

        // Act
        var execution = () => BrazilianIndividualRegistration.GetInfo(individualRegistration);

        // Assert
        execution.Should().Throw<ArgumentOutOfRangeException>().WithMessage("individualRegistration.Length ('10') must be equal to '11'. (Parameter 'individualRegistration.Length')\nActual value was 10.");
    }
    
    [Fact]
    public void GetInfo_WhenIndividualRegistrationHasMoreThanElevenCharacters_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        const string individualRegistration = "347013700536";

        // Act
        var execution = () => BrazilianIndividualRegistration.GetInfo(individualRegistration);

        // Assert
        execution.Should().Throw<ArgumentOutOfRangeException>().WithMessage("individualRegistration.Length ('12') must be equal to '11'. (Parameter 'individualRegistration.Length')\nActual value was 12.");
    }
    
    [Fact]
    public void GetInfo_WhenIndividualRegistrationIsValid_ReturnsIndividualRegistrationInfo()
    {
        // Arrange
        const string individualRegistration = "34701370053";

        // Act
        var result = BrazilianIndividualRegistration.GetInfo(individualRegistration);

        // Assert
        result.Value.Should().Be(individualRegistration);
        result.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public void GetInfo_WhenIndividualRegistrationIsInvalid_ReturnsIndividualRegistrationInfo()
    {
        // Arrange
        const string individualRegistration = "12492185436";

        // Act
        var result = BrazilianIndividualRegistration.GetInfo(individualRegistration);

        // Assert
        result.Value.Should().Be(individualRegistration);
        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void GetInfo_WhenIndividualRegistrationHasFirstVerificationDigit_ReturnsIndividualRegistrationInfo()
    {
        // Arrange
        const string individualRegistration = "34701370053";

        // Act
        var result = BrazilianIndividualRegistration.GetInfo(individualRegistration);

        // Assert
        result.FirstVerificationDigit.Should().Be(5);
    }
    
    [Fact]
    public void GetInfo_WhenIndividualRegistrationHasSecondVerificationDigit_ReturnsIndividualRegistrationInfo()
    {
        // Arrange
        const string individualRegistration = "34701370053";

        // Act
        var result = BrazilianIndividualRegistration.GetInfo(individualRegistration);

        // Assert
        result.SecondVerificationDigit.Should().Be(3);
    }
    
    [Fact]
    public void GetInfo_WhenIndividualRegistrationHasRegion_ReturnsIndividualRegistrationInfo()
    {
        // Arrange
        const string individualRegistration = "34701370053";

        // Act
        var result = BrazilianIndividualRegistration.GetInfo(individualRegistration);

        // Assert
        result.Region.Should().Be(0);
    }
}
