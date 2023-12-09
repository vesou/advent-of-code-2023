namespace AdventOfCode2023.Day9;

public class Problem9
{
    #region Task 1

    public static int Solve1()
    {
        var inputLines = File.ReadAllLines("Day9/input1.txt").ToList();

        List<Oasis> reports = TranslateInput(inputLines);

        int result = SumOfExtrapolatedNumbers(reports);

        return result;
    }

    private static int SumOfExtrapolatedNumbers(List<Oasis> reports)
    {
        int sum = 0;
        foreach (var report in reports)
        {
            sum += PredictNextNumber(report.Numbers);
        }

        return sum;
    }

    public static int PredictNextNumber(int[] numbers)
    {
        int nextNumber = numbers[^1];
        int[] numberDifference = GetNumberDiff(numbers);
        while (NotAllNumbersAreZero(numberDifference))
        {
            nextNumber += numberDifference[^1];
            numberDifference = GetNumberDiff(numberDifference);
        }

        return nextNumber;
    }

    private static bool NotAllNumbersAreZero(int[] numberDifference)
    {
        return numberDifference.Any(number => number != 0);
    }

    private static int[] GetNumberDiff(int[] numbers)
    {
        int[] numberDifference = new int[numbers.Length - 1];
        for (int i = 0; i < numbers.Length - 1; i++)
        {
            numberDifference[i] = numbers[i + 1] - numbers[i];
        }

        return numberDifference;
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