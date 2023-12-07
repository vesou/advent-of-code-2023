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
            ".664.598.."
        };

        char[][] inputData = Problem3.TranslateInput(input);
        var result = Problem3.SumOfParts(inputData);

        result.Should().Be(4361);
    }

    [Fact]
    public void Solve1_FullTest()
    {
        int result = Problem3.Solve1();
        // 535391 was too high
        result.Should().Be(3059);
    }
}
