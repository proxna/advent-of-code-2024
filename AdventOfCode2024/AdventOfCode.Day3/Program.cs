// See https://aka.ms/new-console-template for more information
using AdventOfCode2024.Day3;

Console.WriteLine("Hello, World!");

string lines = await File.ReadAllTextAsync("input.txt");

long finalResult = MulRegexGenerator.GetResultFromLine(lines);
long adjustedFinalResult = MulRegexGenerator.GetResultFromLineWithDoDontInstructions(lines);

Console.WriteLine($"Final Result: {finalResult}");
Console.WriteLine($"Adjusted Final Result: {adjustedFinalResult}");