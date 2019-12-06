using System;
using System.Collections.Generic;

namespace IntcodeComputer
{
  public class InputInstruction : BaseInstruction, IInstruction
  {
    public Parameter Parameter1 => Parameters[0];

    public InputInstruction(int opCode, int parameter1)
    {
      base.OpCode = 3;
      base.PointerAdvancement = 2;

      Parameters = new Parameter[1];
      Parameters[0] = new Parameter() { Value = parameter1 };
    }

    public int Execute(List<int> memory, Queue<int> inputBuffer)
    {
      // pull value from input buffer
      var input = inputBuffer.Dequeue();

      // update address in parameter1
      memory[Parameter1.Value] = input;

      return PointerAdvancement;
    }

  }
}
