namespace AdventOfCode2024.Day17.Instructions
{
    internal class BxcInstruction : BaseInstruction
    {
        public BxcInstruction(int operand) : base(operand)
        {
            _isComboOperand = false;
        }

        public override void Perform(ref int operandIndex)
        {
            long result = Registers.Instance.RegisterB ^ Registers.Instance.RegisterC;
            Registers.Instance.RegisterB = result;
            base.Perform(ref operandIndex);
        }
    }
}
