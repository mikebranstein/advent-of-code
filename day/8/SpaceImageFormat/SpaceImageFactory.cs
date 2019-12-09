
using System;
using System.IO;

namespace SpaceImageFormat
{
  public static class SpaceImageFactory
  {
    public static SpaceImage Create(string inputFileName, int width, int height)
    {
      int[,,] imageArray;

      var fileStream = new FileStream(inputFileName, FileMode.Open);
      using (var streamReader = new StreamReader(fileStream))
      {
        var numLayers = fileStream.Length / (width * height);
        imageArray = new int[numLayers, width, height];

        for (var layerIndex = 0; layerIndex < numLayers; layerIndex++)
        {
          for (var heightIndex = 0; heightIndex < height; heightIndex++)
          {
            for (var widthIndex = 0; widthIndex < width; widthIndex++)
            {
              imageArray[layerIndex, widthIndex, heightIndex] = int.Parse(Convert.ToChar(streamReader.Read()).ToString());
            }
          }
        }
      }

      var spaceImage = new SpaceImage(imageArray);
      return spaceImage;
    }
  }
}
