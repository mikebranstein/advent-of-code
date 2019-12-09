using System;
using System.Collections.Generic;

namespace IntcodeComputer
{
  public class BaseInstruction
  {
    public int OpCode { get; set; }
    public int PointerAdvancement { get; set; }
    public Parameter[] Parameters { get; set; }

    protected void CalculateParameterModes(int fullOpCode)
    {
      var numParameters = Parameters.Length;
      var fullOpCodeDigits = 2 + numParameters;

      // pad opp code with 0's on the left to be a total of 2 + num params
      var paddedOppCodeString =
        fullOpCode.ToString().PadLeft(fullOpCodeDigits).Replace(' ', '0');

      // walk backwards through number of potential digits
      // finding the digit for the modes of potential paramters
      int currentDigit, runningSum = 0;
      for (var x = fullOpCodeDigits - 1; x >= 2; x--)
      {
        var power = (int)Math.Pow((double)10, (double)x);
        currentDigit = (fullOpCode - runningSum) / power;
        runningSum += currentDigit * power;

        // currentDigit hold mode (0, 1, or 2) of the parameter
        Parameters[x - 2].Mode = (ParameterMode)currentDigit;
      }
    }

    protected long GetParameterValue(Parameter parameter, List<long> memory, Dictionary<int, long> virtualMemory, int relativeBase)
    {
      // immediate mode retuns actual parameter value
      if (parameter.Mode == ParameterMode.ImmediateMode) return parameter.Value;

      // position mode does a memory address lookup to get the value
      else if (parameter.Mode == ParameterMode.PositionMode) return ReadFromMemory(memory, virtualMemory, (int)parameter.Value);

      else if (parameter.Mode == ParameterMode.Relative) return ReadFromMemory(memory, virtualMemory, relativeBase + (int)parameter.Value);

      throw new Exception("Invalid parameter mode detected.");
    }

    protected long ReadFromMemory(List<long> memory, Dictionary<int, long> virtualMemory, int address)
    {
      // address is in existing memory range
      if (address < memory.Count) return memory[address];

      // otherwise it's a virtual memory
      // check to see if the address exists, if so, return it, if not, initialize to 0 then return
      if (!virtualMemory.ContainsKey(address)) virtualMemory.Add(address, 0);

      // retrieve value and return
      long virtualMemoryValue;
      virtualMemory.TryGetValue(address, out virtualMemoryValue);
      return virtualMemoryValue; 
    }

    protected void WriteToMemory(List<long> memory, Dictionary<int, long> virtualMemory, int address, long value)
    {
      // address is in existing memory range
      if (address < memory.Count) memory[address] = value;

      // otherwise it's virtual memory
      // check to see if the address exists yet, if not, initialize to 0
      if (!virtualMemory.ContainsKey(address)) virtualMemory.Add(address, 0);

      // now write the value to the address in virtual memory
      virtualMemory[address] = value;
    }
  }

}
