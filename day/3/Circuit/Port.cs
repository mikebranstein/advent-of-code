using System;
namespace CircuitLibrary
{
  public class Port
  {
    public int X { get; set; }
    public int Y { get; set; }

    public Port(int X, int Y)
    {
      this.X = X;
      this.Y = Y;
      IsCentralPort = false;
      WireState = new WireState() { State = State.None, Wire2StepCount = 0, Wire1StepCount = 0 };
    }

    public WireState WireState { get; set; }
    public bool IsCentralPort { get; set; }
  }

  public enum State
  {
    None = 0,
    Wire1Present = 1,
    Wire2Present = 2,
    Wire1And2Present = 99
  }
}
