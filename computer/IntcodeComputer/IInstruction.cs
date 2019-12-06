using System;
using System.Collections.Generic;

namespace IntcodeComputer
{
  public interface IInstruction
  {
    void Execute(List<int> memory, ref int instructionPointer, Queue<int> inputBuffer, Queue<int> outputBuffer);
  }
}
