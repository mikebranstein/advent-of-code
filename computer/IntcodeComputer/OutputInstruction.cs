using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace IntcodeComputer
{
  public class OutputInstruction : BaseInstruction, IInstruction
  {
    public Parameter Parameter1 => Parameters[0];

    public OutputInstruction(int opCode, long parameter1)
    {
      base.OpCode = 4;
      base.PointerAdvancement = 2;

      Parameters = new Parameter[1];
      Parameters[0] = new Parameter() { Value = parameter1 };

      // determine parameter modes
      CalculateParameterModes(opCode);
    }

    public void Execute(List<long> memory, Dictionary<int, long> virtualMemory, ref int instructionPointer, ref int relativeBase, BufferBlock<long> inputBuffer, BufferBlock<long> outputBuffer)
    {
      // get value from addrss in parameter 1
      var output = GetParameterValue(Parameter1, memory, virtualMemory, relativeBase);

      // pull value from input buffer
      SendAsync(outputBuffer, output).Wait();

      instructionPointer += PointerAdvancement;
    }

    private async Task SendAsync(BufferBlock<long> outputBuffer, long outputValue)
    {
      await outputBuffer.SendAsync(outputValue);
    }


  }
}
