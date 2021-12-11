using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_11
{
    public class Day11 : IDay
    {
        private static readonly (int x, int y)[] AdjacentCells =
        {
            (1, 1),
            (1, 0),
            (1, -1),
            (0, 1),
            (0, -1),
            (-1, 1),
            (-1, 0),
            (-1, -1)
        };

        private static IEnumerable<string> ReadInput()
        {
            return File.ReadAllLines("Day 11/input");
        }

        public static int[,] ParseInput(IEnumerable<string> input)
        {
            var lines = input.ToArray();
            var grid = new int[lines.Length, lines[0].Length];

            for (var y = 0; y < lines.Length; y++)
            {
                for (var x = 0; x < lines[y].Length; x++)
                {
                    grid[y, x] = lines[y][x] - '0';
                }
            }

            return grid;
        }

        public static int RunStep(int[,] grid)
        {
            var flashMap = new bool[grid.GetLength(0), grid.GetLength(1)];

            for (var y = 0; y < grid.GetLength(0); y++)
            {
                for (var x = 0; x < grid.GetLength(1); x++)
                {
                    grid[y, x]++;
                }
            }


            do
            {
                for (var y = 0; y < grid.GetLength(0); y++)
                {
                    for (var x = 0; x < grid.GetLength(1); x++)
                    {
                        if (grid[y, x] > 9)
                        {
                            Flash(grid, flashMap, x, y);
                        }
                    }
                }
            } while (grid.Cast<int>().Any(val => val > 9));

            return flashMap.Cast<bool>().Count(flash => flash);
        }

        public static void Flash(int[,] grid, bool[,] flashMap, int x, int y)
        {
            grid[y, x] = 0;
            flashMap[y, x] = true;

            foreach (var (ay, ax) in AdjacentCells)
            {
                if (y + ay < 0 || y + ay >= grid.GetLength(0)) continue;
                if (x + ax < 0 || x + ax >= grid.GetLength(1)) continue;

                if (!flashMap[y + ay, x + ax])
                {
                    grid[y + ay, x + ax]++;
                }
            }
        }

        public static int FindSyncStep(int[,] grid)
        {
            var step = 1;
            while (RunStep(grid) != grid.GetLength(0) * grid.GetLength(1))
            {
                step++;
            }

            return step;
        }

        public void PrintSolution1()
        {
            var grid = ParseInput(ReadInput());
            Console.WriteLine(Enumerable.Range(0, 100).Select(day => RunStep(grid)).Sum());
        }

        public void PrintSolution2()
        {
            var grid = ParseInput(ReadInput());
            Console.WriteLine(FindSyncStep(grid));
        }
    }
}