using System;
using Advent_of_Code_2021.Day_1;

namespace Advent_of_Code_2021
{
    class Program
    {
        static void Main(string[] args)
        {
            var dayPath = $"Advent_of_Code_2021.Day_{DateTime.Today.Day}.Day{DateTime.Today.Day}";
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
    }
}