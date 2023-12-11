namespace AdventOfCode2023.Day11;

public class Problem11
{
    public static char SpaceChar = '.';
    public static char GalaxyChar = '#';

    #region Task 2

    public static int Solve2()
    {
        var inputLines = File.ReadAllLines("Day11/input1.txt").ToList();
        var maze = TranslateInput(inputLines);

        return 0;
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
}
