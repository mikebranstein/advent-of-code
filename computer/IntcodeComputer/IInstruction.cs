using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;

namespace IntcodeComputer
{
  public interface IInstruction
  {
    void Execute(List<int> memory, ref int instructionPointer, BufferBlock<int> inputBuffer, BufferBlock<int> outputBuffer);
  }
}
