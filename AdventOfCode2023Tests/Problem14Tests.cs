using System.Diagnostics;
using AdventOfCode2023.Day14;
using FluentAssertions;
using Xunit.Abstractions;

namespace AdventOfCode2023Tests;

public class Problem14Tests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public Problem14Tests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Solve1_FullTest()
    {
        var result = Problem14.Solve1();

        result.Should().Be(111339);
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

        var result = Problem14.MoveRocksNorthAndCalculateLineLoad(rockFormations.Rocks, x);

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

        var result = Problem14.MoveNorthAndCalculateTotalLoad(rockFormations);

        result.Should().Be(136);
    }

    [Fact]
    public void Solve2_FullTest()
    {
        var result = Problem14.Solve2();

        result.Should().Be(93736);
    }

    [Fact]
    public void Solve2_Spin()
    {
        List<string> inputLines = new List<string>()
        {
            "O....#....\nO.OO#....#\n.....##...\nOO.#O....O\n.O.....O#.\nO.#..O.#.#\n..O..#O..O\n.......O..\n#....###..\n#OO..#...."
        };

        List<string> expectedLines = new List<string>()
        {
            ".....#....\n....#...O#\n...OO##...\n.OO#......\n.....OOO#.\n.O#...O#.#\n....O#....\n......OOOO\n#...O###..\n#..OO#...."
        };
        expectedLines = expectedLines.SelectMany(x => x.Split("\n")).ToList();
        char[][] expectedArray = expectedLines.Select(x => x.ToCharArray()).ToArray();

        inputLines = inputLines.SelectMany(x => x.Split("\n")).ToList();
        var rockFormations = Problem14.TranslateInput(inputLines);
        rockFormations.Spin2(1);

        rockFormations.Rocks[0].Should().BeEquivalentTo(expectedArray[0]);
        rockFormations.Rocks[1].Should().BeEquivalentTo(expectedArray[1]);
        rockFormations.Rocks[2].Should().BeEquivalentTo(expectedArray[2]);
        rockFormations.Rocks[3].Should().BeEquivalentTo(expectedArray[3]);
        rockFormations.Rocks[4].Should().BeEquivalentTo(expectedArray[4]);
        rockFormations.Rocks[5].Should().BeEquivalentTo(expectedArray[5]);
        rockFormations.Rocks[6].Should().BeEquivalentTo(expectedArray[6]);
        rockFormations.Rocks[7].Should().BeEquivalentTo(expectedArray[7]);
        rockFormations.Rocks[8].Should().BeEquivalentTo(expectedArray[8]);
        rockFormations.Rocks[9].Should().BeEquivalentTo(expectedArray[9]);
        rockFormations.Rocks.Should().BeEquivalentTo(expectedArray);
    }

    [Fact]
    public void Solve2_Tilt()
    {
        List<string> inputLines = new List<string>()
        {
            "O....#....\nO.OO#....#\n.....##...\nOO.#O....O\n.O.....O#.\nO.#..O.#.#\n..O..#O..O\n.......O..\n#....###..\n#OO..#...."
        };

        List<string> expectedLines = new List<string>()
        {
            "OOOO.#.O..\nOO..#....#\nOO..O##..O\nO..#.OO...\n........#.\n..#....#.#\n..O..#.O.O\n..O.......\n#....###..\n#....#...."
        };
        expectedLines = expectedLines.SelectMany(x => x.Split("\n")).ToList();
        char[][] expectedArray = expectedLines.Select(x => x.ToCharArray()).ToArray();

        inputLines = inputLines.SelectMany(x => x.Split("\n")).ToList();
        var rockFormations = Problem14.TranslateInput(inputLines);
        rockFormations.Tilt(0, -1);

        rockFormations.Rocks[0].Should().BeEquivalentTo(expectedArray[0]);
        rockFormations.Rocks[1].Should().BeEquivalentTo(expectedArray[1]);
        rockFormations.Rocks[2].Should().BeEquivalentTo(expectedArray[2]);
        rockFormations.Rocks[3].Should().BeEquivalentTo(expectedArray[3]);
        rockFormations.Rocks[4].Should().BeEquivalentTo(expectedArray[4]);
        rockFormations.Rocks[5].Should().BeEquivalentTo(expectedArray[5]);
        rockFormations.Rocks[6].Should().BeEquivalentTo(expectedArray[6]);
        rockFormations.Rocks[7].Should().BeEquivalentTo(expectedArray[7]);
        rockFormations.Rocks[8].Should().BeEquivalentTo(expectedArray[8]);
        rockFormations.Rocks[9].Should().BeEquivalentTo(expectedArray[9]);
        rockFormations.Rocks.Should().BeEquivalentTo(expectedArray);
    }

    /*
     * OOOO.#.O..
       OO..#....#
       OO..O##..O
       O..#.OO...
       ........#.
       ..#....#.#
       ..O..#.O.O
       ..O.......
       #....###..
       #....#....

       OOOO.#O...
       OO..#....#
       OOO..##O..
       O..#OO....
       ........#.
       ..#....#.#
       O....#OO..
       O.........
       #....###..
       #....#....
     */
    [Fact]
    public void Solve2_Tilt2()
    {
        List<string> inputLines = new List<string>()
        {
            "O....#....\nO.OO#....#\n.....##...\nOO.#O....O\n.O.....O#.\nO.#..O.#.#\n..O..#O..O\n.......O..\n#....###..\n#OO..#...."
        };

        List<string> expectedLines = new List<string>()
        {
            "OOOO.#O...\nOO..#....#\nOOO..##O..\nO..#OO....\n........#.\n..#....#.#\nO....#OO..\nO.........\n#....###..\n#....#...."
        };
        expectedLines = expectedLines.SelectMany(x => x.Split("\n")).ToList();
        char[][] expectedArray = expectedLines.Select(x => x.ToCharArray()).ToArray();

        inputLines = inputLines.SelectMany(x => x.Split("\n")).ToList();
        var rockFormations = Problem14.TranslateInput(inputLines);
        rockFormations.Tilt(0, -1);
        rockFormations.Tilt(-1, 0);

        rockFormations.Rocks[0].Should().BeEquivalentTo(expectedArray[0]);
        rockFormations.Rocks[1].Should().BeEquivalentTo(expectedArray[1]);
        rockFormations.Rocks[2].Should().BeEquivalentTo(expectedArray[2]);
        rockFormations.Rocks[3].Should().BeEquivalentTo(expectedArray[3]);
        rockFormations.Rocks[4].Should().BeEquivalentTo(expectedArray[4]);
        rockFormations.Rocks[5].Should().BeEquivalentTo(expectedArray[5]);
        rockFormations.Rocks[6].Should().BeEquivalentTo(expectedArray[6]);
        rockFormations.Rocks[7].Should().BeEquivalentTo(expectedArray[7]);
        rockFormations.Rocks[8].Should().BeEquivalentTo(expectedArray[8]);
        rockFormations.Rocks[9].Should().BeEquivalentTo(expectedArray[9]);
        rockFormations.Rocks.Should().BeEquivalentTo(expectedArray);
    }

     /*
     *  OOOO.#O...
        OO..#....#
        OOO..##O..
        O..#OO....
        ........#.
        ..#....#.#
        O....#OO..
        O.........
        #....###..
        #....#....

       .....#....
       ....#.O..#
       O..O.##...
       O.O#......
       O.O....O#.
       O.#..O.#.#
       O....#....
       OO....OO..
       #O...###..
       #O..O#....
     */
    [Fact]
    public void Solve2_Tilt3()
    {
        List<string> inputLines = new List<string>()
        {
            "O....#....\nO.OO#....#\n.....##...\nOO.#O....O\n.O.....O#.\nO.#..O.#.#\n..O..#O..O\n.......O..\n#....###..\n#OO..#...."
        };

        List<string> expectedLines = new List<string>()
        {
            ".....#....\n....#.O..#\nO..O.##...\nO.O#......\nO.O....O#.\nO.#..O.#.#\nO....#....\nOO....OO..\n#O...###..\n#O..O#...."
        };
        expectedLines = expectedLines.SelectMany(x => x.Split("\n")).ToList();
        char[][] expectedArray = expectedLines.Select(x => x.ToCharArray()).ToArray();

        inputLines = inputLines.SelectMany(x => x.Split("\n")).ToList();
        var rockFormations = Problem14.TranslateInput(inputLines);
        rockFormations.Tilt(0, -1);
        rockFormations.Tilt(-1, 0);
        rockFormations.TiltDescendingY(0, 1);

        rockFormations.Rocks[0].Should().BeEquivalentTo(expectedArray[0]);
        rockFormations.Rocks[1].Should().BeEquivalentTo(expectedArray[1]);
        rockFormations.Rocks[2].Should().BeEquivalentTo(expectedArray[2]);
        rockFormations.Rocks[3].Should().BeEquivalentTo(expectedArray[3]);
        rockFormations.Rocks[4].Should().BeEquivalentTo(expectedArray[4]);
        rockFormations.Rocks[5].Should().BeEquivalentTo(expectedArray[5]);
        rockFormations.Rocks[6].Should().BeEquivalentTo(expectedArray[6]);
        rockFormations.Rocks[7].Should().BeEquivalentTo(expectedArray[7]);
        rockFormations.Rocks[8].Should().BeEquivalentTo(expectedArray[8]);
        rockFormations.Rocks[9].Should().BeEquivalentTo(expectedArray[9]);
        rockFormations.Rocks.Should().BeEquivalentTo(expectedArray);
    }

    [Fact]
    public void Solve2_Spin3()
    {
        List<string> inputLines = new List<string>()
        {
            "O....#....\nO.OO#....#\n.....##...\nOO.#O....O\n.O.....O#.\nO.#..O.#.#\n..O..#O..O\n.......O..\n#....###..\n#OO..#...."
        };

        List<string> expectedLines = new List<string>()
        {
            ".....#....\n....#...O#\n.....##...\n..O#......\n.....OOO#.\n.O#...O#.#\n....O#...O\n.......OOO\n#...O###.O\n#.OOO#...O"
        };
        expectedLines = expectedLines.SelectMany(x => x.Split("\n")).ToList();
        char[][] expectedArray = expectedLines.Select(x => x.ToCharArray()).ToArray();

        inputLines = inputLines.SelectMany(x => x.Split("\n")).ToList();
        var rockFormations = Problem14.TranslateInput(inputLines);
        rockFormations.Spin2(3);

        rockFormations.Rocks[0].Should().BeEquivalentTo(expectedArray[0]);
        rockFormations.Rocks[1].Should().BeEquivalentTo(expectedArray[1]);
        rockFormations.Rocks[2].Should().BeEquivalentTo(expectedArray[2]);
        rockFormations.Rocks[3].Should().BeEquivalentTo(expectedArray[3]);
        rockFormations.Rocks[4].Should().BeEquivalentTo(expectedArray[4]);
        rockFormations.Rocks[5].Should().BeEquivalentTo(expectedArray[5]);
        rockFormations.Rocks[6].Should().BeEquivalentTo(expectedArray[6]);
        rockFormations.Rocks[7].Should().BeEquivalentTo(expectedArray[7]);
        rockFormations.Rocks[8].Should().BeEquivalentTo(expectedArray[8]);
        rockFormations.Rocks[9].Should().BeEquivalentTo(expectedArray[9]);
        rockFormations.Rocks.Should().BeEquivalentTo(expectedArray);

        var result = Problem14.CalculateLoad(rockFormations.Rocks);
        result.Should().Be(69);
    }

    [Fact]
    public void Solve2_SpinLoads2()
    {
        List<string> inputLines = new List<string>()
        {
            "O....#....\nO.OO#....#\n.....##...\nOO.#O....O\n.O.....O#.\nO.#..O.#.#\n..O..#O..O\n.......O..\n#....###..\n#OO..#...."
        };

        inputLines = inputLines.SelectMany(x => x.Split("\n")).ToList();
        var rockFormations = Problem14.TranslateInput(inputLines);
        var stopwatch = Stopwatch.StartNew();
        rockFormations.Spin2(1_000_000_000);
        _testOutputHelper.WriteLine(stopwatch.ElapsedMilliseconds.ToString());

        var result = Problem14.CalculateLoad(rockFormations.Rocks);

        result.Should().Be(64);
    }
}
