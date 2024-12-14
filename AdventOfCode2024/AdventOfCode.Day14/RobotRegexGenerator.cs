using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Day14
{
    internal static partial class RobotRegexGenerator
    {
        [GeneratedRegex("p=(?<positionX>\\d+),(?<positionY>\\d+)\\sv=(?<velocityX>-?\\d+),(?<velocityY>-?\\d+)")]
        private static partial Regex RobotRegex();

        public static Robot ReadRobotData(string input)
        {
            if(string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException("input");

            Match robotMatch = RobotRegex().Match(input);

            if (!robotMatch.Success)
                throw new ArgumentException("Cannot Read Data");

            int positionX = int.Parse(robotMatch.Groups["positionX"].Value);
            int positionY = int.Parse(robotMatch.Groups["positionY"].Value);
            int velocityX = int.Parse(robotMatch.Groups["velocityX"].Value);
            int velocityY = int.Parse(robotMatch.Groups["velocityY"].Value);
            return new Robot((positionX, positionY), (velocityX, velocityY));
        }
    }
}
