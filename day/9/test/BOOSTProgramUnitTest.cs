using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using IntcodeComputer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test
{
  [TestClass]
  public class BOOSTProgramUnitTest
  {
    [TestMethod]
    public void Part_1()
    {
      // arrange
      var computer = new Computer();
      var memory = computer.ReadMemoryFromFile("TestFiles/input.txt");
      var inputBuffer = new BufferBlock<long>();
      var outputBuffer = new BufferBlock<long>();

      Task.Run(() => inputBuffer.SendAsync(1)).Wait();

      // act
      computer.ExecuteProgram(memory, inputBuffer, outputBuffer);

      // assert
      Assert.AreEqual(outputBuffer.Count, 1);
      Assert.AreEqual(outputBuffer.Receive(), 4261108180);
    }
  }
}
