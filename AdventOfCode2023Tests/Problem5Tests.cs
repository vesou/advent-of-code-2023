using AdventOfCode2023.Day04;
using AdventOfCode2023.Day05;
using AdventOfCode2023.Day12;
using FluentAssertions;

namespace AdventOfCode2023Tests;

public class Problem5Tests
{
    [Theory]
    [InlineData(79, 81)]
    [InlineData(14, 14)]
    [InlineData(55, 57)]
    [InlineData(13, 13)]
    public void Solve1_TranslateSeedToSoil(int source, int destination)
    {
        List<string> inputLines = "seeds: 79 14 55 13\n\nseed-to-soil map:\n50 98 2\n52 50 48\n\nsoil-to-fertilizer map:\n0 15 37\n37 52 2\n39 0 15\n\nfertilizer-to-water map:\n49 53 8\n0 11 42\n42 0 7\n57 7 4\n\nwater-to-light map:\n88 18 7\n18 25 70\n\nlight-to-temperature map:\n45 77 23\n81 45 19\n68 64 13\n\ntemperature-to-humidity map:\n0 69 1\n1 0 69\n\nhumidity-to-location map:\n60 56 37\n56 93 4"
            .Split("\n")
            .ToList();
        var data = Problem5.TranslateInput(inputLines);
        var result = data.SeedToSoil.Transform(source);

        result.Should().Be(destination);
    }

    [Theory]
    [InlineData(81, 81)]
    [InlineData(14, 53)]
    [InlineData(57, 57)]
    [InlineData(13, 52)]
    public void Solve1_TranslateSoilToFertilizer(int source, int destination)
    {
        List<string> inputLines = "seeds: 79 14 55 13\n\nseed-to-soil map:\n50 98 2\n52 50 48\n\nsoil-to-fertilizer map:\n0 15 37\n37 52 2\n39 0 15\n\nfertilizer-to-water map:\n49 53 8\n0 11 42\n42 0 7\n57 7 4\n\nwater-to-light map:\n88 18 7\n18 25 70\n\nlight-to-temperature map:\n45 77 23\n81 45 19\n68 64 13\n\ntemperature-to-humidity map:\n0 69 1\n1 0 69\n\nhumidity-to-location map:\n60 56 37\n56 93 4"
            .Split("\n")
            .ToList();
        var data = Problem5.TranslateInput(inputLines);
        var result = data.SoilToFertilizer.Transform(source);

        result.Should().Be(destination);
    }

    [Theory]
    [InlineData(81, 81)]
    [InlineData(53, 49)]
    [InlineData(57, 53)]
    [InlineData(52, 41)]
    public void Solve1_TranslateFertilizerToWater(int source, int destination)
    {
        List<string> inputLines = "seeds: 79 14 55 13\n\nseed-to-soil map:\n50 98 2\n52 50 48\n\nsoil-to-fertilizer map:\n0 15 37\n37 52 2\n39 0 15\n\nfertilizer-to-water map:\n49 53 8\n0 11 42\n42 0 7\n57 7 4\n\nwater-to-light map:\n88 18 7\n18 25 70\n\nlight-to-temperature map:\n45 77 23\n81 45 19\n68 64 13\n\ntemperature-to-humidity map:\n0 69 1\n1 0 69\n\nhumidity-to-location map:\n60 56 37\n56 93 4"
            .Split("\n")
            .ToList();
        var data = Problem5.TranslateInput(inputLines);
        var result = data.FertilizerToWater.Transform(source);

        result.Should().Be(destination);
    }

    [Fact]
    public void Solve1_TranslateAllSeedsToLocations()
    {
        List<string> inputLines = "seeds: 79 14 55 13\n\nseed-to-soil map:\n50 98 2\n52 50 48\n\nsoil-to-fertilizer map:\n0 15 37\n37 52 2\n39 0 15\n\nfertilizer-to-water map:\n49 53 8\n0 11 42\n42 0 7\n57 7 4\n\nwater-to-light map:\n88 18 7\n18 25 70\n\nlight-to-temperature map:\n45 77 23\n81 45 19\n68 64 13\n\ntemperature-to-humidity map:\n0 69 1\n1 0 69\n\nhumidity-to-location map:\n60 56 37\n56 93 4"
            .Split("\n")
            .ToList();
        var result = Problem5.TranslateInput(inputLines);
        result.TranslateAllSeedsToLocations();

        result.SeedsToLocation.Should().BeEquivalentTo(new List<long> { 82, 43, 86, 35 });
    }

    [Fact]
    public void Solve1_FullTest()
    {
        long result = Problem5.Solve1();

        result.Should().BeLessThan(218367919);
        result.Should().Be(51580674);
    }
}
