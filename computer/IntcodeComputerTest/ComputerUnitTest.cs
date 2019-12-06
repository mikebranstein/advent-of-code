using System.Collections.Generic;
using IntcodeComputer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntmemoryComputerTest
{
  [TestClass]
  public class ComputerUnitTest
  {
    [TestMethod]
    public void Can_Ingest_memory_1()
    {
      // arrange
      var computer = new Computer();

      // act
      var memory = computer.ReadMemoryFromFile("TestFile/test_input_1.txt");

      // assert
      Assert.AreEqual(memory.Count, 5);
      Assert.AreEqual(memory[0], 1);
      Assert.AreEqual(memory[1], 1);
      Assert.AreEqual(memory[2], 2);
      Assert.AreEqual(memory[3], 3);
      Assert.AreEqual(memory[4], 99);
    }


    [TestMethod]
    public void Can_Identify_Operation_1()
    {
      // arrange
      var computer = new Computer();
      var memory = computer.ReadMemoryFromFile("TestFile/op_1.txt");

      // act
      var instruction = (AddInstruction)InstructionFactory.ParseInstruction(memory, 0);

      // assert no exception thrown
      Assert.AreEqual(instruction.OpCode, 1);
      Assert.AreEqual(instruction.Parameter1.Value, 2);
      Assert.AreEqual(instruction.Parameter2.Value, 3);
      Assert.AreEqual(instruction.Parameter3.Value, 4);
    }


    [TestMethod]
    public void Can_Identify_Operation_2()
    {
      // arrange
      var computer = new Computer();
      var memory = computer.ReadMemoryFromFile("TestFile/op_2.txt");

      // act
      var instruction = (MultiplyInstruction)InstructionFactory.ParseInstruction(memory, 0);

      // assert no exception thrown
      Assert.AreEqual(instruction.OpCode, 2);
      Assert.AreEqual(instruction.Parameter1.Value, 1);
      Assert.AreEqual(instruction.Parameter2.Value, 3);
      Assert.AreEqual(instruction.Parameter3.Value, 4);
    }


    [TestMethod]
    public void Can_Identify_Operation_99()
    {
      // arrange
      var computer = new Computer();
      var memory = computer.ReadMemoryFromFile("TestFile/op_99.txt");

      // act
      var instruction = (HaltInstruction)InstructionFactory.ParseInstruction(memory, 4);

      // assert 
      Assert.AreEqual(instruction.OpCode, 99);
      Assert.AreEqual(instruction.Parameters.Length, 0);
    }

    [TestMethod]
    public void Can_Process_Operation_1_Add_Simple()
    {
      // arrange
      var memory = new List<int> { 1, 0, 3, 3, 99 };

      // act
      var instruction = (AddInstruction)InstructionFactory.ParseInstruction(memory, 0);
      instruction.Execute(memory, null, null);

      // assert memory not changed
      Assert.AreEqual(memory[0], 1);
      Assert.AreEqual(memory[1], 0);
      Assert.AreEqual(memory[2], 3);
      Assert.AreEqual(memory[3], 4);
      Assert.AreEqual(memory[4], 99);
    }

    [TestMethod]
    public void Can_Process_Operation_1_Add_Chain()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<int> { 1, 3, 2, 5, 1, 0, 0, 1, 99 };

      // act - add one
      var instruction = (AddInstruction)InstructionFactory.ParseInstruction(memory, 0);
      instruction.Execute(memory, null, null);

      // assert 
      Assert.AreEqual(memory[0], 1);
      Assert.AreEqual(memory[1], 3);
      Assert.AreEqual(memory[2], 2);
      Assert.AreEqual(memory[3], 5);
      Assert.AreEqual(memory[4], 1);
      Assert.AreEqual(memory[5], 7);
      Assert.AreEqual(memory[6], 0);
      Assert.AreEqual(memory[7], 1);
      Assert.AreEqual(memory[8], 99);

      // act - second add
      instruction = (AddInstruction)InstructionFactory.ParseInstruction(memory, 4);
      instruction.Execute(memory, null, null);

      // assert second operation
      Assert.AreEqual(memory[0], 1);
      Assert.AreEqual(memory[1], 2);
      Assert.AreEqual(memory[2], 2);
      Assert.AreEqual(memory[3], 5);
      Assert.AreEqual(memory[4], 1);
      Assert.AreEqual(memory[5], 7);
      Assert.AreEqual(memory[6], 0);
      Assert.AreEqual(memory[7], 1);
      Assert.AreEqual(memory[8], 99);
    }

    [TestMethod]
    public void Can_Process_Operation_2_Multiply_Simple()
    {
      // arrange
      var memory = new List<int> { 2, 0, 3, 5, 99, 0 };

      // act
      var instruction = (MultiplyInstruction)InstructionFactory.ParseInstruction(memory, 0);
      instruction.Execute(memory, null, null);

      // assert memory not changed
      Assert.AreEqual(memory[0], 2);
      Assert.AreEqual(memory[1], 0);
      Assert.AreEqual(memory[2], 3);
      Assert.AreEqual(memory[3], 5);
      Assert.AreEqual(memory[4], 99);
      Assert.AreEqual(memory[5], 10);
    }

    [TestMethod]
    public void Can_Process_Operation_2_Multiply_Chain()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<int> { 2, 3, 2, 5, 2, 0, 0, 1, 99, 20, 30};

      // act - add one
      var instruction = (MultiplyInstruction)InstructionFactory.ParseInstruction(memory, 0);
      instruction.Execute(memory, null, null);

      // assert 
      Assert.AreEqual(memory[0], 2);
      Assert.AreEqual(memory[1], 3);
      Assert.AreEqual(memory[2], 2);
      Assert.AreEqual(memory[3], 5);
      Assert.AreEqual(memory[4], 2);
      Assert.AreEqual(memory[5], 10);
      Assert.AreEqual(memory[6], 0);
      Assert.AreEqual(memory[7], 1);
      Assert.AreEqual(memory[8], 99);
      Assert.AreEqual(memory[9], 20);
      Assert.AreEqual(memory[10], 30);

      // act - second add
      instruction = (MultiplyInstruction)InstructionFactory.ParseInstruction(memory, 4);
      instruction.Execute(memory, null, null);

      // assert second operation
      Assert.AreEqual(memory[0], 2);
      Assert.AreEqual(memory[1], 60);
      Assert.AreEqual(memory[2], 2);
      Assert.AreEqual(memory[3], 5);
      Assert.AreEqual(memory[4], 2);
      Assert.AreEqual(memory[5], 10);
      Assert.AreEqual(memory[6], 0);
      Assert.AreEqual(memory[7], 1);
      Assert.AreEqual(memory[8], 99);
      Assert.AreEqual(memory[9], 20);
      Assert.AreEqual(memory[10], 30);
    }

    [TestMethod]
    public void Can_Process_Multi_Steps_1()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<int> { 2, 3, 2, 5, 2, 0, 0, 1, 99, 20, 30 };

      // act
      computer.ExecuteProgram(memory, null, null);

      // assert second operation
      Assert.AreEqual(memory[0], 2);
      Assert.AreEqual(memory[1], 60);
      Assert.AreEqual(memory[2], 2);
      Assert.AreEqual(memory[3], 5);
      Assert.AreEqual(memory[4], 2);
      Assert.AreEqual(memory[5], 10);
      Assert.AreEqual(memory[6], 0);
      Assert.AreEqual(memory[7], 1);
      Assert.AreEqual(memory[8], 99);
      Assert.AreEqual(memory[9], 20);
      Assert.AreEqual(memory[10], 30);
    }

    [TestMethod]
    public void Can_Process_Multi_Steps_2()
    {
      // 1,9,10,3,2,3,11,0,99,30,40,50

      // arrange
      var computer = new Computer();
      var memory = new List<int> { 1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50 };

      // act
      computer.ExecuteProgram(memory, null, null);

      // assert second operation
      Assert.AreEqual(memory[0], 3500);
      Assert.AreEqual(memory[1], 9);
      Assert.AreEqual(memory[2], 10);
      Assert.AreEqual(memory[3], 70);
      Assert.AreEqual(memory[4], 2);
      Assert.AreEqual(memory[5], 3);
      Assert.AreEqual(memory[6], 11);
      Assert.AreEqual(memory[7], 0);
      Assert.AreEqual(memory[8], 99);
      Assert.AreEqual(memory[9], 30);
      Assert.AreEqual(memory[10], 40);
      Assert.AreEqual(memory[11], 50);
    }

    [TestMethod]
    public void Small_Program_1()
    {
      // 1,0,0,0,99

      // arrange
      var computer = new Computer();
      var memory = new List<int> { 1, 0, 0, 0, 99 };

      // act
      computer.ExecuteProgram(memory, null, null);

      // assert second operation
      // 2,0,0,0,99
      Assert.AreEqual(memory[0], 2);
      Assert.AreEqual(memory[1], 0);
      Assert.AreEqual(memory[2], 0);
      Assert.AreEqual(memory[3], 0);
      Assert.AreEqual(memory[4], 99);
    }

    [TestMethod]
    public void Small_Program_2()
    {
      // 2,3,0,3,99

      // arrange
      var computer = new Computer();
      var memory = new List<int> { 2, 3, 0, 3, 99 };

      // act
      computer.ExecuteProgram(memory, null, null);

      // assert second operation
      // 2,3,0,6,99
      Assert.AreEqual(memory[0], 2);
      Assert.AreEqual(memory[1], 3);
      Assert.AreEqual(memory[2], 0);
      Assert.AreEqual(memory[3], 6);
      Assert.AreEqual(memory[4], 99);
    }

    [TestMethod]
    public void Small_Program_3()
    {
      // arrange
      var computer = new Computer();

      // act
      var memory = computer.Run("TestFile/small_program_3.txt", null, null);

      // assert second operation
      // 2,4,4,5,99,9801
      Assert.AreEqual(memory, "2,4,4,5,99,9801");
    }

    [TestMethod]
    public void Can_Convert_memory_to_String()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<int> { 1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50 };

      // act
      var memoryString = computer.ConvertMemoryToString(memory);

      // assert 
      Assert.AreEqual(memoryString, "1,9,10,3,2,3,11,0,99,30,40,50");
    }

    [TestMethod]
    public void Small_Program_4()
    {
      // arrange
      var computer = new Computer();

      // act
      var memoryString = computer.Run("TestFile/small_program_4.txt", null, null);

      // assert second operation
      // 30,1,1,4,2,5,6,0,99
      Assert.AreEqual(memoryString, "30,1,1,4,2,5,6,0,99");
    }

    [TestMethod]
    public void Complete_Input_Output()
    {
      // arrange
      var computer = new Computer();
      var outputMemoryString =
        computer.ConvertMemoryToString(
          computer.ReadMemoryFromFile("TestFile/output.txt"));

      // act
      var memory = computer.Run("TestFile/input.txt", null, null);

      // assert
      Assert.AreEqual(memory, outputMemoryString);
    }

    [TestMethod]
    public void Complete_Input_Output_1202_State()
    {
      // arrange
      var computer = new Computer();
      var outputMemoryString =
        computer.ConvertMemoryToString(
          computer.ReadMemoryFromFile("TestFile/output_1202.txt"));

      // act
      var memory = computer.Run("TestFile/input_1202.txt", null, null);

      // assert
      Assert.AreEqual(memory, outputMemoryString);
    }

    [TestMethod]
    public void Can_Generate_Output_1202_State()
    {
      // arrange
      var computer = new Computer();
      var expectedOutput = computer.ReadMemoryFromFile("TestFile/output_1202.txt")[0];

      // act
      var memory = computer.ReadMemoryFromFile("TestFile/input_1202.txt");
      var output = computer.Run(memory, null, null);

      // assert
      Assert.AreEqual(output, expectedOutput);
    }

    [TestMethod]
    public void Can_Generate_Output()
    {
      // arrange
      var computer = new Computer();
      var expectedOutput = computer.ReadMemoryFromFile("TestFile/output.txt")[0];

      // act
      var memory = computer.ReadMemoryFromFile("TestFile/input.txt");
      var output = computer.Run(memory, null, null);

      // assert
      Assert.AreEqual(output, expectedOutput);
    }

    [TestMethod]
    public void Can_Generate_Output_2003_State()
    {
      // arrange
      var computer = new Computer();
      var expectedOutput = 19690720;

      // act
      var memory = computer.ReadMemoryFromFile("TestFile/input_2003.txt");
      var output = computer.Run(memory, null, null);

      // assert
      Assert.AreEqual(output, expectedOutput);
    }
  }
}
