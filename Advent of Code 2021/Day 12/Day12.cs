using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_12
{
    public class Node
    {
        public string Name { get; set; }
        public List<Node> Connections { get; set; } = new List<Node>();

        private bool IsSmallNode => Name.All(char.IsLower);

        public IEnumerable<IEnumerable<Node>> FindPathsToEnd(Stack<Node> pathToHere)
        {
            pathToHere.Push(this);
            
            if (Name == "end")
            {
                yield return pathToHere.ToArray();
            }
            
            foreach (var connection in Connections)
            {
                if (connection.IsSmallNode && pathToHere.Contains(connection))
                {
                    continue;
                }

                foreach (var path in connection.FindPathsToEnd(pathToHere))
                {
                    yield return path;
                }
            }

            pathToHere.Pop();
        }
        
        public IEnumerable<IEnumerable<Node>> FindPathsToEndAllowTwice(Stack<Node> pathToHere)
        {
            pathToHere.Push(this);
            
            if (Name == "end")
            {
                yield return pathToHere.ToArray();
            }
            else
            {
                foreach (var connection in Connections)
                {
                    if (connection.Name == "start")
                    {
                        continue;
                    }
                    if (connection.IsSmallNode)
                    {
                        var anyVisitedTwice = pathToHere
                            .Where(node => node.IsSmallNode)
                            .GroupBy(node => node)
                            .Any(group => group.Count() == 2);

                        if (anyVisitedTwice)
                        {
                            if (pathToHere.Contains(connection))
                            {
                                continue;
                            }
                        }
                        if (pathToHere.Count(node => node == connection) == 2)
                        {
                            continue;
                        }
                    }

                    foreach (var path in connection.FindPathsToEndAllowTwice(pathToHere))
                    {
                        yield return path;
                    }
                }
            }

            pathToHere.Pop();
        }
    }
    
    public class Day12: IDay
    {
        
        private static IEnumerable<string> ReadInput()
        {
            return File.ReadAllLines("Day 12/input");
        }
        
        public static Dictionary<string, Node> ParseInput(IEnumerable<string> lines)
        {
            var nodes = new Dictionary<string, Node>();
            foreach (var connectionNodes in lines.Select(line => line.Split('-')))
            {
                foreach (var connectionNode in connectionNodes)
                {
                    if (!nodes.ContainsKey(connectionNode))
                    {
                        nodes[connectionNode] = new Node() {Name = connectionNode};
                    }
                }
                
                nodes[connectionNodes[0]].Connections.Add(nodes[connectionNodes[1]]);
                nodes[connectionNodes[1]].Connections.Add(nodes[connectionNodes[0]]);
            }

            return nodes;
        }

        public static int CountPaths(Dictionary<string, Node> nodes)
        {
            return nodes["start"].FindPathsToEnd(new Stack<Node>()).Count();
        }
        
        public static int CountPathsPart2(Dictionary<string, Node> nodes)
        {
            return nodes["start"].FindPathsToEndAllowTwice(new Stack<Node>()).Count();
        }
        
        public void PrintSolution1()
        {
            Console.WriteLine(CountPaths(ParseInput(ReadInput())));
        }

        public void PrintSolution2()
        {
            Console.WriteLine(CountPathsPart2(ParseInput(ReadInput())));
        }
    }
}