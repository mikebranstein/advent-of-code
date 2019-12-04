using Microsoft.VisualStudio.TestTools.UnitTesting;
using CircuitLibrary;
using challenge;
using System.Collections.Generic;

namespace test
{
  [TestClass]
  public class CircuitUnitTest
  {
    [TestMethod]
    public void Circuit_Created_With_Correct_Size()
    {
      // arrange
      var wire1 = new Wire(1, "R100");
      var wire2 = new Wire(2, "U150");
      var expectedWidth = 100 + 5; // 5 is buffer
      var expectedLength = 150 + 5; // 5 is buffer

      // act
      var circuit = new Circuit(new List<Wire>() { wire1, wire2 });

      // assert
      Assert.AreEqual(circuit.Panel.GetLength(0), expectedWidth);
      Assert.AreEqual(circuit.Panel.GetLength(1), expectedLength);
    }

    [TestMethod]
    public void Circuit_Created_With_Correct_EvenCentralPoint()
    {
      // arrange
      var wire1 = new Wire(1, "R100");
      var wire2 = new Wire(2, "U150");

      var expectedCentralPortX = 2; // 2 is buffer
      var expectedCentralPortY = 2; // 2 is buffer

      // act
      var circuit = new Circuit(new List<Wire>() { wire1, wire2 });
      var centralPort = circuit.GetCentralPort();

      // assert
      Assert.AreEqual(centralPort.X, expectedCentralPortX);
      Assert.AreEqual(centralPort.Y, expectedCentralPortY);
    }

    [TestMethod]
    public void Can_Parse_Wire()
    {
      // arrange
      var wirePath = "R75,D30,R83,U83,L12,D49,R71,U7,L72";

      // act
      var wire = new Wire(1, wirePath);

      // assert
      Assert.AreEqual(wire.Path.Count, 9);
      Assert.AreEqual(wire.Path[0].Magnitude, 75);
      Assert.AreEqual(wire.Path[0].Direction, Direction.Right);
      Assert.AreEqual(wire.Path[1].Magnitude, 30);
      Assert.AreEqual(wire.Path[1].Direction, Direction.Down);
      Assert.AreEqual(wire.Path[2].Magnitude, 83);
      Assert.AreEqual(wire.Path[2].Direction, Direction.Right);
      Assert.AreEqual(wire.Path[3].Magnitude, 83);
      Assert.AreEqual(wire.Path[3].Direction, Direction.Up);
      Assert.AreEqual(wire.Path[4].Magnitude, 12);
      Assert.AreEqual(wire.Path[4].Direction, Direction.Left);
      Assert.AreEqual(wire.Path[5].Magnitude, 49);
      Assert.AreEqual(wire.Path[5].Direction, Direction.Down);
      Assert.AreEqual(wire.Path[6].Magnitude, 71);
      Assert.AreEqual(wire.Path[6].Direction, Direction.Right);
      Assert.AreEqual(wire.Path[7].Magnitude, 7);
      Assert.AreEqual(wire.Path[7].Direction, Direction.Up);
      Assert.AreEqual(wire.Path[8].Magnitude, 72);
      Assert.AreEqual(wire.Path[8].Direction, Direction.Left);
    }

    [TestMethod]
    public void Wire_State_None_To_Wire_1()
    {
      // arrange
      var wirePath = "R75";
      var wire = new Wire(1, wirePath);
      var port = new Port(0, 0) { WireState = new WireState() { State = State.None } };
      var expectedWireState = State.Wire1Present;

      // act
      var circuit = new Circuit();
      var newWireState = circuit.GetNewWireState(port, wire.Number);

      // assert
      Assert.AreEqual(newWireState, expectedWireState);
    }

    [TestMethod]
    public void Wire_State_None_To_Wire_2()
    {
      // arrange
      var wirePath = "R75";
      var wire = new Wire(2, wirePath);
      var port = new Port(0,0) { WireState = new WireState() { State = State.None } };
      var expectedWireState = State.Wire2Present;

      // act
      var circuit = new Circuit();
      var newWireState = circuit.GetNewWireState(port, wire.Number);

      // assert
      Assert.AreEqual(newWireState, expectedWireState);
    }

    [TestMethod]
    public void Wire_State_Wire_1_To_Wire_1()
    {
      // arrange
      var wirePath = "R75";
      var wire = new Wire(1, wirePath);
      var port = new Port(0,0) { WireState = new WireState() { State = State.None } };
      var expectedWireState = State.Wire1Present;

      // act
      var circuit = new Circuit();
      var newWireState = circuit.GetNewWireState(port, wire.Number);

      // assert
      Assert.AreEqual(newWireState, expectedWireState);
    }


    [TestMethod]
    public void Wire_State_Wire_2_To_Wire_2()
    {
      // arrange
      var wirePath = "R75";
      var wire = new Wire(2, wirePath);
      var port = new Port(0,0) { WireState = new WireState() { State = State.None } };
      var expectedWireState = State.Wire2Present;

      // act
      var circuit = new Circuit();
      var newWireState = circuit.GetNewWireState(port, wire.Number);

      // assert
      Assert.AreEqual(newWireState, expectedWireState);
    }


    [TestMethod]
    public void Wire_State_Wire_1_To_Both()
    {
      // arrange
      var wirePath = "R75";
      var wire = new Wire(2, wirePath);
      var port = new Port(0,0) { WireState = new WireState() { State = State.Wire1Present } };
      var expectedWireState = State.Wire1And2Present;

      // act
      var circuit = new Circuit();
      var newWireState = circuit.GetNewWireState(port, wire.Number);

      // assert
      Assert.AreEqual(newWireState, expectedWireState);
    }

    [TestMethod]
    public void Wire_State_Wire_2_To_Both()
    {
      // arrange
      var wirePath = "R75";
      var wire = new Wire(1, wirePath);
      var port = new Port(0,0) { WireState = new WireState() { State = State.Wire2Present } };
      var expectedWireState = State.Wire1And2Present;

      // act
      var circuit = new Circuit();
      var newWireState = circuit.GetNewWireState(port, wire.Number);

      // assert
      Assert.AreEqual(newWireState, expectedWireState);
    }

    [TestMethod]
    public void Can_Add_Wire_1_RightOnly()
    {
      // arrange
      var wirePath = "R2";
      var wire = new Wire(1, wirePath);
      var width = 6;
      var height = 6;
      var circuit = new Circuit(width, height, 3, 3);

      //......
      //......
      //...o--
      //......
      //......
      //......
      //central port = 3,3 = o
      // 4,3 is wire 1
      // 5,3 is wire 1

      // act
      circuit.AddWire(wire);

      // assert
      for (var x = 0; x < width; x++)
      {
        for (var y = 0; y < height; y++)
        {
          if (x == 4 && y == 3)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);
          else if (x == 5 && y == 3)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);
          else
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.None);
        }
      }
    }

    [TestMethod]
    public void Can_Add_Wire_1_LeftOnly()
    {
      // arrange
      var wirePath = "L2";
      var wire = new Wire(1, wirePath);
      var width = 6;
      var height = 6;
      var circuit = new Circuit(width, height, 3, 3);

      //......
      //......
      //.--o..
      //......
      //......
      //......
      //central port = 3,3 = o
      // 1,3 is wire 1
      // 2,3 is wire 1

      // act
      circuit.AddWire(wire);

      // assert
      for (var x = 0; x < width; x++)
      {
        for (var y = 0; y < height; y++)
        {
          if (x == 1 && y == 3)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);
          else if (x == 2 && y == 3)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);
          else
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.None);
        }
      }
    }

    [TestMethod]
    public void Can_Add_Wire_1_UpOnly()
    {
      // arrange
      var wirePath = "U2";
      var wire = new Wire(1, wirePath);
      var width = 6;
      var height = 6;

      var circuit = new Circuit(width, height, 3, 3);

      //...|..
      //...|..
      //...o..
      //......
      //......
      //......
      //central port = 3,3 = o
      // 3,4 is wire 1
      // 3,5 is wire 1

      // act
      circuit.AddWire(wire);

      // assert
      for (var x = 0; x < width; x++)
      {
        for (var y = 0; y < height; y++)
        {
          if (x == 3 && y == 4)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);
          else if (x == 3 && y == 5)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);
          else
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.None);
        }
      }
    }

    [TestMethod]
    public void Can_Add_Wire_1_DownOnly()
    {
      // arrange
      var wirePath = "D2";
      var wire = new Wire(1, wirePath);
      var width = 6;
      var height = 6;
      var circuit = new Circuit(width, height, 3, 3);

      //......
      //......
      //...o..
      //...|..
      //...|..
      //......
      //central port = 3,3 = o
      // 3,1 is wire 1
      // 3,2 is wire 1

      // act
      circuit.AddWire(wire);

      // assert
      for (var x = 0; x < width; x++)
      {
        for (var y = 0; y < height; y++)
        {
          if (x == 3 && y == 1)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);
          else if (x == 3 && y == 2)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);
          else
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.None);
        }
      }
    }

    [TestMethod]
    public void Can_Add_Wire_1_Overlapping()
    {
      // arrange
      var wirePath = "D2,R2,U1,L3";
      var wire = new Wire(1, wirePath);
      var width = 6;
      var height = 6;
      var circuit = new Circuit(width, height, 3, 3);

      // 012345
      //5......
      //4......
      //3...o..
      //2..-+-+
      //1...+--
      //0......
      //central port = 3,3 = o
      // 2,2 is wire 1
      // 3,2 is wire 1
      // 4,2 is wire 1
      // 5,2 is wire 1
      // 3,1 is wire 1
      // 4,1 is wire 1
      // 5,1 is wire 1

      // act
      circuit.AddWire(wire);

      // assert
      for (var x = 0; x < width; x++)
      {
        for (var y = 0; y < height; y++)
        {
          if (x == 2 && y == 2)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);
          else if (x == 3 && y == 2)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);
          else if (x == 4 && y == 2)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);
          else if (x == 5 && y == 2)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);
          else if (x == 3 && y == 1)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);
          else if (x == 4 && y == 1)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);
          else if (x == 5 && y == 1)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);
          else
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.None);
        }
      }
    }

    [TestMethod]
    public void Can_Add_Wire_1_and_Wire_2()
    {
      // arrange
      var wire1Path = "R8,U5,L5,D3";
      var wire1 = new Wire(1, wire1Path);

      var wire2Path = "U7,R6,D4,L4";
      var wire2 = new Wire(2, wire2Path);

      var width = 26;
      var height = 26;

      var circuit = new Circuit(width, height, 13, 13);

      //            1111111111222222
      //  01234567890123456789012345
      //25..........................
      //24..........................
      //23..........................
      //22..........................
      //21..........................
      //20.............+-----+......
      //19.............|.....|......
      //18.............|..+--X-+....
      //17.............|..|..|.|....
      //16.............|.-X--|.|....
      //15.............|..|....|....
      //14.............|.......|....
      //13.............o-------+....
      //12..........................
      //11..........................
      //10..........................
      // 9..........................
      // 8..........................
      // 7..........................
      // 6..........................
      // 5..........................
      // 4..........................
      // 3..........................
      // 2..........................
      // 1..........................
      // 0..........................
      //central port = 13,13 = o
      // 13,20 is wire 2
      // 14,20 is wire 2
      // 15,20 is wire 2
      // 16,20 is wire 2
      // 17,20 is wire 2
      // 18,20 is wire 2
      // 19,20 is wire 2
      //
      // 13,19 is wire 2
      // 19,19 is wire 2
      //
      // 13,18 is wire 2
      // 16,18 is wire 1
      // 17,18 is wire 1
      // 18,18 is wire 1
      // 19,18 is both
      // 20,18 is wire 1
      // 21,18 is wire 1
      //
      // 13,17 is wire 2
      // 16,17 is wire 1
      // 19,17 is wire 2
      // 21,17 is wire 1
      //
      // 13,16 is wire 2
      // 15,16 is wire 2
      // 16,16 is both
      // 17,16 is wire 2
      // 18,16 is wire 2
      // 19,16 is wire 2
      // 21,16 is wire 1
      //
      // 13,15 is wire 2
      // 16,15 is wire 1
      // 21,15 is wire 1
      //
      // 13,14 is wire 2
      // 21,14 is wire 1
      //
      // 14,13 is wire 1
      // 15,13 is wire 1
      // 16,13 is wire 1
      // 17,13 is wire 1
      // 18,13 is wire 1
      // 19,13 is wire 1
      // 20,13 is wire 1
      // 21,13 is wire 1

      // act
      circuit.AddWire(wire1);
      circuit.AddWire(wire2);

      // assert
      for (var x = 0; x < width; x++)
      {
        for (var y = 0; y < height; y++)
        {
          // 13,20 is wire 2
          // 14,20 is wire 2
          // 15,20 is wire 2
          // 16,20 is wire 2
          // 17,20 is wire 2
          // 18,20 is wire 2
          // 19,20 is wire 2
          if (x == 13 && y == 20)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire2Present);
          else if (x == 14 && y == 20)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire2Present);
          else if (x == 15 && y == 20)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire2Present);
          else if (x == 16 && y == 20)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire2Present);
          else if (x == 17 && y == 20)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire2Present);
          else if (x == 18 && y == 20)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire2Present);
          else if (x == 19 && y == 20)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire2Present);

          // 13,19 is wire 2
          // 19,19 is wire 2
          else if (x == 13 && y == 19)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire2Present);
          else if (x == 19 && y == 19)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire2Present);

          // 13,18 is wire 2
          // 16,18 is wire 1
          // 17,18 is wire 1
          // 18,18 is wire 1
          // 19,18 is both
          // 20,18 is wire 1
          // 21,18 is wire 1
          else if (x == 13 && y == 18)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire2Present);
          else if (x == 16 && y == 18)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);
          else if (x == 17 && y == 18)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);
          else if (x == 18 && y == 18)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);
          else if (x == 19 && y == 18)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1And2Present);
          else if (x == 20 && y == 18)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);
          else if (x == 21 && y == 18)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);

          // 13,17 is wire 2
          // 16,17 is wire 1
          // 19,17 is wire 2
          // 21,17 is wire 1
          else if (x == 13 && y == 17)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire2Present);
          else if (x == 16 && y == 17)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);
          else if (x == 19 && y == 17)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire2Present);
          else if (x == 21 && y == 17)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);

          // 13,16 is wire 2
          // 15,16 is wire 2
          // 16,16 is both
          // 17,16 is wire 2
          // 18,16 is wire 2
          // 19,16 is wire 2
          // 21,16 is wire 1
          else if (x == 13 && y == 16)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire2Present);
          else if (x == 15 && y == 16)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire2Present);
          else if (x == 16 && y == 16)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1And2Present);
          else if (x == 17 && y == 16)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire2Present);
          else if (x == 18 && y == 16)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire2Present);
          else if (x == 19 && y == 16)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire2Present);
          else if (x == 21 && y == 16)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);

          // 13,15 is wire 2
          // 16,15 is wire 1
          // 21,15 is wire 1
          else if (x == 13 && y == 15)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire2Present);
          else if (x == 16 && y == 15)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);
          else if (x == 21 && y == 15)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);

          // 13,14 is wire 2
          // 21,14 is wire 1
          else if (x == 13 && y == 14)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire2Present);
          else if (x == 21 && y == 14)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);



          //
          // 14,13 is wire 1
          // 15,13 is wire 1
          // 16,13 is wire 1
          // 17,13 is wire 1
          // 18,13 is wire 1
          // 19,13 is wire 1
          // 20,13 is wire 1
          // 21,13 is wire 1
          else if (x == 14 && y == 13)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);
          else if (x == 15 && y == 13)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);
          else if (x == 16 && y == 13)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);
          else if (x == 17 && y == 13)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);
          else if (x == 18 && y == 13)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);
          else if (x == 19 && y == 13)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);
          else if (x == 20 && y == 13)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);
          else if (x == 21 && y == 13)
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.Wire1Present);


          else
            Assert.AreEqual(circuit.Panel[x, y].WireState.State, State.None);
        }
      }
    }


    [TestMethod]
    public void Manhattan_Distance_1()
    {
      // arrange
      (int X, int Y) point1 = (X: 1, Y: 65);
      (int X, int Y) point2 = (X: 34, Y: 12);
      var expectedDistance = 86;

      // act
      var circuit = new Circuit(1,1,0,0);
      var distance = circuit.GetManhattanDistance(point1, point2);

      // assert
      Assert.AreEqual(distance, expectedDistance);
    }

    [TestMethod]
    public void Manhattan_Distance_2()
    {
      // arrange
      (int X, int Y) point1 = (X: 55, Y: 7);
      (int X, int Y) point2 = (X: 2, Y: 9);
      var expectedDistance = 55;

      // act
      var circuit = new Circuit(1,1,0,0);
      var distance = circuit.GetManhattanDistance(point1, point2);

      // assert
      Assert.AreEqual(distance, expectedDistance);
    }

    [TestMethod]
    public void Intersections_1()
    {
      // arrange
      // arrange
      var wire1Path = "R8,U5,L5,D3";
      var wire1 = new Wire(1, wire1Path);

      var wire2Path = "U7,R6,D4,L4";
      var wire2 = new Wire(2, wire2Path);

      var width = 26;
      var height = 26;
      var circuit = new Circuit(width, height, 13, 13);

      // act
      circuit.AddWire(wire1);
      circuit.AddWire(wire2);
      var intersections = circuit.GetIntersectionPorts();

      // assert
      Assert.AreEqual(intersections.Count, 2);

      // 16,16 intersection
      Assert.AreEqual(intersections[0].X, 16);
      Assert.AreEqual(intersections[0].Y, 16);

      // 19,18 intersection
      Assert.AreEqual(intersections[1].X, 19);
      Assert.AreEqual(intersections[1].Y, 18);
    }

    [TestMethod]
    public void Shortest_Manhattan_Distance_1()
    {
      // arrange
      // arrange
      var wire1Path = "R8,U5,L5,D3";
      var wire1 = new Wire(1, wire1Path);

      var wire2Path = "U7,R6,D4,L4";
      var wire2 = new Wire(2, wire2Path);

      var width = 26;
      var height = 26;
      var circuit = new Circuit(width, height, 13, 13);
      circuit.AddWire(wire1);
      circuit.AddWire(wire2);

      var expectedShortestDistance = 6;

      // act
      var shortestDistance = circuit.GetClosestIntersectionPortDistance();

      // assert
      Assert.AreEqual(shortestDistance, expectedShortestDistance);
    }

    [TestMethod]
    public void Shortest_Manhattan_Distance_2()
    {
      // arrange
      // arrange
      var wire1Path = "R75,D30,R83,U83,L12,D49,R71,U7,L72";
      var wire1 = new Wire(1, wire1Path);

      var wire2Path = "U62,R66,U55,R34,D71,R55,D58,R83";
      var wire2 = new Wire(2, wire2Path);

      var width = 1000;
      var height = 1000;
      var circuit = new Circuit(width, height, 500, 500);
      circuit.AddWire(wire1);
      circuit.AddWire(wire2);

      var expectedShortestDistance = 159;

      // act
      var shortestDistance = circuit.GetClosestIntersectionPortDistance();

      // assert
      Assert.AreEqual(shortestDistance, expectedShortestDistance);
    }

    [TestMethod]
    public void Shortest_Manhattan_Distance_3()
    {
      // arrange
      // arrange
      var wire1Path = "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51";
      var wire1 = new Wire(1, wire1Path);

      var wire2Path = "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7";
      var wire2 = new Wire(2, wire2Path);

      var width = 1000;
      var height = 1000;
      var circuit = new Circuit(width, height, 500, 500);
      circuit.AddWire(wire1);
      circuit.AddWire(wire2);

      var expectedShortestDistance = 135;

      // act
      var shortestDistance = circuit.GetClosestIntersectionPortDistance();

      // assert
      Assert.AreEqual(shortestDistance, expectedShortestDistance);
    }


    [TestMethod]
    public void Shortest_Manhattan_Distance_4()
    {
      // arrange
      var program = new Program();
      var inputFileName = "input_1.txt";

      // act
      var wirePaths = program.ReadWirePaths(inputFileName);

      var wire1Path = wirePaths[0];
      var wire1 = new Wire(1, wire1Path);

      var wire2Path = wirePaths[1];
      var wire2 = new Wire(2, wire2Path);

      var width = 1000;
      var height = 1000;
      var circuit = new Circuit(width, height, 500, 500);
      circuit.AddWire(wire1);
      circuit.AddWire(wire2);

      var expectedShortestDistance = 159;

      // act
      var shortestDistance = circuit.GetClosestIntersectionPortDistance();

      // assert
      Assert.AreEqual(shortestDistance, expectedShortestDistance);
    }

    [TestMethod]
    public void Shortest_Manhattan_Distance_5()
    {
      // arrange
      var program = new Program();
      var inputFileName = "input_2.txt";

      // act
      var wirePaths = program.ReadWirePaths(inputFileName);

      var wire1Path = wirePaths[0];
      var wire1 = new Wire(1, wire1Path);

      var wire2Path = wirePaths[1];
      var wire2 = new Wire(2, wire2Path);

      var width = 1000;
      var height = 1000;
      var circuit = new Circuit(width, height, 500, 500);
      circuit.AddWire(wire1);
      circuit.AddWire(wire2);

      var expectedShortestDistance = 135;

      // act
      var shortestDistance = circuit.GetClosestIntersectionPortDistance();

      // assert
      Assert.AreEqual(shortestDistance, expectedShortestDistance);
    }

    [TestMethod]
    public void Full_Run_1()
    {
      // arrange
      var program = new Program();
      var inputFileName = "input_1.txt";
      var expectedShortestDistance = 159;

      // act
      var shortestDistance = program.Run(inputFileName);

      // assert
      Assert.AreEqual(shortestDistance, expectedShortestDistance);
    }

    [TestMethod]
    public void Full_Run_2()
    {
      // arrange
      var program = new Program();
      var inputFileName = "input_2.txt";
      var expectedShortestDistance = 135;

      // act
      var shortestDistance = program.Run(inputFileName);

      // assert
      Assert.AreEqual(shortestDistance, expectedShortestDistance);
    }

    [TestMethod]
    public void Estimate_Panel_Reach_1()
    {
      // arrange
      var program = new Program();
      var inputFileName = "panel_reach_1.txt";

      // act
      var wirePaths = program.ReadWirePaths(inputFileName);

      var wire1Path = wirePaths[0];
      var wire1 = new Wire(1, wire1Path);

      var wire2Path = wirePaths[1];
      var wire2 = new Wire(2, wire2Path);

      // act
      var width = 1000;
      var height = 1000;
      var circuit = new Circuit(width, height, 500, 500);
      var panelReach = circuit.GetWireReach(new List<Wire>() { wire1, wire2 });

      // assert
      Assert.AreEqual(panelReach.maxX, 8);
      Assert.AreEqual(panelReach.minX, 0);
      Assert.AreEqual(panelReach.maxY, 7);
      Assert.AreEqual(panelReach.minY, 0);
    }

    [TestMethod]
    public void Estimate_Panel_Size_1()
    {
      // arrange
      var program = new Program();
      var inputFileName = "panel_size_1.txt";

      // act
      var wirePaths = program.ReadWirePaths(inputFileName);

      var wire1Path = wirePaths[0];
      var wire1 = new Wire(1, wire1Path);

      var wire2Path = wirePaths[1];
      var wire2 = new Wire(2, wire2Path);

      // act
      var circuit = new Circuit();
      var dimensions = circuit.GetPanelDimensions(new List<Wire>() { wire1, wire2 });

      // assert - panel size should be min+max+5 buffer
      Assert.AreEqual(dimensions.width, 13);
      Assert.AreEqual(dimensions.height, 12);
      Assert.AreEqual(dimensions.centralPortX, 2);
      Assert.AreEqual(dimensions.centralPortY, 2);
    }

    [TestMethod]
    public void Verify_Wire_Reach_1()
    {
      // arrange
      var wire1 = new Wire(1, "R75,D30,R83,U83,L12,D49,R71,U7,L72");
      var wire2 = new Wire(2, "U62,R66,U55,R34,D71,R55,D58,R83");

      // act
      //var circuit = new Circuit(new List<Wire>() { wire1, wire2 });
      var circuit = new Circuit();
      var wireReach = circuit.GetWireReach(new List<Wire>() { wire1, wire2 });

      // assert 
      Assert.AreEqual(wireReach.maxX, 238);
      Assert.AreEqual(wireReach.minX, 0);
      Assert.AreEqual(wireReach.maxY, 117);
      Assert.AreEqual(wireReach.minY, -30);
    }

    [TestMethod]
    public void Verify_Dimensions_1()
    {
      // arrange
      var wire1 = new Wire(1, "R75,D30,R83,U83,L12,D49,R71,U7,L72");
      var wire2 = new Wire(2, "U62,R66,U55,R34,D71,R55,D58,R83");

      // act
      //var circuit = new Circuit(new List<Wire>() { wire1, wire2 });
      var circuit = new Circuit();
      var dimensions = circuit.GetPanelDimensions(new List<Wire>() { wire1, wire2 });

      // assert - panel size should be abs(min)+max+5 buffer
      Assert.AreEqual(dimensions.width, 0 + 238 + 5);
      Assert.AreEqual(dimensions.height, 30 + 117 + 5);

      // assert - central location is abs(minX) + 2, abs(minY) + 2
      Assert.AreEqual(dimensions.centralPortX, 2);
      Assert.AreEqual(dimensions.centralPortY, 32);
    }

    // takes really long ;-)
    //[TestMethod]
    //public void Step_One_Validation()
    //{
    //  // arrange
    //  var program = new Program();
    //  var inputFileName = "input_step_1.txt";
    //  var expectedShortestDistance = 4981;
    //
    //  // act
    //  var shortestDistance = program.Run(inputFileName);
    //
    //  // assert
    //  Assert.AreEqual(shortestDistance, expectedShortestDistance);
    //}


    [TestMethod]
    public void Closest_Wire_Intersection_By_Wire_Path_1()
    {
      // arrange
      var wire1 = new Wire(1, "R75,D30,R83,U83,L12,D49,R71,U7,L72");
      var wire2 = new Wire(2, "U62,R66,U55,R34,D71,R55,D58,R83");
      var expectedShortestSteps = 610;

      // act
      var circuit = new Circuit(new List<Wire>() { wire1, wire2 });
      var shortestSteps = circuit.GetClosestIntersectionByWirePath();

      // assert - panel size should be abs(min)+max+5 buffer
      Assert.AreEqual(shortestSteps, expectedShortestSteps);
    }

    [TestMethod]
    public void Closest_Wire_Intersection_By_Wire_Path_2()
    {
      // arrange
      var wire1 = new Wire(1, "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51");
      var wire2 = new Wire(2, "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7");
      var expectedShortestSteps = 410;

      // act
      var circuit = new Circuit(new List<Wire>() { wire1, wire2 });
      var shortestSteps = circuit.GetClosestIntersectionByWirePath();

      // assert - panel size should be abs(min)+max+5 buffer
      Assert.AreEqual(shortestSteps, expectedShortestSteps);
    }

    [TestMethod]
    public void Step_Two_Validation_1()
    {
      // arrange
      var program = new Program();
      var inputFileName = "input_1.txt";
      var expectedShortestSteps = 610;

      // act
      var shortestSteps = program.RunWirePathDistance(inputFileName);

      // assert
      Assert.AreEqual(shortestSteps, expectedShortestSteps);
    }

    [TestMethod]
    public void Step_Two_Validation_2()
    {
      // arrange
      var program = new Program();
      var inputFileName = "input_2.txt";
      var expectedShortestSteps = 410;
    
      // act
      var shortestSteps = program.RunWirePathDistance(inputFileName);
    
      // assert
      Assert.AreEqual(shortestSteps, expectedShortestSteps);
    }


    [TestMethod]
    public void Closest_Wire_Intersection_By_Wire_Path_3_With_Loops()
    {
      // arrange
      var wire1 = new Wire(1, "U2,R1,D1,L2,U2,R3,D1");
      var wire2 = new Wire(2, "R3,U1,L1,D1,R1,U2,L2");
      var expectedShortestSteps = 3 + 7; // 3 = wire 1 (no loop), 7 = wire 2 (with loop)

      // act
      var circuit = new Circuit(new List<Wire>() { wire1, wire2 });
      var shortestSteps = circuit.GetClosestIntersectionByWirePath();

      // assert - panel size should be abs(min)+max+5 buffer
      Assert.AreEqual(shortestSteps, expectedShortestSteps);
    }


    [TestMethod]
    public void Closest_Wire_Intersection_By_Wire_Path_4_With_Loops()
    {
      // arrange
      var wire1 = new Wire(1, "U3,R2,D2,L1,U1,R5,U3,L3,D4,R1,U3,R3,U3,L6");
      var wire2 = new Wire(2, "R8,U8,L3,D2,L2,U2");
      var expectedShortestSteps = 18 + 20; // 18 = wire 1 (loops), 30 = wire 2 (no loops)

      // act
      var circuit = new Circuit(new List<Wire>() { wire1, wire2 });
      var shortestSteps = circuit.GetClosestIntersectionByWirePath();

      // assert - panel size should be abs(min)+max+5 buffer
      Assert.AreEqual(shortestSteps, expectedShortestSteps);
    }

    [TestMethod]
    public void Step_Two_Validation_Final()
    {
      // arrange
      var program = new Program();
      var inputFileName = "input_step_2.txt";
      var expectedShortestSteps = 24052;

      // act
      var shortestSteps = program.RunWirePathDistance(inputFileName);

      // assert
      Assert.AreEqual(shortestSteps, expectedShortestSteps);
    }

  }
}
