using System;
using System.Collections.Generic;
using System.IO;

namespace challenge
{
  public static class AsteroidFieldFactory
  {

    public static AsteroidField CreateFromFile(string inputFileName)
    {
      var width = 0;
      var height = 0;
      var map = new List<Coordinate>();

      var fileStream = new FileStream(inputFileName, FileMode.Open);
      using (var streamReader = new StreamReader(fileStream))
      {
        while (!streamReader.EndOfStream)
        {
          var mapLine = streamReader.ReadLine();
          width = mapLine.Length;

          for (var i = 0; i < width; i++)
          {
            if (mapLine[i] == '#')
              map.Add(new Coordinate(i, height));
          }

          height++;
        }

        return new AsteroidField(map, width, height);
      }
    }
  }
}
