using System;
using System.Collections.Generic;
using System.IO;

namespace Moons
{
  public class Moon
  {
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    public int VelocityX { get; set; }
    public int VelocityY { get; set; }
    public int VelocityZ { get; set; }

    public int PotentialEnergy => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
    public int KineticEnergy => Math.Abs(VelocityX) + Math.Abs(VelocityY) + Math.Abs(VelocityZ);
    public int TotalEnergy => PotentialEnergy * KineticEnergy;

    public Moon(int x, int y, int z)
    {
      X = x;
      Y = y;
      Z = z;
      VelocityX = 0;
      VelocityY = 0;
      VelocityZ = 0;  
    }

    public static List<Moon> ParseInputFile(string inputFileName)
    {
      var moons = new List<Moon>();

      var fileStream = new FileStream(inputFileName, FileMode.Open);
      using (var streamReader = new StreamReader(fileStream))
      {
        while (!streamReader.EndOfStream)
        {
          var moon = streamReader.ReadLine().Split(",");

          var x = int.Parse(moon[0].Trim().Substring(3));
          var y = int.Parse(moon[1].Trim().Substring(2));
          var z = int.Parse(moon[2].Trim().Substring(2, moon[2].Trim().Length - 3));

          moons.Add(new Moon(x, y, z));          
        }
      }

      return moons;
    }

    public void UpdateVelocity(Moon other)
    {
      var deltaX = CalculateDeltaV(this.X, other.X);
      this.VelocityX += deltaX.thisChange;
      other.VelocityX += deltaX.otherChange;

      var deltaY = CalculateDeltaV(this.Y, other.Y);
      this.VelocityY += deltaY.thisChange;
      other.VelocityY += deltaY.otherChange;

      var deltaZ = CalculateDeltaV(this.Z, other.Z);
      this.VelocityZ += deltaZ.thisChange;
      other.VelocityZ += deltaZ.otherChange;
    }

    public static (int thisChange, int otherChange) CalculateDeltaV(int thisPosition, int otherPosition)
    {
      if (thisPosition > otherPosition) return (thisChange: -1, otherChange: 1);
      if (thisPosition < otherPosition) return (thisChange: 1, otherChange: -1);
      return (thisChange: 0, otherChange: 0);
    }

    public bool Equals(Moon other)
    {
      if (other is null)
        return false;

      return this.X == other.X && this.Y == other.Y && this.Z == other.Z &&
        this.VelocityX == other.VelocityX && this.VelocityY == other.VelocityY && this.VelocityZ == other.VelocityZ;
    }

    public override bool Equals(object obj) => Equals(obj as Moon);
    public override int GetHashCode() => (X, Y, Z, VelocityX, VelocityY, VelocityZ).GetHashCode();
  }
}
