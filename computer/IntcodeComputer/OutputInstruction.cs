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

    public OutputInstruction(int opCode, int parameter1)
    {
      base.OpCode = 4;
      base.PointerAdvancement = 2;

      Parameters = new Parameter[1];
      Parameters[0] = new Parameter() { Value = parameter1 };

      // determine parameter modes
      CalculateParameterModes(opCode);
    }

    public void Execute(List<int> memory, ref int instructionPointer, BufferBlock<int> inputBuffer, BufferBlock<int> outputBuffer)
    {
      // get value from addrss in parameter 1
      var output = GetParameterValue(Parameter1, memory);

      // pull value from input buffer
      SendAsync(outputBuffer, output).Wait();

      instructionPointer += PointerAdvancement;
    }

    private async Task SendAsync(BufferBlock<int> outputBuffer, int outputValue)
    {
      await outputBuffer.SendAsync(outputValue);
    }


  }
}
