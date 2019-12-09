using System;
using System.Collections.Generic;

namespace IntcodeComputer
{
  public class BaseInstruction
  {
    public int OpCode { get; set; }
    public int PointerAdvancement { get; set; }
    public Parameter[] Parameters { get; set; }
    public int RelativeBase { get; set; }

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

    protected int GetParameterValue(Parameter parameter, List<int> memory)
    {
      // immediate mode retuns actual parameter value
      if (parameter.Mode == ParameterMode.ImmediateMode) return parameter.Value;

      // position mode does a memory address lookup to get the value
      else if (parameter.Mode == ParameterMode.PositionMode) return memory[parameter.Value];

      else if (parameter.Mode == ParameterMode.Relative) return memory[RelativeBase + parameter.Value];

      throw new Exception("Invalid parameter mode detected.");
    }
  }

}
