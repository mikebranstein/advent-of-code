using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceImageFormat;

namespace SpaceImageFormatTest
{
  [TestClass]
  public class SpaceImageUnitTest
  {
    [TestMethod]
    public void Can_Get_Lowest_Zero_Layer()
    {
      // arrange
      var width = 3;
      var height = 2;
      var inputFileName = "TestFiles/simple_image_1.txt"; // 123456789012
      var spaceImage = SpaceImageFactory.Create(inputFileName, width, height);
      var expectedLayer = 0;

      // act
      var layer = spaceImage.GetLayerWithFewestZeros();

      // assert
      Assert.AreEqual(layer, expectedLayer);
    }

    [TestMethod]
    public void Can_Get_Lowest_Zero_Layer_2()
    {
      // arrange
      var width = 3;
      var height = 2;
      var inputFileName = "TestFiles/simple_image_2.txt"; // 220202 929092
      var spaceImage = SpaceImageFactory.Create(inputFileName, width, height);
      var expectedLayer = 1;

      // act
      var layer = spaceImage.GetLayerWithFewestZeros();

      // assert
      Assert.AreEqual(layer, expectedLayer);
    }

    [TestMethod]
    public void Can_Get_Layer_Digit_Multiple()
    {
      // arrange
      var width = 3;
      var height = 2;
      var inputFileName = "TestFiles/simple_image_2.txt"; // 220202 929092
      var spaceImage = SpaceImageFactory.Create(inputFileName, width, height);
      var expectedMultiple = 6; // uses layer 1, 2 (2x2s) times 3 (3x9s) = 6

      // act
      var multiple = spaceImage.GetZeroLayerDigitMuliple(2, 9);

      // assert
      Assert.AreEqual(multiple, expectedMultiple);
    }

    [TestMethod]
    public void Verify_Part_1()
    {
      // arrange
      var width = 25;
      var height = 6;
      var inputFileName = "TestFiles/input.txt"; 
      var spaceImage = SpaceImageFactory.Create(inputFileName, width, height);
      var expectedMultiple = 2193; 

      // act
      var multiple = spaceImage.GetZeroLayerDigitMuliple(1, 2);

      // assert
      Assert.AreEqual(multiple, expectedMultiple);
    }

    [TestMethod]
    public void Can_Merge_Simple_Image_Layers()
    {
      // arrange
      var width = 2;
      var height = 2;
      var inputFileName = "TestFiles/simple_image_3.txt"; // 0222112222120000
      var spaceImage = SpaceImageFactory.Create(inputFileName, width, height);

      // act
      var mergedImage = spaceImage.MergeLayers();

      // assert
      Assert.AreEqual(mergedImage[0, 0], 0);
      Assert.AreEqual(mergedImage[0, 1], 1);
      Assert.AreEqual(mergedImage[1, 0], 1);
      Assert.AreEqual(mergedImage[1, 1], 0);
    }

  }
}
