using System;
using Amplification;

namespace challenge
{
  class Program
  {
    static void Main(string[] args)
    {
      // Part 1
      Console.WriteLine("Beginning max thruster output calculation...");

      var amplificationTester = new AmplificationTester();
      var output = amplificationTester.CalculateMaxOutputSetting("input.txt");

      Console.WriteLine($"Max thruster output: {output.maxOutputSetting}, phase settings: {output.maxPhaseSettings[0]}, {output.maxPhaseSettings[1]}, {output.maxPhaseSettings[2]}, {output.maxPhaseSettings[3]}, {output.maxPhaseSettings[4]}");

      Console.WriteLine("Finished max thruster output calculation...");

      Console.ReadKey();
    }
  }


}
