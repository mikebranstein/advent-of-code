using System;
using System.Collections.Generic;
using System.IO;
using IntcodeComputer;

namespace challenge
{
  public class Program
  {

    static void Main(string[] args)
    {
      Console.WriteLine("Running program.");
      Console.WriteLine("");

      var computer = new Computer();
      var result = computer.Run("input.txt");

      Console.WriteLine($"End state: {result}");
      Console.WriteLine("");

      Console.WriteLine("Finished running program.");
      Console.ReadLine();
    }

  }
}
