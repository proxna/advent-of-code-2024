var input = await File.ReadAllTextAsync("input.txt");

var directions = new Dictionary<char, Direction>
{
    ['^'] = Direction.Up,
    ['v'] = Direction.Down,
    ['<'] = Direction.Left,
    ['>'] = Direction.Right
};

var boxes = new List<Position>();
var walls = new List<Position>();
var boxes2 = new List<DoubleNode>();
var walls2 = new List<DoubleNode>();
var start = new Position(-1, -1);

var mapInput = input.Split($"{Environment.NewLine}{Environment.NewLine}")[0].Split(Environment.NewLine);

for (var row = 0; row < mapInput.Length; row++)
{
    for (var col = 0; col < mapInput[row].Length; col++)
    {
        var cell = mapInput[row][col];
        switch (cell)
        {
            case '#':
                walls.Add(new Position(row, col));
                walls2.Add(new DoubleNode(new Position(row, col * 2), new Position(row, col * 2 + 1)));
                break;
            case 'O':
                boxes.Add(new Position(row, col));
                var box = new DoubleNode(new Position(row, col * 2), new Position(row, col * 2 + 1));
                if (boxes2.All(b => b.Right != box.Left))
                {
                    boxes2.Add(box);
                }
                break;
            case '@':
                start = new Position(row, col);
                break;
        }
    }
}

var moves = new List<Direction>();
var movesInput = input.Split($"{Environment.NewLine}{Environment.NewLine}")[1].Split(Environment.NewLine);

foreach (var line in movesInput)
{
    moves.AddRange(line.Select(move => directions[move]));
}

//Part 1
var robot = start;
foreach (var direction in moves)
{
    var next = robot.Move(direction);

    if (walls.Contains(next))
    {
        continue;
    }

    if (boxes.Contains(next))
    {
        var nextBox = next.Move(direction);

        while (boxes.Contains(nextBox))
        {
            nextBox = nextBox.Move(direction);
        }

        if (walls.Contains(nextBox))
        {
            continue;
        }

        boxes.Remove(next);
        boxes.Add(nextBox);
    }

    robot = next;
}

Console.WriteLine($"Part 1: {boxes.Sum(b => b.GpsCoordinates)}");

//Part 2
robot = start with { Col = start.Col * 2 };
foreach (var direction in moves)
{
    var next = robot.Move(direction);

    if (walls2.Any(w => w.Contains(next)))
    {
        continue;
    }

    var nextBox = boxes2.FirstOrDefault(b => b.Contains(next));
    var canMove = true;

    if (nextBox != null)
    {
        var boxesToMove = new HashSet<DoubleNode> { nextBox };
        var queue = new Queue<DoubleNode>();
        queue.Enqueue(nextBox);

        while (queue.Count > 0)
        {
            var box = queue.Dequeue();
            var nextLeft = box.Left.Move(direction);
            var nextRight = box.Right.Move(direction);

            if (walls2.Any(w => w.Contains(nextLeft)) || walls2.Any(w => w.Contains(nextRight)))
            {
                canMove = false;
                break;
            }

            var nextLeftBox = boxes2.FirstOrDefault(b => b.Contains(nextLeft));
            if (nextLeftBox != null && boxesToMove.Add(nextLeftBox))
            {
                queue.Enqueue(nextLeftBox);
            }

            var nextRightBox = boxes2.FirstOrDefault(b => b.Contains(nextRight));
            if (nextRightBox != null && boxesToMove.Add(nextRightBox))
            {
                queue.Enqueue(nextRightBox);
            }
        }

        if (canMove)
        {
            foreach (var box in boxesToMove)
            {
                boxes2.Remove(box);
                boxes2.Add(new DoubleNode(box.Left.Move(direction), box.Right.Move(direction)));
            }
        }
    }

    if (canMove)
    {
        robot = next;
    }
}

Console.WriteLine($"Part 2: {boxes2.Sum(b => b.Left.GpsCoordinates)}");

internal record DoubleNode(Position Left, Position Right)
{
    public bool Contains(Position position) => Left.Row <= position.Row && position.Row <= Right.Row &&
                                               Left.Col <= position.Col && position.Col <= Right.Col;
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

    public int GpsCoordinates => Row * 100 + Col;
}