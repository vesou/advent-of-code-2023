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

        RockFormation rockFormations = TranslateInput(inputLines);
        rockFormations.Spin(1000000000);

        return CalculateTotalLoad(rockFormations);
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

    public void Spin(int numberOfSpins)
    {
        for (int i = 0; i < numberOfSpins; i++)
        {
            Tilt(0, -1);
            Tilt(-1, 0);
            TiltDescendingY(0, 1);
            TiltDescendingX(1, 0);
        }
    }

    public void TiltDescendingY(int xChange, int yChange)
    {
        for (int y = Rocks.Length - 1; y >= 0; y--)
        {
            for (int x = 0; x < Rocks[y].Length; x++)
            {
                if (Rocks[y][x] == Problem14.RoundRock)
                {
                    MoveRoundRock(x, y, xChange, yChange);
                }
            }
        }
    }

    public void TiltDescendingX(int xChange, int yChange)
    {
        for (int y = 0; y < Rocks.Length; y++)
        {
            for (int x = Rocks[y].Length - 1; x >= 0; x--)
            {
                if (Rocks[y][x] == Problem14.RoundRock)
                {
                    MoveRoundRock(x, y, xChange, yChange);
                }
            }
        }
    }

    public void Tilt(int xChange, int yChange)
    {
        for (int y = 0; y < Rocks.Length; y++)
        {
            for (int x = 0; x < Rocks[y].Length; x++)
            {
                if (Rocks[y][x] == Problem14.RoundRock)
                {
                    MoveRoundRock(x, y, xChange, yChange);
                }
            }
        }
    }

    private void MoveRoundRock(int x, int y, int xChange, int yChange)
    {
        if (x + xChange < 0 || x + xChange >= Rocks[y].Length)
        {
            return;
        }

        if (y + yChange < 0 || y + yChange >= Rocks.Length)
        {
            return;
        }

        if (Rocks[y + yChange][x + xChange] != Problem14.Space)
            return;

        Rocks[y + yChange][x + xChange] = Problem14.RoundRock;
        Rocks[y][x] = Problem14.Space;
        MoveRoundRock(x + xChange, y + yChange, xChange, yChange);
    }
}
