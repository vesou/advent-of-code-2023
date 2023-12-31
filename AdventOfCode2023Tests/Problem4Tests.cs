using AdventOfCode2023.Day04;
using FluentAssertions;

namespace AdventOfCode2023Tests;

public class Problem4Tests
{
    [Theory]
    [InlineData("41 48 83 86 17 | 83 86  6 31 17  9 48 53", 8)]
    [InlineData("13 32 20 16 61 | 61 30 68 82 17 32 24 19", 2)]
    [InlineData("1 21 53 59 44 | 69 82 63 72 16 21 14  1", 2)]
    [InlineData("41 92 73 84 69 | 59 84 76 51 58  5 54 83", 1)]
    [InlineData("87 83 26 28 32 | 88 30 70 12 93 22 82 36", 0)]
    [InlineData("31 18 13 56 72 | 74 77 10 23 35 67 36 11", 0)]
    public void Solve1_CalculateGamePoints(string input, int expectedResult)
    {
        var result = Problem4.CalculateGamePoints(input);

        result.Should().Be(expectedResult);
    }

    [Fact]
    public void Solve1_FullTest()
    {
        int result = Problem4.Solve1();

        result.Should().Be(20855);
    }

    [Fact]
    public void Solve2_CountOfAllScratchCardGames()
    {
        List<string> inputLines = new List<string>
        {
            "41 48 83 86 17 | 83 86  6 31 17  9 48 53",
            "13 32 20 16 61 | 61 30 68 82 17 32 24 19",
            "1 21 53 59 44 | 69 82 63 72 16 21 14  1",
            "41 92 73 84 69 | 59 84 76 51 58  5 54 83",
            "87 83 26 28 32 | 88 30 70 12 93 22 82 36",
            "31 18 13 56 72 | 74 77 10 23 35 67 36 11"
        };

        List<ScratchCardGame> scratchCardGames = Problem4.TranslateInput(inputLines);
        var result = Problem4.CountOfAllScratchCardGames(scratchCardGames);

        result.Should().Be(30);
    }

    [Fact]
    public void Solve2_FullTest()
    {
        int result = Problem4.Solve2();

        result.Should().Be(5489600);
    }
}
