using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SecureContainer
{
  public class Password
  {
    public List<int> GetPasswordRange(int start, int end)
    {
      var possiblePasswords = new List<int>();
      for (var x = start; x <= end; x++)
      {
        possiblePasswords.Add(x);
      }
      return possiblePasswords;
    }

    public static bool hasRepeatingDigits(int password)
    {
      var digits = password.ToString().Length;
      int currentDigit, previousDigit = int.MinValue;
      int runningSum = 0;
      for (var x = digits - 1; x >= 0; x--)
      {
        var power = (int)Math.Pow((double)10, (double)x);
        currentDigit = (password-runningSum) / power;
        runningSum += currentDigit * power;

        if (previousDigit == currentDigit) return true;

        previousDigit = currentDigit;
      }
      return false;
    }

    public static bool hasNonDecreasingDigits(int password)
    {
      var digits = password.ToString().Length;
      int currentDigit, previousDigit = int.MinValue;
      int runningSum = 0;
      for (var x = digits - 1; x >= 0; x--)
      {
        var power = (int)Math.Pow((double)10, (double)x);
        currentDigit = (password - runningSum) / power;
        runningSum += currentDigit * power;

        if (previousDigit > currentDigit) return false;

        previousDigit = currentDigit;
      }
      return true;
    }


    public static bool isEven(int password)
    {
      return ((password % 2) == 0);
    }

  }
}
