using AdventOfCode2023.Day19;
using FluentAssertions;
using Xunit.Abstractions;

namespace AdventOfCode2023Tests;

public class Problem19Tests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public Problem19Tests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Solve1_FullTest()
    {
        var result = Problem19.Solve1();

        result.Should().Be(342650);
    }

    [Fact]
    public void Solve1_Test1()
    {
        List<string> inputLines = new List<string>()
        {
            "px{a<2006:qkq,m>2090:A,rfg}\npv{a>1716:R,A}\nlnx{m>1548:A,A}\nrfg{s<537:gd,x>2440:R,A}\nqs{s>3448:A,lnx}\nqkq{x<1416:A,crn}\ncrn{x>2662:A,R}\nin{s<1351:px,qqz}\nqqz{s>2770:qs,m<1801:hdj,R}\ngd{a>3333:R,R}\nhdj{m>838:A,pv}\n\n{x=787,m=2655,a=1222,s=2876}\n{x=1679,m=44,a=2067,s=496}\n{x=2036,m=264,a=79,s=2244}\n{x=2461,m=1339,a=466,s=291}\n{x=2127,m=1623,a=2188,s=1013}"
        };
        inputLines = inputLines.SelectMany(x => x.Split("\n")).ToList();
        var data = Problem19.TranslateInput(inputLines);
        long result = Problem19.SumOfAcceptedParts(data);

        result.Should().Be(19114);
    }

    [Fact]
    public void Solve2_FullTest()
    {
        var result = Problem19.Solve2();

        result.Should().Be(0);
    }
}
