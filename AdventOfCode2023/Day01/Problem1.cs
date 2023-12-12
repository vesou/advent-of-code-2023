namespace AdventOfCode2023.Day01;

public class Problem1
{
    public static readonly Dictionary<string, int> Translator = new()
    {
        { "0", 0 }, { "1", 1 }, { "2", 2 }, { "3", 3 }, { "4", 4 },
        { "5", 5 }, { "6", 6 }, { "7", 7 }, { "8", 8 }, { "9", 9 }
    };

    public static readonly Dictionary<string, int> Translator2 = new()
    {
        { "0", 0 }, { "1", 1 }, { "2", 2 }, { "3", 3 }, { "4", 4 },
        { "5", 5 }, { "6", 6 }, { "7", 7 }, { "8", 8 }, { "9", 9 },
        { "one", 1 }, { "two", 2 }, { "three", 3 }, { "four", 4 }, { "five", 5 },
        { "six", 6 }, { "seven", 7 }, { "eight", 8 }, { "nine", 9 }
    };

    #region Task 2

    public static int Solve2()
    {
        var inputLines = File.ReadAllLines("Day01/input1.txt").ToList();

        var totalSum = SumLines2(inputLines, Translator2);
        return totalSum;
    }

    private static int SumLines2(List<string> inputLines, Dictionary<string, int> translator,
        bool printLineSums = false)
    {
        var totalSum = 0;
        foreach (var inputLine in inputLines) totalSum += SumLine2(inputLine, translator, printLineSums);

        return totalSum;
    }

    private static int SumLine2(string inputLine, Dictionary<string, int> translator, bool printLineSums)
    {
        var firstDigit = FindFirstDigit2(inputLine, translator);
        var lastDigit = FindLastDigit2(inputLine, translator);
        if (printLineSums)
            Console.WriteLine($"First digit: {firstDigit}, Last digit: {lastDigit}, Sum: {firstDigit + lastDigit}");

        return firstDigit * 10 + lastDigit;
    }

    public static int FindFirstDigit2(string inputLine, Dictionary<string, int> translator)
    {
        var lowestIndex = int.MaxValue;
        var firstValue = -1;
        foreach (var key in translator.Keys)
        {
            var index = inputLine.IndexOf(key);
            if (index != -1 && index < lowestIndex)
            {
                lowestIndex = index;
                firstValue = translator[key];
            }
        }

        return firstValue;
    }

    public static int FindLastDigit2(string inputLine, Dictionary<string, int> translator)
    {
        var highestIndex = int.MinValue;
        var lastValue = -1;
        foreach (var key in translator.Keys)
        {
            var index = FindLastIndex(inputLine, key);
            if (index != -1 && index > highestIndex)
            {
                highestIndex = index;
                lastValue = translator[key];
            }
        }

        return lastValue;
    }

    private static int FindLastIndex(string inputLine, string key, int startIndex = 0)
    {
        var index = inputLine.Substring(startIndex).IndexOf(key);
        if (index == -1) return -1;
        var nextIndex = FindLastIndex(inputLine, key, index + startIndex + 1);

        return nextIndex == -1 ? index + startIndex : nextIndex;
    }

    #endregion

    #region Task 1

    public static int Solve1()
    {
        var inputLines = File.ReadAllLines("Day01/input1.txt").ToList();

        var totalSum = SumLines1(inputLines, Translator);
        return totalSum;
    }

    private static int SumLines1(List<string> inputLines, Dictionary<string, int> translator,
        bool printLineSums = false)
    {
        var totalSum = 0;
        foreach (var inputLine in inputLines) totalSum += SumLine1(inputLine, translator, printLineSums);

        return totalSum;
    }

    private static int SumLine1(string inputLine, Dictionary<string, int> translator, bool printLineSums)
    {
        var firstDigit = FindFirstDigit1(inputLine, translator);
        var lastDigit = FindLastDigit1(inputLine, translator);
        if (printLineSums)
            Console.WriteLine($"First digit: {firstDigit}, Last digit: {lastDigit}, Sum: {firstDigit + lastDigit}");

        return firstDigit * 10 + lastDigit;
    }

    public static int FindLastDigit1(string inputLine, Dictionary<string, int> translator)
    {
        return FindFirstDigit1(ReverseString(inputLine), translator);
    }

    private static string ReverseString(string input)
    {
        var charArray = input.ToCharArray();
        Array.Reverse(charArray);

        return new string(charArray);
    }

    public static int FindFirstDigit1(string inputLine, Dictionary<string, int> translator)
    {
        var lowestIndex = int.MaxValue;
        foreach (var key in translator.Keys)
        {
            var index = inputLine.IndexOf(key);
            if (index != -1 && index < lowestIndex) lowestIndex = index;
        }

        return translator[inputLine[lowestIndex].ToString()];
    }

    #endregion
}
