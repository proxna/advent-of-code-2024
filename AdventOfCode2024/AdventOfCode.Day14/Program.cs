// See https://aka.ms/new-console-template for more information
using AdventOfCode.Day14;

Console.WriteLine("Hello, World!");

(int x, int y) mapSize = (101, 103);
(int x, int y) quadrantBorders = ((int)(mapSize.x / 2), (int)(mapSize.y / 2));
int seconds = 100;

string[] lines = await File.ReadAllLinesAsync("input.txt");

List<Robot> robots = new List<Robot>();

foreach (string line in lines)
{
    robots.Add(RobotRegexGenerator.ReadRobotData(line));
}

for (int i = 0; i < seconds; i++)
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
