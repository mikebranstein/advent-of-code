using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using IntcodeComputer;

namespace challenge
{
  public class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");
    }

    public Program()
    {
      GameBoard = new Dictionary<Coordinate, TileType>();
    }

    public Dictionary<Coordinate, TileType> GameBoard { get; set; }

    public async Task Run(string inputFileName)
    {
      var computer = new Computer();
      var memory = computer.ReadMemoryFromFile(inputFileName);

      // start the computer
      var inputBuffer = new BufferBlock<long>();
      var outputBuffer = new BufferBlock<long>();
      var computerExecution = Task.Run(() => computer.ExecuteProgram(memory, inputBuffer, outputBuffer));

      var instructions = new List<long>();
      while (!computerExecution.IsCompleted)
      {
        try
        {
          var instruction = await outputBuffer.ReceiveAsync(new TimeSpan(500));
          instructions.Add(instruction);

          // wait until 3 instrucitons are collected
          if (instructions.Count == 3)
          {
            ProcessInstruction((int)instructions[0], (int)instructions[1], (TileType)(int)instructions[2]);

            // clean out the instruction to allow the next 3 to flow in
            instructions.Clear();
          }
        }
        catch (TimeoutException timeout)
        {
          // do noting
        }
      }
    }

    private void ProcessInstruction(int x, int y, TileType tileType)
    {
      switch (tileType)
      {
        case TileType.Empty:
          SetTileType(x, y, tileType);
          break;

        case TileType.Wall:
          SetTileType(x, y, tileType);
          break;

        case TileType.Block:
          SetTileType(x, y, tileType);
          break;

        case TileType.Paddle:
          SetTileType(x, y, tileType);
          break;

        case TileType.Ball:
          SetTileType(x, y, tileType);
          break;
      }

    }

    private TileType ReadTileType(int x, int y)
    {
      var coordinate = new Coordinate(x, y);
      if (!GameBoard.ContainsKey(coordinate)) return TileType.Empty;

      return GameBoard[coordinate];
    }

    private void SetTileType(int x, int y, TileType tileType)
    {
      var coordinate = new Coordinate(x, y);

      if (!GameBoard.ContainsKey(coordinate))
        GameBoard.Add(coordinate, tileType);

      else GameBoard[coordinate] = tileType;
    }

  }

  // 0 is an empty tile.No game object appears in this tile.
  // 1 is a wall tile.Walls are indestructible barriers.
  // 2 is a block tile.Blocks can be broken by the ball.
  // 3 is a horizontal paddle tile. The paddle is indestructible.
  // 4 is a ball tile.The ball moves diagonally and bounces off objects.

  public enum TileType
  {
    Empty = 0,
    Wall = 1,
    Block = 2,
    Paddle = 3,
    Ball = 4
  }

  public class Coordinate
  {
    public int X { get; set; }
    public int Y { get; set; }

    public Coordinate(int x, int y)
    {
      X = x;
      Y = y;
    }

    public bool Equals(Coordinate other)
    {
      if (other is null)
        return false;

      return this.X == other.X && this.Y == other.Y;
    }

    public override bool Equals(object obj) => Equals(obj as Coordinate);
    public override int GetHashCode() => (X, Y).GetHashCode();
  }
}
