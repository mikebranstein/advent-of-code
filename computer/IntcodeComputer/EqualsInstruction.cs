using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;

namespace IntcodeComputer
{
  public class EqualsInstruction : BaseInstruction, IInstruction
  {
    public Parameter Parameter1 => Parameters[0];
    public Parameter Parameter2 => Parameters[1];
    public Parameter Parameter3 => Parameters[2];

    public EqualsInstruction(int opCode, long parameter1, long parameter2, long parameter3)
    {
      base.OpCode = 8;
      base.PointerAdvancement = 4;

      Parameters = new Parameter[3];
      Parameters[0] = new Parameter() { Value = parameter1 };
      Parameters[1] = new Parameter() { Value = parameter2 };
      Parameters[2] = new Parameter() { Value = parameter3 };

      // determine parameter modes
      CalculateParameterModes(opCode);
    }

    public void Execute(List<long> memory, Dictionary<int, long> virtualMemory, ref int instructionPointer, ref int relativeBase, BufferBlock<long> inputBuffer, BufferBlock<long> outputBuffer)
    {
      // if the first parameter is equal to the second parameter,
      // it stores 1 in the position given by the third parameter.
      // Otherwise, it stores 0.

      var value1 = GetParameterValue(Parameter1, memory, virtualMemory, relativeBase);
      var value2 = GetParameterValue(Parameter2, memory, virtualMemory, relativeBase);
      var writeToAddress = ConvertWriteToAddress(Parameter3, relativeBase);

      if (value1 == value2)
      {
        // write to memory - this will NEVER be in immediate mode, so it's always an address
        WriteToMemory(memory, virtualMemory, (int)writeToAddress, 1);
      }
      else WriteToMemory(memory, virtualMemory, (int)writeToAddress, 0);

      // function point advances 4 places
      instructionPointer += PointerAdvancement;
    }
  }
}
