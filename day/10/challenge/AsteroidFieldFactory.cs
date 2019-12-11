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
      var mapList = new List<bool[]>();

      var fileStream = new FileStream(inputFileName, FileMode.Open);
      using (var streamReader = new StreamReader(fileStream))
      {
        while (!streamReader.EndOfStream)
        {
          var mapLine = streamReader.ReadLine();
          width = mapLine.Length;

          var line = new bool[width];
          for (var i = 0; i < width; i++)
          {
            line[i] = mapLine[i] == '#';
          }

          mapList.Add(line);
          height++;
        }

        var map = new bool[width, height];
        for (var h = 0; h < height; h++)
        {
          for (var w = 0; w < width; w++)
          {
            map[w, h] = mapList[h][w];
          }
        }

        return new AsteroidField(map);
      }
    }
  }
}
