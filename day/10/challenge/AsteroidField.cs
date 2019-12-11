using System;
namespace challenge
{
  public class AsteroidField
  {
    public bool[,] Map { get; private set; }

    public AsteroidField(bool[,] map)
    {
      Map = map;
    }

    public string[] ToStringArray()
    {
      var map = new string[Map.GetLength(1)];
      var line = "";

      for (var h = 0; h < Map.GetLength(1); h++)
      {
        line = "";
        for (var w = 0; w < Map.GetLength(0); w++)
        {
          line += Map[w,h] ? "#" : ".";
        }
        map[h] = line;
      }

      return map;
    }
  }
}
