using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace IntcodeComputer
{
  public class AdjustRelativeBaseInstruction : BaseInstruction, IInstruction
  {
    public Parameter Parameter1 => Parameters[0];

    public AdjustRelativeBaseInstruction(int opCode, int parameter1)
    {
      base.OpCode = 9;
      base.PointerAdvancement = 2;

      Parameters = new Parameter[1];
      Parameters[0] = new Parameter() { Value = parameter1 };

      // determine parameter modes
      CalculateParameterModes(opCode);
    }

    public void Execute(List<int> memory, Dictionary<int, int> virtualMemory, ref int instructionPointer, ref int relativeBase, BufferBlock<int> inputBuffer, BufferBlock<int> outputBuffer)
    {
      // get value from addrss in parameter 1
      var output = GetParameterValue(Parameter1, memory, virtualMemory, relativeBase);

      // update the relative base
      relativeBase += output;

      instructionPointer += PointerAdvancement;
    }

  }
}
