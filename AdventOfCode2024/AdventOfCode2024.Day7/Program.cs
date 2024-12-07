Console.WriteLine("Hello, World!");

string[] lines = await File.ReadAllLinesAsync("input.txt");

HashSet<(long expectedResult, long[] operationItems)> operationList = new();

foreach (string line in lines)
{
    string[] items = line.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    long expectedResult = long.Parse(items[0]);
    string[] operationItems = items[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    long[] convertedItem = operationItems.Select(x => long.Parse(x)).ToArray();
    operationList.Add((expectedResult, convertedItem));
}

long result = 0;

foreach((long expectedResult, long[] operationItems) in operationList)
{
    foreach (long[] operationArray in GenerateAllArrays(1, operationItems.Length - 1))
    {
        long[] itemWithOperations = CombineArrays(operationItems, operationArray);
        long operationResult = itemWithOperations[0];
        for(int i = 1; i < itemWithOperations.Length; i += 2)
        {
            operationResult = Calculate(operationResult, itemWithOperations[i + 1], itemWithOperations[i], false);
        }

        if (operationResult == expectedResult)
        {
            result += operationResult;
            break;
        }
    }
}

Console.WriteLine(result);

long resultWithCon = 0;

foreach ((long expectedResult, long[] operationItems) in operationList)
{
    foreach (long[] operationArray in GenerateAllArrays(2, operationItems.Length - 1))
    {
        long[] itemWithOperations = CombineArrays(operationItems, operationArray);
        long operationResult = itemWithOperations[0];
        for (int i = 1; i < itemWithOperations.Length; i += 2)
        {
            operationResult = Calculate(operationResult, itemWithOperations[i + 1], itemWithOperations[i], true);
        }

        if (operationResult == expectedResult)
        {
            resultWithCon += operationResult;
            break;
        }
    }
}

Console.WriteLine(resultWithCon);

long Calculate(long firstItem, long secondItem, long operation, bool concentationEnabled)
{
    switch (operation)
    {
        case 0:
            return firstItem + secondItem;
        case 1:
            return firstItem * secondItem;
        case 2:
            if (!concentationEnabled)
                throw new NotImplementedException();
            return firstItem * (long)Math.Pow(10, CountDigits(secondItem)) + secondItem;
        default:
            throw new NotImplementedException();
    }
}

static List<long[]> GenerateAllArrays(int n, int length)
{
    var result = new List<long[]>();
    long[] current = new long[length];
    GenerateArraysRecursive(result, current, n, length, 0);
    return result;
}

static void GenerateArraysRecursive(List<long[]> result, long[] current, int n, int length, int index)
{
    if (index == length)
    {
        result.Add((long[])current.Clone()); // Add a copy of the current array
        return;
    }

    for (int i = 0; i <= n; i++)
    {
        current[index] = i;
        GenerateArraysRecursive(result, current, n, length, index + 1);
    }
}

static long[] CombineArrays(long[] array1, long[] array2)
{
    // The resulting array will have length = array1.Length + array2.Length
    long[] result = new long[array1.Length + array2.Length];

    int index1 = 0, index2 = 0;

    for (int i = 0; i < result.Length; i++)
    {
        // Place items from array1 at even indices
        if (i % 2 == 0)
        {
            result[i] = array1[index1++];
        }
        // Place items from array2 at odd indices
        else
        {
            result[i] = array2[index2++];
        }
    }

    return result;
}

static int CountDigits(long number)
{
    number = Math.Abs(number); // Handle negative numbers
    if (number == 0) return 1; // Special case for 0

    int count = 0;

    while (number > 0)
    {
        count++;
        number /= 10; // Remove the last digit
    }

    return count;
}