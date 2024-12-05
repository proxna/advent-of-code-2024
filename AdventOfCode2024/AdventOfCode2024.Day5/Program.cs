// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

string[] lines = await File.ReadAllLinesAsync("input.txt");

int dataBorder = 0;

// this dictionary stores data about which pages needs to be before page from key
Dictionary<int, List<int>> pageRules = new Dictionary<int, List<int>>();

foreach (string line in lines)
{
    if (!line.Contains("|"))
        break;

    string[] values = line.Split('|', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

    int latePage = int.Parse(values[1]);

    int beforePage = int.Parse(values[0]);

    if(!pageRules.TryAdd(latePage, new() { beforePage }))
        pageRules[latePage].Add(beforePage);

    dataBorder++;
}

int sum = 0;

List<int> incorrectRows = new List<int>();

for (int i = dataBorder;  i < lines.Length; i++)
{
    bool isCorrect = true;

    if (!lines[i].Contains(","))
        continue;
    string[] values = lines[i].Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    var parsedValues = values.Select(x => int.Parse(x)).ToList();
    for(int j = 0; j < parsedValues.Count; j++)
    {
        int pageValue = parsedValues[j];
        if (!pageRules.ContainsKey(pageValue))
            continue;

        List<int> pagesShouldBeBefore = pageRules[pageValue];

        List<int> pagesToCheck = parsedValues[j..];

        if (pagesShouldBeBefore.Any(x => pagesToCheck.Contains(x)))
        {
            isCorrect = false;
            incorrectRows.Add(i);
            break;
        }
    }

    if (isCorrect)
        sum += parsedValues[(parsedValues.Count - 1) / 2];
}

Console.WriteLine($"Sum: {sum}");

int incorrectsum = 0;

foreach (int i in incorrectRows)
{
    string[] values = lines[i].Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    var parsedValues = values.Select(x => int.Parse(x)).ToArray();

    while (true)
    {
        bool isCorrect = true;

        for(int j = 0; j < parsedValues.Length; j++)
        {
            int pageValue = parsedValues[j];
            if (!pageRules.ContainsKey(pageValue))
                continue;

            List<int> pagesShouldBeBefore = pageRules[pageValue];

            int[] pagesToCheck = parsedValues[j..];

            for (int k = 0; k < pagesToCheck.Length; k++)
            {
                if (pagesShouldBeBefore.Contains(pagesToCheck[k]))
                {
                    isCorrect = false;
                    int tmp = pagesToCheck[k];
                    parsedValues[j + k] = parsedValues[j];
                    parsedValues[j] = tmp;
                    break;
                }
            }

            if (!isCorrect)
                break;
        }

        if (isCorrect)
        {
            incorrectsum += parsedValues[(parsedValues.Length - 1) / 2];
            break;
        }
    }
}

Console.WriteLine($"Incorrect sum: {incorrectsum}");