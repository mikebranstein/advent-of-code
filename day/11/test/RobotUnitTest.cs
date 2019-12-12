using System.Threading.Tasks;
using HullPainting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test
{
  [TestClass]
  public class RobotUnitTest
  {
    [TestMethod]
    public void TestMethod1()
    {
      // arrange
      string inputFileName = "TestFiles/input.txt";
      var robot = new Robot(inputFileName);
      var expectedPanelsPainted = 2141;

      // act
      Task.Run(() => robot.Run(Color.Black)).Wait();
      var panelsPainted = robot.GetNumPanelsPainted();

      // assert
      Assert.AreEqual(panelsPainted, expectedPanelsPainted);
    }
  }
}
