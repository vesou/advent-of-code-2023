namespace AdventOfCode2023.Day3;

public class Problem3
{
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
            for (int currX = x-1; currX <= x+numberSize+1; currX++)
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
