using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrbitMap;

namespace test
{
  [TestClass]
  public class MergeUnitTest
  {
    [TestMethod]
    public void Simple_Merge()
    {
      // arrange
      // COM)B
      var orbit1 = new OrbitTree(new CelestialObject("COM", 0));
      orbit1.AddChild("B");

      // B)C
      var orbit2 = new OrbitTree(new CelestialObject("B", 0));
      orbit2.AddChild("C");

      var expectedDistance = 3;

      // act
      // should be COM)B)C 
      orbit1.Merge(orbit2);      

      // assert
      Assert.AreEqual(orbit1.GetObjectDistances(), expectedDistance);
    }


    [TestMethod]
    public void Merge_Children_1()
    {
      // arrange
      // COM)B)C)D
      // B)E
      var orbit1 = new OrbitTree(new CelestialObject("COM", 0));
      orbit1.AddChild("B");
      orbit1.Search("B").AddChild("C");
      orbit1.Search("B").AddChild("E");
      orbit1.Search("C").AddChild("D");

      // B)F)G
      var orbit2 = new OrbitTree(new CelestialObject("B", 0));
      orbit2.AddChild("F");
      orbit2.Search("F").AddChild("G");

      var expectedDistance = 13;

      // act
      // should be COM)B)C)D
      // B)E
      // B)F)G
      orbit1.Merge(orbit2);

      // assert
      Assert.AreEqual(orbit1.GetObjectDistances(), expectedDistance);
    }

    [TestMethod]
    public void Complex_Merge_1()
    {
      // arrange
      // 1. COM)A)B)C)D)E
      var orbit1 = new OrbitTree(new CelestialObject("COM", 0));
      orbit1.AddChild("A");
      orbit1.Search("A").AddChild("B");
      orbit1.Search("B").AddChild("C");
      orbit1.Search("C").AddChild("D");
      orbit1.Search("D").AddChild("E");

      // 2. COM)F)G)H
      var orbit2 = new OrbitTree(new CelestialObject("COM", 0));
      orbit2.AddChild("F");
      orbit2.Search("F").AddChild("G");
      orbit2.Search("G").AddChild("H");

      // 3. COM)I)J)K
      var orbit3 = new OrbitTree(new CelestialObject("COM", 0));
      orbit3.AddChild("I");
      orbit3.Search("I").AddChild("J");
      orbit3.Search("J").AddChild("K");

      // 4. J)O)P
      var orbit4 = new OrbitTree(new CelestialObject("J", 0));
      orbit4.AddChild("O");
      orbit4.Search("O").AddChild("P");

      // 5. J)L)M
      var orbit5 = new OrbitTree(new CelestialObject("J", 0));
      orbit5.AddChild("L");
      orbit5.Search("L").AddChild("M");

      // 6. J)N
      var orbit6 = new OrbitTree(new CelestialObject("J", 0));
      orbit6.AddChild("N");

      // 7. B)Q
      var orbit7 = new OrbitTree(new CelestialObject("B", 0));
      orbit7.AddChild("Q");

      // 8. B)R
      var orbit8 = new OrbitTree(new CelestialObject("B", 0));
      orbit8.AddChild("R");

      // 9. B)S
      var orbit9 = new OrbitTree(new CelestialObject("B", 0));
      orbit9.AddChild("S");

      // 10. B)T
      var orbit10 = new OrbitTree(new CelestialObject("B", 0));
      orbit10.AddChild("T");

      var expectedDistance = 56;

      // act
      orbit1.Merge(orbit2);
      orbit1.Merge(orbit3);
      orbit1.Merge(orbit4);
      orbit1.Merge(orbit5);
      orbit1.Merge(orbit6);
      orbit1.Merge(orbit7);
      orbit1.Merge(orbit8);
      orbit1.Merge(orbit9);
      orbit1.Merge(orbit10);

      // assert
      Assert.AreEqual(orbit1.GetObjectDistances(), expectedDistance);
    }

    [TestMethod]
    public void Merge_Is_Successful()
    {
      // arrange
      // COM)B
      var orbit1 = new OrbitTree(new CelestialObject("COM", 0));
      orbit1.AddChild("B");

      // B)C
      var orbit2 = new OrbitTree(new CelestialObject("B", 0));
      orbit2.AddChild("C");

      // act
      // should be COM)B)C 
      var mergeSuccesful = orbit1.Merge(orbit2);

      // assert
      Assert.AreEqual(mergeSuccesful, true);
    }

    [TestMethod]
    public void Merge_Is_Not_Successful()
    {
      // arrange
      // COM)B
      var orbit1 = new OrbitTree(new CelestialObject("COM", 0));
      orbit1.AddChild("B");

      // B)C
      var orbit2 = new OrbitTree(new CelestialObject("E", 0));
      orbit2.AddChild("C");

      // act
      var mergeSuccesful = orbit1.Merge(orbit2);

      // assert
      Assert.AreEqual(mergeSuccesful, false);
    }
  }
}
