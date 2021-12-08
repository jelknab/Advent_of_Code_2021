using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_8
{
    public class Day8 : IDay
    {
        private static string[] referenceNumberSegmentMap = new string[]
        {
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

        private string[] ReadInput()
        {
            return File.ReadAllLines("Day 8/input");
        }

        public static IEnumerable<(string[] patterns, string[] output)> ParseInput(IEnumerable<string> lines)
        {
            return lines
                .Select(line =>
                {
                    var split = line.Split(" | ");
                    return (split[0].Split(' '), split[1].Split(' '));
                });
        }

        public static int CountEasySegmentNumbers(IEnumerable<(string[] patterns, string[] output)> inputs)
        {
            return inputs
                .SelectMany(input => input.output)
                .Count(output => new[] {2, 3, 4, 7}.Contains(output.Length));
        }

        private static IEnumerable<string> GetSegmentWithLength(string[] patterns, int length)
        {
            return patterns.Where(pattern => pattern.Length == length);
        }

        public static IEnumerable<(string pattern, int value)> MatchNumbersToPatters(string[] patterns)
        {
            var one = GetSegmentWithLength(patterns, 2).First().ToCharArray();
            var four = GetSegmentWithLength(patterns, 4).First().ToCharArray();
            var seven = GetSegmentWithLength(patterns, 3).First().ToCharArray();
            var eight = GetSegmentWithLength(patterns, 7).First().ToCharArray();

            var twoThreeFive = GetSegmentWithLength(patterns, 5).ToArray();
            var zeroSixNine = GetSegmentWithLength(patterns, 6).ToArray();

            
            var segmentA = seven.Except(four).First();
            
            var segmentGorD = twoThreeFive
                .SelectMany(s => s.ToCharArray())
                .Where(c => c != segmentA)
                .GroupBy(p => p)
                .Where(group => group.Count() == 3)
                .Select(group => group.Key)
                .ToArray();

            var knownSegments = new char[7];

            var segmentG = four.Except(one).Intersect(segmentGorD).First();
            var segmentD = segmentGorD.Except(new[] {segmentG}).First();

            knownSegments[0] = segmentA;
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
                    .Select(c => Array.FindIndex(knownSegments, item => item == c))
                    .Select(index => (char) ('a' + index))
                    .OrderBy(c => c)
                    .ToArray();

                var patternOrdered = new string(pattern.OrderBy(c => c).ToArray());
                var actualValue = Array.FindIndex(
                    referenceNumberSegmentMap,
                    reference => new string(fixedSegments) == reference
                );
                return (
                    patternOrdered,
                    actualValue
                );
            });
        }

        public static int GetValueOfPatternsAndOutput(string[] patterns, IEnumerable<string> outputs)
        {
            var matchedPatterns = MatchNumbersToPatters(patterns);

            return (int) outputs
                .Select(output => new string(output.OrderBy(c => c).ToArray()))
                .Select(output => matchedPatterns.First(mp => mp.pattern == output).value)
                .Select((val, index) => val * Math.Pow(10, 3 - index))
                .Sum();
        }

        public void PrintSolution1()
        {
            Console.WriteLine(CountEasySegmentNumbers(ParseInput(ReadInput())));
        }

        public void PrintSolution2()
        {
            Console.WriteLine(ParseInput(ReadInput())
                .Select(tuple => GetValueOfPatternsAndOutput(tuple.patterns, tuple.output))
                .Sum()
            );
        }
    }
}