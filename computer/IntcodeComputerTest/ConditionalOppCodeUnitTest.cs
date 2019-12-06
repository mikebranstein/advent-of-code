using System.Collections.Generic;
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
      var memory = new List<int>() { 3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8 };

      var inputBuffer = new Queue<int>();
      inputBuffer.Enqueue(8);

      var outputBuffer = new Queue<int>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Dequeue(), 1);

    }

    [TestMethod]
    public void Not_Equal_to_8()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<int>() { 3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8 };

      var inputBuffer = new Queue<int>();
      inputBuffer.Enqueue(9);

      var outputBuffer = new Queue<int>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Dequeue(), 0);
    }

    [TestMethod]
    public void Less_Than_8()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<int>() { 3, 9, 7, 9, 10, 9, 4, 9, 99, -1, 8 };

      var inputBuffer = new Queue<int>();
      inputBuffer.Enqueue(3);

      var outputBuffer = new Queue<int>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Dequeue(), 1);
    }

    [TestMethod]
    public void Not_Less_Than_8()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<int>() { 3, 9, 7, 9, 10, 9, 4, 9, 99, -1, 8 };

      var inputBuffer = new Queue<int>();
      inputBuffer.Enqueue(77);

      var outputBuffer = new Queue<int>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Dequeue(), 0);
    }

    [TestMethod]
    public void Equals_8_Immediate()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<int>() { 3, 3, 1108, -1, 8, 3, 4, 3, 99 };

      var inputBuffer = new Queue<int>();
      inputBuffer.Enqueue(8);

      var outputBuffer = new Queue<int>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Dequeue(), 1);
    }

    [TestMethod]
    public void Not_Equals_8_Immediate()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<int>() { 3, 3, 1108, -1, 8, 3, 4, 3, 99 };

      var inputBuffer = new Queue<int>();
      inputBuffer.Enqueue(2);

      var outputBuffer = new Queue<int>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Dequeue(), 0);
    }

    [TestMethod]
    public void Less_Than_8_Immediate()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<int>() { 3, 3, 1107, -1, 8, 3, 4, 3, 99 };

      var inputBuffer = new Queue<int>();
      inputBuffer.Enqueue(2);

      var outputBuffer = new Queue<int>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Dequeue(), 1);
    }


    [TestMethod]
    public void Not_Less_Than_8_Immediate()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<int>() { 3, 3, 1107, -1, 8, 3, 4, 3, 99 };

      var inputBuffer = new Queue<int>();
      inputBuffer.Enqueue(8);

      var outputBuffer = new Queue<int>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Dequeue(), 0);
    }

    [TestMethod]
    public void Jump_Outputs_Zero()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<int>() { 3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9 };

      var inputBuffer = new Queue<int>();
      inputBuffer.Enqueue(0);

      var outputBuffer = new Queue<int>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Dequeue(), 0);
    }

    [TestMethod]
    public void Jump_Outputs_One()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<int>() { 3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9 };

      var inputBuffer = new Queue<int>();
      inputBuffer.Enqueue(65);

      var outputBuffer = new Queue<int>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Dequeue(), 1);
    }

    [TestMethod]
    public void Jump_Outputs_Zero_Immediate()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<int>() { 3, 3, 1105, -1, 9, 1101, 0, 0, 12, 4, 12, 99, 1 };

      var inputBuffer = new Queue<int>();
      inputBuffer.Enqueue(0);

      var outputBuffer = new Queue<int>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Dequeue(), 0);
    }

    [TestMethod]
    public void Jump_Outputs_One_Immediate()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<int>() { 3, 3, 1105, -1, 9, 1101, 0, 0, 12, 4, 12, 99, 1 };

      var inputBuffer = new Queue<int>();
      inputBuffer.Enqueue(765);

      var outputBuffer = new Queue<int>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Dequeue(), 1);
    }

    [TestMethod]
    public void Less_Than_8_Outputs_999()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<int>() { 3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99 };

      var inputBuffer = new Queue<int>();
      inputBuffer.Enqueue(5);

      var outputBuffer = new Queue<int>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Dequeue(), 999);
    }

    [TestMethod]
    public void Equal_8_Outputs_1000()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<int>() { 3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31, 1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104, 999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99 };

      var inputBuffer = new Queue<int>();
      inputBuffer.Enqueue(8);

      var outputBuffer = new Queue<int>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Dequeue(), 1000);
    }

    [TestMethod]
    public void Greater_Than_8_Outputs_1001()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<int>() { 3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31, 1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104, 999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99 };

      var inputBuffer = new Queue<int>();
      inputBuffer.Enqueue(54378);

      var outputBuffer = new Queue<int>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Dequeue(), 1001);
    }
  }
}
