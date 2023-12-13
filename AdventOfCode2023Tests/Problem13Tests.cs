using AdventOfCode2023.Day13;
using FluentAssertions;

namespace AdventOfCode2023Tests;

public class Problem13Tests
{
    [Fact]
    public void Solve1_FullTest()
    {
        var result = Problem13.Solve1();

        result.Should().Be(0);
    }

    [Theory]
    [InlineData("???.### 1,1,3", 1)]
    public void Solve1_GetNumberOfCombinations(string inputLine, int numberOfCombinations)
    {
        List<string> inputLines = new List<string>();
        inputLines.Add(inputLine);
        var dmgReport = Problem13.TranslateInput(inputLines);
        var result = Problem13.Solve1();

        result.Should().Be(numberOfCombinations);
    }
}
