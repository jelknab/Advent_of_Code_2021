using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_09
{
    public class Day9: IDay
    {
        private static readonly (int x, int y)[] Adjacent = {
            (-1, 0),
            (0, -1),
            (1, 0),
            (0, 1)
        };
        
        private static IEnumerable<string> ReadInput()
        {
            return File.ReadAllLines("Day 09/input");
        }
        
        public static int[,] ParseGrid(IEnumerable<string> rows)
        {
            rows = rows.ToList();

            var rowCount = rows.Count();
            var colCount = rows.First().Length;

            var grid = new int[rowCount, colCount];

            var allValues = rows.SelectMany(row => row.ToCharArray().Select(c => c - '0')).ToArray();
            for (var index = 0; index < allValues.Length; index++)
            {
                grid[index / colCount, index % colCount] = allValues[index];
            }

            return grid;
        }

        private static int? GetCellValueIgnoreBound(int[,] grid, int x, int y)
        {
            try
            {
                return grid[y, x];
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
        }
        
        public static IEnumerable<(int x, int y, int value)> FindLowSpots(int[,] grid)
        {
            for (var y = 0; y < grid.GetLength(0); y++)
            {
                for (var x = 0; x < grid.GetLength(1); x++)
                {
                    var adjacentCellLower = Adjacent
                        .Select(xy => GetCellValueIgnoreBound(grid, x + xy.x, y + xy.y))
                        .Any(value => value <= grid[y, x]);

                    if (!adjacentCellLower)
                    {
                        yield return (x, y, grid[y, x]);
                    }
                }
            }
        }

        public static int FindBasinSize(int[,] grid, int x, int y, List<(int x, int y)> exclude)
        {
            if (exclude.Contains((x, y)))
            {
                return 0;
            }
            
            exclude.Add((x, y));
            
            if (grid[y, x] == 9)
            {
                return 0;
            }

            var size = 1;
            
            foreach (var (compX, compY) in Adjacent)
            {
                if (GetCellValueIgnoreBound(grid, x + compX, y + compY) > grid[y, x])
                {
                    size += FindBasinSize(grid, x + compX, y + compY, exclude);
                }
            }
            
            return size;
        }

        public static int GetLowSpotsSum(int[,] grid)
        {
            return FindLowSpots(grid).Select(spot => spot.value + 1).Sum();
        }

        public static int MultiplyHighest3Basins(int[,] grid)
        {
            return FindLowSpots(grid)
                .Select(spot => FindBasinSize(grid, spot.x, spot.y, new List<(int x, int y)>()))
                .OrderByDescending(size => size)
                .Take(3)
                .Aggregate((a, b) => a * b);
        }
        
        public void PrintSolution1()
        {
            Console.WriteLine(GetLowSpotsSum(ParseGrid(ReadInput())));
        }

        public void PrintSolution2()
        {
            Console.WriteLine(MultiplyHighest3Basins(ParseGrid(ReadInput())));
        }
    }
}