using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_2
{
    public class Day2: IDay
    {
        private static IEnumerable<string> ReadCommandInput()
        {
            return File.ReadLines("Day 2/input");
        }
        
        public static IEnumerable<(string command, int value)> ReadCommands(IEnumerable<string> input)
        {
            return input
                .Select(i => i.Split(' '))
                .Select(commandParts => (command: commandParts[0], value: int.Parse(commandParts[1])));
        }
        
        public static (int x, int y) RunCommandsPart1(IEnumerable<(string command, int value)> commands)
        {
            var x = 0;
            var y = 0;
            foreach (var (command, value) in commands)
            {
                switch (command)
                {
                    case "forward":
                        x += value;
                        break;
                    case "down":
                        y += value;
                        break;
                    case "up":
                        y -= value;
                        break;
                    
                    default:
                        throw new Exception($"Unknown command {command}");
                        
                }
            }

            return (x, y);
        }
        
        public void PrintSolution1()
        {
            var (x, y) = RunCommandsPart1(ReadCommands(ReadCommandInput()));
            Console.WriteLine(x * y);
        }

        public static (int x, int y) RunCommandsPart2(IEnumerable<(string command, int value)> commands)
        {
            var x = 0;
            var y = 0;
            var aim = 0;
            foreach (var (command, value) in commands)
            {
                switch (command)
                {
                    case "forward":
                        x += value;
                        y += value * aim;
                        break;
                    case "down":
                        aim += value;
                        break;
                    case "up":
                        aim -= value;
                        break;
                    
                    default:
                        throw new Exception($"Unknown command {command}");
                }
            }

            return (x, y);
        }

        public void PrintSolution2()
        {
            var (x, y) = RunCommandsPart2(ReadCommands(ReadCommandInput()));
            Console.WriteLine(x * y);
        }
    }
}