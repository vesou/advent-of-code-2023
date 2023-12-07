namespace AdventOfCode2023.Day2;

public class Problem2
{
    #region Task 1

    public static int Solve1(int maxBlueAllowed, int maxRedAllowed, int maxGreenAllowed)
    {
        var inputLines = File.ReadAllLines("Day2/input1.txt").ToList();
        var games = inputLines.Select(MapIntoGame);

        int successfulGameIndexTotal = games.Select(x => CheckGame(x, maxBlueAllowed, maxRedAllowed, maxGreenAllowed) ? x.Number : 0).Sum();

        return successfulGameIndexTotal;
    }

    public static int Solve2()
    {
        var inputLines = File.ReadAllLines("Day2/input1.txt").ToList();
        var games = inputLines.Select(MapIntoGame);

        int sumOfMultipliedMaxValues = games.Select(GetMultipliedSumOfMaxValues).Sum();

        return sumOfMultipliedMaxValues;
    }

    public static int GetMultipliedSumOfMaxValues(Game game)
    {
        return game.MaxBlue * game.MaxRed * game.MaxGreen;
    }

    public static bool CheckGame(Game game, int maxBlueAllowed, int maxRedAllowed, int maxGreenAllowed)
    {
        return maxBlueAllowed >= game.MaxBlue && maxRedAllowed >= game.MaxRed && maxGreenAllowed >= game.MaxGreen;
    }

    public static Game MapIntoGame(string line)
    {
        var game = new Game();
        if(string.IsNullOrWhiteSpace(line)) return game;
        var indexOfColon = line.IndexOf(':');
        if(indexOfColon == -1)
            return game;
        var gameNumber = line.Substring(5, indexOfColon -5);
        game.Number = int.Parse(gameNumber);
        line = line.Substring(5 + gameNumber.Length + 1);
        var gameSessions = line.Split(';');
        foreach (var gameSession in gameSessions)
        {
            (int blue, int red, int green) = GetCountsForSession(gameSession);
            game.MaxBlue = blue > game.MaxBlue ? blue : game.MaxBlue;
            game.MaxRed = red > game.MaxRed ? red : game.MaxRed;
            game.MaxGreen = green > game.MaxGreen ? green : game.MaxGreen;
        }

        return game;
    }

    private static (int blue, int red, int green) GetCountsForSession(string gameSession)
    {
        int blue = 0, red = 0, green = 0;
        var colors = gameSession.Split(',');
        foreach (var singleColor in colors)
        {
            var color = singleColor.Trim();
            if (color.Contains("blue")) blue = int.Parse(color.Substring(0, color.IndexOf("blue")));
            else if (color.Contains("red")) red = int.Parse(color.Substring(0, color.IndexOf("red")));
            else if (color.Contains("green")) green = int.Parse(color.Substring(0, color.IndexOf("green")));
        }

        return (blue, red, green);
    }

    #endregion
}

/// <summary>
/// Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
/// </summary>
public class Game
{
    public int Number { get; set; } = 0;
    public int MaxBlue { get; set; } = 0;
    public int MaxRed { get; set; } = 0;
    public int MaxGreen { get; set; } = 0;
}
