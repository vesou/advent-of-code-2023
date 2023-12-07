using AdventOfCode2023.Day2;
using FluentAssertions;

namespace AdventOfCode2023Tests;

public class Problem2Tests
{
    [Theory]
    [InlineData("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", true)]
    [InlineData("Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue", true)]
    [InlineData("Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red", false)]
    [InlineData("Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red", false)]
    [InlineData("Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green", true)]
    public void Solve1_CheckGame(string line, bool gameIsPossible)
    {
        Game game = Problem2.MapIntoGame(line);
        bool result = Problem2.CheckGame(game, 14, 12,13);
        result.Should().Be(gameIsPossible);
    }

    [Fact]
    public void Solve1_FullTest()
    {
        int result = Problem2.Solve1(14, 12, 13);

        result.Should().Be(3059);
    }

    [Theory]
    [InlineData("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", 48)]
    [InlineData("Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue", 12)]
    [InlineData("Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red", 1560)]
    [InlineData("Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red", 630)]
    [InlineData("Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green", 36)]
    public void Solve2_GetMultipliedSumOfMaxValues(string line, int gamePower)
    {
        Game game = Problem2.MapIntoGame(line);
        int result = Problem2.GetMultipliedSumOfMaxValues(game);
        result.Should().Be(gamePower);
    }

    [Fact]
    public void Solve2_FullTest()
    {
        int result = Problem2.Solve2();

        result.Should().Be(65371);
    }
}
