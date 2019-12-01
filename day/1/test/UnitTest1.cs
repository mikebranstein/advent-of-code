using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace test
{
  [TestClass]
  public class UnitTest1
  {
    [TestMethod]
    public void CanRun()
    {
      // arrange
      var program = new challenge.Program();

      // act
      program.ExecuteProgram(String.Empty);

      // assert
    }

    [TestMethod]
    public void Mass_12_returns_2()
    {
      // arrange
      var program = new challenge.Program();
      var mass = 12;

      // act
      var fuel = program.CalculateFuel(mass);

      // assert
      Assert.AreEqual(fuel, 2);
    }

    [TestMethod]
    public void Mass_14_returns_2()
    {
      // arrange
      var program = new challenge.Program();
      var mass = 14;

      // act
      var fuel = program.CalculateFuel(mass);

      // assert
      Assert.AreEqual(fuel, 2);
    }

    [TestMethod]
    public void Mass_1969_returns_654()
    {
      // arrange
      var program = new challenge.Program();
      var mass = 1969;

      // act
      var fuel = program.CalculateFuel(mass);

      // assert
      Assert.AreEqual(fuel, 654);
    }

    [TestMethod]
    public void Mass_100756_returns_33583()
    {
      // arrange
      var program = new challenge.Program();
      var mass = 100756;

      // act
      var fuel = program.CalculateFuel(mass);

      // assert
      Assert.AreEqual(fuel, 33583);
    }

    [TestMethod]
    public void Recursive_Mass_14_returns_2()
    {
      // arrange
      var program = new challenge.Program();
      var mass = 14;

      // act
      var fuel = program.CalculateRecursiveFuel(mass);

      // assert
      Assert.AreEqual(fuel, 2);
    }

    [TestMethod]
    public void Recursive_Mass_1969_returns_966()
    {
      // arrange
      var program = new challenge.Program();
      var mass = 1969;

      // act
      var fuel = program.CalculateRecursiveFuel(mass);

      // assert
      Assert.AreEqual(fuel, 966);
    }

    [TestMethod]
    public void Recursive_Mass_100756_returns_50346()
    {
      // arrange
      var program = new challenge.Program();
      var mass = 100756;

      // act
      var fuel = program.CalculateRecursiveFuel(mass);

      // assert
      Assert.AreEqual(fuel, 50346);
    }
  }
}
