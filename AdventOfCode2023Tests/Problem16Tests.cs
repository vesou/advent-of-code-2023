using AdventOfCode2023.Day16;
using FluentAssertions;
using Xunit.Abstractions;

namespace AdventOfCode2023Tests;

public class Problem16Tests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public Problem16Tests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Solve1_FullTest()
    {
        var result = Problem16.Solve1();

        result.Should().Be(8323);
    }

    [Theory]
    [InlineData(Problem16.Up, Problem16.MirrorLeft, Problem16.Right)]
    [InlineData(Problem16.Up, Problem16.MirrorRight, Problem16.Left)]
    [InlineData(Problem16.Down, Problem16.MirrorLeft, Problem16.Left)]
    [InlineData(Problem16.Down, Problem16.MirrorRight, Problem16.Right)]
    [InlineData(Problem16.Left, Problem16.MirrorLeft, Problem16.Down)]
    [InlineData(Problem16.Left, Problem16.MirrorRight, Problem16.Up)]
    [InlineData(Problem16.Right, Problem16.MirrorLeft, Problem16.Up)]
    [InlineData(Problem16.Right, Problem16.MirrorRight, Problem16.Down)]
    public void Solve1_ChangeDirection(int x, char mirrorType, int expectedResult)
    {
        var result = Problem16.ChangeDirection(x, mirrorType);

        result.Should().Be(expectedResult);
    }

    [Fact]
    public void Solve1_Scenario1()
    {
        List<string> inputLines = new List<string>()
        {
            ".|...\\....\n|.-.\\.....\n.....|-...\n........|.\n..........\n.........\\\n..../.\\\\..\n.-.-/..|..\n.|....-|.\\\n..//.|...."
        };
        inputLines = inputLines.SelectMany(x => x.Split("\n")).ToList();
        var data = Problem16.TranslateInput(inputLines);
        Problem16.ReflectLight(data);

        string test = Problem16.ShowVisitedPixels(data.VisitedPixel);
        long result = Problem16.CountPixelsWithLight(data);

        result.Should().Be(46);
    }

    [Fact]
    public void Solve2_Scenario1()
    {
        List<string> inputLines = new List<string>()
        {
            ".|...\\....\n|.-.\\.....\n.....|-...\n........|.\n..........\n.........\\\n..../.\\\\..\n.-.-/..|..\n.|....-|.\\\n..//.|...."
        };
        inputLines = inputLines.SelectMany(x => x.Split("\n")).ToList();
        var data = Problem16.TranslateInput(inputLines);
        Problem16.ReflectLight(data);

        string test = Problem16.ShowVisitedPixels(data.VisitedPixel);
        long result = Problem16.FindBestLightReflectionScore(data);

        result.Should().Be(51);
    }


    [Fact]
    public void Solve2_FullTest()
    {
        var result = Problem16.Solve2();

        result.Should().Be(8491);
    }
}
