Console.WriteLine("Hello, World!");

int saveReports = await CountValidReports();
Console.WriteLine($"SaveReports: {saveReports}");

int saveAdjustedReports = await CountValidAdjustedReports();
Console.WriteLine($"SaveAdjustedReports: {saveAdjustedReports}");

async Task<int> CountValidReports()
{
    int count = 0;

    await foreach (var level in GetLevelsLogsFromFile())
    {
        if (IsAscendingOrDescending(level) && LevelChangeIsSafe(level.ToArray()))
        {
            count++;
        }
    }

    return count;
}

async Task<int> CountValidAdjustedReports()
{
    int count = 0;

    await foreach (var level in GetLevelsLogsFromFile())
    {
        for (int i = 0; i < level.Count; i++)
        {
            var adjustedLevel = level.Where((_, index) => index != i).ToArray();

            if (IsAscendingOrDescending(adjustedLevel.ToList()) && LevelChangeIsSafe(adjustedLevel))
            {
                count++;
                break;
            }
        }
    }

    return count;
}

async IAsyncEnumerable<List<int>> GetLevelsLogsFromFile()
{
    string[] levels = await File.ReadAllLinesAsync("input.txt");
    foreach (var level in levels)
    {
        yield return level
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();
    }
}

bool IsAscendingOrDescending(List<int> levels)
{
    var levelsAsc = levels.OrderBy(x => x).ToList();
    var levelsDesc = levels.OrderByDescending(x => x).ToList();

    return levels.SequenceEqual(levelsAsc) || levels.SequenceEqual(levelsDesc);
}

bool LevelChangeIsSafe(int[] levels)
{
    for (int i = 0; i < levels.Length - 1; i++)
    {
        int change = Math.Abs(levels[i + 1] - levels[i]);
        if (change == 0 || change > 3)
        {
            return false;
        }
    }

    return true;
}