using AdventOfCode2023.Day13;
using AdventOfCode2023.Day14;
using FluentAssertions;

namespace AdventOfCode2023Tests;

public class Problem14Tests
{
    [Fact]
    public void Solve1_FullTest()
    {
        var result = Problem14.Solve1();

        result.Should().Be(0);
    }

    [Theory]
    [InlineData(0, 34)]
    [InlineData(1, 27)]
    [InlineData(2, 17)]
    [InlineData(3, 10)]
    [InlineData(4, 8)]
    [InlineData(5, 7)]
    [InlineData(6, 7)]
    [InlineData(7, 14)]
    [InlineData(8, 0)]
    [InlineData(9, 12)]
    public void Solve1_CalculateLineLoad(int x, int expectedResult)
    {
        List<string> inputLines = new List<string>()
        {
            "O....#....\nO.OO#....#\n.....##...\nOO.#O....O\n.O.....O#.\nO.#..O.#.#\n..O..#O..O\n.......O..\n#....###..\n#OO..#...."
        };
        inputLines = inputLines.SelectMany(x => x.Split("\n")).ToList();
        var rockFormations = Problem14.TranslateInput(inputLines);

        var result = Problem14.CalculateLineLoad(rockFormations.Rocks, x);

        result.Should().Be(expectedResult);
    }

    [Fact]
    public void Solve1_CalculateTotalLoad()
    {
        List<string> inputLines = new List<string>()
        {
            "O....#....\nO.OO#....#\n.....##...\nOO.#O....O\n.O.....O#.\nO.#..O.#.#\n..O..#O..O\n.......O..\n#....###..\n#OO..#...."
        };
        inputLines = inputLines.SelectMany(x => x.Split("\n")).ToList();
        var rockFormations = Problem14.TranslateInput(inputLines);

        var result = Problem14.CalculateTotalLoad(rockFormations);

        result.Should().Be(136);
    }
}
