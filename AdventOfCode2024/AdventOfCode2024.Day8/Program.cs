var input = System.IO.File.ReadAllLines("input.txt");
var mapBounds = new Vector2i(X: input[0].Length, Y: input.Length);

// Parse input into Antennas
var antennas = input
    .SelectMany((row, y) => row
        .Select((cell, x) => new Antenna(new Vector2i(x, y), cell))
        .Where(a => a.Frequency != '.'))
    .ToList();

// Group antennas by frequency
var antennaMap = antennas
    .GroupBy(a => a.Frequency)
    .ToDictionary(
        group => group.Key,
        group => group.ToList());

var antinodes = new HashSet<Vector2i>();

// For each antenna
antennas.ForEach(antenna =>
{
    // Find all other antennas that share the same frequency
    var sameFrequencyAntennas = antennaMap[antenna.Frequency]
           .Where(other => other.pos.X != antenna.pos.X || other.pos.Y != antenna.pos.Y)
           .ToList();

    // For each of those antennas, compute antinodes
    sameFrequencyAntennas.ForEach(other =>
       FindAndAddAntinodes(antenna, other, sameFrequencyAntennas.Count, mapBounds, antinodes, checkSingleAntinode: false)
    );
});

Console.WriteLine(antinodes.Count);

static void FindAndAddAntinodes(
   Antenna antenna,
   Antenna other,
   int sameFrequencyCount,
   Vector2i mapBounds,
   HashSet<Vector2i> antinodes,
   bool checkSingleAntinode)
{
    var foundAntinodes = new List<Vector2i>();

    // Calculate the delta between the two antennas
    var delta = antenna.pos - other.pos;

    // The antinode could be at the position of the current antenna + the distance between the two antennas
    var potentialAntinode = antenna.pos + delta;

    // For part A, we just need to check if the potential antinode is within the map bounds
    if (checkSingleAntinode)
    {
        if (IsInBounds(potentialAntinode, mapBounds))
            foundAntinodes.Add(potentialAntinode);
    }
    else
    {
        // For part B, we need to keep adding the delta to the potential antinode until it's out of bounds
        // so we can find all antinodes on the line.
        foundAntinodes.Add(antenna.pos);
        while (IsInBounds(potentialAntinode, mapBounds))
        {
            foundAntinodes.Add(potentialAntinode);
            potentialAntinode = potentialAntinode + delta;
        }
    }
    foundAntinodes.ForEach(node => antinodes.Add(node));
}

static bool IsInBounds(Vector2i point, Vector2i bounds)
{
    return point.X >= 0 && point.X < bounds.X &&
           point.Y >= 0 && point.Y < bounds.Y;
}

record struct Antenna(Vector2i pos, char Frequency);

record struct Vector2i(int X, int Y)
{
    public static Vector2i operator +(Vector2i a, Vector2i b) => new Vector2i(a.X + b.X, a.Y + b.Y);
    public static Vector2i operator -(Vector2i a, Vector2i b) => new Vector2i(a.X - b.X, a.Y - b.Y);
}

//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

//string[] lines = await File.ReadAllLinesAsync("input.txt");

//char[,] map = new char[lines.Length, lines[0].Length];

//for (int i = 0; i < map.GetLength(0); i++)
//{
//    for (int j = 0; j < map.GetLength(1); j++)
//    {
//        map[i,j] = lines[i][j];
//    }
//}
//HashSet<(int, int)> antinodes = new();

//for (int firstI = 0; firstI < map.GetLength(0); firstI++)
//{
//    for (int firstJ = 0; firstJ < map.GetLength(1); firstJ++)
//    {
//        if (map[firstI, firstJ] == '.')
//            continue;

//        for (int i = 0; i <map.GetLength(0); i++)
//        {
//            for (int j = 0; j < map.GetLength(1); j++)
//            {
//                if (map[i,j] == map[firstI, firstJ] && !(firstI == i && firstJ == j))
//                {
//                    antinodes.Add((i, j));
//                    for(int gridI = i - firstI, gridJ = j - firstJ; true; gridI += gridI, gridJ += gridJ)
//                    {
//                        (int x, int y) loc = (firstI - gridI, firstJ - gridJ);
//                        if (!CanBePlaced(loc))
//                        {
//                            break;
//                        }
//                        antinodes.Add(loc);
//                    }
//                }
//            }
//        }
//    }
//}

//Console.WriteLine(antinodes.Count());

//bool CanBePlaced((int x, int y) location)
//{
//    try
//    {
//        return map[location.x, location.y] != '#';
//    }
//    catch { return false; }
//}