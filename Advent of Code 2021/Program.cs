using System;
using System.Text;

namespace Advent_of_Code_2021
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintDaySolutions(DateTime.Today.Day);
            Console.WriteLine();
            PrintTree();
        }

        private static void PrintDaySolutions(int day)
        {
            var dayLeadingZeros = day.ToString("D2"); 
            var dayPath = $"Advent_of_Code_2021.Day_{dayLeadingZeros}.Day{day}";
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
        }

        private static void PrintTree()
        {
            Console.WriteLine(Encoding.UTF8.GetString(Convert.FromBase64String(XMassTree)));
        }

        private const string XMassTree = "ICAgKiAgICAqICAoKSAgICogICAqCiogICAgICAgICogL1wgICAgICAgICAqCiAgICAgICogICAvaVxcICAgICogICoKICAgICogICAgIG8vXFwgICogICAgICAqCiAqICAgICAgIC8vL1xpXCAgICAqCiAgICAgKiAgIC8qL29cXCAgKiAgICAqCiAgICogICAgL2kvL1wqXCAgICAgICoKICAgICAgICAvby8qXFxpXCAgICoKICAqICAgIC8vaS8vb1xcXFwgICAgICoKICAgICogLyovLy8vXFxcXGlcKgogKiAgICAvL28vL2lcXCpcXFwgICAqCiAgICogL2kvLy8qL1xcXFxcb1wgICAqCiAgKiAgICAqICAgfHwgICAgICogICAg";
    }
}