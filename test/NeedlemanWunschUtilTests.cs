using FluentAssertions;
using Soenneker.Tests.FixturedUnit;
using Xunit;
using Xunit.Abstractions;

namespace Soenneker.Utils.String.NeedlemanWunsch.Tests;

[Collection("Collection")]
public class NeedlemanWunschUtilTests : FixturedUnitTest
{
    public NeedlemanWunschUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Theory]
    [InlineData("", "", 0)]
    [InlineData("abc", "", 3)]
    [InlineData("", "xyz", 3)]
    [InlineData("kitten", "sitting", 3)]
    [InlineData("kitten", "kitten", 0)]
    [InlineData("abc", "def", 3)]
    [InlineData("abcdef", "abc", 3)]
    [InlineData("abc", "abcd", 1)]
    public void CalculateSimilarity_Returns_Correct_Similarity_Score(string str1, string str2, int expectedScore)
    {
        int similarityScore = NeedlemanWunschUtil.CalculateSimilarity(str1, str2);

        similarityScore.Should().Be(expectedScore);
    }

    [Theory]
    [InlineData("", "", 0)]
    [InlineData("abc", "", 3)]
    [InlineData("", "xyz", 3)]
    [InlineData("kitten", "sitting", 3)]
    [InlineData("kitten", "kitten", 0)]
    [InlineData("abc", "def", 3)]
    [InlineData("abcdef", "abc", 3)]
    [InlineData("abc", "abcd", 1)]
    public void CalculateSimilarityInParallel_Returns_Correct_Similarity_Score(string str1, string str2, int expectedScore)
    {
        int similarityScore = NeedlemanWunschUtil.CalculateSimilarityInParallel(str1, str2);

        similarityScore.Should().Be(expectedScore);
    }

    [Theory]
    [InlineData("", "", 100.0)]
    [InlineData("abc", "", 0.0)]
    [InlineData("", "xyz", 0.0)]
    [InlineData("kitten", "sitting", 57.1428)]
    [InlineData("kitten", "kitten", 100.0)]
    [InlineData("abc", "def", 0.0)]
    [InlineData("abcdef", "abc", 50.0)]
    [InlineData("abc", "abcd", 75.0)]
    public void CalculateSimilarityPercentage_Returns_Correct_Percentage(string str1, string str2, double expectedPercentage)
    {
        double similarityPercentage = NeedlemanWunschUtil.CalculateSimilarityPercentage(str1, str2);

        similarityPercentage.Should().BeApproximately(expectedPercentage, 0.001);
    }

    [Theory]
    [InlineData("", "", 100.0)]
    [InlineData("abc", "", 0.0)]
    [InlineData("", "xyz", 0.0)]
    [InlineData("kitten", "sitting", 57.1428)]
    [InlineData("kitten", "kitten", 100.0)]
    [InlineData("abc", "def", 0.0)]
    [InlineData("abcdef", "abc", 50.0)]
    [InlineData("abc", "abcd", 75.0)]
    public void CalculateSimilarityPercentage_with_parallel_Returns_Correct_Percentage(string str1, string str2, double expectedPercentage)
    {
        double similarityPercentage = NeedlemanWunschUtil.CalculateSimilarityPercentage(str1, str2, true);

        similarityPercentage.Should().BeApproximately(expectedPercentage, 0.001);
    }
}
