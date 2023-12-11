namespace AdventOfCode2023.Day11;

public class Problem11
{
    public static char SpaceChar = '.';
    public static char GalaxyChar = '#';

    #region Task 2

    public static long Solve2(int expansionSize)
    {
        var inputLines = File.ReadAllLines("Day11/input1.txt").ToList();
        var universe = TranslateInput(inputLines);
        (var rowsToExpand, var columnsToExpand) = FindExpandingRowsAndColumns(universe.Matrix);
        universe.RowsToExpand = rowsToExpand;
        universe.ColumnsToExpand = columnsToExpand;

        var result = SumAllPaths2(universe, expansionSize);

        return result;
    }

    public static (bool[] rowsToExpand, bool[] columnsToExpand) FindExpandingRowsAndColumns(char[][] matrix)
    {
        bool[] rowsToExpand = new bool[matrix.Length];

        for (int y = 0; y < matrix.Length; y++)
        {
            if (matrix[y].All(x => x == Problem11.SpaceChar))
            {
                rowsToExpand[y] = true;
            }
        }

        bool[] columnsToExpand = new bool[matrix[0].Length];
        for (int x = 0; x < matrix[0].Length; x++)
        {
            bool shouldExpand = true;
            for (int y = 0; y < matrix.Length; y++)
            {
                if (matrix[y][x] != Problem11.SpaceChar) shouldExpand = false;
            }

            columnsToExpand[x] = shouldExpand;
        }

        return (rowsToExpand, columnsToExpand);
    }

    public static long SumAllPaths2(Universe universe, int expansionSize)
    {
        List<PathToCalculate> allPaths = FindAllPaths2(universe, expansionSize);

        return allPaths.Sum(x => (long)x.MinimalPath);
    }

    public static List<PathToCalculate> FindAllPaths2(Universe universe, int expansionSize)
    {
        List<PathToCalculate> paths = new List<PathToCalculate>();
        Galaxy[] galaxies = FindAllGalaxies(universe.Matrix);
        bool[][] visited = new bool[galaxies.Length][];

        for (int i = 0; i < galaxies.Length; i++)
            visited[i] = new bool[galaxies.Length];

        for (int i = 0; i < galaxies.Length; i++)
        {
            for (int j = 0; j < galaxies.Length; j++)
            {
                if (j == i) continue;
                if(visited[i][j]) continue;
                if(visited[j][i]) continue;

                paths.Add(new PathToCalculate
                {
                    PointA = galaxies[i],
                    PointB = galaxies[j],
                    MinimalPath = CalculateShortestPath2(galaxies[i], galaxies[j], universe.RowsToExpand, universe.ColumnsToExpand, expansionSize)
                });

                visited[i][j] = true;
                visited[j][i] = true;
            }
        }

        return paths;
    }


    public static int CalculateShortestPath2(Galaxy pointA, Galaxy pointB, bool[] universeRowsToExpand, bool[] universeColumnsToExpand, int expansionSize)
    {
        int horizontalDistance = CalculateHorizontalDistance(pointA.X, pointB.X, universeColumnsToExpand, expansionSize);
        int verticalDistance = CalculateVerticalDistance(pointA.Y, pointB.Y, universeRowsToExpand, expansionSize);

        return horizontalDistance + verticalDistance;
    }

    private static int CalculateVerticalDistance(int pointAY, int pointBY, bool[] universeRowsToExpand, int expansionSize)
    {
        int smallerY = pointAY <= pointBY ? pointAY : pointBY;
        int biggerY = pointAY > pointBY ? pointAY : pointBY;
        int wholeDistance = 0;
        for (int x = smallerY; x < biggerY; x++)
        {
            int distance = 1;
            if (universeRowsToExpand[x])
            {
                distance *= expansionSize;
            }

            wholeDistance += distance;
        }

        return wholeDistance;
    }

    public static int CalculateHorizontalDistance(int pointAX, int pointBX, bool[] universeColumnsToExpand, int expansionSize)
    {
        int smallerX = pointAX <= pointBX ? pointAX : pointBX;
        int biggerX = pointAX > pointBX ? pointAX : pointBX;
        int wholeDistance = 0;
        for (int x = smallerX; x < biggerX; x++)
        {
            int distance = 1;
            if (universeColumnsToExpand[x])
            {
                distance *= expansionSize;
            }

            wholeDistance += distance;
        }

        return wholeDistance;
    }

    #endregion

    #region Task 1

    public static int Solve1()
    {
        var inputLines = File.ReadAllLines("Day11/input1.txt").ToList();

        var universe = TranslateInput(inputLines);

        var expandedMatrix = universe.Expand();

        return SumAllPaths(expandedMatrix);
    }

    public static int SumAllPaths(char[][] expandedMatrix)
    {
        List<PathToCalculate> allPaths = FindAllPaths(expandedMatrix);

        return allPaths.Sum(x => x.MinimalPath);
    }

    public static List<PathToCalculate> FindAllPaths(char[][] expandedMatrix)
    {
        List<PathToCalculate> paths = new List<PathToCalculate>();
        Galaxy[] galaxies = FindAllGalaxies(expandedMatrix);
        bool[][] visited = new bool[galaxies.Length][];

        for (int i = 0; i < galaxies.Length; i++)
            visited[i] = new bool[galaxies.Length];

        for (int i = 0; i < galaxies.Length; i++)
        {
            for (int j = 0; j < galaxies.Length; j++)
            {
                if (j == i) continue;
                if(visited[i][j]) continue;
                if(visited[j][i]) continue;

                paths.Add(new PathToCalculate
                {
                    PointA = galaxies[i],
                    PointB = galaxies[j],
                    MinimalPath = CalculateShortestPath(galaxies[i], galaxies[j])
                });

                visited[i][j] = true;
                visited[j][i] = true;
            }
        }

        return paths;
    }

    private static Galaxy[] FindAllGalaxies(char[][] expandedMatrix)
    {
        List<Galaxy> galaxies = new List<Galaxy>();
        for (int y = 0; y < expandedMatrix.Length; y++)
        {
            for (int x = 0; x < expandedMatrix[y].Length; x++)
            {
                if (expandedMatrix[y][x] == GalaxyChar)
                {
                    galaxies.Add(new Galaxy(x, y));
                }
            }
        }

        return galaxies.ToArray();
    }

    public static int CalculateShortestPath(Galaxy pointA, Galaxy pointB)
    {
        return Math.Abs(pointA.X-pointB.X) + Math.Abs(pointA.Y-pointB.Y);
    }

    #endregion

    public static Universe TranslateInput(List<string> inputLines)
    {
        return new Universe(inputLines);
    }
}

public class PathToCalculate
{
    public Galaxy PointA { get; set; }
    public Galaxy PointB { get; set; }
    public int MinimalPath { get; set; }
}

public class Galaxy
{
    public Galaxy(int x, int y)
    {
        X = x;
        Y = y;
    }
    public int X { get; set; }
    public int Y { get; set; }
}

public class Universe
{
    public Universe(List<string> inputLines)
    {
        Matrix = inputLines.Select(x => x.ToCharArray()).ToArray();
    }

    public char[][] Expand()
    {
        bool[] rowsToExpand = new bool[Matrix.Length];

        for (int y = 0; y < Matrix.Length; y++)
        {
            if (Matrix[y].All(x => x == Problem11.SpaceChar))
            {
                rowsToExpand[y] = true;
            }
        }

        char[][] withExpandedRows = ExpandEmptyRows(rowsToExpand, Matrix);

        bool[] columnsToExpand = new bool[Matrix[0].Length];
        for (int x = 0; x < Matrix[0].Length; x++)
        {
            bool shouldExpand = true;
            for (int y = 0; y < Matrix.Length; y++)
            {
                if (Matrix[y][x] != Problem11.SpaceChar) shouldExpand = false;
            }

            columnsToExpand[x] = shouldExpand;
        }

        char[][] fullyExpanded = ExpandEmptyColumns(columnsToExpand, withExpandedRows);
        return fullyExpanded;
    }

    private char[][] ExpandEmptyColumns(bool[] columnsToExpand, char[][] matrix)
    {
        int countOfExtraColumns = columnsToExpand.Count(x => x);
        char[][] expandedMatrix = new char[matrix.Length][];
        for (int y = 0; y < matrix.Length; y++)
        {
            expandedMatrix[y] = new char[matrix[y].Length + countOfExtraColumns];
            for (int x = 0, newX = 0; x < matrix[y].Length; x++, newX++)
            {
                expandedMatrix[y][newX] = matrix[y][x];
                if(columnsToExpand[x])
                    expandedMatrix[y][++newX] = matrix[y][x];
            }
        }

        return expandedMatrix;
    }

    private char[][] ExpandEmptyRows(bool[] rowsToExpand, char[][] matrix)
    {
        int countOfExtraRows = rowsToExpand.Count(x => x);
        char[][] expandedMatrix = new char[matrix.Length+countOfExtraRows][];
        for (int y = 0, newY = 0; y < matrix.Length; y++, newY++)
        {
            expandedMatrix[newY] = matrix[y];
            if(rowsToExpand[y])
                expandedMatrix[++newY] = matrix[y];
        }

        return expandedMatrix;
    }

    public char[][] Matrix { get; set; }

    public bool[] RowsToExpand { get; set; }
    public bool[] ColumnsToExpand { get; set; }
}
