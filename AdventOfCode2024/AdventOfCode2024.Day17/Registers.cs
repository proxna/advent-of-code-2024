namespace AdventOfCode2024.Day17
{
    internal class Registers
    {
        public long RegisterA { get; set; }
        public long RegisterB { get; set; }
        public long RegisterC { get; set; }

        public string Buffer { get; set; } = string.Empty;

        public int[] BufferArray
        {
            get => Buffer.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(el => int.Parse(el)).ToArray();
        }

        public Registers()
        {

        }

        private static Registers _instance;

        public static Registers Instance
        {
            get => _instance ?? (_instance = new Registers());
        }

        public void Init(string input)
        {
            string[] registersInput = input.Split("\r\n");
            RegisterA = long.Parse(registersInput[0].Split(' ').Last());
            RegisterB = long.Parse(registersInput[1].Split(' ').Last());
            RegisterC = long.Parse(registersInput[2].Split(' ').Last());
            Buffer = string.Empty;
        }

        public void Init(long aRegister)
        {
            RegisterA = aRegister;
            RegisterB = 0;
            RegisterC = 0;
            Buffer = string.Empty;
        }
    }
}
