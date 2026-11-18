using AwesomeAssertions;
using Soenneker.Tests.HostedUnit;


namespace Soenneker.Utils.String.NeedlemanWunsch.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class NeedlemanWunschUtilTests : HostedUnitTest
{
    public NeedlemanWunschUtilTests(Host host) : base(host)
    {
    }

    [Test]
    [Arguments(63)]
    [Arguments(64)]
    [Arguments(65)]
    [Arguments(128)]
    [Arguments(129)]
    [Arguments(1024)]
    [Arguments(1025)]
    public void Distance_counts_edits_across_word_boundaries(int length)
    {
        string middle = new string('a', length - 2);
        string first = "x" + middle + "y";
        string second = "λ" + middle.Insert(middle.Length / 2, "!") + "z";

        NeedlemanWunschStringUtil.CalculateSimilarity(first, second).Should().Be(3);
        NeedlemanWunschStringUtil.CalculateSimilarity(second, first).Should().Be(3);
    }

    [Test]
    [Arguments("", "", 0)]
    [Arguments("abc", "", 3)]
    [Arguments("", "xyz", 3)]
    [Arguments("kitten", "sitting", 3)]
    [Arguments("kitten", "kitten", 0)]
    [Arguments("abc", "def", 3)]
    [Arguments("abcdef", "abc", 3)]
    [Arguments("abc", "abcd", 1)]
    public void CalculateSimilarity_Returns_Correct_Similarity_Score(string str1, string str2, int expectedScore)
    {
        int similarityScore = NeedlemanWunschStringUtil.CalculateSimilarity(str1, str2);

        similarityScore.Should().Be(expectedScore);
    }


    [Test]
    [Arguments("", "", 100.0)]
    [Arguments("abc", "", 0.0)]
    [Arguments("", "xyz", 0.0)]
    [Arguments("kitten", "sitting", 57.1428)]
    [Arguments("kitten", "kitten", 100.0)]
    [Arguments("abc", "def", 0.0)]
    [Arguments("abcdef", "abc", 50.0)]
    [Arguments("abc", "abcd", 75.0)]
    public void CalculateSimilarityPercentage_Returns_Correct_Percentage(string str1, string str2, double expectedPercentage)
    {
        double similarityPercentage = NeedlemanWunschStringUtil.CalculateSimilarityPercentage(str1, str2);

        similarityPercentage.Should().BeApproximately(expectedPercentage, 0.001);
    }
}

