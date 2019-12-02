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
      // Part 1
      // Console.WriteLine("Running program.");
      // Console.WriteLine("");
      // 
      // var computer = new Computer();
      // var result = computer.Run("input_1202.txt");
      // 
      // Console.WriteLine($"End state: {result}");
      // Console.WriteLine("");
      // 
      // Console.WriteLine("Finished running program.");
      // Console.ReadLine();

      // Part 2

      var desiredOutput = 19690720;

      for (var noun = 0; noun <= 99; noun++)
      {
        for (var verb = 0; verb <= 99; verb++)
        {
          var computer = new Computer();
          var memory = computer.ReadMemoryFromFile("input.txt");

          // replace noun and verb
          memory[1] = noun;
          memory[2] = verb;

          // run
          var output = computer.Run(memory);

          if (output == desiredOutput)
          {
            Console.WriteLine($"Found desired output of {output}. Noun: {noun}, verb: {verb}");
            Console.WriteLine($"State is {(100 * noun) + verb}");
            break;
          }
        }
      }

    }

  }
}
