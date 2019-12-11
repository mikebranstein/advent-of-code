using System;

namespace challenge
{
  class Program
  {
    static void Main(string[] args)
    {
      // part 1
      Console.WriteLine("Finding best viewing location in asteroid field...");

      var inputFileName = "input.txt";

      var asteroidField = AsteroidFieldFactory.CreateFromFile(inputFileName);
      var coordinate = asteroidField.FindBestViewingLocation();
      var asteroidCount = asteroidField.NumAsteroidsInDirectSight(coordinate.X, coordinate.Y);

      Console.WriteLine($"Best viewing location: ({coordinate.X}, {coordinate.Y}), asteroid count: {asteroidCount}");

      var asteroid200Calculation = asteroidField.GetPart2Answer();
      Console.WriteLine($"200th asteroid to be destroyed: {asteroid200Calculation}");

      Console.WriteLine("Finished.");

      Console.ReadLine();
    }
  }
}
