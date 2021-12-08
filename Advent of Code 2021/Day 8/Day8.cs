using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_8
{
    public class Day8 : IDay
    {
        private static readonly string[] _referenceSegments = {
            "abcdef",
            "bc",
            "abdeg",
            "abcdg",
            "bcfg",
            "acdfg",
            "acdefg",
            "abc",
            "abcdefg",
            "abcdfg"
        };

        private static IEnumerable<string> ReadInput()
        {
            return File.ReadAllLines("Day 8/input");
        }

        public static IEnumerable<(string[] patterns, string[] output)> ParseInput(IEnumerable<string> lines)
        {
            return lines
                .Select(line => line.Split(" | "))
                .Select(items => (items[0].Split(' '), items[1].Split(' ')));
        }

        public static int CountLengthDetectableSegmentNumbers(IEnumerable<(string[] patterns, string[] output)> inputs)
        {
            return inputs
                .SelectMany(input => input.output)
                .Count(output => new[] {2, 3, 4, 7}.Contains(output.Length));
        }

        private static IEnumerable<string> GetSegmentsByLength(IEnumerable<string> patterns, int length)
        {
            return patterns.Where(pattern => pattern.Length == length);
        }

        public static IEnumerable<(string pattern, int value)> MatchPattersToSegments(string[] patterns)
        {
            var one = GetSegmentsByLength(patterns, 2).First().ToCharArray();
            var four = GetSegmentsByLength(patterns, 4).First().ToCharArray();
            var seven = GetSegmentsByLength(patterns, 3).First().ToCharArray();
            var eight = GetSegmentsByLength(patterns, 7).First().ToCharArray();

            var twoThreeFive = GetSegmentsByLength(patterns, 5).ToArray();
            var zeroSixNine = GetSegmentsByLength(patterns, 6).ToArray();


            var knownSegments = new char[7];
            var segmentA = seven.Except(four).First();
            knownSegments[0] = segmentA;

            var segmentGorD = twoThreeFive
                .SelectMany(s => s.ToCharArray())
                .Where(c => !knownSegments.Contains(c))
                .GroupBy(p => p)
                .Where(group => group.Count() == 3)
                .Select(group => group.Key)
                .ToArray();
            
            var segmentG = four.Except(one).Intersect(segmentGorD).First();
            var segmentD = segmentGorD.Except(new[] {segmentG}).First();

            knownSegments[3] = segmentD;
            knownSegments[6] = segmentG;

            var segmentEorB = zeroSixNine
                .SelectMany(s => s.ToCharArray())
                .Where(c => !knownSegments.Contains(c))
                .GroupBy(p => p)
                .Where(group => group.Count() == 2)
                .Select(group => group.Key)
                .ToArray();

            var segmentB = segmentEorB.Intersect(one).First();
            var segmentE = segmentEorB.Except(new[] {segmentB}).First();
            var segmentC = one.Except(new[] {segmentB}).First();

            knownSegments[1] = segmentB;
            knownSegments[2] = segmentC;
            knownSegments[4] = segmentE;

            var segmentF = eight.Except(knownSegments).First();
            knownSegments[5] = segmentF;

            return patterns.Select(pattern =>
            {
                var fixedSegments = pattern
                    .Select(c => (char) ('a' + Array.FindIndex(knownSegments, item => item == c)))
                    .OrderBy(c => c);

                var actualValue = Array.FindIndex(_referenceSegments, r => string.Concat(fixedSegments) == r);
                return (string.Concat(pattern.OrderBy(c => c)), actualValue);
            });
        }

        public static int GetOutputValue(string[] patterns, IEnumerable<string> outputs)
        {
            var segments = MatchPattersToSegments(patterns);

            return (int) outputs
                .Select(output => string.Concat(output.OrderBy(c => c)))
                .Select(output => segments.First(mp => mp.pattern == output).value)
                .Select((val, index) => val * Math.Pow(10, 3 - index))
                .Sum();
        }

        public void PrintSolution1()
        {
            Console.WriteLine(CountLengthDetectableSegmentNumbers(ParseInput(ReadInput())));
        }

        public void PrintSolution2()
        {
            Console.WriteLine(
                ParseInput(ReadInput())
                    .Select(tuple => GetOutputValue(tuple.patterns, tuple.output))
                    .Sum()
            );
        }
    }
}