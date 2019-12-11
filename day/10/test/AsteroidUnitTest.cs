using challenge;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test
{
  [TestClass]
  public class AsteroidUnitTest
  {

    //[TestMethod]
    //public void Can_Generate_Slopes_1()
    //{
    //  // arrange
    //  var inputFileName = "TestFiles/test_1.txt";
    //
    //  // act
    //  var asteroidField = AsteroidFieldFactory.CreateFromFile(inputFileName);
    //
    //  // assert
    //  // .#..#
    //  // .....
    //  Assert.AreEqual(asteroidField._slopes.Count, 6);
    //}


    [TestMethod]
    public void Can_Generate_Slopes_2()
    {
      // arrange
      var inputFileName = "TestFiles/test_2.txt";

      // act
      var asteroidField = AsteroidFieldFactory.CreateFromFile(inputFileName);

      // assert
      // .#..#
      // .....
      Assert.AreEqual(asteroidField._slopes.Count, 6);
    }

    [TestMethod]
    public void Can_Generate_Slope_Coordinates_2()
    {
      // arrange
      var inputFileName = "TestFiles/test_2.txt";

      // act
      var asteroidField = AsteroidFieldFactory.CreateFromFile(inputFileName);

      // assert
      // .........
      // ....o####
      // .........
      Assert.IsTrue(
        asteroidField._quadrant1Points[new Slope(0, 1)]
          .Contains(new Coordinate(1, 0)));
      Assert.IsTrue(
        asteroidField._quadrant1Points[new Slope(0, 1)]
          .Contains(new Coordinate(2, 0)));
      Assert.IsTrue(
        asteroidField._quadrant1Points[new Slope(0, 1)]
          .Contains(new Coordinate(3, 0)));
      Assert.IsTrue(
        asteroidField._quadrant1Points[new Slope(0, 1)]
          .Contains(new Coordinate(4, 0)));

      // .........
      // ....o....
      // ....#....
      Assert.IsTrue(
        asteroidField._quadrant1Points[new Slope(1, 0)]
          .Contains(new Coordinate(0, 1)));

      // .........
      // ....o....
      // .....#...
      Assert.IsTrue(
        asteroidField._quadrant1Points[new Slope(1, 1)]
          .Contains(new Coordinate(1, 1)));

      // .........
      // ....o....
      // ......#..
      Assert.IsTrue(
        asteroidField._quadrant1Points[new Slope(1, 2)]
          .Contains(new Coordinate(2, 1)));

      // .........
      // ....o....
      // .......#.
      Assert.IsTrue(
        asteroidField._quadrant1Points[new Slope(1, 3)]
          .Contains(new Coordinate(3, 1)));

      // .........
      // ....o....
      // ........#
      Assert.IsTrue(
        asteroidField._quadrant1Points[new Slope(1, 4)]
          .Contains(new Coordinate(4, 1)));

      // .....#...
      // ....o....
      // .........
      Assert.IsTrue(
        asteroidField._quadrant2Points[new Slope(1, 1)]
          .Contains(new Coordinate(1, -1)));

      // ......#..
      // ....o....
      // .........
      Assert.IsTrue(
        asteroidField._quadrant2Points[new Slope(1, 2)]
          .Contains(new Coordinate(2, -1)));

      // .......#.
      // ....o....
      // .........
      Assert.IsTrue(
        asteroidField._quadrant2Points[new Slope(1, 3)]
          .Contains(new Coordinate(3, -1)));

      // ........#
      // ....o....
      // .........
      Assert.IsTrue(
        asteroidField._quadrant2Points[new Slope(1, 4)]
          .Contains(new Coordinate(4, -1)));

      // ....#....
      // ....o....
      // .........
      Assert.IsTrue(
        asteroidField._quadrant2Points[new Slope(1, 0)]
          .Contains(new Coordinate(0, -1)));

      // ...#.....
      // ....o....
      // .........
      Assert.IsTrue(
        asteroidField._quadrant3Points[new Slope(1, 1)]
          .Contains(new Coordinate(-1, -1)));

      // ..#......
      // ....o....
      // .........
      Assert.IsTrue(
        asteroidField._quadrant3Points[new Slope(1, 2)]
          .Contains(new Coordinate(-2, -1)));

      // .#.......
      // ....o....
      // .........
      Assert.IsTrue(
        asteroidField._quadrant3Points[new Slope(1, 3)]
          .Contains(new Coordinate(-3, -1)));

      // #........
      // ....o....
      // .........
      Assert.IsTrue(
        asteroidField._quadrant3Points[new Slope(1, 4)]
          .Contains(new Coordinate(-4, -1)));

      // .........
      // ####o....
      // .........
      Assert.IsTrue(
        asteroidField._quadrant4Points[new Slope(0, 1)]
          .Contains(new Coordinate(-1, 0)));
      Assert.IsTrue(
        asteroidField._quadrant4Points[new Slope(0, 1)]
          .Contains(new Coordinate(-2, 0)));
      Assert.IsTrue(
        asteroidField._quadrant4Points[new Slope(0, 1)]
          .Contains(new Coordinate(-3, 0)));
      Assert.IsTrue(
        asteroidField._quadrant4Points[new Slope(0, 1)]
          .Contains(new Coordinate(-4, 0)));

      // .........
      // ....o....
      // ...#.....
      Assert.IsTrue(
        asteroidField._quadrant4Points[new Slope(1, 1)]
          .Contains(new Coordinate(-1, 1)));

      // .........
      // ....o....
      // ..#......
      Assert.IsTrue(
        asteroidField._quadrant4Points[new Slope(1, 2)]
          .Contains(new Coordinate(-2, 1)));

      // .........
      // ....o....
      // .#.......
      Assert.IsTrue(
        asteroidField._quadrant4Points[new Slope(1, 3)]
          .Contains(new Coordinate(-3, 1)));

      // .........
      // ....o....
      // #........
      Assert.IsTrue(
        asteroidField._quadrant4Points[new Slope(1, 4)]
          .Contains(new Coordinate(-4, 1)));
    }

  }
}
