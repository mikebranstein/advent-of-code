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

        // currentDigit hold mode (0 or 1) of the parameter
        Parameters[x - 2].Mode = (ParameterMode)currentDigit;
      }
    }
  }

}
