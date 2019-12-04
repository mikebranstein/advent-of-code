using System;
namespace CircuitLibrary
{
  public class WireState
  {
    public State State { get; set; }
    public int Wire1StepCount { get; set; }
    public int Wire2StepCount { get; set; }

    public WireState()
    {
      Wire1StepCount = 0;
      Wire2StepCount = 0;
      State = State.None;
    }
  }
}
