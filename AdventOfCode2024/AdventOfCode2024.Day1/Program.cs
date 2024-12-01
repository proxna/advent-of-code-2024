// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var (array1, array2) = await GetArraysFromInputAsync();

// Sort the arrays - used array sort because used quicksort which is best choise for arrays with 1000 elements
Array.Sort(array1);
Array.Sort(array2);

// Calculate distance
int distance = 0;
for (int i = 0; i < array1.Length; i++)
{
    distance += Math.Abs(array1[i] - array2[i]);
}

// used string interpolation for better performance
Console.WriteLine($"Distance: {distance}");

// Calculate similarity
Dictionary<int, int> array2Counts = new();
foreach (var num in array2)
{
    if (!array2Counts.TryAdd(num, 1))
    {
        array2Counts[num]++;
    }
}

int similarity = 0;
foreach (var num in array1)
{
    if (array2Counts.TryGetValue(num, out int count))
    {
        similarity += count * num;
    }
}
Console.WriteLine($"Similarity: {similarity}");

async Task<(int[], int[])> GetArraysFromInputAsync()
{
    var lines = await File.ReadAllLinesAsync("input.txt");

    int[] firstArray = new int[lines.Length];
    int[] secondArray = new int[lines.Length];

    for (int i = 0; i < lines.Length; i++)
    {
        var numbers = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        firstArray[i] = int.Parse(numbers[0]);
        secondArray[i] = int.Parse(numbers[^1]);
    }

    return (firstArray, secondArray);
}