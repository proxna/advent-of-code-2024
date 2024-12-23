// See https://aka.ms/new-console-template for more information
using System.Net;

Console.WriteLine("Hello, World!");

string[] lines = await File.ReadAllLinesAsync("input.txt");
long[] firstNumbers = lines.Select(s => long.Parse(s)).ToArray();

int i = 0;

while (i < 2000)
{
    for (int j = 0; j < firstNumbers.Length; j++)
    {
        firstNumbers[j] = StepOne(firstNumbers[j]);
        firstNumbers[j] = StepTwo(firstNumbers[j]);
        firstNumbers[j] = StepThree(firstNumbers[j]);
    }

    i++;
}

long result = firstNumbers.Sum();

Console.WriteLine(result);

List<List<int>> sequences = new List<List<int>>();

for(int j = 0; j < firstNumbers.Length; j++)
{
    sequences.Add(new List<int> { -1 });
}

firstNumbers = lines.Select(s => long.Parse(s)).ToArray();

i = 0;

while (i < 2000)
{
    for (int j = 0; j < firstNumbers.Length; j++)
    {
        firstNumbers[j] = StepOne(firstNumbers[j]);
        firstNumbers[j] = StepTwo(firstNumbers[j]);
        firstNumbers[j] = StepThree(firstNumbers[j]);
        sequences[j].Add((int)(firstNumbers[j] % 10));
    }

    i++;
}

Console.WriteLine();

var input = File.ReadAllLines("input.txt");
MonkeyMarket(input, out var part1, out var part2, iterations: 2000);

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");

// Part 1 can be super small, actually:
var part1OneLiner = input.Select(int.Parse).Sum(seed => (long)Mrng(seed).Skip(2_000).First());
Console.WriteLine($"Part 1 (short): {part1OneLiner}");

static IEnumerable<int> Mrng(int seed)
{
    var next = seed;
    while (true)
    {
        yield return next;
        next = (next ^ (next << 6)) & 0x00FFFFFF;
        next = (next ^ (next >> 5)) & 0x00FFFFFF;
        next = (next ^ (next << 11)) & 0x00FFFFFF;
    }
}

void MonkeyMarket(IEnumerable<string> seeds, out long part1, out int part2, int iterations = 2000)
{
    var intSeeds = seeds.Select(int.Parse).ToArray();
    var tt = new Dictionary<MarketChanges, int>();
    part1 = 0;

    foreach (var seed in intSeeds)
    {
        using var rng = Mrng(seed).GetEnumerator();
        var cc = new HashSet<MarketChanges>();
        var carry = new sbyte[4];
        int? last = default;
        for (int i = 0; i <= iterations && rng.MoveNext(); ++i)
        {
            int curr = rng.Current;
            var value = curr % 10;
            if (last.HasValue)
            {
                carry[i % 4] = (sbyte)(value - last.Value);
            }

            if (i >= 4)
            {
                var key = new MarketChanges(carry[i % 4], carry[(i - 1) % 4], carry[(i - 2) % 4], carry[(i - 3) % 4]);
                if (cc.Add(key))
                {
                    tt[key] = tt.GetValueOrDefault(key) + value;
                }
            }

            last = value;
        }
        part1 += rng.Current;
    }

    part2 = tt.Values.Max();
}

long Mix(long firstNumber, long secondNumber)
{
    return firstNumber^secondNumber;
}

long Prune(long number)
{
    return number % 16777216;
}

long StepOne(long number)
{
    return Prune(Mix(number * 64, number));
}

long StepTwo(long number)
{
    long newNumber = (long)Math.Round((double)(number / 32), 0);
    return Prune(Mix(newNumber, number));
}

long StepThree(long number)
{
    return Prune(Mix(number * 2048, number));
}

internal readonly record struct MarketChanges(sbyte A, sbyte B, sbyte C, sbyte D);