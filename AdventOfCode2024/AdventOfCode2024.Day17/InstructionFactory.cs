using AdventOfCode2024.Day17.Instructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day17
{
    internal class InstructionFactory
    {
        public InstructionFactory() { }

        public BaseInstruction CreateInstruction(int code, int operand) => code switch
        {
            0 => new AdvInstruction(operand),
            1 => new BxlInstruction(operand),
            2 => new BstInstruction(operand),
            3 => new JnzInstruction(operand),
            4 => new BxcInstruction(operand),
            5 => new OutInstruction(operand),
            6 => new BdvInstruction(operand),
            7 => new CdvInstruction(operand),
            _ => throw new NotImplementedException()
        };
    }
}
