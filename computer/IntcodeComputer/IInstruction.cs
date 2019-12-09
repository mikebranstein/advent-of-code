using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;

namespace IntcodeComputer
{
  public interface IInstruction
  {
    void Execute(List<int> memory, Dictionary<int, int> virtualMemory, ref int instructionPointer, ref int relativeBase, BufferBlock<int> inputBuffer, BufferBlock<int> outputBuffer);
  }
}
