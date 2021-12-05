using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic;

namespace Advent_of_Code_2021.Day_5
{
    public struct Line
    {
        public int X1 { get; set; }
        public int X2 { get; set; }
        public int Y1 { get; set; }
        public int Y2 { get; set; }
    }
    
    public class Day5: IDay
    {
        private static IEnumerable<string> ReadInput()
        {
            return File.ReadLines("Day 5/input").ToArray();
        }
        
        public static IEnumerable<Line> ParseLines(IEnumerable<string> input)
        {
            return input
                .Select(line => line.Replace(" -> ", ","))
                .Select(line => line.Split(',').Select(int.Parse).ToArray())
                .Select(line => new Line
                {
                    X1 = line[0], 
                    Y1 = line[1], 
                    X2 = line[2], 
                    Y2 = line[3]
                });
        }

        public static IEnumerable<Line> GetStraightLines(IEnumerable<Line> lines)
        {
            return lines.Where(line => line.X1 == line.X2 || line.Y1 == line.Y2);
        }

        public static int[,] MapLines(IEnumerable<Line> lines)
        {
            lines = lines.ToList();
            var maxX = lines.SelectMany(line => new[] {line.X1, line.X2}).Max() +1;
            var maxY = lines.SelectMany(line => new[] {line.Y1, line.Y2}).Max() +1;
            var grid = new int[maxY, maxX];

            foreach (var line in lines)
            {
                var x = line.X1;
                var y = line.Y1;

                while (x != line.X2 || y != line.Y2)
                {
                    grid[y, x] += 1;
                    
                    x += Math.Sign(line.X2 - x);
                    y += Math.Sign(line.Y2 - y);
                }
                
                grid[line.Y2, line.X2] += 1;
            }

            return grid;
        }

        public static int CountOverlappingPoints(int[,] map)
        {
            return map.Cast<int>().Count(overlaps => overlaps > 1);
        }
        
        public void PrintSolution1()
        {
            var lines = GetStraightLines(ParseLines(ReadInput()));
            Console.WriteLine(CountOverlappingPoints(MapLines(lines)));
        }

        public void PrintSolution2()
        {            
            var lines = ParseLines(ReadInput());
            Console.WriteLine(CountOverlappingPoints(MapLines(lines)));
        }
    }
}