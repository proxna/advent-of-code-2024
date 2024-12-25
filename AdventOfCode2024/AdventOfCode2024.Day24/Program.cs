//See https://aka.ms/new-console-template for more information
using AdventOfCode2024.Day24;

Console.WriteLine("Hello, World!");

string input = await File.ReadAllTextAsync("input.txt");

string[] parts = input.Split("\r\n\r\n");

string[] initialData = parts[0].Split("\r\n");

Dictionary<string, int> data = new();

foreach (string dataEntry in initialData)
{
    string[] dataParts = dataEntry.Split(":", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    data.Add(dataParts[0], int.Parse(dataParts[1]));
}

List<Operation> operations = new List<Operation>();

string[] operationEntries = parts[1].Split("\r\n");

foreach (string operationEntry in operationEntries)
{
    operations.Add(new Operation(operationEntry));
}

int oldDataCount = data.Count;

do
{
    foreach (Operation operation in operations)
    {
        operation.Execute(data);
    }

    if (oldDataCount == data.Count)
        break;

    oldDataCount = data.Count;

} while (true);

string output = string.Concat(data.Where(d => d.Key.StartsWith('z')).OrderByDescending(d => d.Key).Select(d => d.Value));


var z = data.Where(d => d.Key.StartsWith('z')).OrderByDescending(d => d.Key);
var x = data.Where(d => d.Key.StartsWith('x')).OrderByDescending(d => d.Key);
var y = data.Where(d => d.Key.StartsWith('y')).OrderByDescending(d => d.Key);

HashSet<Operation> SwapCandidates = new();

for (int i = 0; i < z.Count(); i++)
{
    if (data.ContainsKey($"x{i:00}"))
    {
        int rightAnswer = data[$"x{i:00}"] ^ data[$"y{i:00}"];
        if (rightAnswer != data[$"z{i:00}"])
        {

        }
    }
}

Console.WriteLine(Convert.ToInt64(output, 2));

//string[] input = File.ReadAllText("input.txt").Split("\r\n\r\n");
//Dictionary<string, string> registers = [];

//foreach (string line in input[0].Split("\r\n"))
//{
//    string[] parts = line.Split(": ");
//    registers[parts[0]] = parts[1];
//}
//foreach (string line in input[1].Split("\r\n"))
//{
//    string[] parts = line.Split(" -> ");
//    registers[parts[1]] = parts[0];
//}

//long answer1 = SolvePart1();
//string answer2 = string.Join(",", SolvePart2().OrderBy(s => s));

//Console.WriteLine($"Part 1 answer: {answer1}");
//Console.WriteLine($"Part 2 answer: {answer2}");
//return;

//long SolvePart1() =>
//    registers.Keys.Where(s => s.StartsWith('z')).OrderByDescending(s => s)
//        .Aggregate<string, long>(0, (current, name) => current * 2 + Evaluate(name));

//IEnumerable<string> SolvePart2()
//{
//    List<string> swaps = [];
//    int index = 0;
//    string? carryReg = "";
//    while (registers.ContainsKey($"x{index:00}") && swaps.Count < 8)
//    {
//        string xReg = $"x{index:00}";
//        string yReg = $"y{index:00}";
//        string zReg = $"z{index:00}";
//        if (index == 0)
//        {
//            carryReg = FindExpression(xReg, "AND", yReg);
//        }
//        else
//        {
//            string? XORReg = FindExpression(xReg, "XOR", yReg);
//            string? ANDReg = FindExpression(xReg, "AND", yReg);
//            string? carryInReg = FindExpression(XORReg, "XOR", carryReg);
//            if (carryInReg == null)
//            {
//                swaps.Add(XORReg);
//                swaps.Add(ANDReg);
//                (registers[XORReg], registers[ANDReg]) = (registers[ANDReg], registers[XORReg]);
//                index = 0;
//                continue;
//            }
//            if (carryInReg != zReg)
//            {
//                swaps.Add(carryInReg);
//                swaps.Add(zReg);
//                (registers[carryInReg], registers[zReg]) = (registers[zReg], registers[carryInReg]);
//                index = 0;
//                continue;
//            }
//            carryInReg = FindExpression(XORReg, "AND", carryReg);
//            carryReg = FindExpression(ANDReg, "OR", carryInReg);
//        }
//        index++;
//    }
//    return swaps;
//}

//string? FindExpression(string op1, string op, string op2)
//{
//    string try1 = registers.FirstOrDefault(r => r.Value == $"{op1} {op} {op2}").Key;
//    string try2 = registers.FirstOrDefault(r => r.Value == $"{op2} {op} {op1}").Key;
//    return try1 ?? try2;
//}

//int Evaluate(string name)
//{
//    string value = registers[name];
//    if (int.TryParse(value, out int result))
//    {
//        return result;
//    }
//    string[] parts = value.Split(' ');
//    int op1 = Evaluate(parts[0]);
//    int op2 = Evaluate(parts[2]);
//    return parts[1] switch
//    {
//        "XOR" => op1 ^ op2,
//        "AND" => op1 & op2,
//        _ => op1 | op2
//    };
//}