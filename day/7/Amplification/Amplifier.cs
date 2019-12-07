using System;
using System.Collections.Generic;
using IntcodeComputer;

namespace Amplification
{
  public class Amplifier
  {
    private Queue<int> _inputBuffer;
    private Queue<int> _outputBuffer;
    private Computer _computer;

    public Amplifier()
    {
      _computer = new Computer();
      _inputBuffer = new Queue<int>();
      _outputBuffer = new Queue<int>();
    }

    public int Amplify(string inputFileName, int phaseSetting, int inputSignal)
    {
      var memory = _computer.ReadMemoryFromFile(inputFileName);

      // input 1 is the phase setting
      // input 2 is the input signal
      _inputBuffer.Clear();
      _inputBuffer.Enqueue(phaseSetting);
      _inputBuffer.Enqueue(inputSignal);

      _outputBuffer.Clear();

      // execute program to amplify signal
      _computer.ExecuteProgram(memory, _inputBuffer, _outputBuffer);

      // output is amplified signal
      return _outputBuffer.Dequeue();
    }

    
  }
}
