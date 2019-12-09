using System;
namespace SpaceImageFormat
{
  public class SpaceImage
  {
    // dimension 0 is the layer
    // dimension 1 is the width
    // dimension 2 is the height
    public int[,,] ImageArray { get; private set; }

    public SpaceImage(int[,,] imageArray)
    {
      ImageArray = imageArray;
    }

    public int GetLayerWithFewestZeros()
    {
      var bestLayer = 0;
      var lowestZeroCount = int.MaxValue;

      var currentZeroCount = 0;
      for (var layer = 0; layer < ImageArray.GetLength(0); layer++)
      {
        currentZeroCount = GetLayerDigitCount(layer, 0);

        if (currentZeroCount < lowestZeroCount)
        {
          bestLayer = layer;
          lowestZeroCount = currentZeroCount;
        }
      }

      return bestLayer;
    }

    public int GetZeroLayerDigitMuliple(int digit1, int digit2)
    {
      // To make sure the image wasn't corrupted during transmission, the
      // Elves would like you to find the layer that contains the fewest 0
      // digits. On that layer, what is the number of 1 digits multiplied
      // by the number of 2 digits?

      var layerWithFewestZeros = GetLayerWithFewestZeros();

      var digit1Count = GetLayerDigitCount(layerWithFewestZeros, digit1);
      var digit2Count = GetLayerDigitCount(layerWithFewestZeros, digit2);

      return digit1Count * digit2Count;
    }

    private int GetLayerDigitCount(int layer, int digit)
    {
      var digitCount = 0;
      for (var width = 0; width < ImageArray.GetLength(1); width++)
      {
        for (var height = 0; height < ImageArray.GetLength(2); height++)
        {
          if (ImageArray[layer, width, height] == digit) digitCount++;
        }
      }
      return digitCount;
    }

    public int[,] MergeLayers()
    {
      int layers = ImageArray.GetLength(0);
      int width = ImageArray.GetLength(1);
      int height = ImageArray.GetLength(2);
      int[,] mergedImage = new int[width, height];


      for (var w = 0; w < width; w++)
      {
        for (var h = 0; h < height; h++)
        {
          var pixelColor = PixelColor.Transparent;
          for (var layer = 0; layer < layers; layer++)
          {
            var currentLayerPixelColor = ImageArray[layer, w, h];
            if (currentLayerPixelColor < (int)pixelColor)
            {
              pixelColor = (PixelColor)currentLayerPixelColor;
              break;
            }
          }

          mergedImage[w, h] = (int)pixelColor;
        }
      }

      return mergedImage;
    }
  }

  public enum PixelColor
  {
    Black = 0,
    White = 1,
    Transparent = 2
  }
}
