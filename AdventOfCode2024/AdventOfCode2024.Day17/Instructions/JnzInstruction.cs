namespace AdventOfCode2024.Day17.Instructions
{
    internal class JnzInstruction : BaseInstruction
    {
        public JnzInstruction(int operand) : base(operand)
        {
            _isComboOperand = false;
        }

        public override void Perform(ref int operandIndex)
        {
            if (Registers.Instance.RegisterA == 0){
                base.Perform(ref operandIndex);
                return;
            }
            operandIndex = (int)Operand;
        }
    }
}
