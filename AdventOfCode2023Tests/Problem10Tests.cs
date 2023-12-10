using AdventOfCode2023.Day10;
using FluentAssertions;

namespace AdventOfCode2023Tests;

public class Problem10Tests
{
    [Theory]
    [InlineData("0 3 6 9 12 15", 18)]
    public void Solve1_xxx(string input, int expectedResult)
    {
        Oasis report = Problem10.TranslateInput(input);
        int result = Problem10.Solve1();

        result.Should().Be(expectedResult);
    }

    [Fact]
    public void Solve1_FullTest()
    {
        int result = Problem10.Solve1();

        result.Should().Be(1938800261);
    }
}
