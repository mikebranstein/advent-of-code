using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;

namespace IntcodeComputer
{
  public class JumpIfFalseInstruction : BaseInstruction, IInstruction
  {
    public Parameter Parameter1 => Parameters[0];
    public Parameter Parameter2 => Parameters[1];

    public JumpIfFalseInstruction(int opCode, int parameter1, int parameter2)
    {
      base.OpCode = 6;
      base.PointerAdvancement = 3;

      Parameters = new Parameter[2];
      Parameters[0] = new Parameter() { Value = parameter1 };
      Parameters[1] = new Parameter() { Value = parameter2 };

      // determine parameter modes
      CalculateParameterModes(opCode);
    }

    public void Execute(List<int> memory, ref int instructionPointer, BufferBlock<int> inputBuffer, BufferBlock<int> outputBuffer)
    {
      // if the first parameter is zero, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
      var booleanValue = GetParameterValue(Parameter1, memory);
      if (booleanValue == 0)
        instructionPointer = GetParameterValue(Parameter2, memory);
      else
        instructionPointer += PointerAdvancement;
    }
  }
}
