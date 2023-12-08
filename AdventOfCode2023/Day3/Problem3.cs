namespace AdventOfCode2023.Day3;

public class Problem3
{
    #region Task 2
    public static int Solve2()
    {
        var inputLines = File.ReadAllLines("Day3/input1.txt").ToList();
        var inputData = TranslateInput(inputLines);

        return SumOfGears(inputData);
    }

    public static int SumOfGears(char[][] inputData)
    {
        int result = 0;
        int x = 0, y = 0;
        (int? nextGearRatio, int foundX, int foundY) = GetNextGearRatio(inputData, y, x);
        while (nextGearRatio.HasValue)
        {
            result += nextGearRatio.Value;
            (nextGearRatio, foundX, foundY) = GetNextGearRatio(inputData, foundY, foundX + 1);
        }

        return result;
    }

    private static (int? nextGearRatio, int foundX, int foundY) GetNextGearRatio(char[][] inputData, int y, int x)
    {
        if (y >= inputData.Length)
        {
            return (null, 0, 0);
        }

        if (x >= inputData[y].Length)
        {
            return GetNextGearRatio(inputData, y + 1, 0);
        }

        // gear char found
        if (IsGearChar(inputData[y][x]))
        {
            (bool isGear, int gear1, int gear2) = GetGears(inputData, y, x);
            // correct gear found
            if (isGear)
            {
                return (gear1 * gear2, x, y);
            }
        }

        return GetNextGearRatio(inputData, y, x+1);
    }

    private static (bool isGear, int gear1, int gear2) GetGears(char[][] inputData, int gearCharY, int gearCharX)
    {
        (char[][] gearData, int newY) = CopyArrayPart(inputData, gearCharY);
        (int? gear1Value, int gear1Size, int gearX, int gearY) = GetGearNumber(gearData, newY, gearCharX);

        if(!gear1Value.HasValue) return (false, 0, 0);
        gearData = RemoveNumber(gearData, gearX, gearY, gear1Size);
        (int? gear2Value, int? gear2Size, int gear2X, int gear2Y) = GetGearNumber(gearData, newY, gearCharX);

        if(!gear2Value.HasValue) return (false, 0, 0);

        return (gear1Value.HasValue && gear2Value.HasValue, gear1Value.Value, gear2Value.Value);
    }

    private static (int? nextNumber, int nextNumberLength, int foundX, int foundY) GetGearNumber(char[][] inputData, int gearCharY, int gearCharX, int currY = 0, int currX = 0)
    {
        if (currY >= inputData.Length)
        {
            return (null, 0, 0, 0);
        }

        if (currX >= inputData[currY].Length)
        {
            return GetGearNumber(inputData, gearCharY, gearCharX, currY+1, 0);
        }

        // first digit found
        if (IsDigit(inputData[currY][currX]))
        {
            (int numberValue, int numberSize) = GetNumber(inputData[currY], currX);
            if(IsGearNumber(inputData, currY, currX, numberSize, gearCharY, gearCharX))
            {
                return (numberValue, numberSize, currX, currY);
            }

            // jump to the end of the number
            return GetGearNumber(inputData, gearCharY, gearCharX, currY, currX+numberSize);
        }

        return GetGearNumber(inputData, gearCharY, gearCharX, currY, currX+1);
    }

    private static char[][] RemoveNumber(char[][] gearData, int gearX, int gearY, int gear1Size)
    {
        for (int j = gearX; j < gearX+gear1Size; j++)
        {
            gearData[gearY][j] = '.';
        }

        return gearData;
    }

    /// <summary>
    /// Copy array using Array.Copy
    /// </summary>
    /// <param name="inputData"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private static (char[][] newArray, int newY) CopyArrayPart(char[][] inputData, int y)
    {
        char[][] result = new char[3][];
        int newY = 1;
        for (int j = y-1, i = 0; i < 3; j++, i++)
        {
            if (j < 0)
            {
                newY = 0;
                continue;
            }

            if (j >= inputData.Length)
                continue;
            result[i] = new char[inputData[j].Length];
            Array.Copy(inputData[j], result[i], inputData[j].Length);
        }

        return (result, newY);
    }

    private static bool IsGearChar(char c)
    {
        return c is '*';
    }

    private static bool IsGearNumber(char[][] inputData, int y, int x, int numberSize, int gearCharY, int gearCharX)
    {
        for (int currY = y-1; currY <= y+1; currY++)
        {
            if(currY < 0 || currY >= inputData.Length) continue;
            for (int currX = x-1; currX <= x+numberSize; currX++)
            {
                if(currX < 0 || currX >= inputData[currY].Length) continue;
                if(IsGearChar(inputData[currY][currX]) && currY == gearCharY && currX == gearCharX)
                {
                    return true;
                }
            }
        }

        return false;
    }

    #endregion

    #region Task 1

    public static int Solve1()
    {
        var inputLines = File.ReadAllLines("Day3/input1.txt").ToList();
        var inputData = TranslateInput(inputLines);

        return SumOfParts(inputData);
    }

    public static int SumOfParts(char[][] inputData)
    {
        int result = 0;
        int x = 0, y = 0;
        (int? nextNumber, int nextNumberLength, int foundX, int foundY) = GetNextNumber(inputData, y, x);
        while (nextNumber.HasValue)
        {
            result += nextNumber.Value;
            (nextNumber, nextNumberLength, foundX, foundY) = GetNextNumber(inputData, foundY, foundX + nextNumberLength);
        }

        return result;
    }

    private static (int? nextNumber, int nextNumberLength, int foundX, int foundY) GetNextNumber(char[][] inputData, int y, int x)
    {
        if (y >= inputData.Length)
        {
            return (null, 0, 0, 0);
        }

        if (x >= inputData[y].Length)
        {
            return GetNextNumber(inputData, y + 1, 0);
        }

        // first digit found
        if (IsDigit(inputData[y][x]))
        {
            (int numberValue, int numberSize) = GetNumber(inputData[y], x);
            if(IsPartNumber(inputData, y, x, numberSize))
            {
                return (numberValue, numberSize, x, y);
            }

            // jump to the end of the number
            return GetNextNumber(inputData, y, x+numberSize);
        }

        return GetNextNumber(inputData, y, x+1);
    }

    private static bool IsDigit(char c)
    {
        return c is >= '0' and <= '9';
    }

    private static bool IsPartNumber(char[][] inputData, int y, int x, int numberSize)
    {
        for (int currY = y-1; currY <= y+1; currY++)
        {
            if(currY < 0 || currY >= inputData.Length) continue;
            for (int currX = x-1; currX <= x+numberSize; currX++)
            {
                if(currX < 0 || currX >= inputData[currY].Length) continue;
                if(inputData[currY][currX] is not '.' && !IsDigit(inputData[currY][currX]))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static (int numberValue, int numberSize) GetNumber(char[] inputData, int x)
    {
        int numberValue = 0;
        int numberSize = 0;
        while (x < inputData.Length && IsDigit(inputData[x]))
        {
            numberValue *= 10;
            numberSize++;
            numberValue += inputData[x] - '0';
            x++;
        }

        return (numberValue, numberSize);
    }

    #endregion

    public static char[][] TranslateInput(List<string> input)
    {
        char[][] result = new char[input.Count][];
        for (int i = 0; i < input.Count; i++)
        {
            result[i] = input[i].ToCharArray();
        }

        return result;
    }
}
