using System;
using System.Collections.Generic;

namespace CircuitLibrary
{
  public class Circuit
  {
    private Port[,] _circuit;
    private List<Wire> _wires = new List<Wire>();
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
      _wires.Add(wire);

      var centralPoint = GetCentralPort();
      var currentX = centralPoint.X;
      var currentY = centralPoint.Y;
      var stepCounter = 0;

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
          _circuit[currentX, currentY].WireState.State = GetNewWireState(port, wire.Number);

          // write the minimum port step count, using part 2 rules of loop detection
          stepCounter++;
          var updatedStepCounter = GetUpdatedStepCount(port, wire.Number, stepCounter);
          if (wire.Number == 1) _circuit[currentX, currentY].WireState.Wire1StepCount = updatedStepCounter;
          else if (wire.Number == 2) _circuit[currentX, currentY].WireState.Wire2StepCount = updatedStepCounter;

          // update step counter in case it was reduced (changed) b/c of a loop
          stepCounter = updatedStepCounter;
        }
      }
    }

    public int GetUpdatedStepCount(Port port, int wireNumber, int stepCounter)
    {
      if (wireNumber == 1)
      {
        if (port.WireState.Wire1StepCount > 0) return port.WireState.Wire1StepCount;
        return stepCounter;
      }
      else if (wireNumber == 2)
      {
        if (port.WireState.Wire2StepCount > 0) return port.WireState.Wire2StepCount;
        return stepCounter;
      }

      return stepCounter;
    }

    public State GetNewWireState(Port port, int wireNumber)
    {
      var newWireState = State.None;
      var currentWireState = port.WireState.State;

      // if nothing is set, set to the current wire state
      if (currentWireState == State.None)
        newWireState = (State)wireNumber;

      // if the wire is already set to same state, keep the state
      else if (currentWireState == (State)wireNumber)
        newWireState = currentWireState;

      // if the wire state is set to a different wire number
      else if ((currentWireState == State.Wire1Present && (State)wireNumber == State.Wire2Present) ||
        (currentWireState == State.Wire2Present && (State)wireNumber == State.Wire1Present))
        newWireState = State.Wire1And2Present;

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
          if (_circuit[x, y].WireState.State == State.Wire1And2Present)
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

    public int CountWireSteps(Wire wire, int endX, int endY)
    {
      var steps = 0;

      var centralPort = GetCentralPort();
      var currentX = centralPort.X;
      var currentY = centralPort.Y;

      foreach (var path in wire.Path)
      {
        for (var x = 0; x < path.Magnitude; x++)
        {
          // move to the new location on the wire

          // up affects +Y direction
          if (path.Direction == Direction.Up) currentY++;
          // down affects -Y direction
          else if (path.Direction == Direction.Down) currentY--;
          // right affects +X direction
          else if (path.Direction == Direction.Right) currentX++;
          // left affects -X direction
          else if (path.Direction == Direction.Left) currentX--;

          // increment step counter
          steps++;

          // when we've reached the end coordinates, stop
          if (currentX == endX && currentY == endY) break;
        }

        if (currentX == endX && currentY == endY) break;
      }

      return steps;
    }

    public int GetClosestIntersectionByWirePath()
    {
      var intersections = GetIntersectionPorts();

      var shortestSteps = int.MaxValue;

      foreach (var intersection in intersections)
      {
        var wire1Steps = _circuit[intersection.X, intersection.Y].WireState.Wire1StepCount;
        var wire2Steps = _circuit[intersection.X, intersection.Y].WireState.Wire2StepCount;

        var totalSteps = wire1Steps + wire2Steps;
        if (totalSteps < shortestSteps) shortestSteps = totalSteps;
      }

      return shortestSteps;
    }
  }
}
