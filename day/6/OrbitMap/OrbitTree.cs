using System;
using System.Collections.Generic;

namespace OrbitMap
{
  public class OrbitTree
  {
    private CelestialObject _celestialObject;
    private LinkedList<OrbitTree> children;
    public int OrbitDistance => _celestialObject.OrbitDistance;
    public string CelestialObjectName => _celestialObject.Name;

    public OrbitTree(CelestialObject celestialObject)
    {
      this._celestialObject = celestialObject;
      children = new LinkedList<OrbitTree>();
    }

    public void AddChild(string celestialObjectName)
    {
      var childOrbitDistance = OrbitDistance + 1;
      children.AddFirst(new OrbitTree(new CelestialObject(celestialObjectName, childOrbitDistance)));
    }

    public OrbitTree GetChild(int i)
    {
      foreach (OrbitTree n in children)
        if (--i == 0)
          return n;
      return null;
    }

    public OrbitTree GetChild(string celestialObjectName)
    {
      foreach (OrbitTree n in children)
      {
        if (n._celestialObject.Name == celestialObjectName) return n;
      }

      return null;
    }

   

    public void Traverse(TreeVisitor visitor)
    {
      Traverse(this, visitor);
    }

    public void Traverse(OrbitTree node, TreeVisitor visitor)
    {
      visitor(node);
      foreach (OrbitTree child in node.children)
        Traverse(child, visitor);
    }

    public int GetObjectDistances()
    {
      var totalDistance = 0;
      Traverse(x => totalDistance += x.OrbitDistance);
      return totalDistance;
    }

    public OrbitTree Search(string celestialObjectName)
    {
      OrbitTree foundObject= null;
      Traverse(x => {
        if (x.CelestialObjectName == celestialObjectName) foundObject = x;
      });
      return foundObject;
    }
  }

  public delegate void TreeVisitor(OrbitTree orbitTree);
}
