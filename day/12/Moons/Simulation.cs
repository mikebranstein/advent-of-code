using System;
using System.Collections.Generic;
using System.Linq;

namespace Moons
{
  public class Simulation
  {
    public List<Moon> Moons { get; set; }
    private HashSet<int> _previousStates;

    public int PotentialEnergy => Moons.Sum(x => x.PotentialEnergy);
    public int KineticEnergy => Moons.Sum(x => x.KineticEnergy);
    public int TotalEnergy => Moons.Sum(x => x.TotalEnergy);
    private string _inputFileName;

    public Simulation(string inputFileName)
    {
      _inputFileName = inputFileName;
      Reset();
    }

    public void Reset()
    {
      Moons = Moon.ParseInputFile(_inputFileName);
      _previousStates = new HashSet<int>();
    }

    public long CalculateRepeatRate()
    {
      // get repeat only for X's
      var initialXState = new List<SingleCoord>()
      {
        new SingleCoord() { Location = Moons[0].X, Velocity = Moons[0].VelocityX },
        new SingleCoord() { Location = Moons[1].X, Velocity = Moons[1].VelocityX },
        new SingleCoord() { Location = Moons[2].X, Velocity = Moons[2].VelocityX },
        new SingleCoord() { Location = Moons[3].X, Velocity = Moons[3].VelocityX }
      };
      var repeatRateX = GetSingleCoordinateRepeatRate(initialXState);

      // get repeat only for Y's
      var initialYState = new List<SingleCoord>()
      {
        new SingleCoord() { Location = Moons[0].Y, Velocity = Moons[0].VelocityY },
        new SingleCoord() { Location = Moons[1].Y, Velocity = Moons[1].VelocityY },
        new SingleCoord() { Location = Moons[2].Y, Velocity = Moons[2].VelocityY },
        new SingleCoord() { Location = Moons[3].Y, Velocity = Moons[3].VelocityY }
      };
      var repeatRateY = GetSingleCoordinateRepeatRate(initialYState);

      // get repeat only for Z's
      var initialZState = new List<SingleCoord>()
      {
        new SingleCoord() { Location = Moons[0].Z, Velocity = Moons[0].VelocityZ },
        new SingleCoord() { Location = Moons[1].Z, Velocity = Moons[1].VelocityZ },
        new SingleCoord() { Location = Moons[2].Z, Velocity = Moons[2].VelocityZ },
        new SingleCoord() { Location = Moons[3].Z, Velocity = Moons[3].VelocityZ }
      };
      var repeatRateZ = GetSingleCoordinateRepeatRate(initialZState);

      var lcm = LCM(LCM(repeatRateX, repeatRateY), repeatRateZ);

      return lcm;

      //return count - 1;
    }

    public static long LCM(long a, long b)
    {
      long num1, num2;
      if (a > b)
      {
        num1 = a; num2 = b;
      }
      else
      {
        num1 = b; num2 = a;
      }

      for (long i = 1; i < num2; i++)
      {
        if ((num1 * i) % num2 == 0)
        {
          return i * num1;
        }
      }
      return num1 * num2;
    }


    private long GetSingleCoordinateRepeatRate(List<SingleCoord> initialState)
    {
      long count = 0;
      var repeatFound = false;

      var initialStateCopy = new List<SingleCoord>();
      foreach (var state in initialState)
      {
        initialStateCopy.Add(new SingleCoord() { Location = state.Location, Velocity = state.Velocity });
      }

      while (!repeatFound)
      {
        SingleCoordinateTick(1, initialState);
        count++;

        repeatFound =
          initialState[0].Location == initialStateCopy[0].Location &&
          initialState[0].Velocity == initialStateCopy[0].Velocity &&
          initialState[1].Location == initialStateCopy[1].Location &&
          initialState[1].Velocity == initialStateCopy[1].Velocity &&
          initialState[2].Location == initialStateCopy[2].Location &&
          initialState[2].Velocity == initialStateCopy[2].Velocity &&
          initialState[3].Location == initialStateCopy[3].Location &&
          initialState[3].Velocity == initialStateCopy[3].Velocity;
      }

      return count;
    }

    public void SingleCoordinateTick(long count, List<SingleCoord> moons)
    {
      for (long tick = 0; tick < count; tick++)
      {
        // update velocity
        for (var i = 0; i < moons.Count; i++)
        { 
          var moon1Location = moons[i].Location;

          for (var j = i + 1; j < moons.Count; j++)
          {
            var moon2Location = moons[j].Location;

            var deltaV = Moon.CalculateDeltaV(moon1Location, moon2Location);
            moons[i].Velocity += deltaV.thisChange;
            moons[j].Velocity += deltaV.otherChange;
          }
        }

        // update position by applying velocity
        moons.ForEach(moon => {
          moon.Location += moon.Velocity;
        });
      }
    }

    public void Tick(long count)
    {
      for (long tick = 0; tick < count; tick++)
      {
        // update velocity
        for (var i = 0; i < Moons.Count; i++)
        {
          var moon1 = Moons[i];
          for (var j = i + 1; j < Moons.Count; j++)
          {
            var moon2 = Moons[j];

            moon1.UpdateVelocity(moon2);
          }
        }

        // update position by applying velocity
        Moons.ForEach(moon => {
          moon.X += moon.VelocityX;
          moon.Y += moon.VelocityY;
          moon.Z += moon.VelocityZ;
        });
      }
    }



  }

  public class SingleCoord
  {
    public int Location { get; set; }
    public int Velocity { get; set; }
  }


}
