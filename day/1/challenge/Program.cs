using System;
using System.IO;

namespace challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();

            //var totalFuel = program.ExecuteProgram("input.txt");
            //Console.WriteLine($"Fuel required is: {totalFuel}.");
        }

        public int ExecuteProgram(string input) {
            var totalFuel = 0;

            var fileStream = new FileStream(input, FileMode.Open);
            using (var streamReader = new StreamReader(fileStream)) {
                while (!streamReader.EndOfStream) {
                    var line = streamReader.ReadLine();
                    var mass = int.Parse(line);

                    totalFuel += CalculateFuel(mass);
                }
            }

            return totalFuel;
        }

        private int CalculateFuel(int mass) {
            return ((int) Math.Floor(mass / 3.0)) - 2;
        }
    }
}
