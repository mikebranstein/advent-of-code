using System;
using System.Collections.Generic;
using System.IO;

namespace IntcodeComputer
{
    public class Computer
    {
    public int Run(List<int> memory, Queue<int> inputBuffer)
    {
      ExecuteProgram(memory, inputBuffer);

      // program execution yields result in address 0
      return memory[0];
    }

    public string Run(string inputFileName, Queue<int> inputBuffer)
    {
      // get code
      var memory = ReadMemoryFromFile(inputFileName);

      Console.WriteLine($"Initial state: {ConvertMemoryToString(memory)}");
      Console.WriteLine("");

      ExecuteProgram(memory, inputBuffer);
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
    public void ExecuteProgram(List<int> memory, Queue<int> inputBuffer)
    {
      int instructionPointer = 0;
      IInstruction instruction = null;
      do
      {
        instruction = InstructionFactory.ParseInstruction(memory, instructionPointer);
        var instructionPointerIncrement = instruction.Execute(memory, inputBuffer);
        
        instructionPointer += instructionPointerIncrement;
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
