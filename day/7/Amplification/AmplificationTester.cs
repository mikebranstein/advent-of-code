using System;
using System.Collections.Generic;
using System.Linq;

namespace Amplification
{
  public class AmplificationTester
  {
    public (int maxOutputSetting, List<int> maxPhaseSettings) CalculateMaxOutputSetting(string inputFileName)
    {
      var maxOutputSignal = 0;
      List<int> maxPhaseSettings = null;

      for (var a = 0; a < 5; a++)
      {
        for (var b = 0; b < 5; b++)
        {
          for (var c = 0; c < 5; c++)
          {
            for (var d = 0; d < 5; d++)
            {
              for (var e = 0; e < 5; e++)
              {
                // test for phase strings that repeat numbers
                var phaseSettings = new List<int>() { a, b, c, d, e };
                if (phaseSettings.GroupBy(x => x).Select(x => x.Key).Count() != 5) continue;

                var amplificiationCircuit = new AmplificationCircuit(5);
                var outputSignal = amplificiationCircuit.Amplify(inputFileName, phaseSettings, 0);

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
