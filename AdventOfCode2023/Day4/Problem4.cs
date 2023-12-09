namespace AdventOfCode2023.Day4;

public class Problem4
{
    #region Task 1

    public static int Solve1()
    {
        var inputLines = File.ReadAllLines("Day4/input1.txt").ToList();
        // remove Card 1: from each line
        inputLines = inputLines.Select(x => x.Substring(x.IndexOf(":")+2)).ToList();

        return SumAllGames(inputLines);
    }

    private static int SumAllGames(List<string> inputLines)
    {
        int result = 0;
        foreach (var inputLine in inputLines)
        {
            result += CalculateGamePoints(inputLine);
        }

        return result;
    }

    public static int CalculateGamePoints(string inputLine)
    {
        List<int> winningNumbers = GetWinningNumbers(inputLine);
        List<int> myNumbers = GetMyNumbers(inputLine);

        int result = -1;
        foreach (var myNumber in myNumbers)
        {
            if (winningNumbers.Contains(myNumber))
            {
                result++;
            }
        }

        if (result == -1)
        {
            return 0;
        }

        return (int)Math.Pow(2, result);
    }

    private static List<int> GetWinningNumbers(string inputLine)
    {
        string[] parts = inputLine.Split('|');

        return GetNumbers(parts[0]);
    }

    private static List<int> GetMyNumbers(string inputLine)
    {
        string[] parts = inputLine.Split('|');

        return GetNumbers(parts[1]);
    }

    private static List<int> GetNumbers(string inputLine)
    {
        List<int> result = new List<int>();
        string[] numbers = inputLine.Split(' ');
        foreach (var winningNumber in numbers.Where(x => !string.IsNullOrWhiteSpace(x)))
        {
            result.Add(int.Parse(winningNumber));
        }

        return result;
    }

    #endregion
}
