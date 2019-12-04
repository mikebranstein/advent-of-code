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

    public static bool hasRepeatingDigitsWithoutLargerGroups(int password)
    {
      var digitCounts = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
      

      var digits = password.ToString().Length;
      int currentDigit, previousDigit = int.MinValue;
      int runningSum = 0;
      for (var x = digits - 1; x >= 0; x--)
      {
        var power = (int)Math.Pow((double)10, (double)x);
        currentDigit = (password - runningSum) / power;
        runningSum += currentDigit * power;

        // if we're a match, increment our counter
        if (previousDigit == currentDigit) digitCounts[currentDigit] = digitCounts[currentDigit] + 1;

        previousDigit = currentDigit;
      }

      // check our digit counts to see if there are any indexes that
      // have a "1", which means there was only a single count and
      // the match doesn't belong to a larger group of matches
      return digitCounts.Exists(x => x == 1);
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
