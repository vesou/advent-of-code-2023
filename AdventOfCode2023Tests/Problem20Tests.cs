using AdventOfCode2023.Day20;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit.Abstractions;

namespace AdventOfCode2023Tests;

public class Problem20Tests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public Problem20Tests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Solve1_FullTest()
    {
        var result = Problem20.Solve1();

        result.Should().Be(0);
    }

    [Theory]
    [InlineData("%a", "a", ModuleType.FlipFlop)]
    [InlineData("&a", "a", ModuleType.Conjunction)]
    [InlineData("broadcaster", "broadcaster", ModuleType.Broadcast)]
    [InlineData("test", "test", ModuleType.Empty)]
    public void Solve1_GetModuleName(string labelWithType, string expectedLabel, ModuleType expectedType)
    {
        var result = Data.GetModule(labelWithType);

        result.label.Should().Be(expectedLabel);
        result.moduleType.Should().Be(expectedType);
    }

    [Theory]
    [InlineData(4, 8)]
    public void Solve1_GetNumberOfHighAndLowPulses1(int numberOfHighPulses, int numberOfLowPulses)
    {
        List<string> inputLines = new List<string>()
        {
            "broadcaster -> a, b, c\n%a -> b\n%b -> c\n%c -> inv\n&inv -> a"
        };
        inputLines = inputLines.SelectMany(x => x.Split("\n")).ToList();
        var data = Problem20.TranslateInput(inputLines);
        var result = Problem20.GetNumberOfHighAndLowPulses(data);

        using (new AssertionScope())
        {
            result.numberOfHighPulses.Should().Be(numberOfHighPulses);
            result.numberOfLowPulses.Should().Be(numberOfLowPulses);
        }
    }

    [Theory]
    [InlineData(4, 4)]
    public void Solve1_GetNumberOfHighAndLowPulses2(int numberOfHighPulses, int numberOfLowPulses)
    {
        List<string> inputLines = new List<string>()
        {
            "broadcaster -> a\n%a -> inv, con\n&inv -> b\n%b -> con\n&con -> output"
        };
        inputLines = inputLines.SelectMany(x => x.Split("\n")).ToList();
        var data = Problem20.TranslateInput(inputLines);
        var result = Problem20.GetNumberOfHighAndLowPulses(data);

        using (new AssertionScope())
        {
            result.numberOfHighPulses.Should().Be(numberOfHighPulses);
            result.numberOfLowPulses.Should().Be(numberOfLowPulses);
        }
    }

    [Fact]
    public void Solve1_GetPulsePower1()
    {
        List<string> inputLines = new List<string>()
        {
            "broadcaster -> a, b, c\n%a -> b\n%b -> c\n%c -> inv\n&inv -> a"
        };
        inputLines = inputLines.SelectMany(x => x.Split("\n")).ToList();
        var data = Problem20.TranslateInput(inputLines);
        var result = Problem20.GetPulsePower(data, 1000);

        result.Should().Be(32000000);
    }

    [Fact]
    public void Solve1_GetPulsePower2()
    {
        List<string> inputLines = new List<string>()
        {
            "broadcaster -> a\n%a -> inv, con\n&inv -> b\n%b -> con\n&con -> output"
        };
        inputLines = inputLines.SelectMany(x => x.Split("\n")).ToList();
        var data = Problem20.TranslateInput(inputLines);
        var result = Problem20.GetPulsePower(data, 4);

        result.Should().Be(17*11);
    }

    [Fact]
    public void Solve2_FullTest()
    {
        var result = Problem20.Solve2();

        result.Should().Be(0);
    }
}
