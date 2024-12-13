namespace AdventOfCode2024.Day13
{
    internal class MachineInfo
    {
        public (int x, int y) ButtonA {  get; set; }
        public (int x, int y) ButtonB { get; set; }

        public (long x, long y) Price {  get; set; }

        private int Rate => ButtonA.x * ButtonB.y - ButtonA.y * ButtonB.x;

        public bool PossibleToWin() => Rate != 0;

        public bool TryGetTokens(bool partOne, out long tokens)
        {
            tokens = 0;
            long rateA = Price.x * ButtonB.y - Price.y * ButtonB.x;
            long rateB = ButtonA.x * Price.y - Price.x * ButtonA.y;

            double pressA = (double)rateA / (double)Rate;
            double pressB = (double)rateB / (double)Rate;

            if (pressA % 1 != 0 || pressB % 1 != 0)
                    return false;

            if(pressA < 0 || pressB < 0) return false;

            if (partOne && (pressA > 100 || pressB > 100)) return false;

            tokens = (long)pressA * 3 + (long)pressB;
            return true;
        }
    }
}
