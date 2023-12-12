namespace AdventOfCode2023.Day09;

public class Problem9
{
    #region Task 2

    public static int Solve2()
    {
        var inputLines = File.ReadAllLines("Day09/input1.txt").ToList();

        List<Oasis> reports = TranslateInput(inputLines);

        int result = SumOfExtrapolatedPreviousNumbers(reports);

        return result;
    }

    private static int SumOfExtrapolatedPreviousNumbers(List<Oasis> reports)
    {
        int sum = 0;
        foreach (var report in reports)
        {
            sum += PredictPreviousNumber(report.Numbers);
        }

        return sum;
    }

    public static int PredictPreviousNumber(int[] numbers)
    {
        numbers = numbers.Reverse().ToArray();
        int nextNumber = numbers[^1];
        int multiplier = 1;
        int[] numberDifference = GetNumberDiff(numbers, true);
        while (NotAllNumbersAreZero(numberDifference))
        {
            nextNumber -= numberDifference[^1] * multiplier;
            multiplier *= -1;

            numberDifference = GetNumberDiff(numberDifference, true);
        }

        return nextNumber;
    }

    #endregion

    #region Task 1

    public static int Solve1()
    {
        var inputLines = File.ReadAllLines("Day09/input1.txt").ToList();

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

    private static int[] GetNumberDiff(int[] numbers, bool reversed = false)
    {
        int[] numberDifference = new int[numbers.Length - 1];
        for (int i = 0; i < numbers.Length - 1; i++)
        {
            if (reversed)
            {
                numberDifference[i] = numbers[i] - numbers[i + 1];
            }
            else
            {
                numberDifference[i] = numbers[i + 1] - numbers[i];
            }
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
