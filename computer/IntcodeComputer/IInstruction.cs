using System;
using System.Collections.Generic;

namespace IntcodeComputer
{
  public interface IInstruction
  {
    int Execute(List<int> memory, Queue<int> inputBuffer, Queue<int> outputBuffer);
  }
}
