using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_7
{
    public class Day7: IDay
    {
        private IEnumerable<int> ReadPositions()
        {
            return File.ReadAllText("Day 7/input").Split(',').Select(int.Parse);
        }
        
        private static int CalculateTriangularNumber(int n) {
            return n * (n + 1) / 2;
        }

        public static IEnumerable<(int fuelCost, int triangularFuelCost)> CalculateFuelCosts(IEnumerable<int> submarinePositions)
        {
            submarinePositions = submarinePositions.ToList();
            foreach (var comparingPosition in Enumerable.Range(0, submarinePositions.Max()))
            {
                var fuelCosts = submarinePositions
                    .Select(s => Math.Abs(comparingPosition - s))
                    .Sum();
                
                var triangularFuelCost = submarinePositions
                    .Select(s => Math.Abs(comparingPosition - s))
                    .Select(CalculateTriangularNumber)
                    .Sum();
                
                yield return (fuelCosts, triangularFuelCost);
            }
        }

        public void PrintSolution1()
        {
            Console.WriteLine(CalculateFuelCosts(ReadPositions()).OrderBy(s => s.fuelCost).First().fuelCost);
        }

        public void PrintSolution2()
        {
            Console.WriteLine(CalculateFuelCosts(ReadPositions()).OrderBy(s => s.triangularFuelCost).First().triangularFuelCost);
        }
    }
}