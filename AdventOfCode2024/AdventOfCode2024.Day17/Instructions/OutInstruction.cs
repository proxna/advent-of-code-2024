namespace AdventOfCode2024.Day17.Instructions
{
    internal class OutInstruction : BaseInstruction
    {
        public OutInstruction(int operand) : base(operand)
        {
            _isComboOperand = true;
        }

        public override void Perform(ref int operandIndex)
        {
            long result = Operand % 8;
            Registers.Instance.Buffer += $"{result},";
            base.Perform(ref operandIndex);
        }
    }
}
