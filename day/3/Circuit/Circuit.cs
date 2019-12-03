using System;
using System.Collections.Generic;

namespace CircuitLibrary
{
  public class Circuit
  {
    private Port[,] _circuit;

    public Port[,] Panel { get { return _circuit; } }

    public Circuit()
    {
      // for unit testing
    }

    public Circuit(List<Wire> wires)
    {
      var dimensions = GetPanelDimensions(wires);

      InitializeCircuit(dimensions.width, dimensions.height, dimensions.centralPortX, dimensions.centralPortY);

      // add wire automatically
      foreach (var wire in wires)
      {
        AddWire(wire);
      }
    }

    public Circuit(int width, int height, int centralPortX, int centralPortY)
    {
      InitializeCircuit(width, height, centralPortX, centralPortY);
    }

    private void InitializeCircuit(int width, int height, int centralPortX, int centralPortY)
    {
      _circuit = new Port[width, height];
      for (var x = 0; x < width; x++)
      {
        for (var y = 0; y < height; y++)
        {
          _circuit[x, y] = new Port(x, y);
        }
      }

      // set central port
      _circuit[centralPortX, centralPortY].IsCentralPort = true;
    }

    public (int maxX, int minX, int maxY, int minY) GetWireReach(List<Wire> wires)
    {
      var maxX = 0;
      var minX = 0;
      var maxY = 0;
      var minY = 0;

      foreach (var wire in wires)
      {
        var currentX = 0;
        var currentY = 0;

        foreach (var wireVector in wire.Path)
        {
          // up affects +Y direction
          if (wireVector.Direction == Direction.Up) currentY += wireVector.Magnitude;
          // down affects -Y direction
          else if (wireVector.Direction == Direction.Down) currentY -= wireVector.Magnitude;
          // right affects +X direction
          else if (wireVector.Direction == Direction.Right) currentX += wireVector.Magnitude;
          // left affects -X direction
          else if (wireVector.Direction == Direction.Left) currentX -= wireVector.Magnitude;

          if (currentX > maxX) maxX = currentX;
          else if (currentX < minX) minX = currentX;
          else if (currentY > maxY) maxY = currentY;
          else if (currentY < minY) minY = currentY;
        }
      }

      return (maxX, minX, maxY, minY);
    }

    public (int width, int height, int centralPortX, int centralPortY)
      GetPanelDimensions(List<Wire> wires)
    {
      var wireReach = GetWireReach(wires);

      return (
        width: Math.Abs(wireReach.minX) + wireReach.maxX + 5,
        height: Math.Abs(wireReach.minY) + wireReach.maxY + 5,
        centralPortX: Math.Abs(wireReach.minX) + 2,
        centralPortY: Math.Abs(wireReach.minY) + 2);
    }


    public Port GetCentralPort()
    {
      var width = _circuit.GetLength(0);
      var length = _circuit.GetLength(1);
      for (var x = 0; x < width; x++)
      {
        for (var y = 0; y < length; y++)
        {
          if (_circuit[x, y].IsCentralPort)
          {
            return _circuit[x, y];
          }
        }
      }
      return null;
    }

    public void AddWire(Wire wire)
    {
      var centralPoint = GetCentralPort();
      var currentX = centralPoint.X;
      var currentY = centralPoint.Y;

      foreach (var path in wire.Path)
      {
        for (var x = 0; x < path.Magnitude; x++)
        {
          // up affects +Y direction
          if (path.Direction == Direction.Up) currentY++;
          // down affects -Y direction
          else if (path.Direction == Direction.Down) currentY--;
          // right affects +X direction
          else if (path.Direction == Direction.Right) currentX++;
          // left affects -X direction
          else if (path.Direction == Direction.Left) currentX--;

          // write the field
          var port = _circuit[currentX, currentY];
          _circuit[currentX, currentY].WireState = GetNewWireState(port, wire);
        }
      }
    }

    public WireState GetNewWireState(Port port, Wire wire)
    {
      var newWireState = WireState.None;
      var currentWireState = port.WireState;

      // if nothing is set, set to the current wire state
      if (currentWireState == WireState.None)
        newWireState = (WireState)wire.Number;

      // if the wire is already set to same state, keep the state
      else if (currentWireState == (WireState)wire.Number)
        newWireState = currentWireState;

      // if the wire state is set to a different wire number
      else if ((currentWireState == WireState.Wire1Present && (WireState)wire.Number == WireState.Wire2Present) ||
        (currentWireState == WireState.Wire2Present && (WireState)wire.Number == WireState.Wire1Present))
        newWireState = WireState.Wire1And2Present;

      return newWireState;
    }

    public int GetManhattanDistance((int X, int Y) point1, (int X, int Y) point2)
    {
      return Math.Abs(point1.X - point2.X) + Math.Abs(point1.Y - point2.Y);
    }

    public List<Port> GetIntersectionPorts()
    {
      var intersections = new List<Port>();

      var size = _circuit.GetLength(0);
      var length = _circuit.GetLength(1);
      for (var x = 0; x < size; x++)
      {
        for (var y = 0; y < length; y++)
        {
          if (_circuit[x, y].WireState == WireState.Wire1And2Present)
            intersections.Add(_circuit[x, y]);
        }
      }

      return intersections;
    }

    public int GetClosestIntersectionPortDistance()
    {
      var centralPort = GetCentralPort();
      var intersectionPorts = GetIntersectionPorts();

      return GetShortestManhattanDistance(centralPort, intersectionPorts);
    }

    public int GetShortestManhattanDistance(Port beginningPort, List<Port> portList)
    {
      var shortestDistance = int.MaxValue;

      foreach (var port in portList)
      {
        var distance = GetManhattanDistance(
          (X: beginningPort.X, Y: beginningPort.Y),
          (X: port.X, Y: port.Y));

        if (distance < shortestDistance) shortestDistance = distance;
      }

      return shortestDistance;
    }
  }
}
