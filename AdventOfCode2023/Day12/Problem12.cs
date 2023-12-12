namespace AdventOfCode2023.Day12;

public class Problem12
{

    public static char Operational { get; set; } = '.';
    public static char Damaged { get; set; } = '#';
    public static char Unknown { get; set; } = '?';
    #region Task 2

    public static long Solve2(int expansionSize)
    {
        var inputLines = File.ReadAllLines("Day12/input1.txt").ToList();
        List<HotSpringDamageReport> reports = TranslateInput(inputLines);
        int result = 0;

        return result;
    }

    #endregion

    #region Task 1

    public static int Solve1()
    {
        var inputLines = File.ReadAllLines("Day12/input1.txt").ToList();
        int result = SumAllCombinations(inputLines);
        return result;
    }

    private static int SumAllCombinations(List<string> inputLines)
    {
        int result = 0;
        var dmgReport = TranslateInput(inputLines);
        foreach (var report in dmgReport)
        {
            var combinations = GetNumberOfCombinations(report.HotSprings, report.DamageNumbers);
            result += combinations;
        }

        return result;
    }

    public static int GetNumberOfCombinations(char[] hotSprings, int[] damageNumbers, int hotSpringIndex = 0, int dmgNumberIndex = 0, bool previousWasOperational = true)
    {
        int numberOfCombinations = 0;
        if (hotSpringIndex >= hotSprings.Length)
        {
            if (dmgNumberIndex < damageNumbers.Length)
                return 0;

            return 1;
        }

        var hotSpring = hotSprings[hotSpringIndex];
        if (hotSpring == Operational)
        {
            numberOfCombinations += GetNumberOfCombinations(hotSprings, damageNumbers, hotSpringIndex+1, dmgNumberIndex, true);
        }
        else if (hotSpring == Damaged && DamagedIsPossible(hotSprings, damageNumbers, hotSpringIndex, dmgNumberIndex, previousWasOperational))
        {
            numberOfCombinations += GetNumberOfCombinations(hotSprings, damageNumbers, hotSpringIndex+damageNumbers[dmgNumberIndex], dmgNumberIndex+1, false);
        }
        if (hotSpring == Unknown)
        {
            // act as operational
            numberOfCombinations += GetNumberOfCombinations(hotSprings, damageNumbers, hotSpringIndex+1, dmgNumberIndex, true);
            // act as damaged
            if (DamagedIsPossible(hotSprings, damageNumbers, hotSpringIndex, dmgNumberIndex, previousWasOperational))
            {
                numberOfCombinations += GetNumberOfCombinations(hotSprings, damageNumbers, hotSpringIndex+damageNumbers[dmgNumberIndex], dmgNumberIndex+1, false);
            }
        }

        return numberOfCombinations;
    }

    private static bool DamagedIsPossible(char[] hotSprings, int[] damageNumbers, int hotSpringIndex, int dmgNumberIndex, bool previousWasOperational = true)
    {
        if (!previousWasOperational || dmgNumberIndex >= damageNumbers.Length)
            return false;
        int sizeOfDamaged = hotSpringIndex + damageNumbers[dmgNumberIndex];
        for (int i = hotSpringIndex; i < sizeOfDamaged; i++)
        {
            if(i >= hotSprings.Length)
                return false;
            if (hotSprings[i] == Operational)
                return false;
        }

        if(sizeOfDamaged < hotSprings.Length && hotSprings[sizeOfDamaged] == Damaged)
            return false;

        return true;
    }

    #endregion

    public static List<HotSpringDamageReport> TranslateInput(List<string> inputLines)
    {
        List<HotSpringDamageReport> reports = new();
        foreach (var line in inputLines)
        {
            var report = new HotSpringDamageReport(line);
            reports.Add(report);
        }

        return reports;
    }
}

public class HotSpringDamageReport
{
    public HotSpringDamageReport(string line)
    {
        var split = line.Split(" ");
        HotSprings = split[0].ToCharArray();
        DamageNumbers = split[1].Split(",").Select(int.Parse).ToArray();
    }

    public char[] HotSprings { get; set; }
    public int[] DamageNumbers { get; set; }
}
