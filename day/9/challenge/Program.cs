using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using IntcodeComputer;

namespace challenge
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Running BOOST Test Mode program...");

      // The BOOST program will ask for a single input; run it in test
      // mode by providing it the value 1. It will perform a series of
      // checks on each opcode, output any opcodes (and the associated
      // parameter modes) that seem to be functioning incorrectly, and
      // finally output a BOOST keycode.

      // Once your Intcode computer is fully functional, the BOOST program
      // should report no malfunctioning opcodes when run in test mode; it
      // should only output a single value, the BOOST keycode.

      var computer = new Computer();
      var memory = computer.ReadMemoryFromFile("input.txt");
      var inputBuffer = new BufferBlock<long>();
      var outputBuffer = new BufferBlock<long>();

      Task.Run(() => inputBuffer.SendAsync(1)).Wait();

      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      var outputBufferCount = outputBuffer.Count;
      for (var i = 0; i < outputBufferCount; i++)
      {
        Console.WriteLine($"BOOST Test Mode output: {outputBuffer.Receive()}");
      }

      Console.WriteLine("BOOST Test Mode program finished...");



      // Part 2
      // Finally, you can lock on to the Ceres distress signal! You just need
      // to boost your sensors using the BOOST program.
      //
      // The program runs in sensor boost mode by providing the input
      // instruction the value 2.Once run, it will boost the sensors
      // automatically, but it might take a few seconds to complete the
      // operation on slower hardware.In sensor boost mode, the program
      // will output a single value: the coordinates of the distress signal.

      Console.WriteLine("Running BOOST Sensor Boost Mode program...");

      computer = new Computer();
      memory = computer.ReadMemoryFromFile("input.txt");
      inputBuffer = new BufferBlock<long>();
      outputBuffer = new BufferBlock<long>();

      Task.Run(() => inputBuffer.SendAsync(2)).Wait();

      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      outputBufferCount = outputBuffer.Count;
      for (var i = 0; i < outputBufferCount; i++)
      {
        Console.WriteLine($"BOOST Sensor Boost Mode output: {outputBuffer.Receive()}");
      }

      Console.WriteLine("BOOST Sensor Boost Mode program finished...");

      Console.ReadLine();
    }
  }
}
