// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

string[] lines = await File.ReadAllLinesAsync("input.txt");

char[,] map = new char[lines.Length, lines[0].Length];

for (int i = 0; i < map.GetLength(0); i++)
{
    for (int j = 0; j < map.GetLength(1); j++)
    {
        map[i,j] = lines[i][j];
    }
}
HashSet<(int, int)> antinodes = new();

for (int firstI = 0; firstI < map.GetLength(0); firstI++)
{
    for (int firstJ = 0; firstJ < map.GetLength(1); firstJ++)
    {
        if (map[firstI, firstJ] == '.' || map[firstI, firstJ] == '#')
            continue;

        for (int i = 0; i <map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i,j] == map[firstI, firstJ] && !(firstI == i && firstJ == j))
                {
                    (int x, int y) loc = (firstI - (i - firstI), firstJ - (j - firstJ));
                    if (CanBePlaced(loc))
                    {
                        antinodes.Add(loc);
                    }
                }
            }
        }
    }
}

Console.WriteLine(antinodes.Count());

for (int i = 0; i < map.GetLength(0); i++)
{
    for (int j = 0; j < map.GetLength(1); j++)
    {
        Console.Write(map[i, j]);
    }

    Console.WriteLine();
}

bool CanBePlaced((int x, int y) location)
{
    try
    {
        return map[location.x, location.y] != '#';
    }
    catch { return false; }
}