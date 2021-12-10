using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_06
{
    public class Day6: IDay
    {
        private IEnumerable<int> ReadInput()
        {
            return File.ReadAllText("Day 06/input").Split(',').Select(int.Parse);
        }

        public static long SimulateDays(IEnumerable<int> fishAges, int days)
        {
            var fishPerAge = new long[9];
            foreach (var age in fishAges) fishPerAge[age]++;

            for (var day = 0; day < days; day++)
            {
                var birthingCount = fishPerAge[0];
                
                for (var i = 0; i < 8; i++)
                {
                    fishPerAge[i] = fishPerAge[i + 1]; // shift ages left
                }

                fishPerAge[6] += birthingCount;
                fishPerAge[8] = birthingCount;
            }

            return fishPerAge.Sum();
        }
        
        public void PrintSolution1()
        {
            Console.WriteLine(SimulateDays(ReadInput(), 80));
        }

        public void PrintSolution2()
        {
            Console.WriteLine(SimulateDays(ReadInput(), 256));
        }
    }
}