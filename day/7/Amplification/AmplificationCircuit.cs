using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

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
        // first
        if (i == 0)
          _amplifiers.Add(new Amplifier(inputFileName, phaseSettings[i], new BufferBlock<int>(), new BufferBlock<int>()));

        // last - output buffer is the first's input buffer
        else if (i == phaseSettings.Count - 1)
          _amplifiers.Add(new Amplifier(inputFileName, phaseSettings[i], _amplifiers[i - 1].OutputBuffer, _amplifiers[0].InputBuffer));

        // others
        else
          _amplifiers.Add(new Amplifier(inputFileName, phaseSettings[i], _amplifiers[i - 1].OutputBuffer, new BufferBlock<int>()));
      }
    }

    //public int Amplify(int primeInputSignal)
    //{
    //  int inputSignal;
    //  var outputSignal = primeInputSignal;
    //  do
    //  {
    //    inputSignal = outputSignal;
    //    outputSignal = AmplifyOnce(inputSignal);
    //  } while (_circuitMode == CircuitMode.FeedbackLoop && inputSignal != outputSignal);
    //
    //  return outputSignal;
    //}

    public int Amplify(int inputSignal)
    {
      var tasks = new List<Task>();
      for (var i = 0; i < _amplifiers.Count; i++)
      {
        // only first task needs primed with an additional input
        if (i == 0)
          tasks.Add(AmplifyAsync(i, inputSignal));
        else
          tasks.Add(AmplifyAsync(i));
      }

      Task.WaitAll(tasks.ToArray());

      var outputSignal = _amplifiers[_amplifiers.Count - 1].OutputBuffer.Receive();
      return outputSignal;
    }

    private Task AmplifyAsync(int amplifierIndex, int inputSignal)
    {
      return Task.Run(() => _amplifiers[amplifierIndex].Amplify(inputSignal));
    }

    private Task AmplifyAsync(int amplifierIndex)
    {
      return Task.Run(() => _amplifiers[amplifierIndex].Amplify());
    }

  }

  public enum CircuitMode
  {
    Normal = 0,
    FeedbackLoop = 1
  }
}
