namespace AdventOfCode2023.Day10;

public class Problem10
{
    #region Task 2

    public static int Solve2()
    {
        var inputLines = File.ReadAllLines("Day10/input1.txt").ToList();

        return 0;
    }

    #endregion

    #region Task 1

    public static int Solve1()
    {
        var inputLines = File.ReadAllLines("Day9/input1.txt").ToList();

        List<Oasis> reports = TranslateInput(inputLines);

        return 0;
    }



    #endregion

    public static List<Oasis> TranslateInput(List<string> inputLines)
    {
        return inputLines.Select(line => new Oasis(line.Split(" ").Select(int.Parse))).ToList();
    }

    public static Oasis TranslateInput(string inputLines)
    {
        return new Oasis(inputLines.Split(" ").Select(int.Parse));
    }
}

public class Oasis
{
    public Oasis(IEnumerable<int> numbers)
    {
        Numbers = numbers.ToArray();
    }

    public int[] Numbers { get; set; }
}
