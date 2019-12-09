using System;
namespace IntcodeComputer
{
  public class Parameter
  {
    public int Value { get; set; }
    public ParameterMode Mode { get; set; }
  }

  public enum ParameterMode
  {
    PositionMode = 0,
    ImmediateMode = 1,
    Relative = 2
  }
}
