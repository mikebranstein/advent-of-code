using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using IntcodeComputer;

namespace HullPainting
{
  public class Robot
  {
    private Computer _computer;
    private List<long> _memory;
    private Dictionary<Coordinate, Color> _path;

    public Robot(string inputFileName)
    {
      _computer = new Computer();
      _memory = _computer.ReadMemoryFromFile(inputFileName);
      _path = new Dictionary<Coordinate, Color>();
    }

    public int GetNumPanelsPainted()
    {
      return _path.Count;
    }

    public List<string> OutputPanelText()
    {
      // get panel dimensions
      var minX = _path.Keys.OrderBy(x => x.X).ToList()[0].X;
      var maxX = _path.Keys.OrderBy(x => x.X).ToList()[_path.Keys.Count - 1].X;
      var minY = _path.Keys.OrderBy(x => x.Y).ToList()[0].Y;
      var maxY = _path.Keys.OrderBy(x => x.Y).ToList()[_path.Keys.Count - 1].Y;

      var panel = new List<string>();

      // create list of strings
      for (var y = maxY; y >= minY; y--)
      {
        var line = "";
        for (var x = minX; x < maxX; x++)
        {
          var color = ReadColor(x, y);
          line += color == Color.Black ? "." : "#";
        }
        panel.Add(line);
      }

      return panel;
    }

    public async Task Run(Color startingPanelColor)
    {
      // start the computer
      var inputBuffer = new BufferBlock<long>();
      var outputBuffer = new BufferBlock<long>();
      var computerExecution = Task.Run(() => _computer.ExecuteProgram(_memory, inputBuffer, outputBuffer));

      // set initial position
      var x = 0;
      var y = 0;
      var currentFacingDirection = Direction.Up;
      SetColor(x, y, startingPanelColor);

      while (!computerExecution.IsCompleted)
      {
        // get color under robot
        var currentColor = ReadColor(x, y);

        // send color to input
        Task.Run(() => inputBuffer.SendAsync((int)currentColor)).Wait();

        try
        {
          // get color and direction
          var newColor = (Color)await outputBuffer.ReceiveAsync(new TimeSpan(0, 0, 5));
          var rotationDirection = (Direction)await outputBuffer.ReceiveAsync(new TimeSpan(0, 0, 5));

          // color the space
          SetColor(x, y, newColor);

          // change direction
          currentFacingDirection = GetNewFacingDirection(currentFacingDirection, rotationDirection);

          // move
          if (currentFacingDirection == Direction.Up) y++;
          else if (currentFacingDirection == Direction.Down) y--;
          else if (currentFacingDirection == Direction.Right) x++;
          else if (currentFacingDirection == Direction.Left) x--;
        }
        catch (TimeoutException timeout)
        {
          if (computerExecution.IsCompleted) return;
        }
      }
    }

    private Direction GetNewFacingDirection(Direction currentDirection, Direction rotationDirection)
    {
      return
        currentDirection == Direction.Up && rotationDirection == Direction.Left ? Direction.Left :
        currentDirection == Direction.Up && rotationDirection == Direction.Right ? Direction.Right :
        currentDirection == Direction.Left && rotationDirection == Direction.Left ? Direction.Down :
        currentDirection == Direction.Left && rotationDirection == Direction.Right ? Direction.Up :
        currentDirection == Direction.Down && rotationDirection == Direction.Left ? Direction.Right :
        currentDirection == Direction.Down && rotationDirection == Direction.Right ? Direction.Left :
        currentDirection == Direction.Right && rotationDirection == Direction.Left ? Direction.Up :
        currentDirection == Direction.Right && rotationDirection == Direction.Right ? Direction.Down:
        Direction.Up;
    }

    private Color ReadColor(int x, int y)
    {
      var coordinate = new Coordinate(x, y);
      if (!_path.ContainsKey(coordinate)) return Color.Black;

      return _path[coordinate];
    } 

    private void SetColor(int x, int y, Color color)
    {
      var coordinate = new Coordinate(x, y);

      if (!_path.ContainsKey(coordinate))
        _path.Add(coordinate, color);

      else _path[coordinate] = color;
    }
  }

  public enum Direction
  {
    Left = 0,
    Right = 1,
    Up = 2,
    Down = 3
  }

  public enum Color
  {
    Black = 0,
    White = 1
  }
}
