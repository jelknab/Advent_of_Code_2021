using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_15
{
    public class Node
    {
        private static (int x, int y)[] _localAdjacentCells = {
            (-1, 0),
            (0, -1),
            (1, 0),
            (0, 1)
        };
        
        public int X { get; set; }
        public int Y { get; set; }
        
        public Node Parent { get; set; }
        
        public bool Visited { get; set; }
        
        public int Weight { get; set; }
        public int TotalWeight { get; set; }

        public IEnumerable<Node> Neighbours(Node[,] grid)
        {
            foreach (var (localX, localY) in _localAdjacentCells)
            {
                var x = X + localX;
                var y = Y + localY;

                if (x >= 0 && x < grid.GetLength(1) && y >= 0 && y < grid.GetLength(0))
                {
                    yield return grid[y, x];
                }
            }
        }
    }
    
    public class Day15: IDay
    {
        public static Node[,] ParseInput(IEnumerable<string> readLines)
        {
            var lines = readLines.ToArray();
            var grid = new Node[lines.Length, lines[0].Length];

            for (var y = 0; y < grid.GetLength(0); y++)
            {
                for (var x = 0; x < grid.GetLength(1); x++)
                {
                    grid[y, x] = new Node()
                    {
                        Parent = null,
                        Visited = false,
                        Weight = lines[y][x] - '0',
                        X = x,
                        Y = y,
                        TotalWeight = 0
                    };
                }
            }

            return grid;
        }

        public static Node[,] ExtendGrid5Times(Node[,] inputGrid)
        {
            var grid = new Node[inputGrid.GetLength(0) * 5, inputGrid.GetLength(1) * 5];

            for (var i = 0; i < 25; i++)
            {
                var xStart = i % 5;
                var yStart = (int) Math.Floor(i / 5.0);

                var increment = xStart + yStart;

                for (var y = 0; y < inputGrid.GetLength(0); y++)
                {
                    for (var x = 0; x < inputGrid.GetLength(1); x++)
                    {
                        var node = new Node()
                        {
                            Parent = null,
                            Visited = false,
                            Weight = 1 + (inputGrid[y, x].Weight + increment - 1) % 9,
                            X = xStart * inputGrid.GetLength(1) + x,
                            Y = yStart * inputGrid.GetLength(0) + y,
                            TotalWeight = 0
                        };

                        if (node.Weight == 0)
                        {
                            node.Weight = 1;
                        }
                        grid[node.Y, node.X] = node;
                    }
                }
            }

            return grid;
        }

        private static Node PopLowestNode(ICollection<Node> nodes)
        {
            var lowest = nodes.First();

            foreach (var node in nodes)
            {
                if (node.TotalWeight < lowest.TotalWeight)
                {
                    lowest = node;
                }
            }

            nodes.Remove(lowest);
            return lowest;
        }
        
        public static IEnumerable<Node> FindLowestRiskPath(Node[,] grid)
        {
            var nextNodes = new List<Node> {grid[0, 0]};

            while (nextNodes.Count > 0)
            {
                var evaluatingNode = PopLowestNode(nextNodes);
                evaluatingNode.Visited = true;

                foreach (var neighbour in evaluatingNode.Neighbours(grid))
                {
                    if (!neighbour.Visited)
                    {
                        nextNodes.Add(neighbour);
                        neighbour.Parent = evaluatingNode;
                        neighbour.TotalWeight = evaluatingNode.TotalWeight + neighbour.Weight;
                        neighbour.Visited = true;
                        continue;
                    }

                    if (evaluatingNode.TotalWeight + neighbour.Weight >= neighbour.TotalWeight) continue;

                    neighbour.Parent = evaluatingNode;
                    neighbour.TotalWeight = evaluatingNode.TotalWeight + neighbour.Weight;
                    nextNodes.Add(neighbour);
                }
            }

            var node = grid[grid.GetLength(0) - 1, grid.GetLength(1) - 1];
            var path = new List<Node>();

            while (node != null)
            {
                path.Add(node);
                node = node.Parent;
            }

            return path.ToArray();
        }
        
        public void PrintSolution1()
        {
            var input = ParseInput(File.ReadLines("Day 15/input"));
            Console.WriteLine(FindLowestRiskPath(input).First().TotalWeight);
        }

        public void PrintSolution2()
        {
            var input = ExtendGrid5Times(ParseInput(File.ReadLines("Day 15/input")));
            Console.WriteLine(FindLowestRiskPath(input).First().TotalWeight);
        }
    }
}