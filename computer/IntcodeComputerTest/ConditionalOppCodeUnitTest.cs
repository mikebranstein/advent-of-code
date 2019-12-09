using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using IntcodeComputer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntcodeComputerTest
{
  [TestClass]
  public class ConditionalOppCodeUnitTest
  {

    [TestMethod]
    public void Equal_to_8()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<long>() { 3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8 };

      var inputBuffer = new BufferBlock<long>();
      Task.Run(() => inputBuffer.SendAsync(8)).Wait();

      var outputBuffer = new BufferBlock<long>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Receive(), 1);

    }

    [TestMethod]
    public void Not_Equal_to_8()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<long>() { 3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8 };

      var inputBuffer = new BufferBlock<long>();
      Task.Run(() => inputBuffer.SendAsync(9)).Wait();

      var outputBuffer = new BufferBlock<long>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Receive(), 0);
    }

    [TestMethod]
    public void Less_Than_8()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<long>() { 3, 9, 7, 9, 10, 9, 4, 9, 99, -1, 8 };

      var inputBuffer = new BufferBlock<long>();
      Task.Run(() => inputBuffer.SendAsync(3)).Wait();

      var outputBuffer = new BufferBlock<long>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Receive(), 1);
    }

    [TestMethod]
    public void Not_Less_Than_8()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<long>() { 3, 9, 7, 9, 10, 9, 4, 9, 99, -1, 8 };

      var inputBuffer = new BufferBlock<long>();
      Task.Run(() => inputBuffer.SendAsync(77)).Wait();

      var outputBuffer = new BufferBlock<long>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Receive(), 0);
    }

    [TestMethod]
    public void Equals_8_Immediate()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<long>() { 3, 3, 1108, -1, 8, 3, 4, 3, 99 };

      var inputBuffer = new BufferBlock<long>();
      Task.Run(() => inputBuffer.SendAsync(8)).Wait();

      var outputBuffer = new BufferBlock<long>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Receive(), 1);
    }

    [TestMethod]
    public void Not_Equals_8_Immediate()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<long>() { 3, 3, 1108, -1, 8, 3, 4, 3, 99 };

      var inputBuffer = new BufferBlock<long>();
      Task.Run(() => inputBuffer.SendAsync(2)).Wait();

      var outputBuffer = new BufferBlock<long>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Receive(), 0);
    }

    [TestMethod]
    public void Less_Than_8_Immediate()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<long>() { 3, 3, 1107, -1, 8, 3, 4, 3, 99 };

      var inputBuffer = new BufferBlock<long>();
      Task.Run(() => inputBuffer.SendAsync(2)).Wait();

      var outputBuffer = new BufferBlock<long>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Receive(), 1);
    }


    [TestMethod]
    public void Not_Less_Than_8_Immediate()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<long>() { 3, 3, 1107, -1, 8, 3, 4, 3, 99 };

      var inputBuffer = new BufferBlock<long>();
      Task.Run(() => inputBuffer.SendAsync(8)).Wait();

      var outputBuffer = new BufferBlock<long>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Receive(), 0);
    }

    [TestMethod]
    public void Jump_Outputs_Zero()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<long>() { 3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9 };

      var inputBuffer = new BufferBlock<long>();
      Task.Run(() => inputBuffer.SendAsync(0)).Wait();

      var outputBuffer = new BufferBlock<long>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Receive(), 0);
    }

    [TestMethod]
    public void Jump_Outputs_One()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<long>() { 3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9 };

      var inputBuffer = new BufferBlock<long>();
      Task.Run(() => inputBuffer.SendAsync(65)).Wait();

      var outputBuffer = new BufferBlock<long>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Receive(), 1);
    }

    [TestMethod]
    public void Jump_Outputs_Zero_Immediate()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<long>() { 3, 3, 1105, -1, 9, 1101, 0, 0, 12, 4, 12, 99, 1 };

      var inputBuffer = new BufferBlock<long>();
      Task.Run(() => inputBuffer.SendAsync(0)).Wait();

      var outputBuffer = new BufferBlock<long>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Receive(), 0);
    }

    [TestMethod]
    public void Jump_Outputs_One_Immediate()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<long>() { 3, 3, 1105, -1, 9, 1101, 0, 0, 12, 4, 12, 99, 1 };

      var inputBuffer = new BufferBlock<long>();
      Task.Run(() => inputBuffer.SendAsync(765)).Wait();

      var outputBuffer = new BufferBlock<long>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Receive(), 1);
    }

    [TestMethod]
    public void Less_Than_8_Outputs_999()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<long>() { 3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99 };

      var inputBuffer = new BufferBlock<long>();
      Task.Run(() => inputBuffer.SendAsync(5)).Wait();

      var outputBuffer = new BufferBlock<long>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Receive(), 999);
    }

    [TestMethod]
    public void Equal_8_Outputs_1000()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<long>() { 3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31, 1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104, 999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99 };

      var inputBuffer = new BufferBlock<long>();
      Task.Run(() => inputBuffer.SendAsync(8)).Wait();

      var outputBuffer = new BufferBlock<long>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Receive(), 1000);
    }

    [TestMethod]
    public void Greater_Than_8_Outputs_1001()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<long>() { 3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31, 1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104, 999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99 };

      var inputBuffer = new BufferBlock<long>();
      Task.Run(() => inputBuffer.SendAsync(54378)).Wait();

      var outputBuffer = new BufferBlock<long>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Receive(), 1001);
    }
  }
}
