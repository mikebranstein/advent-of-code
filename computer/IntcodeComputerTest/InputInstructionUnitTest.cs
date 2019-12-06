﻿using System.Collections.Generic;
using IntcodeComputer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntcodeComputerTest
{
  [TestClass]
  public class InputInstructionUnitTest
  {

    [TestMethod]
    public void Can_Identify_Operation_3()
    {
      // arrange
      var computer = new Computer();
      var memory = computer.ReadMemoryFromFile("TestFile/op_3.txt");

      // act
      var instruction = (InputInstruction)InstructionFactory.ParseInstruction(memory, 0);

      // assert 
      Assert.AreEqual(instruction.OpCode, 3);
      Assert.AreEqual(instruction.Parameter1.Value, 2);
      Assert.AreEqual(instruction.PointerAdvancement, 2);
    }

    [TestMethod]
    public void Can_Identify_Operation_3_from_Middle()
    {
      // arrange
      var computer = new Computer();
      var memory = computer.ReadMemoryFromFile("TestFile/op_3.txt");

      // act
      var instruction = (InputInstruction)InstructionFactory.ParseInstruction(memory, 2);

      // assert 
      Assert.AreEqual(instruction.OpCode, 3);
      Assert.AreEqual(instruction.Parameter1.Value, 4);
      Assert.AreEqual(instruction.PointerAdvancement, 2);
    }

    [TestMethod]
    public void Can_Read_and_Process_1_Input()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<int> { 3, 3, 99, 0 }; // should write input to address 3
      var inputBuffer = new Queue<int>();
      inputBuffer.Enqueue(77);

      // act
      var instruction = (InputInstruction)InstructionFactory.ParseInstruction(memory, 0);
      instruction.Execute(memory, inputBuffer);

      // assert 
      Assert.AreEqual(memory[3], 77);
    }

    [TestMethod]
    public void Can_Read_and_Process_2_Input()
    {
      // arrange
      var computer = new Computer();

      // should write 88 to last bit
      var memory = new List<int> { 3, 3, 3, 0, 99, 0 }; 
      var inputBuffer = new Queue<int>();
      inputBuffer.Enqueue(5);
      inputBuffer.Enqueue(88);

      // act
      var instruction = (InputInstruction)InstructionFactory.ParseInstruction(memory, 0);
      var instructionPointerIncrement = instruction.Execute(memory, inputBuffer);
      instruction = (InputInstruction)InstructionFactory.ParseInstruction(memory, instructionPointerIncrement);
      instruction.Execute(memory, inputBuffer);

      // assert
      Assert.AreEqual(memory[3], 5);
      Assert.AreEqual(memory[5], 88);
    }

  }
}
