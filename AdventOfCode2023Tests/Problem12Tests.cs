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
}
