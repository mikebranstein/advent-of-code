using System;
using System.Collections.Generic;
using System.Linq;
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
      do
      {
        var program = new Program();
        Task.Run(() => program.Run("input_2.txt")).Wait();
        Console.WriteLine("Replay?");
        Console.ReadKey();
      }
      while (true);
    }

    public Program()
    {
      GameBoard = new Dictionary<Coordinate, TileType>();
      Score = 0;
      PreviousBallLocationX = 0;
    }

    public int Score { get; set; }
    public Dictionary<Coordinate, TileType> GameBoard { get; set; }
    public int PreviousBallLocationX { get; set; }

    public void PrintGameBoard()
    {
      // get max x,y coords
      var maxX = GameBoard.Max(x => x.Key.X);
      var maxY = GameBoard.Max(x => x.Key.Y);

      for (var y = 0; y < maxY; y++)
      {
        var line = "";
        for (var x =0; x < maxX; x++)
        {
          var tileType = ReadTileType(x, y);
          var charToAdd =
            tileType == TileType.Empty ? " " :
            tileType == TileType.Ball ? "o" :
            tileType == TileType.Block ? "@" :
            tileType == TileType.Wall ? "#" :
            tileType == TileType.Paddle ? "_" :
            " ";
          line += charToAdd;
        }
        Console.WriteLine($"{line}");
      }
    }

    public async Task Run(string inputFileName)
    {
      var computer = new Computer();
      var memory = computer.ReadMemoryFromFile(inputFileName);

      // start the computer
      var inputBuffer = new BufferBlock<long>();
      var outputBuffer = new BufferBlock<long>();
      var instructionPointer = 0;
      var relativeBase = 0;

      IInstruction computerInstruction = null;
      int lastGameMove = 0;
      do
      {
        var instructions = new List<long>();

        // process output 1
        computerInstruction = computer.ExecuteProgramUntilOutput(memory, inputBuffer, outputBuffer, ref instructionPointer, ref relativeBase);
        if (computerInstruction.GetType() == typeof(HaltInstruction)) continue;

        instructions.Add(await outputBuffer.ReceiveAsync());

        // get output 2
        computerInstruction = computer.ExecuteProgramUntilOutput(memory, inputBuffer, outputBuffer, ref instructionPointer, ref relativeBase);
        instructions.Add(await outputBuffer.ReceiveAsync());

        // get output 3
        computerInstruction = computer.ExecuteProgramUntilOutput(memory, inputBuffer, outputBuffer, ref instructionPointer, ref relativeBase);
        instructions.Add(await outputBuffer.ReceiveAsync());

        ProcessInstruction((int)instructions[0], (int)instructions[1], (int)instructions[2], inputBuffer);
        lastGameMove = (int)instructions[2];

        if (lastGameMove == 3) PrintGameBoard();

        // ball or paddle moved
        if (lastGameMove == 4)
        {
          // print to screen
          PrintGameBoard();

          //// get key
          //Console.WriteLine($"Current score: {Score}");
          //Console.WriteLine("Move paddle? Left, right, none (up)?");
          //
          //var key = Console.ReadKey();
          //while (key.Key != ConsoleKey.LeftArrow && key.Key != ConsoleKey.RightArrow && key.Key != ConsoleKey.UpArrow)
          //{
          //  Console.WriteLine("Move paddle? Left, right, none (up)?");
          //  key = Console.ReadKey();
          //}
          //if (key.Key == ConsoleKey.UpArrow) await inputBuffer.SendAsync(0);
          //else if (key.Key == ConsoleKey.LeftArrow) await inputBuffer.SendAsync(-1);
          //else if (key.Key == ConsoleKey.RightArrow) await inputBuffer.SendAsync(1);
        }
      }
      while (computerInstruction.GetType() != typeof(HaltInstruction));

      Console.WriteLine($"Final score: {Score}");
    }

    private void ProcessInstruction(int x, int y, int parameter, BufferBlock<long> inputBuffer)
    {
      if (x == -1 && y == 0)
      {
        Score = parameter;
        return;
      }

      var tileType = (TileType)parameter;
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

          var paddleLocation = GetPaddleLocation();
          if (paddleLocation == null)
          {
            Task.Run(() => inputBuffer.SendAsync(0)).Wait();
            break;
          }



          var ballMovingRight = (PreviousBallLocationX - x < 0);
          var paddleOnRightOfBall = (paddleLocation.X - x > 0);

          if (ballMovingRight)
          {
            if (paddleOnRightOfBall)
            {
              // move paddle left
              var spacesToMoveLeft = paddleLocation.X - (x + 1);
              //if (spacesToMoveLeft == 0)
              //  Task.Run(() => inputBuffer.SendAsync(0)).Wait();
              //else
              //  for (var i = 0; i < spacesToMoveLeft; i++)
                  Task.Run(() => inputBuffer.SendAsync(-1)).Wait();
            }
            else
            {
              // move paddle right
              var spacesToModeRight = x - paddleLocation.X + 1;
              //if (spacesToModeRight == 0)
              //  Task.Run(() => inputBuffer.SendAsync(0)).Wait();
              //else 
              //  for (var i = 0; i < spacesToModeRight; i++)
                  Task.Run(() => inputBuffer.SendAsync(1)).Wait();
            }            
          }
          else
          {
            if (paddleOnRightOfBall)
            {
              // move paddle left
              var spacesToMoveLeft = paddleLocation.X - x + 1;
              //if (spacesToMoveLeft == 0)
              //  Task.Run(() => inputBuffer.SendAsync(0)).Wait();
              //else
              //  for (var i = 0; i < spacesToMoveLeft; i++)
                  Task.Run(() => inputBuffer.SendAsync(-1)).Wait();
            }
            else
            {
              // move paddle right
              var spacesToModeRight = x - (paddleLocation.X + 1);
              //if (spacesToModeRight == 0)
              //  Task.Run(() => inputBuffer.SendAsync(0)).Wait();
              //else
              //  for (var i = 0; i < spacesToModeRight; i++)
                  Task.Run(() => inputBuffer.SendAsync(1)).Wait();
            }
          }

          PreviousBallLocationX = x;
          break;
      }

    }

    private Coordinate GetPaddleLocation()
    {
      return GameBoard.Where(x => x.Value == TileType.Paddle).FirstOrDefault().Key;
    }

    private Coordinate GetBallLocation()
    {
      return GameBoard.Where(x => x.Value == TileType.Ball).FirstOrDefault().Key;
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
