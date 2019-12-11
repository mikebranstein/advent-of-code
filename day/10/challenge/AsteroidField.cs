using System;
using System.Collections.Generic;
using System.Linq;

namespace challenge
{
  public class AsteroidField
  {
    public int Width { get; set; }
    public int Height { get; set; }

    public List<Coordinate> _map;
    public List<Slope> _slopes;
    public List<Coordinate> _coordinates;

    public Dictionary<Slope, List<Coordinate>> _quadrant1Points;
    public Dictionary<Slope, List<Coordinate>> _quadrant2Points;
    public Dictionary<Slope, List<Coordinate>> _quadrant3Points;
    public Dictionary<Slope, List<Coordinate>> _quadrant4Points;

    public AsteroidField(List<Coordinate> map, int width, int height)
    {
      _map = map;
      Width = width;
      Height = height;
      _quadrant1Points = new Dictionary<Slope, List<Coordinate>>();
      _quadrant2Points = new Dictionary<Slope, List<Coordinate>>();
      _quadrant3Points = new Dictionary<Slope, List<Coordinate>>();
      _quadrant4Points = new Dictionary<Slope, List<Coordinate>>();

      InitializeSlopes();
      GeneratePossibleAsteroidPoints();
    }

    public int NumAsteroidsInDirectSight(int originX, int originY)
    {
      var q1Count = NumAsteroidsInDirectSight(_quadrant1Points, originX, originY);
      var q2Count = NumAsteroidsInDirectSight(_quadrant2Points, originX, originY);
      var q3Count = NumAsteroidsInDirectSight(_quadrant3Points, originX, originY);
      var q4Count = NumAsteroidsInDirectSight(_quadrant4Points, originX, originY);

      var asteroidCount = q1Count + q2Count + q3Count + q4Count;
      return asteroidCount;
    }

    private int NumAsteroidsInDirectSight(Dictionary<Slope, List<Coordinate>> quadrant, int originX, int originY)
    {
      var asteroidCount = 0;

      foreach (var slope in quadrant.Keys)
      {
        var lineOfSightCoordinates = quadrant[slope].ToArray();
        for (var i = 0; i < lineOfSightCoordinates.Length; i++)
        {
          lineOfSightCoordinates[i].X += originX;
          lineOfSightCoordinates[i].Y += originY;
        }

        if (lineOfSightCoordinates.Intersect(_map.ToArray()).Any()) asteroidCount++;
      }
      return asteroidCount;
    }

    private void InitializeSlopes()
    {
      _slopes = new List<Slope>();

      // this generates slopes from 0,0
      for (var h = 0; h < Height; h++)
      {
        for (var w = 0; w < Width; w++)
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

        for (int x = 0, y = 0;
          x < Width && y < Height;
          x += slope.Run, y += slope.Rise)
        {
          if (x == 0 && y == 0) continue;

          if (y == 0)
          {
            AddCoordinate(_quadrant1Points, slope, x, y);
            AddCoordinate(_quadrant4Points, slope, -x, y);
          }
          else if (x == 0)
          {
            AddCoordinate(_quadrant1Points, slope, x, y);
            AddCoordinate(_quadrant2Points, slope, x, -y);
          }
          else
          {
            AddCoordinate(_quadrant1Points, slope, x, y);
            AddCoordinate(_quadrant2Points, slope, x, -y);
            AddCoordinate(_quadrant3Points, slope, -x, -y);
            AddCoordinate(_quadrant4Points, slope, -x, y);
          }
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
      var map = new string[Height];
      var line = "";

      for (var h = 0; h < Height; h++)
      {
        line = "";
        for (var w = 0; w < Width; w++)
        {
          line += _map.Contains(new Coordinate(w,h)) ? "#" : ".";
        }
        map[h] = line;
      }

      return map;
    }
  }


}
