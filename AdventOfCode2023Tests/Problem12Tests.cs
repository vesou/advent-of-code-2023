using AdventOfCode2023.Day12;
using FluentAssertions;

namespace AdventOfCode2023Tests;

public class Problem12Tests
{
    [Fact]
    public void Solve1_FullTest()
    {
        var result = Problem12.Solve1();

        result.Should().Be(7173);
    }

    [Theory]
    [InlineData("???.### 1,1,3", 1)]
    [InlineData(".??..??...?##. 1,1,3", 4)]
    [InlineData("?#?#?#?#?#?#?#? 1,3,1,6", 1)]
    [InlineData("????.#...#... 4,1,1", 1)]
    [InlineData("????.######..#####. 1,6,5", 4)]
    [InlineData("?###???????? 3,2,1", 10)]
    public void Solve1_GetNumberOfCombinations(string inputLine, int numberOfCombinations)
    {
        List<string> inputLines = new List<string>();
        inputLines.Add(inputLine);
        var dmgReport = Problem12.TranslateInput(inputLines);
        var result = Problem12.GetNumberOfCombinations(dmgReport[0].HotSprings, dmgReport[0].DamageNumbers);

        result.Should().Be(numberOfCombinations);
    }

    [Fact]
    public void Solve2_FullTest()
    {
        var result = Problem12.Solve2();

        result.Should().Be(29826669191291);
    }

    [Theory]
    [InlineData("???.### 1,1,3", 1)]
    [InlineData(".??..??...?##. 1,1,3", 16384)]
    [InlineData("?#?#?#?#?#?#?#? 1,3,1,6", 1)]
    [InlineData("????.#...#... 4,1,1", 16)]
    [InlineData("????.######..#####. 1,6,5", 2500)]
    [InlineData("?###???????? 3,2,1", 506250)]
    public void Solve2_GetNumberOfCombinations2(string inputLine, int numberOfCombinations)
    {
        List<string> inputLines = new List<string>();
        inputLines.Add(inputLine);
        inputLines = inputLines.Select(Problem12.ExpandLine).ToList();
        var dmgReport = Problem12.TranslateInput(inputLines);
        Problem12.Combinations = new List<(string combination, long countOfCombinations, int dmgIndex, bool previousWasOperational)>();
        var result = Problem12.GetNumberOfCombinations(dmgReport[0].HotSprings, dmgReport[0].DamageNumbers);

        result.Should().Be(numberOfCombinations);
    }

    [Theory]
    [InlineData(".# 1", ".#?.#?.#?.#?.# 1,1,1,1,1")]
    public void Solve2_ExpandLine(string inputLine, string expandedLine)
    {
        var result = Problem12.ExpandLine(inputLine);

        result.Should().Be(expandedLine);
    }
}
