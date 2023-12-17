using AdventOfCode2023.Day17;
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

        result.Should().Be(0);
    }

    [Theory]
    [InlineData(0, 0, 3, Problem17.Right, 0, 102)]
    public void Solve1_Test1(int x, int y, int maxStepsInOneDirection, int incomingDirection, int directionCount, int expectedResult)
    {
        List<string> inputLines = new List<string>()
        {
            "2413432311323\n3215453535623\n3255245654254\n3446585845452\n4546657867536\n1438598798454\n4457876987766\n3637877979653\n4654967986887\n4564679986453\n1224686865563\n2546548887735\n4322674655533"
        };
        inputLines = inputLines.SelectMany(x => x.Split("\n")).ToList();
        var data = Problem17.TranslateInput(inputLines);
        Problem17.CalculateBestRoutes(data, maxStepsInOneDirection, x, y, incomingDirection, directionCount);

        var result = data.VisitedPixel[x, 0].RoutesList
            .Min(r => r.BestDistanceFromEnd);
        result.Should().Be(expectedResult);
    }

    [Fact]
    public void Solve2_FullTest()
    {
        var result = Problem17.Solve2();

        result.Should().Be(0);
    }
}
