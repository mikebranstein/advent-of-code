using System.Linq;
using System.Threading.Tasks;
using challenge;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test
{
  [TestClass]
  public class UnitTest1
  {
    [TestMethod]
    public void Test_Part_1()
    {
      // arrange
      var program = new Program();
      var inputFileName = "TestFiles/input.txt";
      var expectedBlockTiles = 462;

      // act
      Task.Run(() => program.Run(inputFileName)).Wait();
      var blockTiles = program.GameBoard.Where(x => x.Value == TileType.Block).Count();

      // assert
      Assert.AreEqual(blockTiles, expectedBlockTiles);
    }

    [TestMethod]
    public void Test_Part_2()
    {
      // arrange
      var program = new Program();
      var inputFileName = "TestFiles/input_2.txt";

      // act
      Task.Run(() => program.Run(inputFileName)).Wait();

      // assert
      var i = 0;
    }

  }
}
