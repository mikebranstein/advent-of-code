using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureContainer;
using challenge;

namespace test
{
  [TestClass]
  public class PasswordUnitTest
  {
    [TestMethod]
    public void Can_Detect_Repeating_Digits_10_to_30()
    {
      // arrange
      var password = new Password();
      var possiblePasswords = password.GetPasswordRange(10, 30);

      // act
      var result = possiblePasswords.FindAll(x => Password.hasRepeatingDigits(x));

      // assert
      Assert.AreEqual(result.Count, 2);
      Assert.IsTrue(result.Exists(x => x == 11));
      Assert.IsTrue(result.Exists(x => x == 22));
    }

    [TestMethod]
    public void Can_Detect_Repeating_Digits_532_to_532()
    {
      // arrange
      var password = new Password();
      var possiblePasswords = password.GetPasswordRange(532, 532);

      // act
      var result = possiblePasswords.FindAll(x => Password.hasRepeatingDigits(x));

      // assert
      Assert.AreEqual(result.Count, 0);
    }

    [TestMethod]
    public void Can_Detect_Repeating_Digits_533_to_533()
    {
      // arrange
      var password = new Password();
      var possiblePasswords = password.GetPasswordRange(533, 533);

      // act
      var result = possiblePasswords.FindAll(x => Password.hasRepeatingDigits(x));

      // assert
      Assert.AreEqual(result.Count, 1);
    }

    [TestMethod]
    public void Can_Detect_Repeating_Digits_500_to_600()
    {
      // arrange
      var password = new Password();
      var possiblePasswords = password.GetPasswordRange(500, 600);

      // act
      var result = possiblePasswords.FindAll(x => Password.hasRepeatingDigits(x));

      // assert
      Assert.AreEqual(result.Count, 20);
      Assert.IsTrue(result.Exists(x => x == 500));
      Assert.IsTrue(result.Exists(x => x == 511));
      Assert.IsTrue(result.Exists(x => x == 522));
      Assert.IsTrue(result.Exists(x => x == 533));
      Assert.IsTrue(result.Exists(x => x == 544));
      Assert.IsTrue(result.Exists(x => x == 566));
      Assert.IsTrue(result.Exists(x => x == 577));
      Assert.IsTrue(result.Exists(x => x == 588));
      Assert.IsTrue(result.Exists(x => x == 599));

      Assert.IsTrue(result.Exists(x => x == 550));
      Assert.IsTrue(result.Exists(x => x == 551));
      Assert.IsTrue(result.Exists(x => x == 552));
      Assert.IsTrue(result.Exists(x => x == 553));
      Assert.IsTrue(result.Exists(x => x == 554));
      Assert.IsTrue(result.Exists(x => x == 555));
      Assert.IsTrue(result.Exists(x => x == 556));
      Assert.IsTrue(result.Exists(x => x == 557));
      Assert.IsTrue(result.Exists(x => x == 558));
      Assert.IsTrue(result.Exists(x => x == 559));

      Assert.IsTrue(result.Exists(x => x == 600));
    }

    [TestMethod]
    public void Can_Detect_NonDecreasing_Digits_10_to_10()
    {
      // arrange
      var password = new Password();
      var possiblePasswords = password.GetPasswordRange(10, 10);

      // act
      var result = possiblePasswords.FindAll(x => Password.hasNonDecreasingDigits(x));

      // assert
      Assert.AreEqual(result.Count, 0);
    }

    [TestMethod]
    public void Can_Detect_NonDecreasing_Digits_11_to_11()
    {
      // arrange
      var password = new Password();
      var possiblePasswords = password.GetPasswordRange(11, 11);

      // act
      var result = possiblePasswords.FindAll(x => Password.hasNonDecreasingDigits(x));

      // assert
      Assert.AreEqual(result.Count, 1);
    }

    [TestMethod]
    public void Can_Detect_NonDecreasing_Digits_1_to_10()
    {
      // arrange
      var password = new Password();
      var possiblePasswords = password.GetPasswordRange(1, 10);

      // act
      var result = possiblePasswords.FindAll(x => Password.hasNonDecreasingDigits(x));

      // assert
      Assert.AreEqual(result.Count, 9);
      Assert.IsTrue(result.Exists(x => x == 1));
      Assert.IsTrue(result.Exists(x => x == 2));
      Assert.IsTrue(result.Exists(x => x == 3));
      Assert.IsTrue(result.Exists(x => x == 4));
      Assert.IsTrue(result.Exists(x => x == 5));
      Assert.IsTrue(result.Exists(x => x == 6));
      Assert.IsTrue(result.Exists(x => x == 7));
      Assert.IsTrue(result.Exists(x => x == 8));
      Assert.IsTrue(result.Exists(x => x == 9));
    }


    [TestMethod]
    public void Can_Detect_NonDecreasing_Digits_123460_to_123470()
    {
      // arrange
      var password = new Password();
      var possiblePasswords = password.GetPasswordRange(123460, 123470);

      // act
      var result = possiblePasswords.FindAll(x => Password.hasNonDecreasingDigits(x));

      // assert
      Assert.AreEqual(result.Count, 4);
      Assert.IsTrue(result.Exists(x => x == 123466));
      Assert.IsTrue(result.Exists(x => x == 123467));
      Assert.IsTrue(result.Exists(x => x == 123468));
      Assert.IsTrue(result.Exists(x => x == 123469));
    }

    [TestMethod]
    public void Program_Returns_Possible_Passwords()
    {
      // arrange
      var program = new Program();

      // act
      var passwords = program.Run(123460, 123470);

      // assert
      Assert.AreEqual(passwords.Count, 1);
      Assert.IsTrue(passwords.Passwords.Exists(x => x == 123466));
    }

    [TestMethod]
    public void Program_Returns_Possible_Passwords_Part_1()
    {
      // arrange
      var program = new Program();

      // act
      var passwords = program.Run(146810, 612564);

      // assert
      Assert.AreEqual(passwords.Count, 1748);
    }

    [TestMethod]
    public void Can_Detect_Repeating_Digits_Without_Larger_Group_112233_to_112233()
    {
      // arrange
      var password = new Password();
      var possiblePasswords = password.GetPasswordRange(112233, 112233);

      // act
      var result = possiblePasswords.FindAll(x => Password.hasRepeatingDigitsWithoutLargerGroups(x));

      // assert
      Assert.AreEqual(result.Count, 1);
      Assert.IsTrue(result.Exists(x => x == 112233));
    }

    [TestMethod]
    public void Can_Detect_Repeating_Digits_Without_Larger_Group_123444_to_123444()
    {
      // arrange
      var password = new Password();
      var possiblePasswords = password.GetPasswordRange(123444, 123444);

      // act
      var result = possiblePasswords.FindAll(x => Password.hasRepeatingDigitsWithoutLargerGroups(x));

      // assert
      Assert.AreEqual(result.Count, 0);
    }

    [TestMethod]
    public void Can_Detect_Repeating_Digits_Without_Larger_Group_111122_to_111122()
    {
      // arrange
      var password = new Password();
      var possiblePasswords = password.GetPasswordRange(111122, 111122);

      // act
      var result = possiblePasswords.FindAll(x => Password.hasRepeatingDigitsWithoutLargerGroups(x));

      // assert
      Assert.AreEqual(result.Count, 1);
      Assert.IsTrue(result.Exists(x => x == 111122));
    }

    [TestMethod]
    public void Can_Detect_Repeating_Digits_Without_Larger_Group_555555_to_555555()
    {
      // arrange
      var password = new Password();
      var possiblePasswords = password.GetPasswordRange(555555, 555555);

      // act
      var result = possiblePasswords.FindAll(x => Password.hasRepeatingDigitsWithoutLargerGroups(x));

      // assert
      Assert.AreEqual(result.Count, 0);
    }

    [TestMethod]
    public void Program_Returns_Possible_Passwords_Part_2()
    {
      // arrange
      var program = new Program();

      // act
      var passwords = program.RunPart2(123460, 123470);

      // assert
      Assert.AreEqual(passwords.Count, 1);
      Assert.IsTrue(passwords.Passwords.Exists(x => x == 123466));
    }

    [TestMethod]
    public void Program_Returns_Possible_Passwords_Part_2_2()
    {
      // arrange
      var program = new Program();

      // act
      var passwords = program.RunPart2(123444, 123444);

      // assert
      Assert.AreEqual(passwords.Count, 0);
    }

    [TestMethod]
    public void Program_Returns_Possible_Passwords_Part_2_Final()
    {
      // arrange
      var program = new Program();

      // act
      var passwords = program.RunPart2(146810, 612564);

      // assert
      Assert.AreEqual(passwords.Count, 1180);
    }

  }
}
