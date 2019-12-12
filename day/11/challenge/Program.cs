using System;
using System.Threading.Tasks;
using HullPainting;

namespace challenge
{
  class Program
  {
    static void Main(string[] args)
    {
      // part 1
      Console.WriteLine("Running panel painting robot...");

      var robot = new Robot("input.txt");

      Task.Run(() => robot.Run(Color.Black)).Wait();
      var panelsPainted = robot.GetNumPanelsPainted();

      Console.WriteLine($"Painted {panelsPainted} panels");
      Console.WriteLine("Robot finished running...");

      // Part 2
      robot = new Robot("input.txt");

      Task.Run(() => robot.Run(Color.White)).Wait();
      panelsPainted = robot.GetNumPanelsPainted();

      Console.WriteLine($"Painted {panelsPainted} panels");
      var panels = robot.OutputPanelText();
      foreach (var line in panels)
      {
        Console.WriteLine(line);
      }

      Console.ReadLine();



    }
  }
}
