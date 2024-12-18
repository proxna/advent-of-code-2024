var drops = (await File.ReadAllLinesAsync("input.txt"))
    .Select(line => line.Split(',').Select(int.Parse).ToArray())
    .Select(items => new Position(items[1], items[0])).ToArray();

var size = drops.Max(d => Math.Max(d.Row, d.Col)) + 1;
var nanoseconds = size < 70 ? 12 : 1024;

var start = new Position(0, 0);
var end = new Position(size - 1, size - 1);

Console.WriteLine($"Part 1: {Walk(nanoseconds)}");

// Binary search for the first drop that will block the exit.
var min = 0;
var max = drops.Length - 1;
while (max - min > 1)
{
    var mid = (min + max) / 2;
    if (Walk(mid) == -1)
    {
        max = mid;
    }
    else
    {
        min = mid;
    }
}

Console.WriteLine($"Part 2: {drops[min].Col},{drops[min].Row}");

return;

int Walk(int pointInTime)
{
    var corrupted = new bool[size, size];
    for (var i = 0; i < pointInTime; i++)
    {
        var drop = drops[i];
        corrupted[drop.Row, drop.Col] = true;
    }

    var queue = new PriorityQueue<Position, int>();
    queue.Enqueue(start, 0);
    var visited = new HashSet<Position>();

    while (queue.TryDequeue(out var current, out var time))
    {
        if (current == end)
        {
            return time;
        }

        if (!visited.Add(current))
        {
            continue;
        }

        foreach (var dir in new[] { Direction.Up, Direction.Down, Direction.Left, Direction.Right })
        {
            var next = current.Move(dir);
            if (!next.OutOfBounds(size) && !corrupted[next.Row, next.Col])
            {
                queue.Enqueue(next, time + 1);
            }
        }
    }

    return -1;
}

internal record Direction(int Row, int Col)
{
    public static readonly Direction Up = new(-1, 0);
    public static readonly Direction Down = new(1, 0);
    public static readonly Direction Left = new(0, -1);
    public static readonly Direction Right = new(0, 1);
}

internal record Position(int Row, int Col)
{
    public Position Move(Direction dir) => new(Row + dir.Row, Col + dir.Col);

    public bool OutOfBounds(int size) => OutOfBounds(0, size);

    public bool OutOfBounds(int start, int end) => Row < start || Row >= end || Col < start || Col >= end;
}
