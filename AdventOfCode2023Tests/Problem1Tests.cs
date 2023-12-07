using AdventOfCode2023.Day1;
using FluentAssertions;

namespace AdventOfCode2023Tests;

public class Problem1Tests
{
    [Theory]
    [InlineData("two1nine", 1,1)]
    [InlineData("3one8ncctmbsixeighttwonegb", 3,8)]
    [InlineData("512ninexrqpvktwoner", 5,2)]
    [InlineData("ngjrvdd3onezcklpsfoureighteightwoxg", 3,3)]
    public void Solve1_Tests(string line, int sum1, int sum2)
    {
        int firstDigit = Problem1.FindFirstDigit1(line, Problem1.Translator);
        int lastDigit = Problem1.FindLastDigit1(line, Problem1.Translator);

        firstDigit.Should().Be(sum1);
        lastDigit.Should().Be(sum2);
    }

    [Fact]
    public void Solve1_FullTest()
    {
        int result = Problem1.Solve1();

        result.Should().Be(54644);
    }

    [Theory]
    [InlineData("two1nine", 2,9)]
    [InlineData("3one8ncctmbsixeighttwonegb", 3,1)]
    [InlineData("512ninexrqpvktwoner", 5,1)]
    [InlineData("ngjrvdd3onezcklpsfoureighteightwoxg", 3,2)]
    public void Solve2_Tests(string line, int sum1, int sum2)
    {
        int firstDigit = Problem1.FindFirstDigit2(line, Problem1.Translator2);
        int lastDigit = Problem1.FindLastDigit2(line, Problem1.Translator2);

        firstDigit.Should().Be(sum1);
        lastDigit.Should().Be(sum2);
    }

    [Fact]
    public void Solve2_FullTest()
    {
        int result = Problem1.Solve2();

        result.Should().Be(5464);
    }
}
