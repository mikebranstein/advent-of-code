using System.Collections.Generic;
using Amplification;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test
{
  [TestClass]
  public class AmplifierUnitTest
  {
    [TestMethod]
    public void Amplify_Signal_1()
    {
      // arrange
      var amplificationCircuit = new AmplificationCircuit(5);
      var inputFileName = "TestFiles/amplification_program_1.txt";
      var phaseSettings = new List<int>() { 4, 3, 2, 1, 0 };
      var expectedOutputSignal = 43210;

      // act
      var outputSignal = amplificationCircuit.Amplify(inputFileName, phaseSettings, 0);

      // assert
      Assert.AreEqual(outputSignal, expectedOutputSignal);
    }

    [TestMethod]
    public void Amplify_Signal_2()
    {
      // arrange
      var amplificationCircuit = new AmplificationCircuit(5);
      var inputFileName = "TestFiles/amplification_program_2.txt";
      var phaseSettings = new List<int>() { 0, 1, 2, 3, 4 };
      var expectedOutputSignal = 54321;

      // act
      var outputSignal = amplificationCircuit.Amplify(inputFileName, phaseSettings, 0);

      // assert
      Assert.AreEqual(outputSignal, expectedOutputSignal);
    }


    [TestMethod]
    public void Amplify_Signal_3()
    {
      // arrange
      var amplificationCircuit = new AmplificationCircuit(5);
      var inputFileName = "TestFiles/amplification_program_3.txt";
      var phaseSettings = new List<int>() { 1, 0, 4, 3, 2 };
      var expectedOutputSignal = 65210;

      // act
      var outputSignal = amplificationCircuit.Amplify(inputFileName, phaseSettings, 0);

      // assert
      Assert.AreEqual(outputSignal, expectedOutputSignal);
    }

    [TestMethod]
    public void Max_Signal_Calculation_1()
    {
      // arrange
      var amplificationTester = new AmplificationTester();
    
      var inputFileName = "TestFiles/amplification_program_1.txt";
      var expectedPhaseSettings = new List<int>() { 4, 3, 2, 1, 0 };
      var expectedOutputSignal = 43210;
    
      // act
      var output = amplificationTester.CalculateMaxOutputSetting(inputFileName);
    
      // assert
      Assert.AreEqual(output.maxOutputSetting, expectedOutputSignal);
      Assert.AreEqual(output.maxPhaseSettings[0], expectedPhaseSettings[0]);
      Assert.AreEqual(output.maxPhaseSettings[1], expectedPhaseSettings[1]);
      Assert.AreEqual(output.maxPhaseSettings[2], expectedPhaseSettings[2]);
      Assert.AreEqual(output.maxPhaseSettings[3], expectedPhaseSettings[3]);
      Assert.AreEqual(output.maxPhaseSettings[4], expectedPhaseSettings[4]);
    }

    [TestMethod]
    public void Max_Signal_Calculation_2()
    {
      // arrange
      var amplificationTester = new AmplificationTester();

      var inputFileName = "TestFiles/amplification_program_2.txt";
      var expectedPhaseSettings = new List<int>() { 0, 1, 2, 3, 4 };
      var expectedOutputSignal = 54321;

      // act
      var output = amplificationTester.CalculateMaxOutputSetting(inputFileName);

      // assert
      Assert.AreEqual(output.maxOutputSetting, expectedOutputSignal);
      Assert.AreEqual(output.maxPhaseSettings[0], expectedPhaseSettings[0]);
      Assert.AreEqual(output.maxPhaseSettings[1], expectedPhaseSettings[1]);
      Assert.AreEqual(output.maxPhaseSettings[2], expectedPhaseSettings[2]);
      Assert.AreEqual(output.maxPhaseSettings[3], expectedPhaseSettings[3]);
      Assert.AreEqual(output.maxPhaseSettings[4], expectedPhaseSettings[4]);
    }

    [TestMethod]
    public void Max_Signal_Calculation_3()
    {
      // arrange
      var amplificationTester = new AmplificationTester();

      var inputFileName = "TestFiles/amplification_program_3.txt";
      var expectedPhaseSettings = new List<int>() { 1, 0, 4, 3, 2 };
      var expectedOutputSignal = 65210;

      // act
      var output = amplificationTester.CalculateMaxOutputSetting(inputFileName);

      // assert
      Assert.AreEqual(output.maxOutputSetting, expectedOutputSignal);
      Assert.AreEqual(output.maxPhaseSettings[0], expectedPhaseSettings[0]);
      Assert.AreEqual(output.maxPhaseSettings[1], expectedPhaseSettings[1]);
      Assert.AreEqual(output.maxPhaseSettings[2], expectedPhaseSettings[2]);
      Assert.AreEqual(output.maxPhaseSettings[3], expectedPhaseSettings[3]);
      Assert.AreEqual(output.maxPhaseSettings[4], expectedPhaseSettings[4]);
    }


  }
}
