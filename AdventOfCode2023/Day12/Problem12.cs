using System.Text;

namespace AdventOfCode2023.Day12;

public class Problem12
{

    public static char Operational { get; set; } = '.';
    public static char Damaged { get; set; } = '#';
    public static char Unknown { get; set; } = '?';

    public static List<(string combination, long countOfCombinations, int dmgIndex, bool previousWasOperational)> Combinations = new();

    #region Task 2

    public static long Solve2()
    {
        var inputLines = File.ReadAllLines("Day12/input1.txt").ToList();
        inputLines = inputLines.Select(ExpandLine).ToList();
        Combinations = new List<(string combination, long countOfCombinations, int dmgIndex, bool previousWasOperational)>();
        long result = SumAllCombinations(inputLines);
        return result;
    }

    public static string ExpandLine(string line)
    {
        var split = line.Split(" ");
        var hotSprings = split[0];
        var damageNumbers = split[1];
        var expandedLine = new StringBuilder();
        for (int i = 0; i < 5; i++)
        {
            if (i < 4)
                expandedLine.Append(hotSprings+Unknown);
            else
                expandedLine.Append(hotSprings);
        }

        expandedLine.Append(' ');
        for (int i = 0; i < 5; i++)
        {
            if (i < 4)
                expandedLine.Append(damageNumbers+',');
            else
                expandedLine.Append(damageNumbers);
        }

        return expandedLine.ToString();
    }

    #endregion

    #region Task 1

    public static long Solve1()
    {
        var inputLines = File.ReadAllLines("Day12/input1.txt").ToList();
        long result = SumAllCombinations(inputLines);
        return result;
    }

    private static long SumAllCombinations(List<string> inputLines)
    {
        long result = 0;
        var dmgReport = TranslateInput(inputLines);
        foreach (var report in dmgReport)
        {
            Combinations = new List<(string combination, long countOfCombinations, int dmgIndex, bool previousWasOperational)>();
            var combinations = GetNumberOfCombinations(report.HotSprings, report.DamageNumbers);
            result += combinations;
        }

        return result;
    }

    public static long GetNumberOfCombinations(char[] hotSprings, int[] damageNumbers, int hotSpringIndex = 0, int dmgNumberIndex = 0, bool previousWasOperational = true)
    {
        long numberOfCombinations = 0;
        if (hotSpringIndex >= hotSprings.Length)
        {
            if (dmgNumberIndex < damageNumbers.Length)
            {
                return 0;
            }

            return 1;
        }

        var hotSpring = hotSprings[hotSpringIndex];
        string combination = new string(hotSprings.Skip(hotSpringIndex).ToArray());
        if(Combinations.Exists(x => x.combination == combination && x.dmgIndex == dmgNumberIndex && x.previousWasOperational == previousWasOperational))
        {
            numberOfCombinations += Combinations.Find(x => x.combination == combination && x.dmgIndex == dmgNumberIndex && x.previousWasOperational == previousWasOperational).countOfCombinations;
        }
        else
        {
            if (hotSpring == Operational)
            {
                numberOfCombinations += GetNumberOfCombinations(hotSprings, damageNumbers, hotSpringIndex+1, dmgNumberIndex, true);
                Combinations.Add((combination, numberOfCombinations, dmgNumberIndex,previousWasOperational));
            }
            else if (hotSpring == Damaged && DamagedIsPossible(hotSprings, damageNumbers, hotSpringIndex, dmgNumberIndex, previousWasOperational))
            {
                numberOfCombinations += GetNumberOfCombinations(hotSprings, damageNumbers, hotSpringIndex+damageNumbers[dmgNumberIndex], dmgNumberIndex+1, false);
                Combinations.Add((combination, numberOfCombinations, dmgNumberIndex, previousWasOperational));
            }
            else if (hotSpring == Unknown)
            {
                numberOfCombinations += GetNumberOfCombinations(hotSprings, damageNumbers, hotSpringIndex+1, dmgNumberIndex, true);

                // act as damaged
                if (DamagedIsPossible(hotSprings, damageNumbers, hotSpringIndex, dmgNumberIndex, previousWasOperational))
                {
                    numberOfCombinations += GetNumberOfCombinations(hotSprings, damageNumbers, hotSpringIndex+damageNumbers[dmgNumberIndex], dmgNumberIndex+1, false);

                }

                Combinations.Add((combination, numberOfCombinations, dmgNumberIndex, previousWasOperational));
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
