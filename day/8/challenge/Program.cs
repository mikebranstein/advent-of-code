using System;
using SpaceImageFormat;

namespace challenge
{
  class Program
  {
    static void Main(string[] args)
    {
      // part 1
      Console.WriteLine("Finding layer with fewest zeros, then # of 1's digits multiplied by # of 2's digits...");

      var spaceImage = SpaceImageFactory.Create("input.txt", 25, 6);
      var multiple = spaceImage.GetZeroLayerDigitMuliple(1, 2);
      Console.WriteLine($"Multiple is: {multiple}");

      // part 2
      Console.WriteLine("Printing merged image...");
      var mergedImage = spaceImage.MergeLayers();
      for (var h = 0; h < mergedImage.GetLength(1); h++)
      {
        for (var w = 0; w <mergedImage.GetLength(0); w++)
        {
          var charToOutput = mergedImage[w, h] == 0 ? " " : "#"; 
          Console.Write($"{charToOutput}");
        }

        Console.Write("\n");
      }

      Console.WriteLine("");
      Console.WriteLine("Finished.");



      Console.ReadLine();
    }
  }
}
