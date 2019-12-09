using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceImageFormat;

namespace SpaceImageFormatTest
{
  [TestClass]
  public class FactoryUnitTest
  {
    [TestMethod]
    public void Can_Parse_From_File_1()
    {
      // arrange
      var width = 3;
      var height = 2;
      var inputFileName = "TestFiles/simple_image_1.txt"; // 123456789012

      // act
      var spaceImage = SpaceImageFactory.Create(inputFileName, width, height);

      // assert
      Assert.AreEqual(spaceImage.ImageArray[0, 0, 0], 1);
      Assert.AreEqual(spaceImage.ImageArray[0, 1, 0], 2);
      Assert.AreEqual(spaceImage.ImageArray[0, 2, 0], 3);
      Assert.AreEqual(spaceImage.ImageArray[0, 0, 1], 4);
      Assert.AreEqual(spaceImage.ImageArray[0, 1, 1], 5);
      Assert.AreEqual(spaceImage.ImageArray[0, 2, 1], 6);
      Assert.AreEqual(spaceImage.ImageArray[1, 0, 0], 7);
      Assert.AreEqual(spaceImage.ImageArray[1, 1, 0], 8);
      Assert.AreEqual(spaceImage.ImageArray[1, 2, 0], 9);
      Assert.AreEqual(spaceImage.ImageArray[1, 0, 1], 0);
      Assert.AreEqual(spaceImage.ImageArray[1, 1, 1], 1);
      Assert.AreEqual(spaceImage.ImageArray[1, 2, 1], 2);

    }
  }
}
