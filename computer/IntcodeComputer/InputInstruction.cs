using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace IntcodeComputer
{
  public class InputInstruction : BaseInstruction, IInstruction
  {
    public Parameter Parameter1 => Parameters[0];

    public InputInstruction(int opCode, long parameter1)
    {
      base.OpCode = 3;
      base.PointerAdvancement = 2;

      Parameters = new Parameter[1];
      Parameters[0] = new Parameter() { Value = parameter1 };

      // determine parameter modes
      CalculateParameterModes(opCode);
    }

    public void Execute(List<long> memory, Dictionary<int, long> virtualMemory, ref int instructionPointer, ref int relativeBase, BufferBlock<long> inputBuffer, BufferBlock<long> outputBuffer)
    {
      // pull value from input buffer
      var input = ReceiveAsync(inputBuffer).GetAwaiter().GetResult();

      // write to memory - this will NEVER be in immediate mode, so it's always an address 
      WriteToMemory(memory, virtualMemory, (int)Parameter1.Value, input);

      instructionPointer += PointerAdvancement;
    }

    private async Task<long> ReceiveAsync(BufferBlock<long> inputBuffer)
    {
      return await inputBuffer.ReceiveAsync();
    }
  }
}
