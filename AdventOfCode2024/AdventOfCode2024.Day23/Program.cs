using Graph = System.Collections.Generic.Dictionary<string, System.Collections.Generic.HashSet<string>>;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

string[] input = await File.ReadAllLinesAsync("input.txt");

List<(string, string)> lines = new();

foreach (string item in input)
{
    string[] lineHosts = item.Split('-');
    var conn = (lineHosts[0], lineHosts[1]);
    lines.Add(conn);
    lines.Add(Swap(conn));
}

var tHosts = lines.Where(h => h.Item1.StartsWith('t')).Select(h => h.Item1).Distinct();

HashSet<(string, string, string)> connections = new();

foreach (string tHost in tHosts)
{
    var connectedHostsToT = lines.Where(h => h.Item1 == tHost).Select(h => h.Item2).Distinct();
    foreach (string connectedHost in connectedHostsToT)
    {
        var connectedHosts = lines.Where(h => h.Item1 == connectedHost).Select(h => h.Item2).Distinct();
        foreach (string thirdHostCandidate in connectedHosts)
        {
            if (connectedHostsToT.Contains(thirdHostCandidate))
            {
                connections.Add((tHost, connectedHost, thirdHostCandidate));
                connections.Add((tHost, thirdHostCandidate, connectedHost));
                connections.Add((thirdHostCandidate, connectedHost, tHost));
                connections.Add((thirdHostCandidate, tHost, connectedHost));
                connections.Add((connectedHost, tHost, thirdHostCandidate));
                connections.Add((connectedHost, thirdHostCandidate, tHost));
            }
        }
    }
}

foreach (var connectedHost in connections)
{
    Console.WriteLine($"{connectedHost.Item1},{connectedHost.Item2},{connectedHost.Item3}");
}

Console.WriteLine($"{connections.Count / 6}");

Console.WriteLine(PartTwo(input));

Graph GetGraph(string[] lines)
{
    var edges =
        from line in lines
        let nodes = line.Split("-")
        from edge in new[] { (nodes[0], nodes[1]), (nodes[1], nodes[0]) }
        select (From: edge.Item1, To: edge.Item2);

    return (
         from e in edges
         group e by e.From into g
         select (g.Key, g.Select(e => e.To).ToHashSet())
     ).ToDictionary();
}

string PartTwo(string[] lines)
{
    var g = GetGraph(lines);
    var components = GetSeed(g);
    while (components.Count > 1)
    {
        components = Grow(g, components);
    }
    return components.Single();
}

HashSet<string> GetSeed(Graph g) => g.Keys.ToHashSet();

HashSet<string> Grow(Graph g, HashSet<string> components) => (
    from c in components.AsParallel()
    let members = Members(c)
    from neighbour in members.SelectMany(m => g[m]).Distinct()
    where !members.Contains(neighbour)
    where members.All(m => g[neighbour].Contains(m))
    select Extend(c, neighbour)
).ToHashSet();

IEnumerable<string> Members(string c) =>
    c.Split(",");
string Extend(string c, string item) =>
    string.Join(",", Members(c).Append(item).OrderBy(x => x));

(T, T) Swap<T>((T, T) input) => (input.Item2, input.Item1);
