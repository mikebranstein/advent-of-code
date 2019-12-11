using System;
using System.Collections.Generic;

namespace challenge
{
  public class AsteroidField
  {
    public bool[,] Map { get; private set; }
    public List<Slope> _slopes;
    public List<Coordinate> _coordinates;

    public Dictionary<Slope, List<Coordinate>> _quadrant1Points;
    public Dictionary<Slope, List<Coordinate>> _quadrant2Points;
    public Dictionary<Slope, List<Coordinate>> _quadrant3Points;
    public Dictionary<Slope, List<Coordinate>> _quadrant4Points;

    public AsteroidField(bool[,] map)
    {
      Map = map;
      _quadrant1Points = new Dictionary<Slope, List<Coordinate>>();
      _quadrant2Points = new Dictionary<Slope, List<Coordinate>>();
      _quadrant3Points = new Dictionary<Slope, List<Coordinate>>();
      _quadrant4Points = new Dictionary<Slope, List<Coordinate>>();

      InitializeSlopes();
      GeneratePossibleAsteroidPoints();
    }

    private void InitializeSlopes()
    {
      _slopes = new List<Slope>();

      // this generates slopes from 0,0
      for (var h = 0; h < Map.GetLength(1); h++)
      {
        for (var w = 0; w < Map.GetLength(0); w++)
        {
          if (h == 0 && w == 0) continue;

          // slope is rise over run
          // rise is h
          // run is w
          var slope = new Slope(h, w);
          if (!_slopes.Contains(slope))
            _slopes.Add(slope);
        }
      }
    }

    private void GeneratePossibleAsteroidPoints()
    {
      foreach (var slope in _slopes)
      {
        _quadrant1Points.Add(slope, new List<Coordinate>());
        _quadrant2Points.Add(slope, new List<Coordinate>());
        _quadrant3Points.Add(slope, new List<Coordinate>());
        _quadrant4Points.Add(slope, new List<Coordinate>());

        for (var h = 0; h < Map.GetLength(1); h += slope.Rise)
        {
          // no slope check
          if (slope.Run == 0)
          {
            AddCoordinate(_quadrant1Points, slope, 0, h);
            AddCoordinate(_quadrant2Points, slope, 0, -h);
          }
          else
          {
            for (var w = 0; w < Map.GetLength(0); w += slope.Run)
            {
              if (h == 0 && w == 0) continue;

              AddCoordinate(_quadrant1Points, slope, w, h);
              AddCoordinate(_quadrant2Points, slope, w, -h);
              AddCoordinate(_quadrant3Points, slope, -w, -h);
              AddCoordinate(_quadrant4Points, slope, -w, h);
            }
          }

          if (slope.Rise == 0) break;
        }
      }
    }

    private void AddCoordinate(Dictionary<Slope, List<Coordinate>> quadrantPoints, Slope slope, int x, int y)
    {
      if (x == 0 && y == 0) return;

      var newCoordinate = new Coordinate(x, y);
      if (!quadrantPoints[slope].Contains(newCoordinate))
        quadrantPoints[slope].Add(newCoordinate);
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

  public class Slope
  {
    public int Rise { get; set; }
    public int Run { get; set; }

    public Slope(int rise, int run)
    {
      Rise = rise;
      Run = run;
    }

    public bool Equals(Slope other)
    {
      if (other is null)
        return false;

      var thisHasNoSlope = Run == 0;
      var otherHasNoSlope = other.Run == 0;
      if (thisHasNoSlope && otherHasNoSlope) return true;
      if (thisHasNoSlope || otherHasNoSlope) return false;

      var thisSlope = (double)Rise / (double)(Run == 0 ? 1 : Run);
      var otherSlope = (double)other.Rise / (double)(other.Run == 0 ? 1 : other.Run);

      return thisSlope == otherSlope;
    }

    public override bool Equals(object obj) => Equals(obj as Slope);
    public override int GetHashCode() => (Rise, Run).GetHashCode();
  }

  public class Coordinate
  {
    public int X { get; set; }
    public int Y { get; set; }

    public Coordinate(int x, int y)
    {
      X = x;
      Y = y;
    }

    public bool Equals(Coordinate other)
    {
      if (other is null)
        return false;

      return this.X == other.X && this.Y == other.Y;
    }

    public override bool Equals(object obj) => Equals(obj as Coordinate);
    public override int GetHashCode() => (X, Y).GetHashCode();
  }
}
