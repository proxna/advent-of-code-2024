namespace AdventOfCode2024.Day24
{
    public enum OperationMod
    {
        AND,
        OR,
        XOR
    }

    internal class Operation
    {
        public string InputOne { get; set; }

        public string InputTwo { get; set; }

        public string Output {  get; set; }

        public OperationMod OperationMod { get; set; }

        public Operation(string input)
        {
            string[] parameters = input.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            InputOne = parameters[0];
            InputTwo = parameters[2];
            Output = parameters[4];
            OperationMod = parameters[1] switch
            {
                "AND" => OperationMod.AND,
                "OR" => OperationMod.OR,
                "XOR" => OperationMod.XOR,
                _ => throw new NotImplementedException($"Unsupported operation: {parameters[1]}")
            };
        }

        public void Execute(Dictionary<string, int> data)
        {
            if (data.ContainsKey(InputOne) && data.ContainsKey(InputTwo))
            {
                int output = OperationMod switch
                {
                    OperationMod.AND => data[InputOne] & data[InputTwo],
                    OperationMod.OR => data[InputOne] | data[InputTwo],
                    OperationMod.XOR => data[InputOne] ^ data[InputTwo],
                };

                data.TryAdd(Output, output);
            }
        }
    }
}
