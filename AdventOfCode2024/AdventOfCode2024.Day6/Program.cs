// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

string[] lines = await File.ReadAllLinesAsync("input.txt");

char[,] map = new char[lines.Length, lines[0].Length];

for (int i = 0; i < map.GetLength(0); i++)
{
    for (int j = 0; j < map.GetLength(1); j++)
    {
        map[i, j] = lines[i][j];
    }
}

(int x, int y) startLocation = GetLocation();

(int x, int y) currentLoc = startLocation;

(int x, int y) direction = (-1, 0);

HashSet<(int x, int y)> guardWasThere = new();

do
{
    (int x, int y) positionCandidate = (currentLoc.x + direction.x, currentLoc.y + direction.y);
    try
    {
        if (map[positionCandidate.x, positionCandidate.y] == '#')
        {
            direction = ChangeDirection(direction);
            continue;
        }
    }
    catch
    {
        Console.WriteLine("Leaving the map :)");
    }

    guardWasThere.Add(currentLoc);
    currentLoc = positionCandidate;
} while (!LeavedMap(currentLoc));

Console.WriteLine(guardWasThere.Count);

int obstaclesCausesLoop = 0;

foreach ((int obstaclex, int obstacley) in guardWasThere)
{
    if ((obstaclex, obstacley) == startLocation)
        continue;

    HashSet<(int x, int y, int dx, int dy)> history = new();

    char[,] mapWithObstacle = new char[map.GetLength(0), map.GetLength(1)];
    Array.Copy(map, mapWithObstacle, mapWithObstacle.Length);
    mapWithObstacle[obstaclex, obstacley] = '#';

    direction = (-1, 0);
    currentLoc = startLocation;

    do
    {
        (int x, int y) positionCandidate = (currentLoc.x + direction.x, currentLoc.y + direction.y);
        if (LeavedMap(positionCandidate))
            break;
        if (mapWithObstacle[positionCandidate.x, positionCandidate.y] == '#')
        {
            direction = ChangeDirection(direction);
            continue;
        }
        currentLoc = positionCandidate;
        if (!history.Add((currentLoc.x, currentLoc.y, direction.x, direction.y)))
        {
            obstaclesCausesLoop++;
            break;
        }
    } while (true);
}

Console.WriteLine(obstaclesCausesLoop);

(int x, int y) ChangeDirection((int x, int y) direction)
{
    if (direction.x == -1)
        return (0, 1);
    if (direction.y == 1)
        return (1, 0);
    if (direction.x == 1)
        return (0, -1);
    if (direction.y == -1)
        return (-1, 0);

    return (-1, 0);
}

bool LeavedMap((int x, int y) currentLoc)
{
    return currentLoc.x < 0 || currentLoc.y < 0
        || currentLoc.x >= map.GetLength(0) || currentLoc.y >= map.GetLength(1);
}

void PrintMap()
{
    Console.Clear();

    for (int i = 0; i < map.GetLength(0); i++)
    {
        for (int j = 0; j < map.GetLength(1); j++)
        {
            Console.Write(map[i, j]);
        }

        Console.WriteLine();
    }
}

(int x, int y) GetLocation()
{
    for (int i = 0; i < map.GetLength(0); i++)
    {
        for (int j = 0; j < map.GetLength(1); j++)
        {
            if (map[i, j] == '^')
            {
                return (i, j);
            }
        }
    }

    return (-1, -1);
}