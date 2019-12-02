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

    public string Run(string inputFileName)
    {
      // get code
      var code = IngestCode(inputFileName);

      Console.WriteLine($"Initial state: {ConvertCodeToString(code)}");
      Console.WriteLine("");

      // Once you have a working computer, the first step is to restore the gravity
      // assist program(your puzzle input) to the "1202 program alarm" state it had
      // just before the last computer caught fire. To do this, before running the
      // program, replace position 1 with the value 12 and replace position 2 with
      // the value 2.What value is left at position 0 after the program halts?

      code[1] = 12;
      code[2] = 2;

      Console.WriteLine($"1202 Program Alert state: {ConvertCodeToString(code)}");
      Console.WriteLine("");


      var resultingCode = ExecuteSteps(code);
      var codeString = ConvertCodeToString(resultingCode);

      return codeString;
    }


    public List<int> IngestCode(string inputFileName)
    {
      var code = new List<int>();

      var fileStream = new FileStream(inputFileName, FileMode.Open);
      using (var streamReader = new StreamReader(fileStream))
      {
        if (!streamReader.EndOfStream)
        {
          var codeLine = streamReader.ReadLine();

          var codeArray = codeLine.Split(",");
          foreach (var codeFragment in codeArray)
          {
            // convert to integer and add to list
            code.Add(int.Parse(codeFragment));
          }
        }
      }

      return code;
    }

    // outputs number of steps it's read
    public List<int> ExecuteSteps(List<int> inputCode)
    {
      var code = inputCode;
      int stepNumber = 0;

      for(var index = 0; index <= inputCode.Count; index += 4)
      {
        var operation = ParseOperation(inputCode, index);
        if (operation.Type == 1) code = ProcessOperation1(code, operation);
        else if (operation.Type == 2) code = ProcessOperation2(code, operation);
        else if (operation.Type == 99) break; // 99 menas end of program

        stepNumber++;
      }

      return code;
    }

    public Operation ParseOperation(List<int> code, int stepIndex)
    {
      var operation = new Operation() { Type = code[stepIndex] };

      // operation 99 is end of program
      if (operation.Type == 99) return operation;

      // other operations have more data
      operation.ReadAddress1 = code[stepIndex + 1];
      operation.ReadAddress2 = code[stepIndex + 2];
      operation.OutpotPosition = code[stepIndex + 3];

      return operation;
    }

    public List<int> ProcessOperation1(List<int> code, Operation operation)
    {
      var valueOfAddress1 = code[operation.ReadAddress1.Value];
      var valueOfAddress2 = code[operation.ReadAddress2.Value];

      // addition
      var result = valueOfAddress1 + valueOfAddress2;

      // update code
      code[operation.OutpotPosition.Value] = result;

      return code;
    }

    public List<int> ProcessOperation2(List<int> code, Operation operation)
    {
      var valueOfAddress1 = code[operation.ReadAddress1.Value];
      var valueOfAddress2 = code[operation.ReadAddress2.Value];

      // addition
      var result = valueOfAddress1 * valueOfAddress2;

      // update code
      code[operation.OutpotPosition.Value] = result;

      return code;
    }

    public string ConvertCodeToString(List<int> code)
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

  public class Operation
  {
    public int Type { get; set; }
    public int? ReadAddress1 { get; set; }
    public int? ReadAddress2 { get; set; }
    public int? OutpotPosition { get; set; }
  }
}
