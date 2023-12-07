using AdventOfCode2023.Day3;
using FluentAssertions;

namespace AdventOfCode2023Tests;

public class Problem3Tests
{
    [Theory]
    [InlineData("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", true)]
    public void Solve1_Test(string line, bool gameIsPossible)
    {

    }

    [Fact]
    public void Solve1_FullTest()
    {
        int result = Problem3.Solve1();

        result.Should().Be(3059);
    }
}
