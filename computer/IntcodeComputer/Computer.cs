using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace IntcodeComputer
{
  public class Computer
  {
    public CancellationToken CancellationToken => _cancellationTokenSource.Token;

    private Dictionary<int, long> _virtualMemory;
    private CancellationTokenSource _cancellationTokenSource;

    public Computer()
    {
      _virtualMemory = new Dictionary<int, long>();
      _cancellationTokenSource = new CancellationTokenSource();
    }

    public long Run(List<long> memory, BufferBlock<long> inputBuffer, BufferBlock<long> outputBuffer)
    {
      ExecuteProgram(memory, inputBuffer, outputBuffer);

      // program execution yields result in address 0
      return memory[0];
    }

    public string Run(string inputFileName, BufferBlock<long> inputBuffer, BufferBlock<long> outputBuffer)
    {
      // get code
      var memory = ReadMemoryFromFile(inputFileName);

      Console.WriteLine($"Initial state: {ConvertMemoryToString(memory)}");
      Console.WriteLine("");

      ExecuteProgram(memory, inputBuffer, outputBuffer);
      var memoryString = ConvertMemoryToString(memory);

      return memoryString;
    }


    public List<long> ReadMemoryFromFile(string inputFileName)
    {
      var memory = new List<long>();

      var fileStream = new FileStream(inputFileName, FileMode.Open);
      using (var streamReader = new StreamReader(fileStream))
      {
        if (!streamReader.EndOfStream)
        {
          var line = streamReader.ReadLine();

          var memoryArray = line.Split(",");
          foreach (var address in memoryArray)
          {
            // convert to integer and add to list
            memory.Add(long.Parse(address));
          }
        }
      }

      return memory;
    }

    // outputs number of steps it's read
    public void ExecuteProgram(List<long> memory, BufferBlock<long> inputBuffer, BufferBlock<long> outputBuffer)
    {
      int instructionPointer = 0;
      int relativeBase = 0;
      IInstruction instruction = null;
      do
      {
        instruction = InstructionFactory.ParseInstruction(memory, instructionPointer);
        instruction.Execute(memory, _virtualMemory, ref instructionPointer, ref relativeBase, inputBuffer, outputBuffer);
      }
      while (instruction.GetType() != typeof(HaltInstruction));
    }

    public IInstruction ExecuteProgramUntilOutput(List<long> memory, BufferBlock<long> inputBuffer, BufferBlock<long> outputBuffer, ref int instructionPointer, ref int relativeBase)
    {
      IInstruction instruction = null;
      do
      {
        instruction = InstructionFactory.ParseInstruction(memory, instructionPointer);
        instruction.Execute(memory, _virtualMemory, ref instructionPointer, ref relativeBase, inputBuffer, outputBuffer);
      }
      while (instruction.GetType() != typeof(HaltInstruction) && instruction.GetType() != typeof(OutputInstruction));
      return instruction;
    }

    public string ConvertMemoryToString(List<long> code)
    {
      var codeString = "";

      foreach (var item in code)
      {
        codeString += item.ToString();
        codeString += ",";
      }

      // strip last character off end - ,
      return codeString.Remove(codeString.Length - 1);
    }
  }
}
