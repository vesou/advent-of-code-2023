using AdventOfCode2023.Day10;
using FluentAssertions;

namespace AdventOfCode2023Tests;

public class Problem10Tests
{
    [Fact]
    public void Solve1_FindFurthestTile_EasyLoop()
    {
        List<string> input = ".....\n.S-7.\n.|.|.\n.L-J.\n.....".Split("\n").ToList();
        Maze maze = Problem10.TranslateInput(input);
        int result = Problem10.FindFurthestTile(maze);

        result.Should().Be(4);
    }

    [Fact]
    public void Solve1_FindFurthestTile_WithOtherPipes()
    {
        List<string> input = "-L|F7\n7S-7|\nL|7||\n-L-J|\nL|-JF".Split("\n").ToList();
        Maze maze = Problem10.TranslateInput(input);
        int result = Problem10.FindFurthestTile(maze);

        result.Should().Be(4);
    }

    [Fact]
    public void Solve1_FindFurthestTile_MoreComplexLoop()
    {
        List<string> input = "7-F7-\n.FJ|7\nSJLL7\n|F--J\nLJ.LJ".Split("\n").ToList();
        Maze maze = Problem10.TranslateInput(input);
        int result = Problem10.FindFurthestTile(maze);

        result.Should().Be(8);
    }

    [Fact]
    public void Solve1_FullTest()
    {
        int result = Problem10.Solve1();

        result.Should().Be(0);
    }
}
