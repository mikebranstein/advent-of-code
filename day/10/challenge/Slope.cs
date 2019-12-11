using System;
namespace challenge
{
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
}
