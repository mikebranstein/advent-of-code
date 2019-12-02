using System.Collections.Generic;
using challenge;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test
{
  [TestClass]
  public class UnitTest1
  {
    [TestMethod]
    public void Can_Ingest_Code_1()
    {
      // arrange
      var program = new Program();

      // act
      var code = program.IngestCode("test_input_1.txt");

      // assert
      Assert.AreEqual(code.Count, 5);
      Assert.AreEqual(code[0], 1);
      Assert.AreEqual(code[1], 1);
      Assert.AreEqual(code[2], 2);
      Assert.AreEqual(code[3], 3);
      Assert.AreEqual(code[4], 99);
    }


    [TestMethod]
    public void Can_Identify_Operation_1()
    {
      // arrange
      var program = new Program();
      var code = program.IngestCode("op_1.txt");

      // act
      var operation = program.ParseOperation(code, 0);

      // assert no exception thrown
      Assert.AreEqual(operation.Type, 1);
      Assert.AreEqual(operation.ReadAddress1, 2);
      Assert.AreEqual(operation.ReadAddress2, 3);
      Assert.AreEqual(operation.OutpotPosition, 4);
    }


    [TestMethod]
    public void Can_Identify_Operation_2()
    {
      // arrange
      var program = new Program();
      var code = program.IngestCode("op_2.txt");

      // act
      var operation = program.ParseOperation(code, 0);

      // assert no exception thrown
      Assert.AreEqual(operation.Type, 2);
      Assert.AreEqual(operation.ReadAddress1, 1);
      Assert.AreEqual(operation.ReadAddress2, 3);
      Assert.AreEqual(operation.OutpotPosition, 4);
    }


    [TestMethod]
    public void Can_Identify_Operation_99()
    {
      // arrange
      var program = new Program();
      var code = program.IngestCode("op_99.txt");

      // act
      var operation = program.ParseOperation(code, 4);

      // assert 
      Assert.AreEqual(operation.Type, 99);
      Assert.IsFalse(operation.ReadAddress1.HasValue);
      Assert.IsFalse(operation.ReadAddress2.HasValue);
      Assert.IsFalse(operation.OutpotPosition.HasValue);
    }

    [TestMethod]
    public void Can_Process_Operation_1_Add_Simple()
    {
      // arrange
      var program = new Program();
      var code = new List<int> { 1, 0, 3, 3, 99 };

      // act
      var operation = program.ParseOperation(code, 0);
      var resultingCode = program.ProcessOperation1(code, operation);

      // assert code not changed
      Assert.AreEqual(resultingCode[0], 1);
      Assert.AreEqual(resultingCode[1], 0);
      Assert.AreEqual(resultingCode[2], 3);
      Assert.AreEqual(resultingCode[3], 4);
      Assert.AreEqual(resultingCode[4], 99);
    }

    [TestMethod]
    public void Can_Process_Operation_1_Add_Chain()
    {
      // arrange
      var program = new Program();
      var code = new List<int> { 1, 3, 2, 5, 1, 0, 0, 1, 99 };

      // act - add one
      var operation = program.ParseOperation(code, 0);
      var resultingCode = program.ProcessOperation1(code, operation);

      // assert 
      Assert.AreEqual(resultingCode[0], 1);
      Assert.AreEqual(resultingCode[1], 3);
      Assert.AreEqual(resultingCode[2], 2);
      Assert.AreEqual(resultingCode[3], 5);
      Assert.AreEqual(resultingCode[4], 1);
      Assert.AreEqual(resultingCode[5], 7);
      Assert.AreEqual(resultingCode[6], 0);
      Assert.AreEqual(resultingCode[7], 1);
      Assert.AreEqual(resultingCode[8], 99);

      // act - second add
      var operation2 = program.ParseOperation(resultingCode, 4);
      var resultingCode2 = program.ProcessOperation1(resultingCode, operation2);

      // assert second operation
      Assert.AreEqual(resultingCode2[0], 1);
      Assert.AreEqual(resultingCode2[1], 2);
      Assert.AreEqual(resultingCode2[2], 2);
      Assert.AreEqual(resultingCode2[3], 5);
      Assert.AreEqual(resultingCode2[4], 1);
      Assert.AreEqual(resultingCode2[5], 7);
      Assert.AreEqual(resultingCode2[6], 0);
      Assert.AreEqual(resultingCode2[7], 1);
      Assert.AreEqual(resultingCode2[8], 99);
    }

    [TestMethod]
    public void Can_Process_Operation_2_Multiply_Simple()
    {
      // arrange
      var program = new Program();
      var code = new List<int> { 2, 0, 3, 5, 99, 0 };

      // act
      var operation = program.ParseOperation(code, 0);
      var resultingCode = program.ProcessOperation2(code, operation);

      // assert code not changed
      Assert.AreEqual(resultingCode[0], 2);
      Assert.AreEqual(resultingCode[1], 0);
      Assert.AreEqual(resultingCode[2], 3);
      Assert.AreEqual(resultingCode[3], 5);
      Assert.AreEqual(resultingCode[4], 99);
      Assert.AreEqual(resultingCode[5], 10);
    }

    [TestMethod]
    public void Can_Process_Operation_2_Multiply_Chain()
    {
      // arrange
      var program = new Program();
      var code = new List<int> { 2, 3, 2, 5, 2, 0, 0, 1, 99, 20, 30};

      // act - add one
      var operation = program.ParseOperation(code, 0);
      var resultingCode = program.ProcessOperation2(code, operation);

      // assert 
      Assert.AreEqual(resultingCode[0], 2);
      Assert.AreEqual(resultingCode[1], 3);
      Assert.AreEqual(resultingCode[2], 2);
      Assert.AreEqual(resultingCode[3], 5);
      Assert.AreEqual(resultingCode[4], 2);
      Assert.AreEqual(resultingCode[5], 10);
      Assert.AreEqual(resultingCode[6], 0);
      Assert.AreEqual(resultingCode[7], 1);
      Assert.AreEqual(resultingCode[8], 99);
      Assert.AreEqual(resultingCode[9], 20);
      Assert.AreEqual(resultingCode[10], 30);

      // act - second add
      var operation2 = program.ParseOperation(resultingCode, 4);
      var resultingCode2 = program.ProcessOperation2(resultingCode, operation2);

      // assert second operation
      Assert.AreEqual(resultingCode2[0], 2);
      Assert.AreEqual(resultingCode2[1], 60);
      Assert.AreEqual(resultingCode2[2], 2);
      Assert.AreEqual(resultingCode2[3], 5);
      Assert.AreEqual(resultingCode2[4], 2);
      Assert.AreEqual(resultingCode2[5], 10);
      Assert.AreEqual(resultingCode2[6], 0);
      Assert.AreEqual(resultingCode2[7], 1);
      Assert.AreEqual(resultingCode2[8], 99);
      Assert.AreEqual(resultingCode2[9], 20);
      Assert.AreEqual(resultingCode2[10], 30);
    }

    [TestMethod]
    public void Can_Process_Multi_Steps_1()
    {
      // arrange
      var program = new Program();
      var code = new List<int> { 2, 3, 2, 5, 2, 0, 0, 1, 99, 20, 30 };

      // act
      var resultingCode = program.ExecuteSteps(code);

      // assert second operation
      Assert.AreEqual(resultingCode[0], 2);
      Assert.AreEqual(resultingCode[1], 60);
      Assert.AreEqual(resultingCode[2], 2);
      Assert.AreEqual(resultingCode[3], 5);
      Assert.AreEqual(resultingCode[4], 2);
      Assert.AreEqual(resultingCode[5], 10);
      Assert.AreEqual(resultingCode[6], 0);
      Assert.AreEqual(resultingCode[7], 1);
      Assert.AreEqual(resultingCode[8], 99);
      Assert.AreEqual(resultingCode[9], 20);
      Assert.AreEqual(resultingCode[10], 30);
    }

    [TestMethod]
    public void Can_Process_Multi_Steps_2()
    {
      // 1,9,10,3,2,3,11,0,99,30,40,50

      // arrange
      var program = new Program();
      var code = new List<int> { 1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50 };

      // act
      var resultingCode = program.ExecuteSteps(code);

      // assert second operation
      Assert.AreEqual(resultingCode[0], 3500);
      Assert.AreEqual(resultingCode[1], 9);
      Assert.AreEqual(resultingCode[2], 10);
      Assert.AreEqual(resultingCode[3], 70);
      Assert.AreEqual(resultingCode[4], 2);
      Assert.AreEqual(resultingCode[5], 3);
      Assert.AreEqual(resultingCode[6], 11);
      Assert.AreEqual(resultingCode[7], 0);
      Assert.AreEqual(resultingCode[8], 99);
      Assert.AreEqual(resultingCode[9], 30);
      Assert.AreEqual(resultingCode[10], 40);
      Assert.AreEqual(resultingCode[11], 50);
    }

    [TestMethod]
    public void Small_Program_1()
    {
      // 1,0,0,0,99

      // arrange
      var program = new Program();
      var code = new List<int> { 1, 0, 0, 0, 99 };

      // act
      var resultingCode = program.ExecuteSteps(code);

      // assert second operation
      // 2,0,0,0,99
      Assert.AreEqual(resultingCode[0], 2);
      Assert.AreEqual(resultingCode[1], 0);
      Assert.AreEqual(resultingCode[2], 0);
      Assert.AreEqual(resultingCode[3], 0);
      Assert.AreEqual(resultingCode[4], 99);
    }

    [TestMethod]
    public void Small_Program_2()
    {
      // 2,3,0,3,99

      // arrange
      var program = new Program();
      var code = new List<int> { 2, 3, 0, 3, 99 };

      // act
      var resultingCode = program.ExecuteSteps(code);

      // assert second operation
      // 2,3,0,6,99
      Assert.AreEqual(resultingCode[0], 2);
      Assert.AreEqual(resultingCode[1], 3);
      Assert.AreEqual(resultingCode[2], 0);
      Assert.AreEqual(resultingCode[3], 6);
      Assert.AreEqual(resultingCode[4], 99);
    }

    [TestMethod]
    public void Small_Program_3()
    {
      // arrange
      var program = new Program();

      // act
      var codeString = program.Run("small_program_3.txt");

      // assert second operation
      // 2,4,4,5,99,9801
      Assert.AreEqual(codeString, "2,4,4,5,99,9801");
    }

    [TestMethod]
    public void Can_Convert_Code_to_String()
    {
      // arrange
      var program = new Program();
      var code = new List<int> { 1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50 };

      // act
      var codeString = program.ConvertCodeToString(code);

      // assert 
      Assert.AreEqual(codeString, "1,9,10,3,2,3,11,0,99,30,40,50");
    }

    [TestMethod]
    public void Small_Program_4()
    {
      // arrange
      var program = new Program();

      // act
      var codeString = program.Run("small_program_4.txt");

      // assert second operation
      // 30,1,1,4,2,5,6,0,99
      Assert.AreEqual(codeString, "30,1,1,4,2,5,6,0,99");
    }
  }


}
