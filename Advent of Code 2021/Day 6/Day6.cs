using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_6
{
    public class Day6: IDay
    {
        private IEnumerable<int> ReadInput()
        {
            return File.ReadAllText("Day 6/input").Split(',').Select(int.Parse);
        }

        public static long SimulateDays(IEnumerable<int> fishes, int days)
        {
            var fishPerDay = new long[9];
            foreach (var fish in fishes)
            {
                fishPerDay[fish]++;
            }

            for (var day = 0; day < days; day++)
            {
                var birthingCount = fishPerDay[0];
                
                for (var i = 0; i < 8; i++)
                {
                    fishPerDay[i] = fishPerDay[i + 1];
                }

                fishPerDay[6] += birthingCount;
                fishPerDay[8] = birthingCount;
            }

            return fishPerDay.Sum();
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