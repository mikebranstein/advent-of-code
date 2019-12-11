using challenge;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test
{
  [TestClass]
  public class AsteroidFactoryUnitTest
  {
    [TestMethod]
    public void Can_Parse_1()
    {
      // arrange
      var inputFileName = "TestFiles/test_1.txt";

      // act
      var asteroidField = AsteroidFieldFactory.CreateFromFile(inputFileName);
      var field = asteroidField.ToStringArray();

      // assert
      // .#..#
      // .....
      // #####
      // ....#
      // ...##
      Assert.AreEqual(field.Length, 5);
      Assert.AreEqual(field[0], ".#..#");
      Assert.AreEqual(field[1], ".....");
      Assert.AreEqual(field[2], "#####");
      Assert.AreEqual(field[3], "....#");
      Assert.AreEqual(field[4], "...##");
    }

    [TestMethod]
    public void Can_Parse_2()
    {
      // arrange
      var inputFileName = "TestFiles/test_2.txt";

      // act
      var asteroidField = AsteroidFieldFactory.CreateFromFile(inputFileName);
      var field = asteroidField.ToStringArray();

      // assert
      // .#..#
      // .....
      Assert.AreEqual(field.Length, 2);
      Assert.AreEqual(field[0], ".#..#");
      Assert.AreEqual(field[1], ".....");
    }

  }
}
