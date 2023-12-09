using AdventOfCode2023.Day9;
using FluentAssertions;

namespace AdventOfCode2023Tests;

public class Problem9Tests
{
    [Theory]
    [InlineData("0 3 6 9 12 15", 18)]
    [InlineData("1 3 6 10 15 21", 28)]
    [InlineData("10 13 16 21 30 45", 68)]
    public void Solve1_PredictNextNumber(string input, int expectedResult)
    {
        Oasis report = Problem9.TranslateInput(input);
        var result = Problem9.PredictNextNumber(report.Numbers);

        result.Should().Be(expectedResult);
    }

    [Fact]
    public void Solve1_FullTest()
    {
        int result = Problem9.Solve1();

        result.Should().Be(1938800261);
    }
}
