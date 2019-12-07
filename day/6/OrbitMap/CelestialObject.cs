using System;
namespace OrbitMap
{
  public class CelestialObject
  {
    public string Name { get; private set; }
    public int OrbitDistance { get; private set; }

    public CelestialObject(string name, int orbitDistance)
    {
      Name = name;
      OrbitDistance = orbitDistance;
    }
  }
}
