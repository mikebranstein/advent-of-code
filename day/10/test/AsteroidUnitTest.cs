using System.Linq;
using challenge;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test
{
  [TestClass]
  public class AsteroidUnitTest
  {

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
      Assert.AreEqual(asteroidField._quadrant1Points[new Slope(0, 1)].Count, 4);
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
      Assert.AreEqual(asteroidField._quadrant1Points[new Slope(1, 0)].Count, 1);
      Assert.IsTrue(
        asteroidField._quadrant1Points[new Slope(1, 0)]
          .Contains(new Coordinate(0, 1)));

      // .........
      // ....o....
      // .....#...
      Assert.AreEqual(asteroidField._quadrant1Points[new Slope(1, 1)].Count, 1);
      Assert.IsTrue(
        asteroidField._quadrant1Points[new Slope(1, 1)]
          .Contains(new Coordinate(1, 1)));

      // .........
      // ....o....
      // ......#..
      Assert.AreEqual(asteroidField._quadrant1Points[new Slope(1, 2)].Count, 1);
      Assert.IsTrue(
        asteroidField._quadrant1Points[new Slope(1, 2)]
          .Contains(new Coordinate(2, 1)));

      // .........
      // ....o....
      // .......#.
      Assert.AreEqual(asteroidField._quadrant1Points[new Slope(1, 3)].Count, 1);
      Assert.IsTrue(
        asteroidField._quadrant1Points[new Slope(1, 3)]
          .Contains(new Coordinate(3, 1)));

      // .........
      // ....o....
      // ........#
      Assert.AreEqual(asteroidField._quadrant1Points[new Slope(1, 4)].Count, 1);
      Assert.IsTrue(
        asteroidField._quadrant1Points[new Slope(1, 4)]
          .Contains(new Coordinate(4, 1)));

      // .....#...
      // ....o....
      // .........
      Assert.AreEqual(asteroidField._quadrant2Points[new Slope(1, 1)].Count, 1);
      Assert.IsTrue(
        asteroidField._quadrant2Points[new Slope(1, 1)]
          .Contains(new Coordinate(1, -1)));

      // ......#..
      // ....o....
      // .........
      Assert.AreEqual(asteroidField._quadrant2Points[new Slope(1, 2)].Count, 1);
      Assert.IsTrue(
        asteroidField._quadrant2Points[new Slope(1, 2)]
          .Contains(new Coordinate(2, -1)));

      // .......#.
      // ....o....
      // .........
      Assert.AreEqual(asteroidField._quadrant2Points[new Slope(1, 3)].Count, 1);
      Assert.IsTrue(
        asteroidField._quadrant2Points[new Slope(1, 3)]
          .Contains(new Coordinate(3, -1)));

      // ........#
      // ....o....
      // .........
      Assert.AreEqual(asteroidField._quadrant2Points[new Slope(1, 4)].Count, 1);
      Assert.IsTrue(
        asteroidField._quadrant2Points[new Slope(1, 4)]
          .Contains(new Coordinate(4, -1)));

      // ....#....
      // ....o....
      // .........
      Assert.AreEqual(asteroidField._quadrant2Points[new Slope(1, 0)].Count, 1);
      Assert.IsTrue(
        asteroidField._quadrant2Points[new Slope(1, 0)]
          .Contains(new Coordinate(0, -1)));

      // ...#.....
      // ....o....
      // .........
      Assert.AreEqual(asteroidField._quadrant3Points[new Slope(1, 1)].Count, 1);
      Assert.IsTrue(
        asteroidField._quadrant3Points[new Slope(1, 1)]
          .Contains(new Coordinate(-1, -1)));

      // ..#......
      // ....o....
      // .........
      Assert.AreEqual(asteroidField._quadrant3Points[new Slope(1, 2)].Count, 1);
      Assert.IsTrue(
        asteroidField._quadrant3Points[new Slope(1, 2)]
          .Contains(new Coordinate(-2, -1)));

      // .#.......
      // ....o....
      // .........
      Assert.AreEqual(asteroidField._quadrant3Points[new Slope(1, 3)].Count, 1);
      Assert.IsTrue(
        asteroidField._quadrant3Points[new Slope(1, 3)]
          .Contains(new Coordinate(-3, -1)));

      // #........
      // ....o....
      // .........
      Assert.AreEqual(asteroidField._quadrant3Points[new Slope(1, 4)].Count, 1);
      Assert.IsTrue(
        asteroidField._quadrant3Points[new Slope(1, 4)]
          .Contains(new Coordinate(-4, -1)));

      // .........
      // ####o....
      // .........
      Assert.AreEqual(asteroidField._quadrant4Points[new Slope(0, 1)].Count, 4);
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
      Assert.AreEqual(asteroidField._quadrant4Points[new Slope(1, 1)].Count, 1);
      Assert.IsTrue(
        asteroidField._quadrant4Points[new Slope(1, 1)]
          .Contains(new Coordinate(-1, 1)));

      // .........
      // ....o....
      // ..#......
      Assert.AreEqual(asteroidField._quadrant4Points[new Slope(1, 2)].Count, 1);
      Assert.IsTrue(
        asteroidField._quadrant4Points[new Slope(1, 2)]
          .Contains(new Coordinate(-2, 1)));

      // .........
      // ....o....
      // .#.......
      Assert.AreEqual(asteroidField._quadrant4Points[new Slope(1, 3)].Count, 1);
      Assert.IsTrue(
        asteroidField._quadrant4Points[new Slope(1, 3)]
          .Contains(new Coordinate(-3, 1)));

      // .........
      // ....o....
      // #........
      Assert.AreEqual(asteroidField._quadrant4Points[new Slope(1, 4)].Count, 1);
      Assert.IsTrue(
        asteroidField._quadrant4Points[new Slope(1, 4)]
          .Contains(new Coordinate(-4, 1)));
    }

    [TestMethod]
    public void Can_Count_Asteroids_From_0_0_test_2()
    {
      // arrange
      // 0#..#
      // .....
      var inputFileName = "TestFiles/test_2.txt";
      var originX = 0;
      var originY = 0;
      var expectedAsteroidCount = 1;

      // act
      var asteroidField = AsteroidFieldFactory.CreateFromFile(inputFileName);
      var asteroidCount = asteroidField.NumAsteroidsInDirectSight(originX, originY);

      // assert
      Assert.AreEqual(asteroidCount, expectedAsteroidCount);
    }

    [TestMethod]
    public void Can_Count_Asteroids_From_2_1_test_2()
    {
      // arrange
      // .#..#
      // ..o..
      var inputFileName = "TestFiles/test_2.txt";
      var originX = 2;
      var originY = 1;
      var expectedAsteroidCount = 2;

      // act
      var asteroidField = AsteroidFieldFactory.CreateFromFile(inputFileName);
      var asteroidCount = asteroidField.NumAsteroidsInDirectSight(originX, originY);

      // assert
      Assert.AreEqual(asteroidCount, expectedAsteroidCount);
    }

    [TestMethod]
    public void Can_Count_Asteroids_From_4_0_test_2()
    {
      // arrange
      // .#..o
      // .....
      var inputFileName = "TestFiles/test_2.txt";
      var originX = 4;
      var originY = 0;
      var expectedAsteroidCount = 1;

      // act
      var asteroidField = AsteroidFieldFactory.CreateFromFile(inputFileName);
      var asteroidCount = asteroidField.NumAsteroidsInDirectSight(originX, originY);

      // assert
      Assert.AreEqual(asteroidCount, expectedAsteroidCount);
    }

    [TestMethod]
    public void Can_Count_Asteroids_From_1_0_test_1()
    {
      // arrange
      // .o..#
      // .....
      // #####
      // ....#
      // ...##
      var inputFileName = "TestFiles/test_1.txt";
      var originX = 1;
      var originY = 0;
      var expectedAsteroidCount = 7;

      // act
      var asteroidField = AsteroidFieldFactory.CreateFromFile(inputFileName);
      var asteroidCount = asteroidField.NumAsteroidsInDirectSight(originX, originY);

      // assert
      Assert.AreEqual(asteroidCount, expectedAsteroidCount);
    }

    [TestMethod]
    public void Can_Count_Asteroids_From_4_2_test_1()
    {
      // arrange
      // .#..#
      // .....
      // ####o
      // ....#
      // ...##
      var inputFileName = "TestFiles/test_1.txt";
      var originX = 4;
      var originY = 2;
      var expectedAsteroidCount = 5;

      // act
      var asteroidField = AsteroidFieldFactory.CreateFromFile(inputFileName);
      var asteroidCount = asteroidField.NumAsteroidsInDirectSight(originX, originY);

      // assert
      Assert.AreEqual(asteroidCount, expectedAsteroidCount);
    }

    [TestMethod]
    public void Can_Find_Best_Viewing_Location_test_1()
    {
      // arrange
      // .#..#
      // .....
      // #####
      // ....#
      // ...##
      var inputFileName = "TestFiles/test_1.txt";
      var expectedCoordinate = new Coordinate(3, 4);
      var expectedAsteroidCount = 8;

      // act
      var asteroidField = AsteroidFieldFactory.CreateFromFile(inputFileName);
      var coordinate = asteroidField.FindBestViewingLocation();
      var asteroidCount = asteroidField.NumAsteroidsInDirectSight(coordinate.X, coordinate.Y);

      // assert
      Assert.AreEqual(coordinate, expectedCoordinate);
      Assert.AreEqual(asteroidCount, expectedAsteroidCount);
    }

    [TestMethod]
    public void Can_Find_Best_Viewing_Location_test_3()
    {
      // arrange
      // .#..#
      // .....
      // #####
      // ....#
      // ...##
      var inputFileName = "TestFiles/test_3.txt";
      var expectedCoordinate = new Coordinate(5, 8);
      var expectedAsteroidCount = 33;

      // act
      var asteroidField = AsteroidFieldFactory.CreateFromFile(inputFileName);
      var coordinate = asteroidField.FindBestViewingLocation();
      var asteroidCount = asteroidField.NumAsteroidsInDirectSight(coordinate.X, coordinate.Y);

      // assert
      Assert.AreEqual(coordinate, expectedCoordinate);
      Assert.AreEqual(asteroidCount, expectedAsteroidCount);
    }

    [TestMethod]
    public void Can_Find_Best_Viewing_Location_test_4()
    {
      // arrange
      // .#..#
      // .....
      // #####
      // ....#
      // ...##
      var inputFileName = "TestFiles/test_4.txt";
      var expectedCoordinate = new Coordinate(1, 2);
      var expectedAsteroidCount = 35;

      // act
      var asteroidField = AsteroidFieldFactory.CreateFromFile(inputFileName);
      var coordinate = asteroidField.FindBestViewingLocation();
      var asteroidCount = asteroidField.NumAsteroidsInDirectSight(coordinate.X, coordinate.Y);

      // assert
      Assert.AreEqual(coordinate, expectedCoordinate);
      Assert.AreEqual(asteroidCount, expectedAsteroidCount);
    }

    [TestMethod]
    public void Can_Find_Best_Viewing_Location_test_5()
    {
      // arrange
      // .#..#
      // .....
      // #####
      // ....#
      // ...##
      var inputFileName = "TestFiles/test_5.txt";
      var expectedCoordinate = new Coordinate(6, 3);
      var expectedAsteroidCount = 41;

      // act
      var asteroidField = AsteroidFieldFactory.CreateFromFile(inputFileName);
      var coordinate = asteroidField.FindBestViewingLocation();
      var asteroidCount = asteroidField.NumAsteroidsInDirectSight(coordinate.X, coordinate.Y);

      // assert
      Assert.AreEqual(coordinate, expectedCoordinate);
      Assert.AreEqual(asteroidCount, expectedAsteroidCount);
    }

    [TestMethod]
    public void Can_Find_Best_Viewing_Location_test_6()
    {
      // arrange
      // .#..#
      // .....
      // #####
      // ....#
      // ...##
      var inputFileName = "TestFiles/test_6.txt";
      var expectedCoordinate = new Coordinate(11, 13);
      var expectedAsteroidCount = 210;
    
      // act
      var asteroidField = AsteroidFieldFactory.CreateFromFile(inputFileName);
      var coordinate = asteroidField.FindBestViewingLocation();
      var asteroidCount = asteroidField.NumAsteroidsInDirectSight(coordinate.X, coordinate.Y);
    
      // assert
      Assert.AreEqual(coordinate, expectedCoordinate);
      Assert.AreEqual(asteroidCount, expectedAsteroidCount);
    }


    [TestMethod]
    public void Can_Find_Best_Viewing_Location_part_1()
    {
      // arrange
      // .#..#
      // .....
      // #####
      // ....#
      // ...##
      var inputFileName = "TestFiles/input.txt";
      var expectedCoordinate = new Coordinate(20, 21);
      var expectedAsteroidCount = 247;
    
      // act
      var asteroidField = AsteroidFieldFactory.CreateFromFile(inputFileName);
      var coordinate = asteroidField.FindBestViewingLocation();
      var asteroidCount = asteroidField.NumAsteroidsInDirectSight(coordinate.X, coordinate.Y);
    
      // assert
      Assert.AreEqual(coordinate, expectedCoordinate);
      Assert.AreEqual(asteroidCount, expectedAsteroidCount);
    }

    [TestMethod]
    public void Can_Calculate_Destruction_Order_part_2()
    {
      // arrange
      var inputFileName = "TestFiles/test_6.txt";
    
      // act
      var asteroidField = AsteroidFieldFactory.CreateFromFile(inputFileName);
      var destructionOrder = asteroidField.GetDestructionOrder(11, 13);

      // assert
      //The 1st asteroid to be vaporized is at 11,12.
      //The 2nd asteroid to be vaporized is at 12,1.
      //The 3rd asteroid to be vaporized is at 12,2.
      //The 10th asteroid to be vaporized is at 12,8.
      //The 20th asteroid to be vaporized is at 16,0.
      //The 50th asteroid to be vaporized is at 16,9.
      //The 100th asteroid to be vaporized is at 10,16.
      //The 199th asteroid to be vaporized is at 9,6.
      //The 200th asteroid to be vaporized is at 8,2.
      //The 201st asteroid to be vaporized is at 10,9.
      //The 299th and final asteroid to be vaporized is at 11,1.
      Assert.AreEqual(destructionOrder[0].X, 11);
      Assert.AreEqual(destructionOrder[0].Y, 12);

      Assert.AreEqual(destructionOrder[1].X, 12);
      Assert.AreEqual(destructionOrder[1].Y, 1);

      Assert.AreEqual(destructionOrder[2].X, 12);
      Assert.AreEqual(destructionOrder[2].Y, 2);

      Assert.AreEqual(destructionOrder[9].X, 12);
      Assert.AreEqual(destructionOrder[9].Y, 8);

      Assert.AreEqual(destructionOrder[19].X, 16);
      Assert.AreEqual(destructionOrder[19].Y, 0);

      Assert.AreEqual(destructionOrder[49].X, 16);
      Assert.AreEqual(destructionOrder[49].Y, 9);

      Assert.AreEqual(destructionOrder[99].X, 10);
      Assert.AreEqual(destructionOrder[99].Y, 16);

      Assert.AreEqual(destructionOrder[198].X, 9);
      Assert.AreEqual(destructionOrder[198].Y, 6);

      Assert.AreEqual(destructionOrder[199].X, 8);
      Assert.AreEqual(destructionOrder[199].Y, 2);

      Assert.AreEqual(destructionOrder[200].X, 10);
      Assert.AreEqual(destructionOrder[200].Y, 9);

      Assert.AreEqual(destructionOrder[298].X, 11);
      Assert.AreEqual(destructionOrder[298].Y, 1);
    }

    [TestMethod]
    public void Can_Calculate_Degrees_Offset_UP_1()
    {
      // arrange
      var origin = new Coordinate(2, 3);
      var asteroid = new Coordinate(2, 2);
      var inputFileName = "TestFiles/test_2.txt";

      // act
      var asteroidField = AsteroidFieldFactory.CreateFromFile(inputFileName);
      var degreesOffset = asteroidField.GetDegreesOffset(origin.X, origin.Y, asteroid.X, asteroid.Y);

      // assert
      Assert.AreEqual(degreesOffset, 0);
    }

    [TestMethod]
    public void Can_Calculate_Degrees_Offset_UP_2()
    {
      // arrange
      var origin = new Coordinate(2, 3);
      var asteroid = new Coordinate(2, -5);
      var inputFileName = "TestFiles/test_2.txt";

      // act
      var asteroidField = AsteroidFieldFactory.CreateFromFile(inputFileName);
      var degreesOffset = asteroidField.GetDegreesOffset(origin.X, origin.Y, asteroid.X, asteroid.Y);

      // assert
      Assert.AreEqual(degreesOffset, 0);
    }

    [TestMethod]
    public void Can_Calculate_Degrees_Offset_45_2()
    {
      // arrange
      var origin = new Coordinate(2, 3);
      var asteroid = new Coordinate(3, 2);
      var inputFileName = "TestFiles/test_2.txt";
      var expectedDegreesOffset = 45.0;

      // act
      var asteroidField = AsteroidFieldFactory.CreateFromFile(inputFileName);
      var degreesOffset = asteroidField.GetDegreesOffset(origin.X, origin.Y, asteroid.X, asteroid.Y);

      // assert
      Assert.AreEqual(degreesOffset, expectedDegreesOffset);
    }

  }
}
