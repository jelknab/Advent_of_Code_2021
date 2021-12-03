using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_1
{
    public class Day1: IDay
    {
        private static IEnumerable<int> ReadSonarInput()
        {
            return File.ReadLines("Day 1/input").Select(int.Parse);
        }

        public static int SumMeasurementIncrements(IEnumerable<int> measurements)
        {
            var increments = 0;
            int? lastValue = null;

            foreach (var value in measurements)
            {
                if (value > lastValue) increments++;
                lastValue = value;
            }

            return increments;
        }
        
        public void PrintSolution1()
        {
            Console.WriteLine(SumMeasurementIncrements(ReadSonarInput()));
        }

        public static IEnumerable<int> MakeMeasurementWindows(IEnumerable<int> measurements)
        {
            var values = measurements.ToArray();

            for (var sonarIndex = 2; sonarIndex < values.Length; sonarIndex++)
            {
                yield return values[sonarIndex] + values[sonarIndex - 1] + values[sonarIndex - 2];
            }
        }

        public void PrintSolution2()
        {
            Console.WriteLine(SumMeasurementIncrements(MakeMeasurementWindows(ReadSonarInput())));
        }
    }
}