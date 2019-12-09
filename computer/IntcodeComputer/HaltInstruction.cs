using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;

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

    public void Execute(List<long> memory, Dictionary<int, long> virtualMemory, ref int instructionPointer, ref int relativeBase, BufferBlock<long> inputBuffer, BufferBlock<long> outputBuffer)
    {
      // do nothing - immediately halt
    }
  }
}
