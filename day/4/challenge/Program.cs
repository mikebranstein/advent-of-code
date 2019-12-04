using System;
using System.Collections.Generic;
using SecureContainer;

namespace challenge
{
  public class Program
  {
    static void Main(string[] args)
    {
      // Part 1
      // Console.WriteLine("Beginning password search...");
      // var program = new Program();
      // var passwords = program.Run(146810, 612564);
      // Console.WriteLine($"Num possible passwords: {passwords.Count}");
      // foreach (var password in passwords.Passwords)
      // {
      //   Console.WriteLine($" - {password}");
      // }
      // Console.WriteLine("Completed password search...");
      // Console.ReadKey();

      // Part 2
      Console.WriteLine("Beginning password search...");
      var program = new Program();
      var passwords = program.RunPart2(146810, 612564);
      Console.WriteLine($"Num possible passwords: {passwords.Count}");
      foreach (var password in passwords.Passwords)
      {
        Console.WriteLine($" - {password}");
      }
      Console.WriteLine("Completed password search...");
      Console.ReadKey();

    }

    public (int Count, List<int> Passwords) Run(int begin, int end)
    {
      var password = new Password();
      var possiblePasswords =
        password
          .GetPasswordRange(begin, end)
          .FindAll(Password.hasRepeatingDigits)
          .FindAll(Password.hasNonDecreasingDigits);

      return (Count: possiblePasswords.Count, Passwords: possiblePasswords);
    }

    public (int Count, List<int> Passwords) RunPart2(int begin, int end)
    {
      var password = new Password();
      var possiblePasswords =
        password
          .GetPasswordRange(begin, end)
          .FindAll(Password.hasRepeatingDigitsWithoutLargerGroups)
          .FindAll(Password.hasNonDecreasingDigits);

      return (Count: possiblePasswords.Count, Passwords: possiblePasswords);
    }

  }
}
