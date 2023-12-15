namespace AdventOfCode2023.Day15;

public class Problem15
{
    public const string Day  = "15";

    #region Task 2

    public static long Solve2()
    {
        var inputLines = File.ReadAllLines($"Day{Day}/input1.txt").ToList();

        return 0;
    }

    #endregion

    #region Task 1

    public static long Solve1()
    {
        var inputLines = File.ReadAllLines($"Day{Day}/input1.txt").ToList();

        Data data = TranslateInput(string.Join("\n", inputLines));

        return GetSumOfAllHashes(data);
    }

    public static long GetSumOfAllHashes(Data data)
    {
        long result = 0;
        foreach (string initializationSequence in data.InitializationSequences)
        {
            result += GetHash(initializationSequence);
        }

        return result;
    }

    public static int GetHash(string inputString)
    {
        int result = 0;
        foreach (char c in inputString)
        {
            result += c;
            result *= 17;
            result %= 256;
        }

        return result;
    }

    #endregion

    public static Data TranslateInput(string inputLine)
    {
        return new Data(inputLine);
    }
}

public class Data
{
    public Data(string inputLine)
    {
        InitializationSequences = inputLine.Split(",").ToList();
        InitializationSequences.RemoveAll(s => string.IsNullOrWhiteSpace(s));
    }

    public List<string> InitializationSequences { get; set; }
}
