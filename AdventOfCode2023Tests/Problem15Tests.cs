using AdventOfCode2023.Day14;
using AdventOfCode2023.Day15;
using FluentAssertions;
using Xunit.Abstractions;

namespace AdventOfCode2023Tests;

public class Problem15Tests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public Problem15Tests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Solve1_FullTest()
    {
        var result = Problem15.Solve1();

        result.Should().Be(0);
    }

    [Theory]
    [InlineData("HASH", 52)]
    [InlineData("rn=1", 30)]
    public void Solve1_GetHash(string inputString, int expectedResult)
    {
        var result = Problem15.GetHash(inputString);

        result.Should().Be(expectedResult);
    }

    [Fact]
    public void Solve1_GetSumOfHashes()
    {
        string inputLine = "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7";
        var data = Problem15.TranslateInput(inputLine);

        long result = Problem15.GetSumOfAllHashes(data);

        result.Should().Be(1320);
    }
}
