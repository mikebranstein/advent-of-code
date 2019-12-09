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
    private Dictionary<int, int> _virtualMemory;

    public Computer()
    {
      _virtualMemory = new Dictionary<int, int>();
    }

    public int Run(List<int> memory, BufferBlock<int> inputBuffer, BufferBlock<int> outputBuffer)
    {
      ExecuteProgram(memory, inputBuffer, outputBuffer);

      // program execution yields result in address 0
      return memory[0];
    }

    public string Run(string inputFileName, BufferBlock<int> inputBuffer, BufferBlock<int> outputBuffer)
    {
      // get code
      var memory = ReadMemoryFromFile(inputFileName);

      Console.WriteLine($"Initial state: {ConvertMemoryToString(memory)}");
      Console.WriteLine("");

      ExecuteProgram(memory, inputBuffer, outputBuffer);
      var memoryString = ConvertMemoryToString(memory);

      return memoryString;
    }


    public List<int> ReadMemoryFromFile(string inputFileName)
    {
      var memory = new List<int>();

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
            memory.Add(int.Parse(address));
          }
        }
      }

      return memory;
    }

    // outputs number of steps it's read
    public void ExecuteProgram(List<int> memory, BufferBlock<int> inputBuffer, BufferBlock<int> outputBuffer)
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

    public string ConvertMemoryToString(List<int> code)
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
