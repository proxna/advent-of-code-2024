// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
(int[], int[]) inputArrays = await GetArraysFromInput();

int distance = 0;

for (int i = 0; i < Math.Min(inputArrays.Item1.Length, inputArrays.Item2.Length); i++)
{
    distance += Math.Abs(inputArrays.Item1[i] - inputArrays.Item2[i]);
}

Console.WriteLine(distance);

async Task<(int[], int[])> GetArraysFromInput()
{
    string[] lines = await File.ReadAllLinesAsync("input.txt");
    int[] firstArray = new int[lines.Length];
    int[] secondArray = new int[lines.Length];
    for(int i = 0; i < lines.Length; i++)
    {
        string[] arr = lines[i].Split(' ');
        firstArray[i] = int.Parse(arr[0].Trim());
        secondArray[i] = int.Parse(arr[arr.Length - 1].Trim());
    }

    return (firstArray.OrderBy(x=>x).ToArray(), secondArray.OrderBy(x => x).ToArray());
}