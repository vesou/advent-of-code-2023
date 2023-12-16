using AdventOfCode2023.Day16;
using FluentAssertions;
using Xunit.Abstractions;

namespace AdventOfCode2023Tests;

public class Problem16Tests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public Problem16Tests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Solve1_FullTest()
    {
        var result = Problem16.Solve1();

        result.Should().Be(0);
    }

    [Theory]
    [InlineData(0, 34)]
    public void Solve1_Test1(int x, int expectedResult)
    {
        List<string> inputLines = new List<string>()
        {
            "O....#....\nO.OO#....#\n.....##...\nOO.#O....O\n.O.....O#.\nO.#..O.#.#\n..O..#O..O\n.......O..\n#....###..\n#OO..#...."
        };
        inputLines = inputLines.SelectMany(x => x.Split("\n")).ToList();
        var data = Problem16.TranslateInput(inputLines);
        long result = 0;

        result.Should().Be(expectedResult);
    }

    [Fact]
    public void Solve2_FullTest()
    {
        var result = Problem16.Solve2();

        result.Should().Be(0);
    }
}