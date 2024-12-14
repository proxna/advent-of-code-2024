// See https://aka.ms/new-console-template for more information
using AdventOfCode.Day14;

Console.WriteLine("Hello, World!");

(int x, int y)[] CardinalDirections = [(0, -1), (1, 0), (0, 1), (-1, 0)];

(int x, int y) mapSize = (101, 103);
(int x, int y) quadrantBorders = ((int)(mapSize.x / 2), (int)(mapSize.y / 2));
int seconds = 100;

string[] lines = await File.ReadAllLinesAsync("input.txt");

List<Robot> robots = new List<Robot>();

foreach (string line in lines)
{
    robots.Add(RobotRegexGenerator.ReadRobotData(line));
}

for (int sek = 1; sek < seconds; sek++)
{
    foreach (Robot robot in robots)
    {
        robot.Move(mapSize);
    }
}

List<Quadrant> quadrants = new()
{
    new (0, 0, quadrantBorders.x - 1, quadrantBorders.y - 1),
    new (quadrantBorders.x + 1, 0, mapSize.x - 1, quadrantBorders.y - 1),
    new (0, quadrantBorders.y + 1, quadrantBorders.x - 1, mapSize.y - 1),
    new (quadrantBorders.x + 1, quadrantBorders.y + 1, mapSize.x - 1, mapSize.y - 1)
};

int result = 1;

foreach (Quadrant quadrant in quadrants)
{
    result *= quadrant.GetRobotsFromQuadrant(robots).ToList().Count;
}

Console.WriteLine(result);

robots.Clear();

foreach (string line in lines)
{
    robots.Add(RobotRegexGenerator.ReadRobotData(line));
}

int treeSec = 0;

do
{
    treeSec++;
    foreach (Robot robot in robots)
    {
        robot.Move(mapSize);
    }

    if (LargestGroup(robots) > 25)
    {
        Console.WriteLine($"Printing {treeSec} second");
        PrintRobots(robots, treeSec);
        break;
    }

} while (true);

int LargestGroup(List<Robot> robots)
{
    int largest = 0;

    bool[,] map = new bool[mapSize.x, mapSize.y];
    foreach (Robot robot in robots)
    {
        map[robot.Position.x, robot.Position.y] = true;
    }

    bool[,] visited = new bool[mapSize.x, mapSize.y];
    for (int x = 0; x < mapSize.x; x++)
        for (int y = 0; y < mapSize.x; y++)
        {
            if (visited[x, y]) continue;

            int count = CountGroup((x, y), map, visited);
            if (count > largest) largest = count;
        }

    return largest;
}

int CountGroup((int x, int y) pos, bool[,] robots, bool[,] visited)
{
    if (pos.x < 0 || pos.x >= mapSize.x || pos.y < 0 || pos.y >= mapSize.y) return 0;
    if (!robots[pos.x, pos.y]) return 0;
    if (visited[pos.x, pos.y]) return 0;

    visited[pos.x, pos.y] = true;

    int total = 0;
    foreach ((int dx, int dy) in CardinalDirections)
    {
        total += CountGroup((pos.x + dx, pos.y + dy), robots, visited);
    }

    return total + 1;
}

void PrintRobots(List<Robot> robots, int second)
{
    char[,] map = new char[mapSize.x, mapSize.y];
    for (int i = 0; i < mapSize.x; i++)
    {
        for (int j = 0; j < mapSize.y; j++)
        {
            map[i, j] = ' ';
        }
    }

    foreach (Robot robot in robots)
    {
        map[robot.Position.x, robot.Position.y] = '*';
    }

    using StreamWriter streamWriter = new($"{second}.txt");

    for (int i = 0; i < mapSize.x; i++)
    {
        for (int j = 0; j < mapSize.y; j++)
        {
            streamWriter.Write(map[i, j]);
        }
        streamWriter.WriteLine();
    }
}


