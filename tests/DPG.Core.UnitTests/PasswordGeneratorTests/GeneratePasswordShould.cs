namespace DPG.Core.UnitTests.PasswordGeneratorTests;

public class GeneratePasswordShould
{
    private readonly PasswordGenerator classUnderTest;

    private const string UppercaseCharPattern = @"[A-Z]";
    private const string LowercaseCharPattern = @"[a-z]";
    private const string NumericCharPattern = @"[0-9]";
    private const string SpecialCharPattern = @"[!""#$%&'+,./:;=?@\^|~]";

    public GeneratePasswordShould()
    {
        classUnderTest = new PasswordGenerator();
    }

    [Fact]
    public void GeneratePassword_GivenDefaultOptions()
    {
        var options = new PasswordGeneratorStandardOptions();
        var result = classUnderTest.GeneratePassword(options);

        Assert.NotEmpty(result);
        Assert.Equal(result.Length, options.PasswordLength);
        Assert.Matches(UppercaseCharPattern, result);
        Assert.Matches(LowercaseCharPattern, result);
        Assert.Matches(NumericCharPattern, result);
        Assert.Matches(SpecialCharPattern, result);
    }

    [Fact]
    public void GeneratePasswordWithCorrectLength_GivenSpecifiedPasswordLengthInOptions()
    {
        int passwordLength = 32;
        var options = new PasswordGeneratorStandardOptions()
        {
            PasswordLength = passwordLength
        };
        var result = classUnderTest.GeneratePassword(options);

        Assert.NotEmpty(result);
        Assert.Equal(result.Length, passwordLength);
    }

    [Fact]
    public void ThrowException_GivenPasswordLength0InOptions()
    {
        int passwordLength = 0;
        var options = new PasswordGeneratorStandardOptions()
        {
            PasswordLength = passwordLength
        };

        Assert.Throws<Exception>(() =>
        {
            var result = classUnderTest.GeneratePassword(options);
        });
    }

    [Fact]
    public void GeneratePasswordWithoutUppercaseCharacter_GivenDisabledUppercaseCharOption()
    {
        var enableUppercaseChar = false;
        var options = new PasswordGeneratorStandardOptions
        {
            EnableUppercaseCharacters = enableUppercaseChar
        };

        var result = classUnderTest.GeneratePassword(options);

        Assert.NotEmpty(result);
        Assert.DoesNotMatch(UppercaseCharPattern, result);
    }
}