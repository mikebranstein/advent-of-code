using System.Collections.Generic;
using IntcodeComputer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntcodeComputerTest
{
  [TestClass]
  public class ParameterModeUnitTest
  {

    [TestMethod]
    public void Can_Identify_Long_OpCodes_Add()
    {
      // arrange
      var memory = new List<int>() { 01101, 0, 2, 3, 99 };

      // act
      var instruction = (AddInstruction)InstructionFactory.ParseInstruction(memory, 0);

      // assert 
      Assert.AreEqual(instruction.OpCode, 1);
      Assert.AreEqual(instruction.Parameter1.Value, 0);
      Assert.AreEqual(instruction.Parameter2.Value, 2);
      Assert.AreEqual(instruction.Parameter3.Value, 3);
      Assert.AreEqual(instruction.PointerAdvancement, 4);
    }

    [TestMethod]
    public void Can_Identify_Long_OpCodes_Multiply()
    {
      // arrange
      var memory = new List<int>() { 01102, 0, 2, 3, 99 };

      // act
      var instruction = (MultiplyInstruction)InstructionFactory.ParseInstruction(memory, 0);

      // assert 
      Assert.AreEqual(instruction.OpCode, 2);
      Assert.AreEqual(instruction.Parameter1.Value, 0);
      Assert.AreEqual(instruction.Parameter2.Value, 2);
      Assert.AreEqual(instruction.Parameter3.Value, 3);
      Assert.AreEqual(instruction.PointerAdvancement, 4);
    }

    [TestMethod]
    public void Can_Identify_Long_OpCodes_Input()
    {
      // arrange
      var memory = new List<int>() { 01103, 0, 99 };

      // act
      var instruction = (InputInstruction)InstructionFactory.ParseInstruction(memory, 0);

      // assert 
      Assert.AreEqual(instruction.OpCode, 3);
      Assert.AreEqual(instruction.Parameter1.Value, 0);
      Assert.AreEqual(instruction.PointerAdvancement, 2);
    }

    [TestMethod]
    public void Can_Identify_Long_OpCodes_Output()
    {
      // arrange
      var memory = new List<int>() { 01104, 0, 99 };

      // act
      var instruction = (OutputInstruction)InstructionFactory.ParseInstruction(memory, 0);

      // assert 
      Assert.AreEqual(instruction.OpCode, 4);
      Assert.AreEqual(instruction.Parameter1.Value, 0);
      Assert.AreEqual(instruction.PointerAdvancement, 2);
    }


    [TestMethod]
    public void Can_Identify_Long_OpCodes_Halt()
    {
      // arrange
      var memory = new List<int>() { 99 };

      // act
      var instruction = (HaltInstruction)InstructionFactory.ParseInstruction(memory, 0);

      // assert 
      Assert.AreEqual(instruction.OpCode, 99);
      Assert.AreEqual(instruction.PointerAdvancement, 1);
    }



    [TestMethod]
    public void Can_Parse_Parameter_Mode_Add_1()
    {
      // arrange
      var memory = new List<int>() { 01101, 0, 2, 3, 99 };

      // act
      var instruction = (AddInstruction)InstructionFactory.ParseInstruction(memory, 0);

      // assert 
      Assert.AreEqual(instruction.OpCode, 1);
      Assert.AreEqual(instruction.Parameter1.Value, 0);
      Assert.AreEqual(instruction.Parameter2.Value, 2);
      Assert.AreEqual(instruction.Parameter3.Value, 3);
      Assert.AreEqual(instruction.Parameter1.Mode, ParameterMode.ImmediateMode);
      Assert.AreEqual(instruction.Parameter2.Mode, ParameterMode.ImmediateMode);
      Assert.AreEqual(instruction.Parameter3.Mode, ParameterMode.PositionMode);
      Assert.AreEqual(instruction.PointerAdvancement, 4);
    }

    [TestMethod]
    public void Can_Parse_Parameter_Mode_Add_2()
    {
      // arrange
      var memory = new List<int>() { 101, 0, 2, 3, 99 };

      // act
      var instruction = (AddInstruction)InstructionFactory.ParseInstruction(memory, 0);

      // assert 
      Assert.AreEqual(instruction.OpCode, 1);
      Assert.AreEqual(instruction.Parameter1.Value, 0);
      Assert.AreEqual(instruction.Parameter2.Value, 2);
      Assert.AreEqual(instruction.Parameter3.Value, 3);
      Assert.AreEqual(instruction.Parameter1.Mode, ParameterMode.ImmediateMode);
      Assert.AreEqual(instruction.Parameter2.Mode, ParameterMode.PositionMode);
      Assert.AreEqual(instruction.Parameter3.Mode, ParameterMode.PositionMode);
      Assert.AreEqual(instruction.PointerAdvancement, 4);
    }

    [TestMethod]
    public void Can_Parse_Parameter_Mode_Add_3()
    {
      // arrange
      var memory = new List<int>() { 1, 0, 2, 3, 99 };

      // act
      var instruction = (AddInstruction)InstructionFactory.ParseInstruction(memory, 0);

      // assert 
      Assert.AreEqual(instruction.OpCode, 1);
      Assert.AreEqual(instruction.Parameter1.Value, 0);
      Assert.AreEqual(instruction.Parameter2.Value, 2);
      Assert.AreEqual(instruction.Parameter3.Value, 3);
      Assert.AreEqual(instruction.Parameter1.Mode, ParameterMode.PositionMode);
      Assert.AreEqual(instruction.Parameter2.Mode, ParameterMode.PositionMode);
      Assert.AreEqual(instruction.Parameter3.Mode, ParameterMode.PositionMode);
      Assert.AreEqual(instruction.PointerAdvancement, 4);
    }

    [TestMethod]
    public void Can_Parse_Parameter_Mode_Add_4()
    {
      // arrange
      var memory = new List<int>() { 10001, 0, 2, 3, 99 };

      // act
      var instruction = (AddInstruction)InstructionFactory.ParseInstruction(memory, 0);

      // assert 
      Assert.AreEqual(instruction.OpCode, 1);
      Assert.AreEqual(instruction.Parameter1.Value, 0);
      Assert.AreEqual(instruction.Parameter2.Value, 2);
      Assert.AreEqual(instruction.Parameter3.Value, 3);
      Assert.AreEqual(instruction.Parameter1.Mode, ParameterMode.PositionMode);
      Assert.AreEqual(instruction.Parameter2.Mode, ParameterMode.PositionMode);
      Assert.AreEqual(instruction.Parameter3.Mode, ParameterMode.ImmediateMode);
      Assert.AreEqual(instruction.PointerAdvancement, 4);
    }

    [TestMethod]
    public void Can_Parse_Parameter_Mode_Multiply_1()
    {
      // arrange
      var memory = new List<int>() { 01102, 0, 2, 3, 99 };

      // act
      var instruction = (MultiplyInstruction)InstructionFactory.ParseInstruction(memory, 0);

      // assert 
      Assert.AreEqual(instruction.OpCode, 2);
      Assert.AreEqual(instruction.Parameter1.Value, 0);
      Assert.AreEqual(instruction.Parameter2.Value, 2);
      Assert.AreEqual(instruction.Parameter3.Value, 3);
      Assert.AreEqual(instruction.Parameter1.Mode, ParameterMode.ImmediateMode);
      Assert.AreEqual(instruction.Parameter2.Mode, ParameterMode.ImmediateMode);
      Assert.AreEqual(instruction.Parameter3.Mode, ParameterMode.PositionMode);
      Assert.AreEqual(instruction.PointerAdvancement, 4);
    }

    [TestMethod]
    public void Can_Parse_Parameter_Mode_Multiply_2()
    {
      // arrange
      var memory = new List<int>() { 2, 0, 2, 3, 99 };

      // act
      var instruction = (MultiplyInstruction)InstructionFactory.ParseInstruction(memory, 0);

      // assert 
      Assert.AreEqual(instruction.OpCode, 2);
      Assert.AreEqual(instruction.Parameter1.Value, 0);
      Assert.AreEqual(instruction.Parameter2.Value, 2);
      Assert.AreEqual(instruction.Parameter3.Value, 3);
      Assert.AreEqual(instruction.Parameter1.Mode, ParameterMode.PositionMode);
      Assert.AreEqual(instruction.Parameter2.Mode, ParameterMode.PositionMode);
      Assert.AreEqual(instruction.Parameter3.Mode, ParameterMode.PositionMode);
      Assert.AreEqual(instruction.PointerAdvancement, 4);
    }

    [TestMethod]
    public void Can_Parse_Parameter_Mode_Multiply_3()
    {
      // arrange
      var memory = new List<int>() { 1102, 0, 2, 3, 99 };

      // act
      var instruction = (MultiplyInstruction)InstructionFactory.ParseInstruction(memory, 0);

      // assert 
      Assert.AreEqual(instruction.OpCode, 2);
      Assert.AreEqual(instruction.Parameter1.Value, 0);
      Assert.AreEqual(instruction.Parameter2.Value, 2);
      Assert.AreEqual(instruction.Parameter3.Value, 3);
      Assert.AreEqual(instruction.Parameter1.Mode, ParameterMode.ImmediateMode);
      Assert.AreEqual(instruction.Parameter2.Mode, ParameterMode.ImmediateMode);
      Assert.AreEqual(instruction.Parameter3.Mode, ParameterMode.PositionMode);
      Assert.AreEqual(instruction.PointerAdvancement, 4);
    }

    [TestMethod]
    public void Can_Parse_Parameter_Mode_Input_1()
    {
      // arrange
      var memory = new List<int>() { 103, 0, 99 };

      // act
      var instruction = (InputInstruction)InstructionFactory.ParseInstruction(memory, 0);

      // assert 
      Assert.AreEqual(instruction.OpCode, 3);
      Assert.AreEqual(instruction.Parameter1.Value, 0);
      Assert.AreEqual(instruction.Parameter1.Mode, ParameterMode.ImmediateMode);
      Assert.AreEqual(instruction.PointerAdvancement, 2);
    }

    [TestMethod]
    public void Can_Parse_Parameter_Mode_Input_2()
    {
      // arrange
      var memory = new List<int>() { 003, 0, 99 };

      // act
      var instruction = (InputInstruction)InstructionFactory.ParseInstruction(memory, 0);

      // assert 
      Assert.AreEqual(instruction.OpCode, 3);
      Assert.AreEqual(instruction.Parameter1.Value, 0);
      Assert.AreEqual(instruction.Parameter1.Mode, ParameterMode.PositionMode);
      Assert.AreEqual(instruction.PointerAdvancement, 2);
    }

    [TestMethod]
    public void Can_Parse_Parameter_Mode_Output_1()
    {
      // arrange
      var memory = new List<int>() { 104, 0, 99 };

      // act
      var instruction = (OutputInstruction)InstructionFactory.ParseInstruction(memory, 0);

      // assert 
      Assert.AreEqual(instruction.OpCode, 4);
      Assert.AreEqual(instruction.Parameter1.Value, 0);
      Assert.AreEqual(instruction.Parameter1.Mode, ParameterMode.ImmediateMode);
      Assert.AreEqual(instruction.PointerAdvancement, 2);
    }

    [TestMethod]
    public void Can_Parse_Parameter_Mode_Output_2()
    {
      // arrange
      var memory = new List<int>() { 004, 0, 99 };

      // act
      var instruction = (OutputInstruction)InstructionFactory.ParseInstruction(memory, 0);

      // assert 
      Assert.AreEqual(instruction.OpCode, 4);
      Assert.AreEqual(instruction.Parameter1.Value, 0);
      Assert.AreEqual(instruction.Parameter1.Mode, ParameterMode.PositionMode);
      Assert.AreEqual(instruction.PointerAdvancement, 2);
    }


    [TestMethod]
    public void Can_Parse_Parameter_Mode_Halt_1()
    {
      // arrange
      var memory = new List<int>() { 99 };

      // act
      var instruction = (HaltInstruction)InstructionFactory.ParseInstruction(memory, 0);

      // assert 
      Assert.AreEqual(instruction.OpCode, 99);
      Assert.AreEqual(instruction.PointerAdvancement, 1);
    }



  }
}
