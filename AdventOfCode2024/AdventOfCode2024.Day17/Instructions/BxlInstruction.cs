namespace AdventOfCode2024.Day17.Instructions
{
    internal class BxlInstruction : BaseInstruction
    {
        public BxlInstruction(int operand) : base(operand)
        {
            _isComboOperand = false;
        }

        public override void Perform(ref int operandIndex)
        {
            long result = Registers.Instance.RegisterB ^ Operand;
            Registers.Instance.RegisterB = result;
            base.Perform(ref operandIndex);
        }
    }
}
