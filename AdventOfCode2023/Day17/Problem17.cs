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

        return 0;
    }

    #endregion

    #region Task 1

    public static long Solve1()
    {
        var inputLines = File.ReadAllLines($"Day{Day}/input1.txt").ToList();

        Data data = TranslateInput(inputLines);

        // first direction doesn't count as we appear on first step
        CalculateBestRoutes(data, 3, 0, 0, Right, 0);

        return data.VisitedPixel[0,0].RoutesList
            .Min(r => r.BestDistanceFromEnd);
    }

    public static void CalculateBestRoutes(Data data, int maxNumberOfConsecutiveStepsSameDirection, int currentX, int currentY, int incomingDirection, int directionCount)
    {
        if (currentX < 0 || currentX >= data.Grid.GetLength(1) || currentY < 0 || currentY >= data.Grid.GetLength(0))
            return;

        if (currentX == data.Grid.GetLength(1) - 1 && currentY == data.Grid.GetLength(0) - 1)
        {
            // found end
            data.VisitedPixel[currentY, currentX].RoutesList.Add(new Route(incomingDirection, directionCount, data.Grid[currentY, currentX]));
            return;
        }

        int backwardsDirection = (incomingDirection + 2) % 4;
        int bestDistance = int.MaxValue;
        int distance = GetDistanceAndCalculateBestRoutes(data, maxNumberOfConsecutiveStepsSameDirection, currentX + 1, currentY, Right,
            incomingDirection == Right ? directionCount + 1 : 1, backwardsDirection);
        bestDistance = Math.Min(bestDistance, distance);
        distance = GetDistanceAndCalculateBestRoutes(data, maxNumberOfConsecutiveStepsSameDirection, currentX - 1, currentY, Left, incomingDirection == Left ? directionCount + 1 : 1, backwardsDirection);
        bestDistance = Math.Min(bestDistance, distance);
        distance = GetDistanceAndCalculateBestRoutes(data, maxNumberOfConsecutiveStepsSameDirection, currentX, currentY + 1, Down, incomingDirection == Down ? directionCount + 1 : 1, backwardsDirection);
        bestDistance = Math.Min(bestDistance, distance);
        distance = GetDistanceAndCalculateBestRoutes(data, maxNumberOfConsecutiveStepsSameDirection, currentX, currentY - 1, Up, incomingDirection == Up ? directionCount + 1 : 1, backwardsDirection);
        bestDistance = Math.Min(bestDistance, distance);


        data.VisitedPixel[currentY, currentX].RoutesList.Add(new Route(incomingDirection, directionCount, bestDistance+data.Grid[currentY, currentX]));
    }

    private static int GetDistanceAndCalculateBestRoutes(Data data, int maxNumberOfConsecutiveStepsSameDirection, int currentX, int currentY, int incomingDirection, int directionCount, int impossibleDirection)
    {
        if (currentX < 0 || currentX >= data.Grid.GetLength(1) || currentY < 0 || currentY >= data.Grid.GetLength(0))
            return int.MaxValue;

        if(incomingDirection == impossibleDirection)
            return int.MaxValue;

        if(directionCount > maxNumberOfConsecutiveStepsSameDirection)
            return int.MaxValue;

        var route = data.VisitedPixel[currentY, currentX].RoutesList.Find(r =>
            r.IncomingDirection == incomingDirection && r.IncomingDirectionCount == directionCount);

        if (route != null)
            return route.BestDistanceFromEnd;

        CalculateBestRoutes(data, maxNumberOfConsecutiveStepsSameDirection, currentX, currentY, incomingDirection, directionCount);

        return data.VisitedPixel[currentY, currentX].RoutesList.Find(r =>
                r.IncomingDirection == incomingDirection && r.IncomingDirectionCount == directionCount)?.BestDistanceFromEnd ?? int.MaxValue;
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
        VisitedPixel = new Routes[inputLines.Count, inputLines[0].Length];

        for (int y = 0; y < inputLines.Count; y++)
        {
            for (int x = 0; x < inputLines[y].Length; x++)
            {
                Grid[y, x] = inputLines[y][x] - 48; // char to int
                VisitedPixel[y, x] = new Routes(x, y);
            }
        }
    }

    public int[,] Grid { get; set; }
    public Routes[,] VisitedPixel { get; set; }
}

public class Routes
{
    public Routes(int x, int y)
    {
        RoutesList = new List<Route>();
        X = x;
        Y = y;
    }

    public List<Route> RoutesList { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}

public class Route
{
    public Route(int incomingDirection, int incomingDirectionCount, int bestDistanceFromEnd)
    {
        IncomingDirection = incomingDirection;
        IncomingDirectionCount = incomingDirectionCount;
        BestDistanceFromEnd = bestDistanceFromEnd;
    }

    public int IncomingDirection { get; set; }
    public int IncomingDirectionCount { get; set; }
    public int BestDistanceFromEnd { get; set; }
}
