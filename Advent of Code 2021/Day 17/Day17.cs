using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2021.Day_17
{
    public class TargetArea
    {
        public int X1 { get; set; }
        public int X2 { get; set; }
        public int Y1 { get; set; }
        public int Y2 { get; set; }
    }
    
    public class Day17 : IDay
    {
        public static TargetArea ParseInput(string line)
        {
            var match = Regex.Match(line, "target area: x=(-?\\d+)\\.\\.(-?\\d+), y=(-?\\d+)\\.\\.(-?\\d+)")
                .Groups
                .Values
                .ToArray();

            return new TargetArea{
                X1 = int.Parse(match[1].Value),
                X2 = int.Parse(match[2].Value),
                Y1 = int.Parse(match[3].Value),
                Y2 = int.Parse(match[4].Value)
            };
        }

        public static int FindMinXStartVelocity(TargetArea targetArea)
        {
            var x = 0;
            for (var velocity = 0;; velocity++)
            {
                x += velocity;
                if (x >= targetArea.X1) return velocity;
            }
        }
        
        public static int FindMaxXStartVelocity(TargetArea targetArea)
        {
            var x = 0;
            var velocity = 0;
            for (;; velocity++)
            {
                x += velocity;
                if (x > targetArea.X2) return velocity - 1;
            }
        }
        
        public static int FindMinYStartVelocity(TargetArea targetArea)
        {
            var velocityAtZero = 0;

            for (;; velocityAtZero++)
            {
                if (velocityAtZero >= Math.Abs(targetArea.Y2))
                {
                    return velocityAtZero;
                }
            }
        }

        public static int FindMaxYStartVelocity(TargetArea targetArea)
        {
            var velocityAtZero = 0;

            for (;; velocityAtZero++)
            {
                if (velocityAtZero >= Math.Abs(targetArea.Y1))
                {
                    return velocityAtZero - 1;
                }
            }
        }

        private static bool hits(int velX, int velY, TargetArea targetArea)
        {
            var x = 0;
            var y = 0;
            for (var step = 0;; step++)
            {
                x += Math.Max(velX--, 0);
                y += velY--;

                if (x >= targetArea.X1 && x <= targetArea.X2)
                {
                    if (y >= targetArea.Y1 && y <= targetArea.Y2)
                    {
                        return true;
                    }
                }

                if (x > targetArea.X2)
                {
                    return false;
                }

                if (y < targetArea.Y1)
                {
                    return false;
                }
            }
        }

        public static int CountAllLaunchOptions(TargetArea targetArea)
        {
            var hitCount = 0;
            
            for (var x = FindMinXStartVelocity(targetArea); x <= targetArea.X2; x++)
            {
                for (var y = targetArea.Y1; y <= FindMaxYStartVelocity(targetArea); y++)
                {
                    if (hits(x, y, targetArea))
                    {
                        hitCount++;
                    }
                }
            }

            return hitCount;
        }

        public void PrintSolution1()
        {
            var targetArea = ParseInput("target area: x=88..125, y=-157..-103");
            var maxStartY = FindMaxYStartVelocity(targetArea);
            Console.WriteLine(maxStartY * (maxStartY+1) / 2);
        }

        public void PrintSolution2()
        {
            var targetArea = ParseInput("target area: x=88..125, y=-157..-103");
            Console.WriteLine(CountAllLaunchOptions(targetArea));
        }
    }
}