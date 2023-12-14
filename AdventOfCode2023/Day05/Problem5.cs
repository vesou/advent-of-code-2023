namespace AdventOfCode2023.Day05;

public class Problem5
{
    #region Task 2

    public static long Solve2()
    {
        var inputLines = File.ReadAllLines("Day05/input1.txt").ToList();
        var allData = TranslateInput2(inputLines);
        var result = allData.FindLowestLocation();

        return result;
    }

    public static Data TranslateInput2(List<string> inputLines)
    {
        Data data = new Data();
        data.Seeds = inputLines[0].Substring("seeds: ".Length).Split(" ").Select(long.Parse).ToList();
        data.SeedRanges = new List<(long, long)>();
        for (int i = 0; i < data.Seeds.Count; i+=2)
        {
            data.SeedRanges.Add((data.Seeds[i], data.Seeds[i + 1]));
        }

        int indexOfSeedToSoil = inputLines.IndexOf("seed-to-soil map:");
        int indexOfSoilToFertilizer = inputLines.IndexOf("soil-to-fertilizer map:");
        int indexOfFertilizerToWater = inputLines.IndexOf("fertilizer-to-water map:");
        int indexOfWaterToLight = inputLines.IndexOf("water-to-light map:");
        int indexOfLightToTemperature = inputLines.IndexOf("light-to-temperature map:");
        int indexOfTemperatureToHumidity = inputLines.IndexOf("temperature-to-humidity map:");
        int indexOfHumidityToLocation = inputLines.IndexOf("humidity-to-location map:");
        data.SeedToSoil = GetRuleSet(inputLines, indexOfSeedToSoil, indexOfSoilToFertilizer);
        data.SoilToFertilizer = GetRuleSet(inputLines, indexOfSoilToFertilizer, indexOfFertilizerToWater);
        data.FertilizerToWater = GetRuleSet(inputLines, indexOfFertilizerToWater, indexOfWaterToLight);
        data.WaterToLight = GetRuleSet(inputLines, indexOfWaterToLight, indexOfLightToTemperature);
        data.LightToTemperature = GetRuleSet(inputLines, indexOfLightToTemperature, indexOfTemperatureToHumidity);
        data.TemperatureToHumidity = GetRuleSet(inputLines, indexOfTemperatureToHumidity, indexOfHumidityToLocation);
        data.HumidityToLocation = GetRuleSet(inputLines, indexOfHumidityToLocation, inputLines.Count);

        return data;
    }

    #endregion

    #region Task 1

    public static long Solve1()
    {
        var inputLines = File.ReadAllLines("Day05/input1.txt").ToList();
        var allData = TranslateInput(inputLines);
        allData.TranslateAllSeedsToLocations();

        return GetLowestLocation(allData);
    }

    private static long GetLowestLocation(Data data)
    {
        return data.SeedsToLocation.Min();
    }

    #endregion

    public static Data TranslateInput(List<string> inputLines)
    {
        Data data = new Data();
        data.Seeds = inputLines[0].Substring("seeds: ".Length).Split(" ").Select(long.Parse).ToList();
        data.SeedToSoil = new RuleSet();
        int indexOfSeedToSoil = inputLines.IndexOf("seed-to-soil map:");
        int indexOfSoilToFertilizer = inputLines.IndexOf("soil-to-fertilizer map:");
        int indexOfFertilizerToWater = inputLines.IndexOf("fertilizer-to-water map:");
        int indexOfWaterToLight = inputLines.IndexOf("water-to-light map:");
        int indexOfLightToTemperature = inputLines.IndexOf("light-to-temperature map:");
        int indexOfTemperatureToHumidity = inputLines.IndexOf("temperature-to-humidity map:");
        int indexOfHumidityToLocation = inputLines.IndexOf("humidity-to-location map:");
        data.SeedToSoil = GetRuleSet(inputLines, indexOfSeedToSoil, indexOfSoilToFertilizer);
        data.SoilToFertilizer = GetRuleSet(inputLines, indexOfSoilToFertilizer, indexOfFertilizerToWater);
        data.FertilizerToWater = GetRuleSet(inputLines, indexOfFertilizerToWater, indexOfWaterToLight);
        data.WaterToLight = GetRuleSet(inputLines, indexOfWaterToLight, indexOfLightToTemperature);
        data.LightToTemperature = GetRuleSet(inputLines, indexOfLightToTemperature, indexOfTemperatureToHumidity);
        data.TemperatureToHumidity = GetRuleSet(inputLines, indexOfTemperatureToHumidity, indexOfHumidityToLocation);
        data.HumidityToLocation = GetRuleSet(inputLines, indexOfHumidityToLocation, inputLines.Count);

        return data;
    }

    private static RuleSet GetRuleSet(List<string> inputLines, int startIndex, int endIndex)
    {
        RuleSet ruleSet = new RuleSet();
        for (int i = startIndex + 1; i < endIndex - 1; i++)
        {
            var rule = GetRule(inputLines[i]);
            ruleSet.Rules.Add(rule);
        }

        ruleSet.Rules = ruleSet.Rules.OrderBy(x => x.Source).ToList();

        return ruleSet;
    }

    private static Rule GetRule(string inputLine)
    {
        var parts = inputLine.Split(" ");
        return new Rule()
        {
            Source = long.Parse(parts[1]),
            Destination = long.Parse(parts[0]),
            Count = long.Parse(parts[2])
        };
    }
}

public class Data()
{
    public List<long> Seeds { get; set; }
    public List<(long, long)> SeedRanges { get; set; }
    public List<long> SeedsToLocation { get; set; }
    public RuleSet SeedToSoil { get; set; }
    public RuleSet SoilToFertilizer { get; set; }
    public RuleSet FertilizerToWater { get; set; }
    public RuleSet WaterToLight { get; set; }
    public RuleSet LightToTemperature { get; set; }
    public RuleSet TemperatureToHumidity { get; set; }
    public RuleSet HumidityToLocation { get; set; }

    public void TranslateAllSeedsToLocations()
    {
        SeedsToLocation = Seeds.Select(TranslateSeedToLocation).ToList();
    }

    public long FindLowestLocation()
    {
        long lowestLocation = long.MaxValue;

        return lowestLocation;
    }

    public long TranslateSeedToLocation(long seedNumber)
    {
        seedNumber = SeedToSoil.Transform(seedNumber);
        seedNumber = SoilToFertilizer.Transform(seedNumber);
        seedNumber = FertilizerToWater.Transform(seedNumber);
        seedNumber = WaterToLight.Transform(seedNumber);
        seedNumber = LightToTemperature.Transform(seedNumber);
        seedNumber = TemperatureToHumidity.Transform(seedNumber);
        seedNumber = HumidityToLocation.Transform(seedNumber);

        return seedNumber;
    }
}

public class RuleSet
{
    public List<Rule> Rules { get; set; } = new List<Rule>();

    public long Transform(long input)
    {
        if(input < Rules[0].Source)
            return input;

        if(input > Rules[^1].Source + Rules[^1].Count)
            return input;

        Rule impactingRule = FindRule(input);

        return impactingRule.Destination + (input - impactingRule.Source);
    }

    private Rule FindRule(long input)
    {
        foreach (var rule in Rules)
        {
            if(rule.Source <= input && rule.Source + rule.Count > input)
                return rule;
        }

        return new Rule()
        {
            Source = 0,
            Destination = 0,
            Count = 0
        };
    }
}
public class Rule
{
    public long Source { get; set; }
    public long Destination { get; set; }
    public long Count { get; set; }
}
