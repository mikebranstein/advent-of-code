using System;
using System.Collections.Generic;
using System.IO;
using CircuitLibrary;

namespace challenge
{
  public class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Distance calculation beginning.");
      var program = new Program();
      var shortestDistance = program.Run("input.txt");

      Console.WriteLine($"Shortest distance: {shortestDistance}");
      Console.WriteLine("Finished distance calculation.");

      Console.ReadKey();
    }


    public int Run(string inputFileName)
    {
      var wirePaths = ReadWirePaths(inputFileName);

      var wire1Path = wirePaths[0];
      var wire1 = new Wire(1, wire1Path);

      var wire2Path = wirePaths[1];
      var wire2 = new Wire(2, wire2Path);

      var circuit = new Circuit(new List<Wire>() { wire1, wire2 });

      return circuit.GetClosestIntersectionPortDistance();
    }

    public List<string> ReadWirePaths(string inputFileName)
    {
      var wirePaths = new List<string>();

      var fileStream = new FileStream(inputFileName, FileMode.Open);
      using (var streamReader = new StreamReader(fileStream))
      {
        while (!streamReader.EndOfStream)
        {
          var line = streamReader.ReadLine();
          wirePaths.Add(line);
        }
      }

      return wirePaths;
    }

  }
}
