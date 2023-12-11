using AdventOfCode2023.Day11;
using FluentAssertions;

namespace AdventOfCode2023Tests;

public class Problem11Tests
{
    [Fact]
    public void Solve1_Expand()
    {
        List<string> inputLines = "...#......\n.......#..\n#.........\n..........\n......#...\n.#........\n.........#\n..........\n.......#..\n#...#....."
            .Split("\n")
            .ToList();
        var universe = Problem11.TranslateInput(inputLines);
        var expanded = universe.Expand();
        expanded.Length.Should().Be(inputLines.Count+2);
        expanded[0].Length.Should().Be(inputLines[0].Length+3);
    }

    [Fact]
    public void Solve1_FindAllPaths()
    {
        List<string> inputLines = "...#......\n.......#..\n#.........\n..........\n......#...\n.#........\n.........#\n..........\n.......#..\n#...#....."
            .Split("\n")
            .ToList();
        var universe = Problem11.TranslateInput(inputLines);
        var expanded = universe.Expand();
        var result = Problem11.FindAllPaths(expanded);
        result.Count.Should().Be(36);
    }

    /*
     * ...#......
       .......#..
       #.........
       ..........
       ......#...
       .#........
       .........#
       ..........
       .......#..
       #...#.....
     */
    [Theory]
    [InlineData(1, 6, 5, 11, 9)]
    [InlineData(4, 0, 9, 10, 15)]
    public void Solve1_CalculateShortestPath1(int x1, int y1, int x2, int y2, int minimalPathLength)
    {
        List<string> inputLines = "...#......\n.......#..\n#.........\n..........\n......#...\n.#........\n.........#\n..........\n.......#..\n#...#....."
            .Split("\n")
            .ToList();
        var universe = Problem11.TranslateInput(inputLines);
        var expanded = universe.Expand();
        Galaxy pointA = new Galaxy(x1, y1);
        var pointB = new Galaxy(x2, y2);
        int result = Problem11.CalculateShortestPath(pointA, pointB);
        result.Should().Be(minimalPathLength);
    }

    [Fact]
    public void Solve1_SumAllPaths()
    {
        List<string> inputLines = "...#......\n.......#..\n#.........\n..........\n......#...\n.#........\n.........#\n..........\n.......#..\n#...#....."
            .Split("\n")
            .ToList();
        var universe = Problem11.TranslateInput(inputLines);
        var expanded = universe.Expand();
        int result = Problem11.SumAllPaths(expanded);
        result.Should().Be(374);
    }

    [Fact]
    public void Solve1_FullTest()
    {
        int result = Problem11.Solve1();
        // 18890336 was too high
        result.Should().BeLessThan(18890336);
        result.Should().Be(18890336);
    }
}
