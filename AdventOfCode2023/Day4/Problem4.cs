namespace AdventOfCode2023.Day4;

public class Problem4
{
    #region Task 2

    public static int Solve2()
    {
        var inputLines = File.ReadAllLines("Day4/input1.txt").ToList();
        // remove Card 1: from each line
        inputLines = inputLines.Select(x => x.Substring(x.IndexOf(":")+2)).ToList();
        List<ScratchCardGame> scratchCardGames = TranslateInput(inputLines);

        return CountOfAllScratchCardGames(scratchCardGames);
    }

    public static int CountOfAllScratchCardGames(List<ScratchCardGame> availableScratchCards)
    {
        int result = 0;
        for (int i = 0; i < availableScratchCards.Count; i++)
        {
            PlayNextGame(availableScratchCards.ToArray(), i);
        }

        result = availableScratchCards.Sum(x => x.NumberOfCopies);

        return result;
    }

    private static void PlayNextGame(ScratchCardGame[] scratchCardGames, int currentGameIndex)
    {
        List<int> winningNumbers = scratchCardGames[currentGameIndex].WinningNumbers;
        List<int> myNumbers = scratchCardGames[currentGameIndex].MyNumbers;
        int numberOfCopies = scratchCardGames[currentGameIndex].NumberOfCopies;

        int result = myNumbers.Count(x => winningNumbers.Contains(x));

        if (result == 0)
        {
            return;
        }

        AddCopiesOfGame(scratchCardGames, currentGameIndex, result, numberOfCopies);
    }

    private static void AddCopiesOfGame(ScratchCardGame[] scratchCardGames, int currentGameIndex, int scratchCardsWon, int numberOfCopies)
    {
        for (int i = currentGameIndex+1; i <= currentGameIndex+scratchCardsWon; i++)
        {
            scratchCardGames[i].NumberOfCopies += numberOfCopies;
        }
    }

    #endregion

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

    public static List<ScratchCardGame> TranslateInput(List<string> inputLines)
    {
        List<ScratchCardGame> result = new List<ScratchCardGame>();
        foreach (var inputLine in inputLines)
        {
            List<int> winningNumbers = GetWinningNumbers(inputLine);
            List<int> myNumbers = GetMyNumbers(inputLine);
            result.Add(new ScratchCardGame(winningNumbers, myNumbers));
        }

        return result;
    }
}

public class ScratchCardGame(List<int> winningNumbers, List<int> myNumbers, int numberOfCopies = 1)
{
    public List<int> WinningNumbers { get; set; } = winningNumbers;
    public List<int> MyNumbers { get; set; } = myNumbers;
    public int NumberOfCopies { get; set; } = numberOfCopies;
}
