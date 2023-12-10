namespace AdventOfCode2023.Day5;

public class Problem5
{
    #region Task 2

    public static int Solve2()
    {
        var inputLines = File.ReadAllLines("Day5/input1.txt").ToList();

        return 0;
    }



    #endregion

    #region Task 1

    public static int Solve1()
    {
        var inputLines = File.ReadAllLines("Day5/input1.txt").ToList();

        return 0;
    }


    #endregion

    public static List<Data> TranslateInput(List<string> inputLines)
    {

        return new List<Data>();
    }
}

public class Data()
{
    public List<int> WinningNumbers { get; set; }
    public List<int> MyNumbers { get; set; }
    public int NumberOfCopies { get; set; }
}
