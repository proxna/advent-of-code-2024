// See https://aka.ms/new-console-template for more information
using AdventOfCode2024.Day17;

Console.WriteLine("Hello, World!");

string input = await File.ReadAllTextAsync("index.txt");

string[] inputParts = input.Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

Registers.Instance.Init(inputParts[0]);

string programStr = inputParts[1].Replace("Program:", string.Empty).Trim();

int[] program = programStr
    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.RemoveEmptyEntries)
    .Select(x => int.Parse(x))
    .ToArray();

InstructionFactory instructionFactory = new InstructionFactory();

int i = 0;

while (i < program.Length - 1)
{
    BaseInstruction instruction = instructionFactory.CreateInstruction(program[i], program[i + 1]);
    instruction.Perform(ref i);
}

Console.WriteLine(Registers.Instance.Buffer);

long a = 0L;

while (true)
{
    Registers.Instance.Init(a);

    int j = 0;

    while (j < program.Length - 1)
    {
        BaseInstruction instruction = instructionFactory.CreateInstruction(program[j], program[j + 1]);
        instruction.Perform(ref j);
    }

    var match = program.TakeLast(Registers.Instance.BufferArray.Length)
                .Zip(Registers.Instance.BufferArray, (p, ot) => p == ot)
                .All(b => b);

    if (match && Registers.Instance.BufferArray.Length == program.Length)
        break;

    a = match ? a << 3 : a + 1;
    Console.WriteLine($"Current a: {a}");
}

Console.WriteLine(a);