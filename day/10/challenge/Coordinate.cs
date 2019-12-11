using System;
namespace challenge
{
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
