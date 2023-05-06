namespace DPG.Core.UnitTests.EncodePasswordGeneratorTests;

public class GeneratePasswordShould
{
    private readonly EncodePasswordGenerator classUnderTest;

    public GeneratePasswordShould()
    {
        classUnderTest = new EncodePasswordGenerator();
    }

    [Fact]
    public void GeneratePassword_GivenValidPassPhraseInput()
    {
        var givenPassPhrase = "password";
        var options = new PasswordGeneratorEncodeOptions
        {
            PassPhrase = givenPassPhrase
        };

        var result = classUnderTest.GeneratePassword(options);

        Assert.NotEmpty(result);
        Assert.Equal(givenPassPhrase.Length, result.Length);
        Assert.True(givenPassPhrase != result);
    }

    [Fact]
    public void ThrowException_GivenBlankPassPhraseInput()
    {
        var givenPassPhrase = string.Empty;
        var options = new PasswordGeneratorEncodeOptions
        {
            PassPhrase = givenPassPhrase
        };

        Assert.Throws<Exception>(() =>
        {
            var result = classUnderTest.GeneratePassword(options);
        });
    }

    [Fact]
    public void ThrowException_GivenNullOptionInput()
    {
        var givenPassPhrase = string.Empty;
        var options = null as PasswordGeneratorEncodeOptions;

        Assert.Throws<Exception>(() =>
        {
            var result = classUnderTest.GeneratePassword(options);
        });
    }

    [Fact]
    public void GeneratePasswordWithoutSpaces_GivenPassPhraseInputAndDisabledTheSubstituteOption()
    {
        var givenPassPhrase = "the quick brown fox";
        var options = new PasswordGeneratorEncodeOptions
        {
            PassPhrase = givenPassPhrase,
            SubstituteSpacesWithUnderscores = false
        };

        var result = classUnderTest.GeneratePassword(options);

        Assert.NotEmpty(result);
        Assert.NotEqual(givenPassPhrase.Length, result.Length);

        var passPhraseSpaceCount = givenPassPhrase.Count(c => c == ' ');
        Assert.True(result.Length == givenPassPhrase.Length - passPhraseSpaceCount);
    }

    [Fact]
    public void GeneratePasswordWithSpaces_GivenPassPhraseInputAndEnabledSubstituteOption()
    {
        var givenPassPhrase = "the quick brown fox";
        var options = new PasswordGeneratorEncodeOptions
        {
            PassPhrase = givenPassPhrase,
            SubstituteSpacesWithUnderscores = true
        };

        var result = classUnderTest.GeneratePassword(options);

        Assert.NotEmpty(result);
        Assert.Equal(givenPassPhrase.Length, result.Length);
        Assert.True(givenPassPhrase != result);
    }
}
