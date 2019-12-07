using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrbitMap;

namespace test
{
  [TestClass]
  public class OrbitalTransferUnitTest
  {
    [TestMethod]
    public void Distances_From_File_1()
    {
      // arrange
      var inputFileName = "TestFiles/orbital_transfer_1.txt";
      var expectedTransferCount = 4;

      // act
      var map = MapFactory.Create(inputFileName);
      var transferCount = map.GetOrbitalTransferCount("YOU", "SAN");

      // assert
      Assert.AreEqual(transferCount, expectedTransferCount);
    }


    [TestMethod]
    public void Distances_From_File_2()
    {
      // arrange
      var inputFileName = "TestFiles/orbital_transfer_2.txt";
      var expectedTransferCount = 0;

      // act
      var map = MapFactory.Create(inputFileName);
      var transferCount = map.GetOrbitalTransferCount("YOU", "SAN");

      // assert
      Assert.AreEqual(transferCount, expectedTransferCount);
    }


  }
}
