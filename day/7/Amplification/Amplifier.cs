using System;
using System.Collections.Generic;
using IntcodeComputer;

namespace Amplification
{
  public class Amplifier
  {
    private List<int> _memory;
    private Queue<int> _inputBuffer;
    private Queue<int> _outputBuffer;
    private Computer _computer;

    public Amplifier(string inputFileName, int phaseSetting)
    {
      _computer = new Computer();
      _inputBuffer = new Queue<int>();
      _outputBuffer = new Queue<int>();

      // initialize memory and phase setting
      _memory = _computer.ReadMemoryFromFile(inputFileName);
      _inputBuffer.Enqueue(phaseSetting);
    }

    public int Amplify(int inputSignal)
    {
      // input 1 is the phase setting
      // input 2 is the input signal
      _inputBuffer.Enqueue(inputSignal);

      // execute program to amplify signal
      _computer.ExecuteProgram(_memory, _inputBuffer, _outputBuffer);

      // output is amplified signal
      return _outputBuffer.Dequeue();
    }

    
  }
}
