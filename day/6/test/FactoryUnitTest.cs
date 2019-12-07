using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrbitMap;

namespace test
{
  [TestClass]
  public class FactoryUnitTest
  {
    [TestMethod]
    public void Distances_From_File_1()
    {
      // arrange
      var inputFileName = "TestFiles/test_1.txt";
      var expectedDistance = 3;

      // act
      var map = MapFactory.Create(inputFileName);

      // assert
      Assert.AreEqual(map.GetObjectDistances(), expectedDistance);
    }

    [TestMethod]
    public void Distances_From_File_2()
    {
      // arrange
      var inputFileName = "TestFiles/test_2.txt";
      var expectedDistance = 13;

      // act
      var map = MapFactory.Create(inputFileName);

      // assert
      Assert.AreEqual(map.GetObjectDistances(), expectedDistance);
    }

    [TestMethod]
    public void Distances_From_File_3()
    {
      // arrange
      var inputFileName = "TestFiles/test_3.txt";
      var expectedDistance = 56;

      // act
      var map = MapFactory.Create(inputFileName);

      // assert
      Assert.AreEqual(map.GetObjectDistances(), expectedDistance);
    }

    [TestMethod]
    public void Part_1_Distance()
    {
      // arrange
      var inputFileName = "TestFiles/input.txt";
      var expectedDistance = 295936;

      // act
      var map = MapFactory.Create(inputFileName);

      // assert
      Assert.AreEqual(map.GetObjectDistances(), expectedDistance);
    }

  }
}
