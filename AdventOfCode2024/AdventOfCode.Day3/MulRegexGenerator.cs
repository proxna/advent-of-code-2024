using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day3
{
    internal static partial class MulRegexGenerator
    {
        [GeneratedRegex("mul\\((?<first>\\d+),(?<second>\\d+)\\)", RegexOptions.IgnoreCase, "en-US")]
        private static partial Regex MulGeneratedRegex();

        public static long GetResultFromLine(string line)
        {
            long result = 0;
            foreach (Match match in MulGeneratedRegex().Matches(line))
            {
                long mulresult = long.Parse(match.Groups["first"].Value)
                    * long.Parse(match.Groups["second"].Value);
                result += mulresult;
            }

            return result;
        }

        public static long GetResultFromLineWithDoDontInstructions(string line)
        {
            long result = 0;
            foreach (Match match in MulGeneratedRegex().Matches(line))
            {
                bool allowToResult = GetLastInstructionForCalculation(line, match.Index);

                if (!allowToResult)
                    continue;

                long mulresult = long.Parse(match.Groups["first"].Value)
                    * long.Parse(match.Groups["second"].Value);


                result += mulresult;
            }

            return result;
        }

        private static bool GetLastInstructionForCalculation(string line, int index)
        {
            string lineFragment = line.Substring(0, index);
            int doIndex = lineFragment.LastIndexOf("do()");
            int doNotIndex = lineFragment.LastIndexOf("don't()");

            return doNotIndex == -1 || doIndex > doNotIndex;
        }
    }
}
