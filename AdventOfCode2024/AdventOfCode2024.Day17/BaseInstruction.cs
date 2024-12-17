namespace AdventOfCode2024.Day17
{
    internal abstract class BaseInstruction
    {
        protected bool _isComboOperand;

        private int _operand;

        public long Operand { get => CalculateComboOperand(); }

        public BaseInstruction(int operand)
        {
            _operand = operand;
        }

        private long CalculateComboOperand()
        {
            if (!_isComboOperand)
                return _operand;

            if (_operand >= 0 && _operand <= 3)
                return _operand;

            if (_operand == 4)
                return Registers.Instance.RegisterA;

            if (_operand == 5)
                return Registers.Instance.RegisterB;

            if (_operand == 6)
                return Registers.Instance.RegisterC;

            throw new NotImplementedException();
        }

        public virtual void Perform(ref int operandIndex)
        {
            operandIndex += 2;
        }
    }
}
