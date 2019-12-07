using System;
using System.Collections.Generic;
using System.IO;

namespace OrbitMap
{
  public static class MapFactory
  {

    public static OrbitTree Create(string inputFileName)
    {
      var orbitList = new List<OrbitTreeItem>();

      var fileStream = new FileStream(inputFileName, FileMode.Open);
      using (var streamReader = new StreamReader(fileStream))
      {
        while (!streamReader.EndOfStream)
        {
          var orbit = streamReader.ReadLine();

          var celestialBodies = orbit.Split(")");
          var body1 = celestialBodies[0];
          var body2 = celestialBodies[1];

          var orbitTree = new OrbitTree(new CelestialObject(body1, 0));
          orbitTree.AddChild(body2);

          orbitList.Add(new OrbitTreeItem(orbitTree));
        }
      }

      // merge
      // find COM item
      var universalOrbitTreeItem = orbitList.Find(x => x.OrbitTree.CelestialObjectName == "COM");
      universalOrbitTreeItem.IsMerged = true;

      var orbitTreeItemsToMerge = orbitList.FindAll(x => !x.IsMerged);
      while (orbitTreeItemsToMerge.Count != 0)
      {
        foreach (var orbitTreeItem in orbitTreeItemsToMerge)
        {
          var orbitTree = orbitTreeItem.OrbitTree;
          var mergeSuccessful = universalOrbitTreeItem.OrbitTree.Merge(orbitTree);
          if (mergeSuccessful) orbitTreeItem.IsMerged = true;
        }

        orbitTreeItemsToMerge = orbitList.FindAll(x => !x.IsMerged);
      }
      return universalOrbitTreeItem.OrbitTree;
    }

  }

  public class OrbitTreeItem
  {
    public OrbitTree OrbitTree { get; private set; }
    public bool IsMerged { get; set; }

    public OrbitTreeItem(OrbitTree orbitTree)
    {
      OrbitTree = orbitTree;
      IsMerged = false;
    }
  }
}
