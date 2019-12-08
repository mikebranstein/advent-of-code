using System;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using IntcodeComputer;

namespace Amplification
{
  public class Amplifier
  {
    private List<int> _memory;
    public BufferBlock<int> InputBuffer { get; private set; }
    public BufferBlock<int> OutputBuffer { get; private set; }
    private Computer _computer;

    public Amplifier(string inputFileName, int phaseSetting, BufferBlock<int> inputBuffer, BufferBlock<int> outputBuffer)
    {
      _computer = new Computer();
      InputBuffer = inputBuffer;
      OutputBuffer = outputBuffer;

      // initialize memory and phase setting
      _memory = _computer.ReadMemoryFromFile(inputFileName);
      InputBuffer.SendAsync(phaseSetting);
    }

    public void Amplify(int inputSignal)
    {
      // input 1 is the phase setting
      // input 2 is the input signal
      InputBuffer.SendAsync(inputSignal);

      // execute program to amplify signal
      _computer.ExecuteProgram(_memory, InputBuffer, OutputBuffer);

      // output is amplified signal
      //return _outputBuffer.Receive();
    }

    public void Amplify()
    {
      // use to kickoff the computer for amplifiers that have a connected input
      // and do not need primed with an input
      _computer.ExecuteProgram(_memory, InputBuffer, OutputBuffer);
    }


  }
}
