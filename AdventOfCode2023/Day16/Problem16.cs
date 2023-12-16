using System.Text;

namespace AdventOfCode2023.Day16;

public class Problem16
{
    public const string Day  = "16";
    public const char EmptySpace  = '.';
    public const char MirrorLeft  = '/';
    public const char MirrorRight  = '\\';
    public const char SplitterVertical  = '|';
    public const char SplitterHorizontal  = '-';
    public const int Up  = 0;
    public const int Right  = 1;
    public const int Down  = 2;
    public const int Left  = 3;

    #region Task 2

    public static long Solve2()
    {
        var inputLines = File.ReadAllLines($"Day{Day}/input1.txt").ToList();

        Data data = TranslateInput(inputLines);
        return FindBestLightReflectionScore(data);
    }

    public static long FindBestLightReflectionScore(Data data)
    {
        long bestResult = 0;

        List<Point> startingPoints = new List<Point>();

        // top row
        for (int x = 0; x < data.Grid.GetLength(1); x++)
        {
            startingPoints.Add(new Point(x, 0));
        }
        long bestResultGoingOneDirection = FindBestResultGoingOneDirection(data, Down, startingPoints);
        if(bestResultGoingOneDirection > bestResult)
            bestResult = bestResultGoingOneDirection;

        // bottom row
        for (int x = 0; x < data.Grid.GetLength(1); x++)
        {
            startingPoints.Add(new Point(x, data.Grid.GetLength(0) - 1));
        }
        bestResultGoingOneDirection = FindBestResultGoingOneDirection(data, Up, startingPoints);
        if(bestResultGoingOneDirection > bestResult)
            bestResult = bestResultGoingOneDirection;

        // left row
        for (int y = 0; y < data.Grid.GetLength(0); y++)
        {
            startingPoints.Add(new Point(0, y));
        }
        bestResultGoingOneDirection = FindBestResultGoingOneDirection(data, Right, startingPoints);
        if(bestResultGoingOneDirection > bestResult)
            bestResult = bestResultGoingOneDirection;

        // right row
        for (int y = 0; y < data.Grid.GetLength(0); y++)
        {
            startingPoints.Add(new Point(data.Grid.GetLength(1) - 1, y));
        }
        bestResultGoingOneDirection = FindBestResultGoingOneDirection(data, Left, startingPoints);
        if(bestResultGoingOneDirection > bestResult)
            bestResult = bestResultGoingOneDirection;


        return bestResult;
    }

    private static long FindBestResultGoingOneDirection(Data data, int direction, List<Point> startingPoints)
    {
        long bestResult = 0;
        foreach (var startingPoint in startingPoints)
        {
            data.VisitedPixel = ClearVisitedPixels(data.VisitedPixel);
            ReflectLight(data, startingPoint.X, startingPoint.Y, direction);
            long result = CountPixelsWithLight(data);
            if(result > bestResult)
                bestResult = result;
        }

        return bestResult;
    }

    private static int[,][] ClearVisitedPixels(int[,][] visitedPixels)
    {
        for (int y = 0; y < visitedPixels.GetLength(0); y++)
        {
            for (int x = 0; x < visitedPixels.GetLength(1); x++)
            {
                for (int i = 0; i < visitedPixels[y, x].Length; i++)
                {
                    visitedPixels[y, x][i] = 0;
                }
            }
        }

        return visitedPixels;
    }

    #endregion

    #region Task 1

    public static long Solve1()
    {
        var inputLines = File.ReadAllLines($"Day{Day}/input1.txt").ToList();

        Data data = TranslateInput(inputLines);
        ReflectLight(data);

        return CountPixelsWithLight(data);
    }

    public static string ShowVisitedPixels(int[,][] visitedPixels)
    {
        StringBuilder result = new StringBuilder();

        for (int y = 0; y < visitedPixels.GetLength(0); y++)
        {
            for (int x = 0; x < visitedPixels.GetLength(1); x++)
            {
                result.Append(visitedPixels[y, x].Any(p => p == 1) ? "#" : ".");
            }

            result.AppendLine();
        }

        return result.ToString();
    }

    public static long CountPixelsWithLight(Data data)
    {
        long result = 0;

        for (int y = 0; y < data.VisitedPixel.GetLength(0); y++)
        {
            for (int x = 0; x < data.VisitedPixel.GetLength(1); x++)
            {
                if (data.VisitedPixel[y, x].Any(p => p > 0))
                {
                    result++;
                }
            }
        }

        return result;
    }

    public static void ReflectLight(Data data, int x = 0, int y = 0, int direction = Right) // 0 - up, 1 - right, 2 - down, 3 - left
    {
        while (true)
        {
            if (x < 0 || x >= data.Grid.GetLength(1) || y < 0 || y >= data.Grid.GetLength(0))
            {
                break;
            }

            if (data.VisitedPixel[y, x][direction] > 0)
            {
                break;
            }

            data.VisitedPixel[y, x][direction]++;

            if (data.Grid[y, x] == EmptySpace)
            {
                (x, y) = Move(x, y, direction);
            }
            else if (data.Grid[y, x] == MirrorLeft)
            {
                direction = ChangeDirection(direction, MirrorLeft);
                (x, y) = Move(x, y, direction);
            }
            else if (data.Grid[y, x] == MirrorRight)
            {
                direction = ChangeDirection(direction, MirrorRight);
                (x, y) = Move(x, y, direction);
            }
            else if (data.Grid[y, x] == SplitterVertical)
            {
                bool isSplittingInTwo = IsSplittingInTwo(direction, SplitterVertical);
                if (!isSplittingInTwo)
                {
                    (x, y) = Move(x, y, direction);
                    continue;
                }

                ReflectLight(data, x, y-1, Up);
                ReflectLight(data, x, y+1, Down);
                break;
            }
            else if (data.Grid[y, x] == SplitterHorizontal)
            {
                bool isSplittingInTwo = IsSplittingInTwo(direction, SplitterHorizontal);
                if (!isSplittingInTwo)
                {
                    (x, y) = Move(x, y, direction);
                    continue;
                }

                ReflectLight(data, x-1, y, Left);
                ReflectLight(data, x+1, y, Right);
                break;
            }
        }
    }

    private static bool IsSplittingInTwo(int direction, char splitter)
    {
        if(splitter == SplitterVertical)
            return direction is Left or Right;
        return direction is Up or Down;
    }

    public static int ChangeDirection(int direction, char mirrorChar)
    {
        // / - left, \ - right
        if (mirrorChar == MirrorLeft)
        {
            switch (direction)
            {
                case Up:
                    return Right;
                case Right:
                    return Up;
                case Down:
                    return Left;
                case Left:
                    return Down;
            }
        }

        switch (direction)
        {
            case Up:
                return Left;
            case Right:
                return Down;
            case Down:
                return Right;
            case Left:
                return Up;
        }

        return direction;
    }

    private static (int x, int y) Move(int x, int y, int direction)
    {
        switch (direction)
        {
            case Up:
                return (x, y-1);
            case Right:
                return (x+1, y);
            case Down:
                return (x, y+1);
            case Left:
                return (x-1, y);

        }

        return (x, y);
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
        Grid = new char[inputLines.Count, inputLines[0].Length];
        VisitedPixel = new int[inputLines.Count, inputLines[0].Length][];

        for (int i = 0; i < inputLines.Count; i++)
        {
            for (int j = 0; j < inputLines[i].Length; j++)
            {
                Grid[i, j] = inputLines[i][j];
                VisitedPixel[i, j] = new int[4];
            }
        }
    }

    public char[,] Grid { get; set; }
    public int[,][] VisitedPixel { get; set; }
}

public class Point
{
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; set; }
    public int Y { get; set; }
}
