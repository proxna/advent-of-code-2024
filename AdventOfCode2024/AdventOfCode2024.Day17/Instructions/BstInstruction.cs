namespace AdventOfCode2024.Day17.Instructions
{
    internal class BstInstruction : BaseInstruction
    {
        public BstInstruction(int operand) : base(operand)
        {
            _isComboOperand = true;
        }

        public override void Perform(ref int operandIndex)
        {
            long result = Operand % 8;
            Registers.Instance.RegisterB = result;
            base.Perform(ref operandIndex);
        }
    }
}
