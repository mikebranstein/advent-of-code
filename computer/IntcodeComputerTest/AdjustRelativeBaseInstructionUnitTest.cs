using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using IntcodeComputer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntcodeComputerTest
{
  [TestClass]
  public class AdjustRelativeBaseInstructionUnitTest
  {

    [TestMethod]
    public void Can_Identify_Operation_9()
    {
      // arrange
      var computer = new Computer();
      var memory = computer.ReadMemoryFromFile("TestFile/op_9.txt");

      // act
      var instruction = (AdjustRelativeBaseInstruction)InstructionFactory.ParseInstruction(memory, 0);

      // assert 
      Assert.AreEqual(instruction.OpCode, 9);
      Assert.AreEqual(instruction.Parameter1.Value, 19);
      Assert.AreEqual(instruction.PointerAdvancement, 2);
    }

    [TestMethod]
    public void Can_Read_and_Process_Op_9()
    {
      // arrange
      var computer = new Computer();

      // should write 88 to last bit
      var memory = new List<long> { 109, 19, 99 };
      var virtualMemory = new Dictionary<int, long>();
      var inputBuffer = new BufferBlock<long>();
      var outputBuffer = new BufferBlock<long>();
      var instructionPointer = 0;
      var relativeBase = 2000;

      // act
      var instruction = (AdjustRelativeBaseInstruction)InstructionFactory.ParseInstruction(memory, instructionPointer);
      instruction.Execute(memory, virtualMemory, ref instructionPointer, ref relativeBase, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(relativeBase, 2019);
      Assert.AreEqual(instructionPointer, 2);
    }

    [TestMethod]
    public void Can_Read_and_Process_Op_9_2()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<long> { 109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99 };
      var inputBuffer = new BufferBlock<long>();
      var outputBuffer = new BufferBlock<long>();

      // act
      
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert - 109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99
      Assert.AreEqual(outputBuffer.Receive(), 109);
      Assert.AreEqual(outputBuffer.Receive(), 1);
      Assert.AreEqual(outputBuffer.Receive(), 204);
      Assert.AreEqual(outputBuffer.Receive(), -1);
      Assert.AreEqual(outputBuffer.Receive(), 1001);
      Assert.AreEqual(outputBuffer.Receive(), 100);
      Assert.AreEqual(outputBuffer.Receive(), 1);
      Assert.AreEqual(outputBuffer.Receive(), 100);
      Assert.AreEqual(outputBuffer.Receive(), 1008);
      Assert.AreEqual(outputBuffer.Receive(), 100);
      Assert.AreEqual(outputBuffer.Receive(), 16);
      Assert.AreEqual(outputBuffer.Receive(), 101);
      Assert.AreEqual(outputBuffer.Receive(), 1006);
      Assert.AreEqual(outputBuffer.Receive(), 101);
      Assert.AreEqual(outputBuffer.Receive(), 0);
      Assert.AreEqual(outputBuffer.Receive(), 99);
      Assert.AreEqual(outputBuffer.Count, 0);
    }

  }
}
