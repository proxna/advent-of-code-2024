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
        if (IsAscendingOrDescendingOptimized(level) && LevelChangeIsSafe(level))
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
            // brute-force of removing one element from list and checking report
            // reports without need of adjusting still passes
            var adjustedLevel = level.Where((_, index) => index != i);

            if (IsAscendingOrDescendingOptimized(adjustedLevel) && LevelChangeIsSafe(adjustedLevel))
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

bool IsAscendingOrDescendingOptimized(IEnumerable<int> levels)
{
    bool ascending = true, descending = true;

    int? previous = null;
    foreach (var current in levels)
    {
        if (previous.HasValue)
        {
            if (current < previous) ascending = false;
            if (current > previous) descending = false;

            // if both false then return false immidiately
            if (!ascending && !descending) return false;
        }

        previous = current;
    }

    return true;
}

bool LevelChangeIsSafe(IEnumerable<int> levels)
{
    int? previous = null;
    foreach (var current in levels)
    {
        if (previous.HasValue)
        {
            int change = Math.Abs(current - previous.Value);
            if (change == 0 || change > 3)
            {
                return false;
            }
        }

        previous = current;
    }

    return true;
}