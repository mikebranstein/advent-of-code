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

    public List<Coordinate> GetDestructionOrder(int originX, int originY)
    {
      // get intersecting
      var asteroids = GetListOfIntersectingAsteroids(originX, originY);

      // get degrees from direction UP, which is tangent of (deltaY / deltaX)
      // but remember our X,Y coordinates need translated
      var degreesLookup = new Dictionary<double, List<Coordinate>>();
      foreach (var asteroid in asteroids)
      {
        // flip all y values wiht a negative
        var deltaX = asteroid.X - originX;
        var deltaY = (asteroid.Y) - (originY);
        var degrees = Math.Tan((double)deltaY / deltaX) * (double)180 / Math.PI;

        if (!degreesLookup.ContainsKey(degrees))
          degreesLookup.Add(degrees, new List<Coordinate>());

        degreesLookup[degrees].Add(new Coordinate(asteroid.X, asteroid.Y));
      }

      var ordering = new List<Coordinate>();
      // get first asteroid to be destroyed
      while (degreesLookup.Any(x => x.Value.Count > 0))
      {
        foreach (var direction in degreesLookup.OrderBy(x => x.Key).ToList())
        {
          var asteroidToDestroy = direction.Value.OrderBy(x => GetDistance(x.X, x.Y, originX, originY)).FirstOrDefault();
          if (asteroidToDestroy != null)
          {
            // deep the closest asteroid from that direction
            ordering.Add(new Coordinate(asteroidToDestroy.X, asteroidToDestroy.Y));

            // remove it from the list of asteroids
            degreesLookup[direction.Key].Remove(new Coordinate(asteroidToDestroy.X, asteroidToDestroy.Y));
          }
        }
      }
      
      return ordering;
    }

    private double GetDistance(int x1, int y1, int x2, int y2)
    {
      var x = (double)Math.Abs(x1 - x2);
      var y = (double)Math.Abs(y1 - y2);

      return Math.Sqrt(Math.Pow(x, 2.0) + Math.Pow(y, 2.0));
    }

    public List<Coordinate> GetListOfIntersectingAsteroids(int originX, int originY)
    {
      var asteroids = new List<Coordinate>();
      asteroids.AddRange(GetListOfIntersectingAsteroids(_quadrant1Points, originX, originY));
      asteroids.AddRange(GetListOfIntersectingAsteroids(_quadrant2Points, originX, originY));
      asteroids.AddRange(GetListOfIntersectingAsteroids(_quadrant3Points, originX, originY));
      asteroids.AddRange(GetListOfIntersectingAsteroids(_quadrant4Points, originX, originY));

      // perform deep copy
      return asteroids.Select(x => new Coordinate(x.X, x.Y)).ToList();
    }

    public List<Coordinate> GetListOfIntersectingAsteroids(Dictionary<Slope, List<Coordinate>> quadrant, int originX, int originY)
    {
      var asteroids = new List<Coordinate>();

      foreach (var slope in quadrant.Keys)
      {
        // deep copy coordinates to adjsut x,y values on the fly
        // due to origin translation
        var lineOfSightCoordinates = new Coordinate[quadrant[slope].Count];
        for (var i = 0; i < lineOfSightCoordinates.Length; i++)
        {
          lineOfSightCoordinates[i] = new Coordinate(quadrant[slope][i].X + originX, quadrant[slope][i].Y + originY);
        }

        asteroids.AddRange(lineOfSightCoordinates.Intersect(_map.ToArray()).ToList());
      }

      return asteroids;
    }

    public Coordinate FindBestViewingLocation()
    {
      Coordinate bestCoordinate = null;
      var bestAsteroidCount = int.MinValue;

      foreach (var coordinate in _map)
      {
        var currentAsteroidCount = NumAsteroidsInDirectSight(coordinate.X, coordinate.Y);
        if (currentAsteroidCount > bestAsteroidCount)
        {
          bestAsteroidCount = currentAsteroidCount;
          bestCoordinate = coordinate; 
        }
      }

      return bestCoordinate;
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
        // deep copy coordinates to adjsut x,y values on the fly
        // due toorigin translation
        var lineOfSightCoordinates = new Coordinate[quadrant[slope].Count];
        for (var i = 0; i < lineOfSightCoordinates.Length; i++)
        {
          lineOfSightCoordinates[i] = new Coordinate(quadrant[slope][i].X + originX, quadrant[slope][i].Y + originY);
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
