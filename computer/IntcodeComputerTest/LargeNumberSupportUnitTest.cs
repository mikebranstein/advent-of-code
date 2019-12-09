using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using IntcodeComputer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntcodeComputerTest
{
  [TestClass]
  public class LargeNumberSupportUnitTest
  {
    [TestMethod]
    public void Can_Read_and_Process_Op_9_2()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<long> { 1102, 34915192, 34915192, 7, 4, 7, 99, 0 };
      var inputBuffer = new BufferBlock<long>();
      var outputBuffer = new BufferBlock<long>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert - shoudl output a large 16-digit number
      Assert.AreEqual(outputBuffer.Receive(), 1219070632396864);
    }

    [TestMethod]
    public void Can_Read_and_Process_Op_9_3()
    {
      // arrange
      var computer = new Computer();
      var memory = new List<long> { 104, 1125899906842624, 99 };
      var inputBuffer = new BufferBlock<long>();
      var outputBuffer = new BufferBlock<long>();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert - shoudl output a large 16-digit number
      Assert.AreEqual(outputBuffer.Receive(), 1125899906842624);
    }

  }
}
