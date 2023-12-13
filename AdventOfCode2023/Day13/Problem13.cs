namespace AdventOfCode2023.Day13;

public class Problem13
{
    public static char Ash = '.';
    public static char Rock = '#';
    #region Task 2

    public static long Solve2()
    {
        var inputLines = File.ReadAllLines("Day13/input1.txt").ToList();

        List<RockFormation> rockFormations = TranslateInput(inputLines);
        FindMirrorIndexes2(rockFormations);

        return SumMirrorIndexes(rockFormations);
    }

    public static void FindMirrorIndexes2(List<RockFormation> rockFormations)
    {
        foreach (var rockFormation in rockFormations)
        {
            var horizontalMirrorIndex = FindHorizontalMirrorIndex2(rockFormation);
            rockFormation.HorizontalMirrorIndex = horizontalMirrorIndex;
            var verticalMirrorIndex = FindVerticalMirrorIndex2(rockFormation);
            rockFormation.VerticalMirrorIndex = verticalMirrorIndex;
        }
    }

    private static int? FindVerticalMirrorIndex2(RockFormation rockFormation)
    {
        int horizontalSize = rockFormation.Rocks[0].Length;
        for (int x = 1; x < horizontalSize; x++)
        {
            // find out if the columns before are a mirror representation of the columns after x
            // check is mirror by going out from X -1 and +1 and comparing the chars
            int searchIndex = 1;
            bool isMirror = true;
            int totalNumberOfDifferences = 0;
            while(x - searchIndex >= 0 && x + searchIndex - 1 < horizontalSize)
            {
                char[] line1 = GetVerticalLine(rockFormation.Rocks, x - searchIndex);
                char[] line2 = GetVerticalLine(rockFormation.Rocks, x + searchIndex - 1 );
                (bool doesItMatch, int numberOfDifferentFormations) = IsPotentialMirror(line1, line2);
                totalNumberOfDifferences += numberOfDifferentFormations;
                if (totalNumberOfDifferences > 1 || !doesItMatch)
                {
                    isMirror = false;
                    break;
                }
                searchIndex++;
            }

            if (totalNumberOfDifferences != 1 && isMirror)
                isMirror = false;

            if(isMirror)
            {
                return x;
            }
        }

        return null;
    }

    private static int? FindHorizontalMirrorIndex2(RockFormation rockFormation)
    {
        int verticalSize = rockFormation.Rocks.Length;
        for (int y = 1; y < verticalSize; y++)
        {
            // find out if the rows above are a mirror representation of the rows below y
            // check is mirror by going out from Y -1 and +1 and comparing the chars
            int searchIndex = 1;
            bool isMirror = true;
            int totalNumberOfDifferences = 0;
            while(y - searchIndex >= 0 && y + searchIndex - 1 < verticalSize)
            {
                (bool doesItMatch, int numberOfDifferentFormations) = IsPotentialMirror(rockFormation.Rocks[y - searchIndex], rockFormation.Rocks[y + searchIndex - 1 ]);
                totalNumberOfDifferences += numberOfDifferentFormations;
                if (totalNumberOfDifferences > 1 || !doesItMatch)
                {
                    isMirror = false;
                    break;
                }
                searchIndex++;
            }

            if (totalNumberOfDifferences != 1 && isMirror)
                isMirror = false;

            if(isMirror)
            {
                return y;
            }
        }

        return null;
    }

    private static (bool doesItMatch, int numberOfDifferentFormations) IsPotentialMirror(char[] line1, char[] line2)
    {
        bool rowDiffFound = false;
        if (line1.SequenceEqual(line2))
            return (true, 0);
        for (int i = 0; i < line1.Length; i++)
        {
            if (line1[i] == line2[i]) continue;
            if(rowDiffFound)
                return (false, 0);
            rowDiffFound = true;
        }

        return (true, 1);
    }

    #endregion

    #region Task 1

    public static long Solve1()
    {
        var inputLines = File.ReadAllLines("Day13/input1.txt").ToList();

        List<RockFormation> rockFormations = TranslateInput(inputLines);
        FindMirrorIndexes(rockFormations);

        var test = rockFormations.Where(x => x.HorizontalMirrorIndex is null && x.VerticalMirrorIndex is null).Select(x => x.Rocks.Select(y => new string(y)).ToList()).ToList();

        return SumMirrorIndexes(rockFormations);
    }

    public static long SumMirrorIndexes(List<RockFormation> rockFormations)
    {
        long result = 0;
        foreach (var rockFormation in rockFormations)
        {
            if (rockFormation.HorizontalMirrorIndex.HasValue)
            {
                result += 100 * rockFormation.HorizontalMirrorIndex.Value;
            }
            if (rockFormation.VerticalMirrorIndex.HasValue)
            {
                result += rockFormation.VerticalMirrorIndex.Value;
            }
        }

        return result;
    }

    public static void FindMirrorIndexes(List<RockFormation> rockFormations)
    {
        foreach (var rockFormation in rockFormations)
        {
            var horizontalMirrorIndex = FindHorizontalMirrorIndex(rockFormation);
            rockFormation.HorizontalMirrorIndex = horizontalMirrorIndex;
            var verticalMirrorIndex = FindVerticalMirrorIndex(rockFormation);
            rockFormation.VerticalMirrorIndex = verticalMirrorIndex;
        }
    }

    private static int? FindVerticalMirrorIndex(RockFormation rockFormation)
    {
        int horizontalSize = rockFormation.Rocks[0].Length;
        for (int x = 1; x < horizontalSize; x++)
        {
            // find out if the columns before are a mirror representation of the columns after x
            // check is mirror by going out from X -1 and +1 and comparing the chars
            int searchIndex = 1;
            bool isMirror = true;
            while(x - searchIndex >= 0 && x + searchIndex - 1 < horizontalSize)
            {
                char[] line1 = GetVerticalLine(rockFormation.Rocks, x - searchIndex);
                char[] line2 = GetVerticalLine(rockFormation.Rocks, x + searchIndex - 1 );
                if (!IsMirror(line1, line2))
                {
                    isMirror = false;
                    break;
                }
                searchIndex++;
            }

            if(isMirror)
            {
                return x;
            }
        }

        return null;
    }

    private static char[] GetVerticalLine(char[][] rockFormation, int searchIndex)
    {
        return rockFormation.Select(x => x[searchIndex]).ToArray();
    }

    private static int? FindHorizontalMirrorIndex(RockFormation rockFormation)
    {
        int verticalSize = rockFormation.Rocks.Length;
        for (int y = 1; y < verticalSize; y++)
        {
            // find out if the rows above are a mirror representation of the rows below y
            // check is mirror by going out from Y -1 and +1 and comparing the chars
            int searchIndex = 1;
            bool isMirror = true;
            while(y - searchIndex >= 0 && y + searchIndex - 1 < verticalSize)
            {
                if (!IsMirror(rockFormation.Rocks[y - searchIndex], rockFormation.Rocks[y + searchIndex - 1 ]))
                {
                    isMirror = false;
                    break;
                }
                searchIndex++;
            }

            if(isMirror)
            {
                return y;
            }
        }

        return null;
    }

    private static bool IsMirror(char[] line1, char[] line2)
    {
        return line1.SequenceEqual(line2);
    }

    #endregion

    public static List<RockFormation> TranslateInput(List<string> inputLines)
    {
        List<string>[] rockFormationLines = new List<string>[inputLines.Count(string.IsNullOrWhiteSpace) + 1];
        List<string> currentFormation = new List<string>();
        int j = 0;
        foreach (var inputLine in inputLines)
        {
            if (string.IsNullOrWhiteSpace(inputLine))
            {
                rockFormationLines[j++] = currentFormation;
                currentFormation = new List<string>();
                continue;
            }
            currentFormation.Add(inputLine);
        }

        if(currentFormation.Any())
            rockFormationLines[j] = currentFormation;

        return rockFormationLines.Select(t => new RockFormation(t)).ToList();
    }
}

public class RockFormation
{
    public RockFormation(List<string> rockFormationLine)
    {
        Rocks = rockFormationLine.Select(x => x.ToCharArray()).ToArray();
        HorizontalMirrorIndex = null;
        VerticalMirrorIndex = null;
    }

    public char[][] Rocks { get; set; }
    public int? HorizontalMirrorIndex { get; set; }
    public int? VerticalMirrorIndex { get; set; }
}
