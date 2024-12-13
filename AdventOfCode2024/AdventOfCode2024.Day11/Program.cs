// See https://aka.ms/new-console-template for more information
string input = "0 5601550 3914 852 50706 68 6 645371";

List<long> stones = input.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
    .Select(x => long.Parse(x)).ToList();

long result = 0;

Dictionary<(long, int), long> cache = new Dictionary<(long, int), long>();

foreach (var stone in stones)
{
    result += GetStonesCount(stone, 75);
}

Console.WriteLine(result);

// it works slowly and for at most 45 blinks
static List<long> Blink(List<long> stones)
{
    List<long> result = new();

    foreach (long stone in stones)
    {
        if (stone == 0)
            result.Add(1);
        else if (EvenDigitsCount(stone))
        {
            foreach (long newStone in DivideNumber(stone))
                result.Add(newStone);
        }
        else
        {
            result.Add(stone * 2024);
        }
    }

    return result;
}

long GetStonesCount(long number, int times)
{
    if (times == 0) return 1;

    if (cache.TryGetValue((number, times), out long result)) return result;

    if (number == 0)
    {
        result = GetStonesCount(1, times - 1);
    }

    else if (EvenDigitsCount(number))
    {
        foreach (long num in DivideNumber(number))
            result += GetStonesCount(num, times - 1);
    }

    else
    {
        result = GetStonesCount(number * 2024, times - 1);
    }

    cache.Add((number, times), result);
    return result;
}

static bool EvenDigitsCount(long number) => number.ToString().Length % 2 == 0;

static long[] DivideNumber(long number)
{
    long divider = number.ToString().Length / 2;
    long[] result = new long[2];
    result[0] = (long)( number / (Math.Pow(10, divider)));
    result[1] = (long)(number - (result[0] * Math.Pow(10, divider)));
    return result;
}

Console.WriteLine("Hello, World!");
