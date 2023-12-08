using AdventOfCode2023.Day3;
using FluentAssertions;

namespace AdventOfCode2023Tests;

public class Problem3Tests
{
    [Fact]
    public void Solve1_SumOfParts()
    {
        List<string> input = new List<string>()
        {
            "467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
            ".....+.58.",
            "..592.....",
            "......755.",
            "...$.*....",
            ".664.598..",
            ".880\\/818.",
        };

        char[][] inputData = Problem3.TranslateInput(input);
        var result = Problem3.SumOfParts(inputData);

        result.Should().Be(4361+880+818);
    }

    [Fact]
    public void Solve1_FullTest()
    {
        int result = Problem3.Solve1();
        // 535391 was too high
        result.Should().Be(532445);
    }

    [Fact]
    public void Solve2_SumOfGears()
    {
        List<string> input = new List<string>()
        {
            "467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
            ".....+.58.",
            "..592.....",
            "......755.",
            "...$.*....",
            ".664.598..",
            ".880\\/818.",
        };

        char[][] inputData = Problem3.TranslateInput(input);
        var result = Problem3.SumOfGears(inputData);

        result.Should().Be(467835);
    }

    [Fact]
    public void Solve2_FullTest()
    {
        int result = Problem3.Solve2();

        result.Should().Be(532445);
    }
}
