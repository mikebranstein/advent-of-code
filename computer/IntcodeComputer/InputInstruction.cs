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

      // determine parameter modes
      CalculateParameterModes(opCode);
    }

    public int Execute(List<int> memory, Queue<int> inputBuffer, Queue<int> outputBuffer)
    {
      // pull value from input buffer
      var input = inputBuffer.Dequeue();

      // write to memory - this will NEVER be in immediate mode, so it's always an address 
      memory[Parameter1.Value] = input;

      return PointerAdvancement;
    }

  }
}
