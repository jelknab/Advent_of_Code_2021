using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Advent_of_Code_2021.Day_05;

namespace Advent_of_Code_2021.Day_13
{
    public struct FoldInstruction
    {
        public char Axis { get; set; }
        public int FoldDistance { get; set; }
    }

    public class Day13 : IDay
    {
        public static (int[,] dotGrid, FoldInstruction[] instructions) ParseInput(IEnumerable<string> lines)
        {
            lines = lines.ToArray();
            var dots = lines
                .TakeWhile(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => line.Split(','))
                .Select(sCoords => (int.Parse(sCoords[1]), int.Parse(sCoords[0])))
                .ToArray();

            var grid = new int[dots.Select(dot => dot.Item1).Max() + 1, dots.Select(dot => dot.Item2).Max() + 1];
            foreach (var (y, x) in dots)
            {
                grid[y, x] = 1;
            }

            var instructions = lines
                .Skip(dots.Length + 1)
                .Select(line => Regex.Match(line, @"(\w)=(\d+)$"))
                .Select(match => new FoldInstruction
                {
                    Axis = match.Groups[1].Value[0],
                    FoldDistance = int.Parse(match.Groups[2].Value)
                })
                .ToArray();

            return (grid, instructions);
        }

        public static int[,] Fold(int[,] inputGrid, FoldInstruction instruction)
        {
            switch (instruction.Axis)
            {
                case 'y':
                {
                    var outPutGrid = new int[inputGrid.GetLength(0) - instruction.FoldDistance - 1,
                        inputGrid.GetLength(1)];
                    for (var y = 0; y < instruction.FoldDistance; y++)
                    {
                        for (var x = 0; x < inputGrid.GetLength(1); x++)
                        {
                            outPutGrid[y, x] = inputGrid[y, x];
                        }
                    }

                    foreach (var foldRow in Enumerable.Range(0, instruction.FoldDistance))
                    {
                        for (var x = 0; x < inputGrid.GetLength(1); x++)
                        {
                            var fromY = instruction.FoldDistance + foldRow + 1;
                            var toY = instruction.FoldDistance - foldRow - 1;
                            outPutGrid[toY, x] += inputGrid[fromY, x];
                        }
                    }

                    return outPutGrid;
                }
                case 'x':
                {
                    var outPutGrid = new int[inputGrid.GetLength(0),
                        inputGrid.GetLength(1) - instruction.FoldDistance - 1];
                    for (var x = 0; x < instruction.FoldDistance; x++)
                    {
                        for (var y = 0; y < inputGrid.GetLength(0); y++)
                        {
                            outPutGrid[y, x] = inputGrid[y, x];
                        }
                    }

                    foreach (var foldRow in Enumerable.Range(0, instruction.FoldDistance))
                    {
                        for (var y = 0; y < inputGrid.GetLength(0); y++)
                        {
                            var fromX = instruction.FoldDistance + foldRow + 1;
                            var toX = instruction.FoldDistance - foldRow - 1;
                            outPutGrid[y, toX] += inputGrid[y, fromX];
                        }
                    }

                    return outPutGrid;
                }
            }

            return null;
        }


        private void PrintGrid(int[,] grid)
        {
            for (var y = 0; y < grid.GetLength(0); y++)
            {
                for (var x = 0; x < grid.GetLength(1); x++)
                {
                    Console.Write(grid[y, x] > 0 ? '#' : '.');
                }

                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine();
        }

        public void PrintSolution1()
        {
            var input = ParseInput(File.ReadLines("Day 13/input"));

            var singleFold = Fold(input.dotGrid, input.instructions.First());
            Console.WriteLine(singleFold.Cast<int>().Count(dots => dots > 0));
        }

        public void PrintSolution2()
        {
            var input = ParseInput(File.ReadLines("Day 13/input"));

            var foldGrid = input.dotGrid;

            foreach (var inputInstruction in input.instructions)
            {
                foldGrid = Fold(foldGrid, inputInstruction);
            }
            
            PrintGrid(foldGrid);
        }
    }
}