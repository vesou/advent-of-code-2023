namespace AdventOfCode2023.Day15;

public class Problem15
{
    public const string Day  = "15";

    #region Task 2

    public static long Solve2()
    {
        var inputLines = File.ReadAllLines($"Day{Day}/input1.txt").ToList();

        Data data = TranslateInput(string.Join("\n", inputLines));

        ReadAllInstructions(data);

        return SumFocusingPower(data);
    }

    public static long SumFocusingPower(Data data)
    {
        long totalPower = 0;
        for (int i = 0; i < data.Boxes.Length; i++)
        {
            for (int lensIndex = 0; lensIndex < data.Boxes[i].Lenses.Count; lensIndex++)
            {
                long lensPower = i + 1;
                Lens lens = data.Boxes[i].Lenses[lensIndex];
                lensPower *= lensIndex + 1;
                lensPower *= lens.FocalLength;
                totalPower += lensPower;
            }
        }

        return totalPower;
    }

    public static void ReadAllInstructions(Data data)
    {
        foreach (string initializationSequence in data.InitializationSequences)
        {
            ReadInstruction(initializationSequence, data.Boxes);
        }
    }

    private static void ReadInstruction(string initializationSequence, Box[] dataBoxes)
    {
        string label = GetLabel(initializationSequence);
        int? focalLength = GetFocalLength(initializationSequence);
        int labelIndex = GetHash(label);
        int indexOfExistingLens = dataBoxes[labelIndex].Lenses.FindIndex(x => x.Label == label);
        if (focalLength is not null)
        {
            UpdateLenseInBox(dataBoxes[labelIndex], label, focalLength.Value, indexOfExistingLens);
            return;
        }

        RemoveLensFromBox(dataBoxes[labelIndex], indexOfExistingLens);
    }

    private static void RemoveLensFromBox(Box dataBox, int indexOfExistingLens)
    {
        if (indexOfExistingLens == -1) return;

        dataBox.Lenses.RemoveAt(indexOfExistingLens);
    }

    private static int? GetFocalLength(string initializationSequence)
    {
        var labelInfo = initializationSequence.Split("=");
        if (labelInfo.Length > 1)
            return int.Parse(labelInfo[1]);

        return null;
    }

    private static void UpdateLenseInBox(Box dataBoxes, string label, int focalLength, int index)
    {
        if (index == -1)
        {
            dataBoxes.Lenses.Add(new Lens {Label = label, FocalLength = focalLength});
            return;
        }

        dataBoxes.Lenses[index].FocalLength = focalLength;
    }

    private static string GetLabel(string initializationSequence)
    {
        var labelInfo = initializationSequence.Split("=");
        if (labelInfo.Length > 1) return labelInfo[0];

        return initializationSequence.Split("-")[0];
    }

    #endregion

    #region Task 1

    public static long Solve1()
    {
        var inputLines = File.ReadAllLines($"Day{Day}/input1.txt").ToList();

        Data data = TranslateInput(string.Join("\n", inputLines));

        return GetSumOfAllHashes(data);
    }

    public static long GetSumOfAllHashes(Data data)
    {
        long result = 0;
        foreach (string initializationSequence in data.InitializationSequences)
        {
            result += GetHash(initializationSequence);
        }

        return result;
    }

    public static int GetHash(string inputString)
    {
        int result = 0;
        foreach (char c in inputString)
        {
            result += c;
            result *= 17;
            result %= 256;
        }

        return result;
    }

    #endregion

    public static Data TranslateInput(string inputLine)
    {
        return new Data(inputLine);
    }
}

public class Data
{
    public Data(string inputLine)
    {
        InitializationSequences = inputLine.Split(",").ToList();
        InitializationSequences.RemoveAll(s => string.IsNullOrWhiteSpace(s));
        Boxes = new Box[256];
        for (int i = 0; i < Boxes.Length; i++)
        {
            Boxes[i] = new Box();
        }
    }

    public List<string> InitializationSequences { get; set; }
    public Box[] Boxes { get; set; }
}

public class Box
{
    public Box()
    {
        Lenses = new List<Lens>();
    }

    public List<Lens> Lenses { get; set; }
}

public class Lens
{
    public string Label { get; set; }
    public int FocalLength { get; set; } = 1;
}
