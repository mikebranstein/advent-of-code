using System;
using System.Collections.Generic;
using System.IO;

namespace challenge
{
  public class Program
  {

    static void Main(string[] args)
    {
      Console.WriteLine("Running program.");
      Console.WriteLine("");

      var program = new Program();
      var result = program.Run("input.txt");

      Console.WriteLine($"End state: {result}");
      Console.WriteLine("");


      Console.WriteLine("Finished running program.");
      Console.ReadLine();
    }

    public int Run(List<int> memory)
    {
      var memoryResult = ExecuteProgram(memory);

      // program execution yields result in address 0
      return memoryResult[0];
    }

    public string Run(string inputFileName)
    {
      // get code
      var memory = ReadMemoryFromFile(inputFileName);

      Console.WriteLine($"Initial state: {ConvertMemoryToString(memory)}");
      Console.WriteLine("");

      var memoryResult = ExecuteProgram(memory);
      var memoryString = ConvertMemoryToString(memoryResult);

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
    public List<int> ExecuteProgram(List<int> inputMemory)
    {
      var memory = inputMemory;
      int stepNumber = 0;

      for(var index = 0; index <= memory.Count; index += 4)
      {
        var operation = ParseOperation(memory, index);
        if (operation.Opcode == 1) memory = ProcessOperation1(memory, operation);
        else if (operation.Opcode == 2) memory = ProcessOperation2(memory, operation);
        else if (operation.Opcode == 99) break; // 99 menas end of program

        stepNumber++;
      }

      return memory;
    }

    public Instruction ParseOperation(List<int> memory, int instructionPointer)
    {
      var operation = new Instruction() { Opcode = memory[instructionPointer] };

      // operation 99 is end of program
      if (operation.Opcode == 99) return operation;

      // other operations have more data
      operation.Parameter1 = memory[instructionPointer + 1];
      operation.Parameter2 = memory[instructionPointer + 2];
      operation.Parameter3 = memory[instructionPointer + 3];

      return operation;
    }

    public List<int> ProcessOperation1(List<int> code, Instruction operation)
    {
      var valueOfAddress1 = code[operation.Parameter1.Value];
      var valueOfAddress2 = code[operation.Parameter2.Value];

      // addition
      var result = valueOfAddress1 + valueOfAddress2;

      // update code
      code[operation.Parameter3.Value] = result;

      return code;
    }

    public List<int> ProcessOperation2(List<int> code, Instruction operation)
    {
      var valueOfAddress1 = code[operation.Parameter1.Value];
      var valueOfAddress2 = code[operation.Parameter2.Value];

      // addition
      var result = valueOfAddress1 * valueOfAddress2;

      // update code
      code[operation.Parameter3.Value] = result;

      return code;
    }

    public string ConvertMemoryToString(List<int> code)
    {
      var codeString = "";

      foreach(var item in code)
      {
        codeString += item.ToString();
        codeString += ",";
      }

      // strip last character off end - ,
      return codeString.Remove(codeString.Length - 1);
    }
  }

  public class Instruction
  {
    public int Opcode { get; set; }
    public int? Parameter1 { get; set; }
    public int? Parameter2 { get; set; }
    public int? Parameter3 { get; set; }
  }
}
