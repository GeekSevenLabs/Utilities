namespace GeekSevenLabs.Utilities.Documents;

internal static class CharExtensions
{
    /// <summary>
    /// Converts a character to its integer representation.
    /// </summary>
    /// <param name="c">The character to be converted.</param>
    /// <returns>An integer representing the character code of the given character.</returns>
    public static int ToDigitRepresentation(this char c) => c;
    
    /// <summary>
    /// Converts a character to an integer value with an offset applied.
    /// </summary>
    /// <param name="c">The character to be converted.</param>
    /// <param name="offset">The offset to subtract from the character's Unicode value.</param>
    /// <returns>An integer representing the character's Unicode value minus the specified offset.</returns>
    public static int ToDigitRepresentation(this char c, int offset) => c - offset;
    
    /// <summary>
    /// Parses a numeric character into its integer digit equivalent.
    /// </summary>
    /// <param name="digit">The character representing a numeric digit (0-9).</param>
    /// <returns>The integer value of the numeric character.</returns>
    /// <exception cref="FormatException">Thrown if the character is not a valid digit.</exception>
    public static int ToDigit(this char digit) => int.Parse(digit.ToString());
    
    /// <summary>
    /// Checks if the character is a numeric digit (0-9).
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns><c>true</c> if the character is a digit; otherwise, <c>false</c>.</returns>
    public static bool IsNumber(this char c) => c is >= '0' and <= '9';

    /// <summary>
    /// Checks if the character is not a numeric digit (0-9).
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns><c>true</c> if the character is not a digit; otherwise, <c>false</c>.</returns>
    public static bool IsNotNumber(this char c) => !c.IsNumber();
}
