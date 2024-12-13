using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day13
{
    internal static partial class MachineRegexGenerator
    {
        [GeneratedRegex("Button A: X\\+(?<X>\\d+),\\sY\\+(?<Y>\\d+)")]
        private static partial Regex ButtonARegex();

        [GeneratedRegex("Button B: X\\+(?<X>\\d+),\\sY\\+(?<Y>\\d+)")]
        private static partial Regex ButtonBRegex();

        [GeneratedRegex("Prize: X=(?<X>\\d+),\\sY=(?<Y>\\d+)")]
        private static partial Regex PriceRegex();

        public static MachineInfo GetMachineInfo(string input, bool part2)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("Input is null");
            }

            MachineInfo machineInfo = new();

            Match buttonA = ButtonARegex().Match(input);
            machineInfo.ButtonA = (int.Parse(buttonA.Groups["X"].Value), int.Parse(buttonA.Groups["Y"].Value));
            Match buttonB = ButtonBRegex().Match(input);
            machineInfo.ButtonB = (int.Parse(buttonB.Groups["X"].Value), int.Parse(buttonB.Groups["Y"].Value));
            Match price = PriceRegex().Match(input);
            machineInfo.Price = (long.Parse(price.Groups["X"].Value), long.Parse(price.Groups["Y"].Value));
            if (part2)
            {
                machineInfo.Price = (machineInfo.Price.x + 10_000_000_000_000, machineInfo.Price.y + 10_000_000_000_000);
            }
            return machineInfo;
        }
    }
}
