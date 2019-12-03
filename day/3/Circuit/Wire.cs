using System;
using System.Collections.Generic;

namespace CircuitLibrary
{
  public class Wire
  {
    public int Number { get; set; }
    public List<WireVector> Path { get; set; }

    public Wire(int number, string path)
    {
      Number = number;
      Path = ParseWirePath(path);
    }

    private List<WireVector> ParseWirePath(string path)
    {
      var vectorList = new List<WireVector>();

      foreach (var vectorString in path.Split(","))
      {
        var vector = new WireVector();

        // set direction
        var direction = vectorString.ToCharArray()[0];
        if (direction == 'U') vector.Direction = Direction.Up;
        else if (direction == 'D') vector.Direction = Direction.Down;
        else if (direction == 'R') vector.Direction = Direction.Right;
        else if (direction == 'L') vector.Direction = Direction.Left;

        // set magnitude
        var magnitude = int.Parse(vectorString.Substring(1));
        vector.Magnitude = magnitude;

        vectorList.Add(vector);
      }

      return vectorList;
    }

  }

  public class WireVector
  {
    public int Magnitude { get; set; }
    public Direction Direction { get; set; }
  }

  public enum Direction
  {
    None = 0,
    Up = 1,
    Down = 2,
    Left = 3,
    Right = 4
  }

}
