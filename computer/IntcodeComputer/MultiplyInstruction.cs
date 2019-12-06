using System;
using System.Collections.Generic;

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

    public int Execute(List<int> memory, Queue<int> inputBuffer, Queue<int> outputBuffer)
    {
      var value1 = GetParameterValue(Parameter1, memory);
      var value2 = GetParameterValue(Parameter2, memory);

      // multiply
      var result = value1 * value2;

      // write to memory - this will NEVER be in immediate mode, so it's always an address
      memory[Parameter3.Value] = result;

      // function point advances 4 places
      return PointerAdvancement;
    }
  }
}
