// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

string[] lines = await File.ReadAllLinesAsync("input.txt");

int[,] map = new int[lines.Length, lines[0].Length];

for (int i = 0; i < lines.Length; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        map[i, j] = int.Parse(lines[i][j].ToString());
    }
}

List<(int, int)> neighbours = new()
{
    (1, 0),
    (0, 1),
    (-1, 0),
    (0, -1)
};



Console.WriteLine(GetScore(true).ToString());

Console.WriteLine(GetScore(false).ToString());

int GetScore(bool part1)
{
    int counter = 0;
    Stack<((int, int), (int, int), int)> q = new();
    HashSet<((int, int), (int, int))> visited = [];

    for (int i = 0; i < map.GetLength(0); i++)
    {
        for (int j = 0; j < map.GetLength(1); j++)
        {
            if (map[i, j] == 0)
            {
                q.Push(((i, j), (i, j), 0));
            }
        }
    }

    do
    {
        ((int x, int y) start, (int x, int y), int count) = q.Pop();
        foreach ((int dx, int dy) in neighbours)
        {
            (int, int) nextPos = (x + dx, y + dy);
            if (nextPos.Item1 < 0 || nextPos.Item2 < 0 || nextPos.Item1 >= map.GetLength(0) || nextPos.Item2 >= map.GetLength(1))
                continue;

            int nextVal = map[nextPos.Item1, nextPos.Item2];
            if (nextVal != count + 1)
                continue;
            if (part1 && !visited.Add((start, nextPos)))
                continue;
            if (nextVal == 9)
            {
                counter++;
                continue;
            }
            q.Push((start, nextPos, nextVal));
        }
    } while (q.Count > 0);

    return counter;
}