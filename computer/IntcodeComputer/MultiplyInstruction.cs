using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;

namespace IntcodeComputer
{
  public class MultiplyInstruction : BaseInstruction, IInstruction
  {
    public Parameter Parameter1 => Parameters[0];
    public Parameter Parameter2 => Parameters[1];
    public Parameter Parameter3 => Parameters[2];

    public MultiplyInstruction(int opCode, int parameter1, int parameter2, int parameter3)
    {
      base.OpCode = 2;
      base.PointerAdvancement = 4;

      Parameters = new Parameter[3];
      Parameters[0] = new Parameter() { Value = parameter1 };
      Parameters[1] = new Parameter() { Value = parameter2 };
      Parameters[2] = new Parameter() { Value = parameter3 };

      // determine parameter modes
      CalculateParameterModes(opCode);
    }

    public void Execute(List<int> memory, Dictionary<int, int> virtualMemory, ref int instructionPointer, ref int relativeBase, BufferBlock<int> inputBuffer, BufferBlock<int> outputBuffer)
    {
      var value1 = GetParameterValue(Parameter1, memory, virtualMemory, relativeBase);
      var value2 = GetParameterValue(Parameter2, memory, virtualMemory, relativeBase);

      // multiply
      var result = value1 * value2;

      // write to memory - this will NEVER be in immediate mode, so it's always an address
      WriteToMemory(memory, virtualMemory, Parameter3.Value, result);

      // function point advances 4 places
      instructionPointer += PointerAdvancement;
    }
  }
}
