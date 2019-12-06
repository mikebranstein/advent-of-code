using System;
using System.Collections.Generic;

namespace IntcodeComputer
{
  public class HaltInstruction  :BaseInstruction, IInstruction
  {
    public HaltInstruction()
    {
      base.OpCode = 99;
      base.PointerAdvancement = 1;

      base.Parameters = new Parameter[0];
    }

    public int Execute(List<int> memory, Queue<int> inputBuffer)
    {
      // do nothing - immediately halt
      return PointerAdvancement;
    }
  }
}
