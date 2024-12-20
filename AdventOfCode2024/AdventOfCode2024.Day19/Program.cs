// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


string input = await File.ReadAllTextAsync("input.txt");

string[] inputParts = input.Split("\r\n\r\n");

string[] patterns = inputParts[0].Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).OrderByDescending(s => s.Length).ToArray();

string[] towels = inputParts[1].Split("\r\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

Dictionary<string, long> memo = new() { [string.Empty] = 1L };

Console.WriteLine(towels.Count(design => Permute(design, patterns, memo) > 0));
Console.WriteLine(towels.Sum(design => Permute(design, patterns, memo)));

static long Permute(string design, string[] patterns, Dictionary<string, long> memo)
{
    if (memo.TryGetValue(design, out var value))
    {
        return value;
    }

    memo[design] = patterns
        .Where(design.StartsWith)
        .Sum(pattern => Permute(design[pattern.Length..], patterns, memo));
    return memo[design];
}