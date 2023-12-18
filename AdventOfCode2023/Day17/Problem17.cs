using AdventOfCode2023.Day17.part2;

namespace AdventOfCode2023.Day17;

public class Problem17
{
    public const string Day  = "17";
    public const int Up  = 0;
    public const int Right  = 1;
    public const int Down  = 2;
    public const int Left  = 3;

    #region Task 2

    public static long Solve2()
    {
        var inputLines = File.ReadAllLines($"Day{Day}/input1.txt").ToList();

        Data data = TranslateInput(inputLines);
        var result = DijkstraAlgorithm2.Dijkstra(data.Grid, 0, 0, data.Grid.GetLength(1)-1, data.Grid.GetLength(0)-1);
        return result;
    }

    #endregion

    #region Task 1

    public static long Solve1()
    {
        var inputLines = File.ReadAllLines($"Day{Day}/input1.txt").ToList();

        Data data = TranslateInput(inputLines);
        var result = DijkstraAlgorithm.Dijkstra(data.Grid, 0, 0, data.Grid.GetLength(1)-1, data.Grid.GetLength(0)-1);
        return result;
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
        Grid = new int[inputLines.Count, inputLines[0].Length];

        for (int y = 0; y < inputLines.Count; y++)
        {
            for (int x = 0; x < inputLines[y].Length; x++)
            {
                Grid[y, x] = inputLines[y][x] - 48; // char to int
            }
        }
    }

    public int[,] Grid { get; set; }
}
