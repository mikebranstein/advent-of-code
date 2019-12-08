using System;
using System.Collections.Generic;

namespace Amplification
{
  public class AmplificationCircuit
  {
    private List<Amplifier> _amplifiers;
    private CircuitMode _circuitMode;

    public AmplificationCircuit(string inputFileName, List<int> phaseSettings, CircuitMode circuitMode)
    {
      _amplifiers = new List<Amplifier>();
      _circuitMode = circuitMode;
      for (var i = 0; i < phaseSettings.Count; i++)
      {
        _amplifiers.Add(new Amplifier(inputFileName, phaseSettings[i]));
      }
    }

    public int Amplify(int primeInputSignal)
    {
      int inputSignal;
      var outputSignal = primeInputSignal;
      do
      {
        inputSignal = outputSignal;
        outputSignal = AmplifyOnce(inputSignal);
      } while (_circuitMode == CircuitMode.FeedbackLoop && inputSignal != outputSignal);

      return outputSignal;
    }

    private int AmplifyOnce(int primeInputSignal)
    {
      var inputSignal = primeInputSignal;
      var outputSignal = 0;

      for (var i = 0; i < _amplifiers.Count; i++)
      {
        outputSignal = _amplifiers[i].Amplify(inputSignal);
        inputSignal = outputSignal;
      }

      return outputSignal;
    }
  }

  public enum CircuitMode
  {
    Normal = 0,
    FeedbackLoop = 1
  }
}
