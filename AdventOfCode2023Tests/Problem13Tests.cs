using AdventOfCode2023.Day13;
using FluentAssertions;

namespace AdventOfCode2023Tests;

public class Problem13Tests
{
    [Fact]
    public void Solve1_FullTest()
    {
        var result = Problem13.Solve1();

        result.Should().BeGreaterThan(25906);
        result.Should().BeGreaterThan(42906);
        result.Should().Be(42974);
    }

    [Fact]
    public void Solve1_FindMirrorIndexes()
    {
        List<string> inputLines = new List<string>()
        {
            "#.##..##.\n..#.##.#.\n##......#\n##......#\n..#.##.#.\n..##..##.\n#.#.##.#.\n\n#...##..#\n#....#..#\n..##..###\n#####.##.\n#####.##.\n..##..###\n#....#..#"
        };
        inputLines = inputLines.SelectMany(x => x.Split("\n")).ToList();
        var rockFormations = Problem13.TranslateInput(inputLines);
        Problem13.FindMirrorIndexes(rockFormations);
        var result = Problem13.SumMirrorIndexes(rockFormations);

        result.Should().Be(405);
    }

    [Fact]
    public void Solve1_FindMirrorIndexes2()
    {
        List<string> inputLines = new List<string>()
        {
            "#..#.#.###..#.#.#\n#.#..##.#.#####.#\n####.#####.#.....\n####.#####.#.....\n#.#..##.#.#####.#\n#..###.###..#.#.#\n..#.#.#.....##.##\n.#.#..##.##..##..\n.#.#..##.##..##.."
        };
        inputLines = inputLines.SelectMany(x => x.Split("\n")).ToList();
        var rockFormations = Problem13.TranslateInput(inputLines);
        Problem13.FindMirrorIndexes(rockFormations);
        var result = Problem13.SumMirrorIndexes(rockFormations);

        result.Should().Be(800);
    }

    [Fact]
    public void Solve1_FindMirrorIndexes3()
    {
        List<string> inputLines = new List<string>()
        {
            "......####.......\n#..###....###.###\n.##..##..##..##..\n.#..###..###..#..\n...#.#....#.#....\n.#.##.#..#.##.#..\n####.######.#####\n##....#..#....###\n.####..##..####.."
        };
        inputLines = inputLines.SelectMany(x => x.Split("\n")).ToList();
        var rockFormations = Problem13.TranslateInput(inputLines);
        Problem13.FindMirrorIndexes(rockFormations);
        var result = Problem13.SumMirrorIndexes(rockFormations);

        result.Should().Be(16);
    }
}
