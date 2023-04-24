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
}