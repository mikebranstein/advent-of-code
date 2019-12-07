using System;
using System.Collections.Generic;
using System.Linq;

namespace OrbitMap
{
  public class OrbitTree
  {
    public OrbitTree Parent { get; set; }
    private CelestialObject _celestialObject;
    public LinkedList<OrbitTree> Children { get; set; }
    public int OrbitDistance { get; set; }
    public string CelestialObjectName => _celestialObject.Name;

    public OrbitTree(CelestialObject celestialObject)
    {
      Parent = null;
      this._celestialObject = celestialObject;
      Children = new LinkedList<OrbitTree>();
    }

    public void AddChild(string celestialObjectName)
    {
      var child = new OrbitTree(
          new CelestialObject(celestialObjectName, 0));
      AddChild(child);
    }

    public void AddChild(OrbitTree child)
    {
      child.Parent = this;
      Children.AddFirst(child);

      Traverse(child, x => x.OrbitDistance = x.Parent.OrbitDistance + 1);
    }

    public OrbitTree GetChild(int i)
    {
      foreach (OrbitTree n in Children)
        if (--i == 0)
          return n;
      return null;
    }

    public OrbitTree GetChild(string celestialObjectName)
    {
      foreach (OrbitTree n in Children)
      {
        if (n._celestialObject.Name == celestialObjectName) return n;
      }

      return null;
    }

    public void RemoveChild(string celestialObjectName)
    {
      var child = GetChild(celestialObjectName);
      if (child == null) return;

      Children.Remove(child);
    }

    public bool Merge(OrbitTree orbitTreeToMerge)
    {
      OrbitTree mergePoint = Search(orbitTreeToMerge.CelestialObjectName);

      // if no target node found, return.
      if (mergePoint == null) return false; // no merge performed

      // merge 2 orbits, take children of the child, and add them to
      // the targetNode
      var childrenToAdd = new List<OrbitTree>();
      foreach (var child in orbitTreeToMerge.Children)
      {
        childrenToAdd.Add(child);
      }

      foreach (var child in childrenToAdd)
      {
        child.Parent.RemoveChild(child.CelestialObjectName);
        mergePoint.AddChild(child);
      }

      return true; // we performed a merge
    }

    public void Traverse(TreeVisitor visitor)
    {
      Traverse(this, visitor);
    }

    public void Traverse(OrbitTree node, TreeVisitor visitor)
    {
      visitor(node);
      foreach (OrbitTree child in node.Children)
        Traverse(child, visitor);
    }

    public int GetObjectDistances()
    {
      var totalDistance = 0;
      Traverse(x => totalDistance += x.OrbitDistance);
      return totalDistance;
    }

    public int GetOrbitalTransferCount(string celestialObjectSourceName, string celestialObjectDestName)
    {
      // returns number of orbital transfers to move into the same orbit as the destination object

      var sourceObject = Search(celestialObjectSourceName);
      var destinationObject = Search(celestialObjectDestName);
      var closestParent = GetClosestParent(celestialObjectSourceName, celestialObjectDestName);

      var sourceOrbitalTransfersToClosestParent = sourceObject.OrbitDistance - closestParent.OrbitDistance - 1;
      var destinationOrbitalTransfersToClosestParent = destinationObject.OrbitDistance - closestParent.OrbitDistance - 1;

      return sourceOrbitalTransfersToClosestParent + destinationOrbitalTransfersToClosestParent;
    }

    public OrbitTree GetClosestParent(string celestialObjectName1, string celestialObjectName2)
    {
      // create list of parent paths from object 1 and object 2
      // intersect sets, then sort intersection by object distance
      // highest object distance is closest common parent
      var celestialObject1Path = GetPathToUniversalObject(celestialObjectName1);
      var celestialObject2Path = GetPathToUniversalObject(celestialObjectName2);

      var commonPath =
        celestialObject1Path.Intersect(celestialObject2Path, new OrbitTreeComparer());

      return commonPath.OrderByDescending(x => x.OrbitDistance).First();
    }

    public OrbitTree[] GetPathToUniversalObject(string celestialObjectName)
    {
      // returns list of objects from source (supplied) to COM
      var orbitPath = new List<OrbitTree>();

      var sourceObject = Search(celestialObjectName);
      var parent = sourceObject.Parent;

      while (parent != null)
      {
        orbitPath.Add(parent);
        parent = parent.Parent;
      }

      return orbitPath.ToArray();
    }

    public OrbitTree Search(string celestialObjectName)
    {
      OrbitTree foundObject = null;
      Traverse(x =>
      {
        if (x.CelestialObjectName == celestialObjectName) foundObject = x;
      });
      return foundObject;
    }
  }

  public delegate void TreeVisitor(OrbitTree orbitTree);

  public class OrbitTreeComparer : IEqualityComparer<OrbitTree>
  {
    // Products are equal if their names and product numbers are equal.
    public bool Equals(OrbitTree x, OrbitTree y)
    {
      //Check whether the name is the same
      return x.CelestialObjectName == y.CelestialObjectName;
    }

    public int GetHashCode(OrbitTree orbitTree)
    {
      return orbitTree.CelestialObjectName.GetHashCode();
    }

  }

}