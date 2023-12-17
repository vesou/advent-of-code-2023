namespace AdventOfCode2023.Day17;

public class Problem17
{
    public const string Day  = "17";

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

        Data data = TranslateInput(inputLines);

        return 0;
    }

    #endregion

    public static Data TranslateInput(List<string> inputLines)
    {
        return new Data(inputLines);
    }
}

public class Data
{
    public Data(List<string> inputLines)
    {

    }
}
