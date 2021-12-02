using System;
using System.Text;
using Advent_of_Code_2021.Day_1;

namespace Advent_of_Code_2021
{
    class Program
    {
        static void Main(string[] args)
        {
            var day = DateTime.Today.Day;
            var dayPath = $"Advent_of_Code_2021.Day_{day}.Day{day}";
            var dayType = Type.GetType(dayPath) ??
                          throw new Exception($"No class found at: {dayPath}");
            var dayInstance = (IDay) Activator.CreateInstance(dayType) ??
                              throw new Exception("Could not make instance of today's challenge.");

            Console.WriteLine($"Now running: {dayPath}");
            try
            {
                Console.WriteLine("Solution problem 1:");
                dayInstance.PrintSolution1();
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("Day 1 not solved yet");
            }

            Console.WriteLine();

            try
            {
                Console.WriteLine("Solution problem 2:");
                dayInstance.PrintSolution2();
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("Day 2 not solved yet");
            }
            
            Console.WriteLine();
            Console.WriteLine(Encoding.UTF8.GetString(Convert.FromBase64String(_xmassTree)));
        }

        private static string _xmassTree = "ICAgKiAgICAqICAoKSAgICogICAqCiogICAgICAgICogL1wgICAgICAgICAqCiAgICAgICogICAvaVxcICAgICogICoKICAgICogICAgIG8vXFwgICogICAgICAqCiAqICAgICAgIC8vL1xpXCAgICAqCiAgICAgKiAgIC8qL29cXCAgKiAgICAqCiAgICogICAgL2kvL1wqXCAgICAgICoKICAgICAgICAvby8qXFxpXCAgICoKICAqICAgIC8vaS8vb1xcXFwgICAgICoKICAgICogLyovLy8vXFxcXGlcKgogKiAgICAvL28vL2lcXCpcXFwgICAqCiAgICogL2kvLy8qL1xcXFxcb1wgICAqCiAgKiAgICAqICAgfHwgICAgICogICAg";
    }
}