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

        result.Should().Be(6886);
    }

    /*
     * FS-7F-7....
     * L-7||.|....
     * ..|LJ.|....
     * ..|...|....
     * ..L7F7|....
     * ...LJLJ....
     */
    [Fact]
    public void Solve2_CountTilesEnclosedByMainLoop0()
    {
        List<string> input = "FS-7F-7....\nL-7||.|....\n..|LJ.|....\n..|...|....\n..L7F7|....\n...LJLJ....\n"
            .Split("\n")
            .ToList();
        Maze maze = Problem10.TranslateInput(input);
        bool[][] test = maze.Tiles.Select(x => x.Select(y => y.IsInsideMainLoop).ToArray()).ToArray();
        int result = Problem10.CountTilesEnclosedByMainLoop(maze);

        result.Should().Be(5);
    }

    /*
     * ...........
       .S-------7.
       .|F-----7|.
       .||.....||.
       .||.....||.
       .|L-7.F-J|.
       .|..|.|..|.
       .L--J.L--J.
       ...........
     */
    [Fact]
    public void Solve2_CountTilesEnclosedByMainLoop1()
    {
        List<string> input = "...........\n.S-------7.\n.|F-----7|.\n.||.....||.\n.||.....||.\n.|L-7.F-J|.\n.|..|.|..|.\n.L--J.L--J.\n..........."
            .Split("\n")
            .ToList();
        Maze maze = Problem10.TranslateInput(input);
        bool[][] test = maze.Tiles.Select(x => x.Select(y => y.IsInsideMainLoop).ToArray()).ToArray();
        int result = Problem10.CountTilesEnclosedByMainLoop(maze);

        result.Should().Be(4);
    }

    [Fact]
    public void Solve2_CountTilesEnclosedByMainLoop2()
    {
        List<string> input = "..........\n.S------7.\n.|F----7|.\n.||..7-||.\n.||..7-||.\n.|L-7F-J|.\n.|..||..|.\n.L--JL--J.\n.........."
            .Split("\n")
            .ToList();
        Maze maze = Problem10.TranslateInput(input);
        int result = Problem10.CountTilesEnclosedByMainLoop(maze);

        result.Should().Be(4);
    }

    /*
     * .F----7F7F7F7F-7....
       .|F--7||||||||FJ....
       .||.FJ||||||||L7....
       FJL7L7LJLJ||LJ.L-7..
       L--J.L7...LJS7F-7L7.
       ....F-J..F7FJ|L7L7L7
       ....L7.F7||L7|.L7L7|
       .....|FJLJ|FJ|F7|.LJ
       ....FJL-7.||.||||...
       ....L---J.LJ.LJLJ...
     */
    [Fact]
    public void Solve2_CountTilesEnclosedByMainLoop3()
    {
        List<string> input = ".F----7F7F7F7F-7....\n.|F--7||||||||FJ....\n.||.FJ||||||||L7....\nFJL7L7LJLJ||LJ.L-7..\nL--J.L7...LJS7F-7L7.\n....F-J..F7FJ|L7L7L7\n....L7.F7||L7|.L7L7|\n.....|FJLJ|FJ|F7|.LJ\n....FJL-7.||.||||...\n....L---J.LJ.LJLJ..."
            .Split("\n")
            .ToList();
        Maze maze = Problem10.TranslateInput(input);
        int result = Problem10.CountTilesEnclosedByMainLoop(maze);

        result.Should().Be(8);
    }

    /*
     * FF7FSF7F7F7F7F7F---7
       L|LJ||||||||||||F--J
       FL-7LJLJ||||||LJL-77
       F--JF--7||LJLJ7F7FJ-
       L---JF-JLJ.||-FJLJJ7
       |F|F-JF---7F7-L7L|7|
       |FFJF7L7F-JF7|JL---7
       7-L-JL7||F7|L7F-7F7|
       L.L7LFJ|||||FJL7||LJ
       L7JLJL-JLJLJL--JLJ.L
     */
    [Fact]
    public void Solve2_CountTilesEnclosedByMainLoop4()
    {
        List<string> input = "FF7FSF7F7F7F7F7F---7\nL|LJ||||||||||||F--J\nFL-7LJLJ||||||LJL-77\nF--JF--7||LJLJ7F7FJ-\nL---JF-JLJ.||-FJLJJ7\n|F|F-JF---7F7-L7L|7|\n|FFJF7L7F-JF7|JL---7\n7-L-JL7||F7|L7F-7F7|\nL.L7LFJ|||||FJL7||LJ\nL7JLJL-JLJLJL--JLJ.L"
            .Split("\n")
            .ToList();
        Maze maze = Problem10.TranslateInput(input);
        int result = Problem10.CountTilesEnclosedByMainLoop(maze);

        result.Should().Be(10);
    }

    [Fact]
    public void Solve2_FullTest()
    {
        int result = Problem10.Solve2();

        result.Should().Be(371);
    }
}
