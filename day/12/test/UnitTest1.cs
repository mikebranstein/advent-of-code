using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moons;

namespace test
{
  [TestClass]
  public class UnitTest1
  {
    [TestMethod]
    public void Tick_10_Times()
    {
      // arrange
      var simulation = new Simulation("TestFiles/test_1.txt");

      // After 0 steps:
      // pos =< x = -1, y = 0, z = 2 >, vel =< x = 0, y = 0, z = 0 >
      // pos =< x = 2, y = -10, z = -7 >, vel =< x = 0, y = 0, z = 0 >
      // pos =< x = 4, y = -8, z = 8 >, vel =< x = 0, y = 0, z = 0 >
      // pos =< x = 3, y = 5, z = -1 >, vel =< x = 0, y = 0, z = 0 >
      Assert.AreEqual(simulation.Moons[0].X, -1);
      Assert.AreEqual(simulation.Moons[1].X, 2);
      Assert.AreEqual(simulation.Moons[2].X, 4);
      Assert.AreEqual(simulation.Moons[3].X, 3);
      Assert.AreEqual(simulation.Moons[0].Y, 0);
      Assert.AreEqual(simulation.Moons[1].Y, -10);
      Assert.AreEqual(simulation.Moons[2].Y, -8);
      Assert.AreEqual(simulation.Moons[3].Y, 5);
      Assert.AreEqual(simulation.Moons[0].Z, 2);
      Assert.AreEqual(simulation.Moons[1].Z, -7);
      Assert.AreEqual(simulation.Moons[2].Z, 8);
      Assert.AreEqual(simulation.Moons[3].Z, -1);

      // act
      simulation.Tick(1);

      // assert
      // After 1 step:
      // pos =< x = 2, y = -1, z = 1 >, vel =< x = 3, y = -1, z = -1 >
      // pos =< x = 3, y = -7, z = -4 >, vel =< x = 1, y = 3, z = 3 >
      // pos =< x = 1, y = -7, z = 5 >, vel =< x = -3, y = 1, z = -3 >
      // pos =< x = 2, y = 2, z = 0 >, vel =< x = -1, y = -3, z = 1 >

      Assert.AreEqual(simulation.Moons[0].X, 2);
      Assert.AreEqual(simulation.Moons[1].X, 3);
      Assert.AreEqual(simulation.Moons[2].X, 1);
      Assert.AreEqual(simulation.Moons[3].X, 2);
      Assert.AreEqual(simulation.Moons[0].Y, -1);
      Assert.AreEqual(simulation.Moons[1].Y, -7);
      Assert.AreEqual(simulation.Moons[2].Y, -7);
      Assert.AreEqual(simulation.Moons[3].Y, 2);
      Assert.AreEqual(simulation.Moons[0].Z, 1);
      Assert.AreEqual(simulation.Moons[1].Z, -4);
      Assert.AreEqual(simulation.Moons[2].Z, 5);
      Assert.AreEqual(simulation.Moons[3].Z, 0);

      simulation.Tick(1);

      // After 2 steps:
      // pos =< x = 5, y = -3, z = -1 >, vel =< x = 3, y = -2, z = -2 >
      // pos =< x = 1, y = -2, z = 2 >, vel =< x = -2, y = 5, z = 6 >
      // pos =< x = 1, y = -4, z = -1 >, vel =< x = 0, y = 3, z = -6 >
      // pos =< x = 1, y = -4, z = 2 >, vel =< x = -1, y = -6, z = 2 >
      Assert.AreEqual(simulation.Moons[0].X, 5);
      Assert.AreEqual(simulation.Moons[1].X, 1);
      Assert.AreEqual(simulation.Moons[2].X, 1);
      Assert.AreEqual(simulation.Moons[3].X, 1);
      Assert.AreEqual(simulation.Moons[0].Y, -3);
      Assert.AreEqual(simulation.Moons[1].Y, -2);
      Assert.AreEqual(simulation.Moons[2].Y, -4);
      Assert.AreEqual(simulation.Moons[3].Y, -4);
      Assert.AreEqual(simulation.Moons[0].Z, -1);
      Assert.AreEqual(simulation.Moons[1].Z, 2);
      Assert.AreEqual(simulation.Moons[2].Z, -1);
      Assert.AreEqual(simulation.Moons[3].Z, 2);

      // After 10 steps:
      // pos =< x = 2, y = 1, z = -3 >, vel =< x = -3, y = -2, z = 1 >
      // pos =< x = 1, y = -8, z = 0 >, vel =< x = -1, y = 1, z = 3 >
      // pos =< x = 3, y = -6, z = 1 >, vel =< x = 3, y = 2, z = -3 >
      // pos =< x = 2, y = 0, z = 4 >, vel =< x = 1, y = -1, z = -1 >
      simulation.Tick(8);
      Assert.AreEqual(simulation.Moons[0].X, 2);
      Assert.AreEqual(simulation.Moons[1].X, 1);
      Assert.AreEqual(simulation.Moons[2].X, 3);
      Assert.AreEqual(simulation.Moons[3].X, 2);
      Assert.AreEqual(simulation.Moons[0].Y, 1);
      Assert.AreEqual(simulation.Moons[1].Y, -8);
      Assert.AreEqual(simulation.Moons[2].Y, -6);
      Assert.AreEqual(simulation.Moons[3].Y, 0);
      Assert.AreEqual(simulation.Moons[0].Z, -3);
      Assert.AreEqual(simulation.Moons[1].Z, 0);
      Assert.AreEqual(simulation.Moons[2].Z, 1);
      Assert.AreEqual(simulation.Moons[3].Z, 4);
    }

    [TestMethod]
    public void Tick_10_Times_Total_Energy()
    {
      // arrange
      var simulation = new Simulation("TestFiles/test_1.txt");
      var expectedTotalEnergy = 179;

      // act
      simulation.Tick(10);

      // assert
      Assert.AreEqual(simulation.TotalEnergy, expectedTotalEnergy);
    }

    [TestMethod]
    public void Tick_100_Times_Total_Energy()
    {
      // arrange
      var simulation = new Simulation("TestFiles/test_2.txt");
      var expectedTotalEnergy = 1940;

      // act
      simulation.Tick(100);

      // assert
      Assert.AreEqual(simulation.TotalEnergy, expectedTotalEnergy);
    }

    [TestMethod]
    public void Calc_Repeat_Rate_1()
    {
      // arrange
      var simulation = new Simulation("TestFiles/test_1.txt");
      var expectedRepeatRate = 2772;

      // act
      var repeatRate = simulation.CalculateRepeatRate();

      // assert
      Assert.AreEqual(repeatRate, expectedRepeatRate);
    }

    [TestMethod]
    public void Calc_Repeat_Rate_2()
    {
      // arrange
      var simulation = new Simulation("TestFiles/test_2.txt");
      var expectedRepeatRate = 4686774924;

      // act
      var repeatRate = simulation.CalculateRepeatRate();

      // assert
      Assert.AreEqual(repeatRate, expectedRepeatRate);
    }


    [TestMethod]
    public void Calc_Energy_Change()
    {
      // arrange
      var simulation = new Simulation("TestFiles/test_1.txt");

      // act
      simulation.Tick(1);
      var potentialEnergy = simulation.PotentialEnergy;
      var kineticEnergy = simulation.KineticEnergy;
      var totalEnergy = simulation.TotalEnergy;

      simulation.Tick(1);
      potentialEnergy = simulation.PotentialEnergy;
      kineticEnergy = simulation.KineticEnergy;
      totalEnergy = simulation.TotalEnergy;

      simulation.Tick(1);
      potentialEnergy = simulation.PotentialEnergy;
      kineticEnergy = simulation.KineticEnergy;
      totalEnergy = simulation.TotalEnergy;

      simulation.Tick(1);
      potentialEnergy = simulation.PotentialEnergy;
      kineticEnergy = simulation.KineticEnergy;
      totalEnergy = simulation.TotalEnergy;

      simulation.Tick(1);
      potentialEnergy = simulation.PotentialEnergy;
      kineticEnergy = simulation.KineticEnergy;
      totalEnergy = simulation.TotalEnergy;

      // assert
      Assert.AreEqual(0, 0);
    }





  }
}
