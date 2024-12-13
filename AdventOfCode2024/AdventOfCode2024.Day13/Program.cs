// See https://aka.ms/new-console-template for more information
using AdventOfCode2024.Day13;

Console.WriteLine("Hello, World!");
string input = await File.ReadAllTextAsync("input.txt");

string[] machineInfos = input.Split("\r\n\r\n");

long tokens = 0;

foreach (string machineInfo in machineInfos)
{
    var machineInfoObj = MachineRegexGenerator.GetMachineInfo(machineInfo, false);
    if (!machineInfoObj.PossibleToWin())
        continue;

    if (machineInfoObj.TryGetTokens(true, out long newtokens))
        tokens += newtokens;
}


Console.WriteLine(tokens);

long partTwoTokens = 0;

foreach (string machineInfo in machineInfos)
{
    var machineInfoObj = MachineRegexGenerator.GetMachineInfo(machineInfo, true);
    if (!machineInfoObj.PossibleToWin())
        continue;

    if (machineInfoObj.TryGetTokens(false, out long newtokens))
        partTwoTokens += newtokens;
}

Console.WriteLine(partTwoTokens);