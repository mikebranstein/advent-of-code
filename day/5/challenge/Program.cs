using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using IntcodeComputer;

namespace challenge
{
  class Program
  {
    static void Main(string[] args)
    {

      // Part 1
      //Console.WriteLine("Beginning diagnostics run...");
      //var program = new Program();
      //
      //var computer = new Computer();
      //var memory = computer.ReadMemoryFromFile("input.txt");
      //
      //var inputBuffer = new BufferBlock<int>();
      //inputBuffer.Enqueue(1);
      //
      //var outputBuffer = new BufferBlock<int>();
      //
      //computer.ExecuteProgram(memory, inputBuffer, outputBuffer);
      //
      //// all outputs should be 0 except for the final output
      //var numOutputs = outputBuffer.Count;
      //for (var x = 0; x < numOutputs; x++)
      //{
      //  var diagnosticCodeOutput = outputBuffer.Dequeue();
      //  Console.WriteLine($"Diagnostic code: {diagnosticCodeOutput}");
      //
      //  if (x != numOutputs - 1 && diagnosticCodeOutput != 0)
      //    Console.WriteLine("ERROR");
      //}
      //Console.WriteLine("Completed diagnostics run...");
      //Console.ReadKey();

      // Part 2
      Console.WriteLine("Beginning diagnostics run...");
      var program = new Program();

      var computer = new Computer();
      var memory = computer.ReadMemoryFromFile("input.txt");

      var inputBuffer = new BufferBlock<int>();
      Task.Run(() => inputBuffer.SendAsync(5)).Wait();

      var outputBuffer = new BufferBlock<int>();

      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // all outputs should be 0 except for the final output
      var numOutputs = outputBuffer.Count;
      for (var x = 0; x < numOutputs; x++)
      {
        var diagnosticCodeOutput = outputBuffer.Receive();
        Console.WriteLine($"Diagnostic code: {diagnosticCodeOutput}");

        if (x != numOutputs - 1 && diagnosticCodeOutput != 0)
          Console.WriteLine("ERROR");
      }
      Console.WriteLine("Completed diagnostics run...");
      Console.ReadKey();
    }

   
  }
}
