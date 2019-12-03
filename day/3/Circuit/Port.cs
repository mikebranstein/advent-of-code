using System;
namespace CircuitLibrary
{
  public class Port
  {
    private bool _isCentralPort;
    private WireState _wireState;
    public int X { get; set; }
    public int Y { get; set; }

    public Port(int X, int Y)
    {
      this.X = X;
      this.Y = Y;
      _isCentralPort = false;
      _wireState = WireState.None;
    }

    public WireState WireState { get; set; }
    public bool IsCentralPort { get; set; }
  }

  public enum WireState
  {
    None = 0,
    Wire1Present = 1,
    Wire2Present = 2,
    Wire1And2Present = 99
  }
}
