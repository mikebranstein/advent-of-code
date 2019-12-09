using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using IntcodeComputer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntcodeComputerTest
{
  [TestClass]
  public class InputOutputUnitTest
  {

    [TestMethod]
    public void Short_IO_Program_1()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<long> { 3, 5, 4, 5, 99, 0 }; // should read input of 512, write to address 5, then output
      var inputBuffer = new BufferBlock<long>();
      Task.Run(() => inputBuffer.SendAsync(512)).Wait();
      var outputBuffer = new BufferBlock<long>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert 
      Assert.AreEqual(memory[0], 3);
      Assert.AreEqual(memory[1], 5);
      Assert.AreEqual(memory[2], 4);
      Assert.AreEqual(memory[3], 5);
      Assert.AreEqual(memory[4], 99);
      Assert.AreEqual(memory[5], 512);
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Count, 1);
      Assert.AreEqual(outputBuffer.Receive(), 512);
    }

    [TestMethod]
    public void Short_IO_Program_2()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<long> { 1, 4, 10, 5, 3, 0, 2, 0, 2, 13, 4, 13, 99, 0 }; // should output 130 
      var inputBuffer = new BufferBlock<long>();
      Task.Run(() => inputBuffer.SendAsync(9)).Wait();
      var outputBuffer = new BufferBlock<long>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert 
      Assert.AreEqual(memory[0], 1);
      Assert.AreEqual(memory[1], 4);
      Assert.AreEqual(memory[2], 10);
      Assert.AreEqual(memory[3], 5);
      Assert.AreEqual(memory[4], 3);
      Assert.AreEqual(memory[5], 7);
      Assert.AreEqual(memory[6], 2);
      Assert.AreEqual(memory[7], 9);
      Assert.AreEqual(memory[8], 2);
      Assert.AreEqual(memory[9], 13);
      Assert.AreEqual(memory[10], 4);
      Assert.AreEqual(memory[11], 13);
      Assert.AreEqual(memory[12], 99);
      Assert.AreEqual(memory[13], 130);
      Assert.AreEqual(inputBuffer.Count, 0);
      Assert.AreEqual(outputBuffer.Count, 1);
      Assert.AreEqual(outputBuffer.Receive(), 130);
    }

  }
}
