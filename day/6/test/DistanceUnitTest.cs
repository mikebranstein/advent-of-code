using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrbitMap;

namespace test
{
  [TestClass]
  public class DistanceUnitTest
  {
    [TestMethod]
    public void Calculates_Simple_Distance_1()
    {
      // arrange
      var map = new OrbitTree(new CelestialObject("COM", 0));
      map.AddChild("B");
      var expectedDistance = 1;

      // act
      var totalDistance = 0;
      map.Traverse(x => totalDistance += x.OrbitDistance);

      // assert
      Assert.AreEqual(totalDistance, expectedDistance);
    }

    [TestMethod]
    public void Calculates_Simple_Distance_2()
    {
      // arrange
      // COM)B)C
      var map = new OrbitTree(new CelestialObject("COM", 0));
      map.AddChild("B");
      map.GetChild("B").AddChild("C");
      var expectedDistance = 3;

      // act
      var totalDistance = 0;
      map.Traverse(x => totalDistance += x.OrbitDistance);

      // assert
      Assert.AreEqual(totalDistance, expectedDistance);
    }

    [TestMethod]
    public void Calculates_Simple_Distance_3()
    {
      // arrange
      // COM)B)C)D
      // 
      var map = new OrbitTree(new CelestialObject("COM", 0));
      map.AddChild("B");
      map.GetChild("B").AddChild("C");
      map.GetChild("B").GetChild("C").AddChild("D");
      var expectedDistance = 6;

      // act
      var totalDistance = 0;
      map.Traverse(x => totalDistance += x.OrbitDistance);

      // assert
      Assert.AreEqual(totalDistance, expectedDistance);
    }

    [TestMethod]
    public void Calculates_Simple_Distance_4()
    {
      // arrange
      // COM)B)C)D
      // B)E
      var map = new OrbitTree(new CelestialObject("COM", 0));
      map.AddChild("B");
      map.GetChild("B").AddChild("C");
      map.GetChild("B").AddChild("E");
      map.GetChild("B").GetChild("C").AddChild("D");
      var expectedDistance = 8;

      // act
      var totalDistance = map.GetObjectDistances();

      // assert
      Assert.AreEqual(totalDistance, expectedDistance);
    }

    [TestMethod]
    public void Calculates_Simple_Distance_With_Search()
    {
      // arrange
      // COM)B)C)D
      // B)E
      var map = new OrbitTree(new CelestialObject("COM", 0));
      map.AddChild("B");
      map.Search("B").AddChild("C");
      map.Search("B").AddChild("E");
      map.Search("C").AddChild("D");
      var expectedDistance = 8;

      // act
      var totalDistance = map.GetObjectDistances();

      // assert
      Assert.AreEqual(totalDistance, expectedDistance);
    }


  }
}
