﻿// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

int saveReports = 0;

await foreach(List<int> level in GetLevelsLogsFromFile())
{
    if (!IsAscendingOrDescending(level))
        continue;

    if (!LevelChangeIsSafe(level))
        continue;

    saveReports++;
}

Console.WriteLine($"SaveReports: {saveReports}");

async IAsyncEnumerable<List<int>> GetLevelsLogsFromFile()
{
    string[] levels = await File.ReadAllLinesAsync("input.txt");
    foreach (string level in levels)
    {
        yield return level.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => int.Parse(x)).ToList();
    }
}

bool IsAscendingOrDescending(List<int> levels)
{
    List<int> levelsAsc = levels.OrderBy(x => x).ToList();
    List<int> levelsDesc = levels.OrderByDescending(x => x).ToList();

    return GetDistanceBetweenLists(levels, levelsAsc) == 0
        || GetDistanceBetweenLists(levels, levelsDesc) == 0;
}

// if 0 then are equal
int GetDistanceBetweenLists(List<int> levels, List<int> orderedLevels)
{
    if (levels.Count != orderedLevels.Count)
        return -1;

    int distance = 0;

    for (int i = 0; i < levels.Count; i++)
    {
        distance += Math.Abs(levels[i] - orderedLevels[i]);
    }

    return distance;
}

bool LevelChangeIsSafe(List<int> levels)
{
    for (int i = 0; i < levels.Count - 1; i++)
    {
        int change = Math.Abs(levels[i + 1] - levels[i]);
        if (change == 0 || change > 3)
        {
            return false;
        }
    }

    return true;
}