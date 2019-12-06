using System.Collections.Generic;
using IntcodeComputer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntcodeComputerTest
{
  [TestClass]
  public class OutputInstructionUnitTest
  {

    [TestMethod]
    public void Can_Identify_Operation_4()
    {
      // arrange
      var computer = new Computer();
      var memory = computer.ReadMemoryFromFile("TestFile/op_4.txt");

      // act
      var instruction = (OutputInstruction)InstructionFactory.ParseInstruction(memory, 0);

      // assert 
      Assert.AreEqual(instruction.OpCode, 4);
      Assert.AreEqual(instruction.Parameter1.Value, 3);
      Assert.AreEqual(instruction.PointerAdvancement, 2);
    }

    [TestMethod]
    public void Can_Identify_Operation_4_from_Middle()
    {
      // arrange
      var computer = new Computer();
      var memory = computer.ReadMemoryFromFile("TestFile/op_4.txt");

      // act
      var instruction = (OutputInstruction)InstructionFactory.ParseInstruction(memory, 2);

      // assert 
      Assert.AreEqual(instruction.OpCode, 4);
      Assert.AreEqual(instruction.Parameter1.Value, 5);
      Assert.AreEqual(instruction.PointerAdvancement, 2);
    }

    [TestMethod]
    public void Can_Read_and_Process_1_Output()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<int> { 4, 3, 99, 67 }; // should write address 3 (67) to output 
      var inputBuffer = new Queue<int>();
      var outputBuffer = new Queue<int>();

      // act
      var instructionPointer = 0;
      var instruction = InstructionFactory.ParseInstruction(memory, instructionPointer);
      instruction.Execute(memory, ref instructionPointer, inputBuffer, outputBuffer);

      // assert 
      Assert.AreEqual(memory[0], 4);
      Assert.AreEqual(memory[1], 3);
      Assert.AreEqual(memory[2], 99);
      Assert.AreEqual(memory[3], 67);
      Assert.AreEqual(outputBuffer.Dequeue(), 67);
    }

    [TestMethod]
    public void Can_Read_and_Process_2_Output()
    {
      // arrange
      var computer = new Computer();

      // should write 88 to last bit
      var memory = new List<int> { 4, 3, 4, 5, 99, 103 };
      var inputBuffer = new Queue<int>();
      var outputBuffer = new Queue<int>();

      // act
      var instructionPointer = 0;
      var instruction = InstructionFactory.ParseInstruction(memory, instructionPointer);
      instruction.Execute(memory, ref instructionPointer, inputBuffer, outputBuffer);
      instruction = InstructionFactory.ParseInstruction(memory, instructionPointer);
      instruction.Execute(memory, ref instructionPointer, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(memory[0], 4);
      Assert.AreEqual(memory[1], 3);
      Assert.AreEqual(memory[2], 4);
      Assert.AreEqual(memory[3], 5);
      Assert.AreEqual(memory[4], 99);
      Assert.AreEqual(memory[5], 103);
      Assert.AreEqual(outputBuffer.Dequeue(), 5);
      Assert.AreEqual(outputBuffer.Dequeue(), 103);
    }

  }
}
