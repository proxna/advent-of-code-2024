namespace AdventOfCode2024.Day17.Instructions
{
    internal class CdvInstruction : BaseInstruction
    {
        public CdvInstruction(int operand) : base(operand)
        {
            _isComboOperand = true;
        }

        public override void Perform(ref int operandIndex)
        {
            long result = (long)(Registers.Instance.RegisterA / Math.Pow(2, Operand));
            Registers.Instance.RegisterC = result;
            base.Perform(ref operandIndex);
        }
    }
}
