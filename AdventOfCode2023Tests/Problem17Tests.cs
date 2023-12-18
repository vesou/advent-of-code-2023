using AdventOfCode2023.Day17;
using AdventOfCode2023.Day17.part2;
using FluentAssertions;
using Xunit.Abstractions;

namespace AdventOfCode2023Tests;

public class Problem17Tests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public Problem17Tests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Solve1_FullTest()
    {
        var result = Problem17.Solve1();

        result.Should().Be(1099);
    }

    [Theory]
    [InlineData(0, 0,   102)]
    [InlineData(11, 11, 6)]
    [InlineData(10, 10, 15)]
    [InlineData(12, 7, 28)]
    [InlineData(11, 7, 27)]
    [InlineData(11, 4, 47)]
    public void Solve1_TestDijkstra(int x, int y, int expectedResult)
    {
        List<string> inputLines = new List<string>()
        {
            "2413432311323\n3215453535623\n3255245654254\n3446585845452\n4546657867536\n1438598798454\n4457876987766\n3637877979653\n4654967986887\n4564679986453\n1224686865563\n2546548887735\n4322674655533"
        };
        inputLines = inputLines.SelectMany(x => x.Split("\n")).ToList();
        var data = Problem17.TranslateInput(inputLines);
        var result = DijkstraAlgorithm.Dijkstra(data.Grid, x, y, data.Grid.GetLength(1)-1, data.Grid.GetLength(0)-1);

        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(0, 0,   102)]
    [InlineData(11, 11, 6)]
    [InlineData(10, 10, 15)]
    [InlineData(11, 7, 27)]
    [InlineData(11, 4, 47)]
    public void Solve1_TestDijkstra2(int x, int y, int expectedResult)
    {
        List<string> inputLines = new List<string>()
        {
            "2413432311323\n3215453535623\n3255245654254\n3446585845452\n4546657867536\n1438598798454\n4457876987766\n3637877979653\n4654967986887\n4564679986453\n1224686865563\n2546548887735\n4322674655533"
        };
        inputLines = inputLines.SelectMany(x => x.Split("\n")).ToList();
        inputLines = SmallerInput(inputLines, x, y);
        var data = Problem17.TranslateInput(inputLines);
        var result = DijkstraAlgorithm.Dijkstra(data.Grid, 0, 0, data.Grid.GetLength(1)-1, data.Grid.GetLength(0)-1);

        result.Should().Be(expectedResult);
    }

    private List<string> SmallerInput(List<string> inputLines, int x, int y)
    {
        var result = new List<string>();
        for (int i = 0; i < inputLines.Count; i++)
        {
            if (i < y)
                continue;

            result.Add(inputLines[i].Substring(x, inputLines[i].Length - x));
        }

        return result;
    }

    [Fact]
    public void Solve2_FullTest()
    {
        var result = Problem17.Solve2();

        result.Should().Be(1266);
    }

    [Theory]
    [InlineData(0, 0,   94)]
    public void Solve2_TestDijkstra(int x, int y, int expectedResult)
    {
        List<string> inputLines = new List<string>()
        {
            "2413432311323\n3215453535623\n3255245654254\n3446585845452\n4546657867536\n1438598798454\n4457876987766\n3637877979653\n4654967986887\n4564679986453\n1224686865563\n2546548887735\n4322674655533"
        };
        inputLines = inputLines.SelectMany(x => x.Split("\n")).ToList();
        var data = Problem17.TranslateInput(inputLines);
        var result = DijkstraAlgorithm2.Dijkstra(data.Grid, x, y, data.Grid.GetLength(1)-1, data.Grid.GetLength(0)-1);

        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(0, 0,   71)]
    public void Solve2_TestDijkstra2(int x, int y, int expectedResult)
    {
        List<string> inputLines = new List<string>()
        {
            "111111111111\n999999999991\n999999999991\n999999999991\n999999999991"
        };
        inputLines = inputLines.SelectMany(x => x.Split("\n")).ToList();
        var data = Problem17.TranslateInput(inputLines);
        var result = DijkstraAlgorithm2.Dijkstra(data.Grid, x, y, data.Grid.GetLength(1)-1, data.Grid.GetLength(0)-1);

        result.Should().Be(expectedResult);
    }
}
