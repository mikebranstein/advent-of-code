using System;
using System.Collections.Generic;

namespace IntcodeComputer
{
  public class AddInstruction : BaseInstruction, IInstruction
  {
    public Parameter Parameter1 => Parameters[0];
    public Parameter Parameter2 => Parameters[1];
    public Parameter Parameter3 => Parameters[2];

    public AddInstruction(int opCode, int parameter1, int parameter2, int parameter3)
    {
      base.OpCode = 1;
      base.PointerAdvancement = 4;

      Parameters = new Parameter[3];
      Parameters[0] = new Parameter() { Value = parameter1 };
      Parameters[1] = new Parameter() { Value = parameter2 };
      Parameters[2] = new Parameter() { Value = parameter3 };
    }

    public int Execute(List<int> memory, Queue<int> inputBuffer)
    {
      var valueOfAddress1 = memory[Parameter1.Value];
      var valueOfAddress2 = memory[Parameter2.Value];

      // addition
      var result = valueOfAddress1 + valueOfAddress2;

      // update code
      memory[Parameter3.Value] = result;

      // function point advances 4 places
      return PointerAdvancement;
    }
  }
}
