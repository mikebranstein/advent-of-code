using System;
using System.Collections.Generic;

namespace Amplification
{
  public class AmplificationCircuit
  {
    private List<Amplifier> _amplifiers;

    public AmplificationCircuit(int amplifierCount)
    {
      _amplifiers = new List<Amplifier>();
      for (var i = 0; i < amplifierCount; i++)
      {
        _amplifiers.Add(new Amplifier());
      }
    }

    public int Amplify(string inputFileName, List<int> phaseSettings, int primeInputSignal)
    {
      if (phaseSettings.Count != _amplifiers.Count) return 0;

      var inputSignal = primeInputSignal;
      var outputSignal = 0;
      for (var i = 0; i < _amplifiers.Count; i++)
      {
        outputSignal = _amplifiers[i].Amplify(inputFileName, phaseSettings[i], inputSignal);
        inputSignal = outputSignal;
      }

      return outputSignal;
    }
  }
}
