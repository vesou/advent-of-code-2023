namespace AdventOfCode2023.Day14;

public class Problem14
{
    public static char Space = '.';
    public static char SupportRock = '#';
    public static char RoundRock = 'O';

    #region Task 2

    public static long Solve2()
    {
        var inputLines = File.ReadAllLines("Day14/input1.txt").ToList();

        return 0;
    }



    #endregion

    #region Task 1

    public static long Solve1()
    {
        var inputLines = File.ReadAllLines("Day14/input1.txt").ToList();

        RockFormation rockFormations = TranslateInput(inputLines);

        return CalculateTotalLoad(rockFormations);
    }

    public static long CalculateTotalLoad(RockFormation rockFormations)
    {
        char[][] rocks = rockFormations.Rocks;
        long totalLoad = 0;
        for (int x = 0; x < rocks[0].Length; x++)
        {
            totalLoad += CalculateLineLoad(rocks, x);
        }

        return totalLoad;
    }

    public static long CalculateLineLoad(char[][] rocks, int x)
    {
        long load = 0;
        long extraLoad = 0;
        for (int y = 0; y < rocks.Length; y++)
        {
            long rockLoad = rocks.Length - y;
            if (rocks[y][x] == RoundRock)
            {
                load += rockLoad + extraLoad;
            }
            else if (rocks[y][x] == Space)
            {
                extraLoad++;
            }
            else if (rocks[y][x] == SupportRock)
            {
                extraLoad = 0;
            }
        }

        return load;
    }

    #endregion

    public static RockFormation TranslateInput(List<string> inputLines)
    {
        RockFormation formation = new RockFormation(inputLines);

        return formation;
    }
}

public class RockFormation
{
    public RockFormation(List<string> rockFormationLines)
    {
        Rocks = rockFormationLines.Select(x => x.ToCharArray()).ToArray();
    }

    public char[][] Rocks { get; set; }
}
