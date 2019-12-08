using System;
using System.Collections.Generic;
using System.Linq;

namespace Amplification
{
  public class AmplificationTester
  {
    private CircuitMode _circuitMode;

    public AmplificationTester(CircuitMode circuitMode)
    {
      _circuitMode = circuitMode;
    }

    public (int maxOutputSetting, List<int> maxPhaseSettings) CalculateMaxOutputSetting(string inputFileName)
    {
      var maxOutputSignal = 0;
      List<int> maxPhaseSettings = null;

      var startPhaseSetting = 0;
      if (_circuitMode == CircuitMode.FeedbackLoop) startPhaseSetting = 5;

      for (var a = startPhaseSetting; a < startPhaseSetting + 5; a++)
      {
        for (var b = startPhaseSetting; b < startPhaseSetting + 5; b++)
        {
          for (var c = startPhaseSetting; c < startPhaseSetting + 5; c++)
          {
            for (var d = startPhaseSetting; d < startPhaseSetting + 5; d++)
            {
              for (var e = startPhaseSetting; e < startPhaseSetting + 5; e++)
              {
                // test for phase strings that repeat numbers
                var phaseSettings = new List<int>() { a, b, c, d, e };
                if (phaseSettings.GroupBy(x => x).Select(x => x.Key).Count() != 5) continue;

                var amplificiationCircuit = new AmplificationCircuit(inputFileName, phaseSettings, _circuitMode);
                var outputSignal = amplificiationCircuit.Amplify(0);

                if (outputSignal > maxOutputSignal)
                {
                  maxOutputSignal = outputSignal;
                  maxPhaseSettings = phaseSettings;
                }
              }
            }
          }
        }
      }

      return (maxOutputSetting: maxOutputSignal, maxPhaseSettings: maxPhaseSettings);
    }
  }
}
