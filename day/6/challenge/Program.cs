using System;
using OrbitMap;

namespace challenge
{
  class Program
  {
    static void Main(string[] args)
    {
      // Part 1
      Console.WriteLine("Beginning map creation...");

      var map = MapFactory.Create("input.txt");
      Console.WriteLine($"Direct and indirect orbit count: {map.GetObjectDistances()}");

      Console.WriteLine("Completed map creation...");

      // Part 2 - count the orbital transfers from YOU to SAN
      // We do this by finding the closest common parent (CCP)
      // then the calculation is (dist(SAN) - dist(CCP) - 1) + (dist(YOU) - dist(CCP) - 1)
      var transferCount = map.GetOrbitalTransferCount("YOU", "SAN");
      Console.WriteLine($"Transfer count for YOU to get to SAN orbit: {transferCount}");

      Console.ReadKey();

    }
  }
}
