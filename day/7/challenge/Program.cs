﻿using System;
using Amplification;

namespace challenge
{
  class Program
  {
    static void Main(string[] args)
    {
      // Part 1
      Console.WriteLine("Beginning max thruster output calculation...");

      var amplificationTester = new AmplificationTester(CircuitMode.Normal);
      var output = amplificationTester.CalculateMaxOutputSetting("input.txt");

      Console.WriteLine($"Max thruster output (normal mode): {output.maxOutputSetting}, phase settings: {output.maxPhaseSettings[0]}, {output.maxPhaseSettings[1]}, {output.maxPhaseSettings[2]}, {output.maxPhaseSettings[3]}, {output.maxPhaseSettings[4]}");

      amplificationTester = new AmplificationTester(CircuitMode.FeedbackLoop);
      output = amplificationTester.CalculateMaxOutputSetting("input.txt");

      Console.WriteLine($"Max thruster output (feedback mode): {output.maxOutputSetting}, phase settings: {output.maxPhaseSettings[0]}, {output.maxPhaseSettings[1]}, {output.maxPhaseSettings[2]}, {output.maxPhaseSettings[3]}, {output.maxPhaseSettings[4]}");

      Console.WriteLine("Finished max thruster output calculation...");

      Console.ReadKey();
    }
  }


}
