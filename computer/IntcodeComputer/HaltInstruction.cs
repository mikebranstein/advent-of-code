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

    public void Execute(List<int> memory, ref int instructionPointer, Queue<int> inputBuffer, Queue<int> outputBuffer)
    {
      // do nothing - immediately halt
    }
  }
}
