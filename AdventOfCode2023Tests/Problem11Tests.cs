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
        result.Should().Be(9445168);
    }

    [Theory]
    [InlineData(0, 3, 10, 12)]
    public void Solve2_CalculateHorizontalDistance(int x1, int x2, int expansionSize, int horizontalDistance)
    {
        List<string> inputLines = "...#......\n.......#..\n#.........\n..........\n......#...\n.#........\n.........#\n..........\n.......#..\n#...#....."
            .Split("\n")
            .ToList();
        var universe = Problem11.TranslateInput(inputLines);
        var expansionResult = Problem11.FindExpandingRowsAndColumns(universe.Matrix);
        int result = Problem11.CalculateHorizontalDistance(x1, x2, expansionResult.columnsToExpand, expansionSize);
        result.Should().Be(horizontalDistance);
    }

    [Theory]
    [InlineData(10, 1030)]
    [InlineData(100, 8410)]
    public void Solve2_SumAllPaths2(int expansionSize, int expected)
    {
        List<string> inputLines = "...#......\n.......#..\n#.........\n..........\n......#...\n.#........\n.........#\n..........\n.......#..\n#...#....."
            .Split("\n")
            .ToList();
        var universe = Problem11.TranslateInput(inputLines);
        (var rowsToExpand, var columnsToExpand) = Problem11.FindExpandingRowsAndColumns(universe.Matrix);
        universe.RowsToExpand = rowsToExpand;
        universe.ColumnsToExpand = columnsToExpand;

        var result = Problem11.SumAllPaths2(universe, expansionSize);
        result.Should().Be(expected);
    }

    [Fact]
    public void Solve2_FullTest()
    {
        long result = Problem11.Solve2(1_000_000);
        result.Should().Be(0);
    }
}
