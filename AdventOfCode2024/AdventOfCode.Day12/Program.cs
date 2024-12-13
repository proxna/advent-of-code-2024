// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");
Dictionary<(int, int), char> grid = new Dictionary<(int, int), char>();

string[] lines = await File.ReadAllLinesAsync("input.txt");
for (int i = 0; i < lines.Length; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        grid.Add((i, j), lines[i][j]);
    }
}

Console.WriteLine(PriceOfAllRegion());
Console.WriteLine(PriceOfAllRegion(true));

int Region(int x, int y, HashSet<(int, int)> seen, bool two = false)
{
    var current = grid[(x, y)];
    var perimeter = 0;
    var queue = new Queue<(int, int)>();
    queue.Enqueue((x, y));
    var count = 0;
    HashSet<(double, double)> pos = new();
    while (queue.Count != 0)
    {
        var (i, j) = queue.Dequeue();
        if (!grid.ContainsKey((i, j)) || grid[(i, j)] != current)
        {
            perimeter++;
            continue;
        }

        if (!seen.Add((i, j))) continue;
        if (two) pos.Add((i, j));
        count++;
        queue.Enqueue((i + 1, j));
        queue.Enqueue((i - 1, j));
        queue.Enqueue((i, j + 1));
        queue.Enqueue((i, j - 1));
    }
    if (!two) return perimeter * count;
    HashSet<(double, double)> possibleCorners = [];
    List<(double i, double j)> direction = [(-0.5, -0.5), (0.5, -0.5), (-0.5, 0.5), (0.5, 0.5)];
    foreach (var (i, j) in pos)
    {
        foreach (var (ni, nj) in direction.Select(p => (i + p.i, j + p.j)))
        {
            possibleCorners.Add((ni, nj));
        }
    }

    var sides = 0;
    foreach (var (ci, cj) in possibleCorners)
    {
        var connectedCoo = direction.Select(p => (ci + p.i, cj + p.j)).Where(p => pos.Contains(p)).ToArray();
        switch (connectedCoo.Length)
        {
            case 1 or 3:
                sides += 1;
                break;
            case 2:
                {
                    if (Subtract(connectedCoo.First(), connectedCoo.Last()) is (1, 1) or (-1, -1) or (-1, 1) or (1, -1))
                        sides += 2;
                    break;
                }
        }
    }
    return sides * count;
}

(double, double) Subtract((double, double) value1, (double, double) value2) => (value1.Item1 - value2.Item1, value1.Item2 - value2.Item2);

(int, int) Next(HashSet<(int, int)> seen)
{
    foreach (var item in grid)
    {
        if (!seen.Contains(item.Key))
            return item.Key;
    }

    return (-1, -1);
}
int PriceOfAllRegion(bool two = false)
{
    var seen = new HashSet<(int, int)>();
    var sum = 0;
    var (x, y) = (0, 0);
    while (x != -1)
    {
        sum += Region(x, y, seen, two);
        (x, y) = Next(seen);
    }
    return sum;
}