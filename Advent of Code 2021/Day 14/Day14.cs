using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_14
{
    public class Day14 : IDay
    {
        public static (string template, Dictionary<string, char> rules) ParseInput(IEnumerable<string> lines)
        {
            lines = lines.ToArray();

            var template = lines.First();

            var ruleDict = new Dictionary<string, char>();
            var rules = lines
                .Skip(2)
                .Select(line => line.Split(" -> "))
                .Select(split => (split[0], split[1][0]));

            foreach (var (input, output) in rules)
            {
                ruleDict[input] = output;
            }

            return (template, ruleDict);
        }

        public static long RunStepsCountScore(string template, Dictionary<string, char> rules, int steps)
        {
            var elementCount = new Dictionary<char, long>();
            foreach (var c in template)
            {
                if (!elementCount.ContainsKey(c))
                {
                    elementCount.Add(c, 0);
                }

                elementCount[c]++;
            }
            
            var pairOccurrences = new Dictionary<string, long>();
            for (var pairIndex = 0; pairIndex < template.Length - 1; pairIndex++)
            {
                var pair = template.Substring(pairIndex, 2);
                if (!pairOccurrences.ContainsKey(pair))
                {
                    pairOccurrences.Add(pair, 0);
                }

                pairOccurrences[pair]++;
            }

            for (int step = 0; step < steps; step++)
            {
                var keys = pairOccurrences.Keys.ToArray();
                
                var newPairOccurrences = new Dictionary<string, long>();
                
                foreach (var pairOccurrencesKey in keys)
                {
                    var pairMakes = rules[pairOccurrencesKey];

                    if (!elementCount.ContainsKey(pairMakes))
                    {
                        elementCount.Add(pairMakes, 0);
                    }

                    elementCount[pairMakes] += pairOccurrences[pairOccurrencesKey];

                    var newPairA = new string(new [] {pairOccurrencesKey[0], pairMakes});
                    var newPairB = new string(new [] {pairMakes, pairOccurrencesKey[1]});

                    if (!newPairOccurrences.ContainsKey(newPairA))
                    {
                        newPairOccurrences.Add(newPairA, 0);
                    }

                    if (!newPairOccurrences.ContainsKey(newPairB))
                    {
                        newPairOccurrences.Add(newPairB, 0);
                    }

                    newPairOccurrences[newPairA] += pairOccurrences[pairOccurrencesKey];
                    newPairOccurrences[newPairB] += pairOccurrences[pairOccurrencesKey];
                }

                pairOccurrences = newPairOccurrences;
            }

            return elementCount.Values.Max() - elementCount.Values.Min();
        }

        public void PrintSolution1()
        {
            var input = ParseInput(File.ReadLines("Day 14/input"));
            Console.WriteLine(RunStepsCountScore(input.template, input.rules, 10));
        }

        public void PrintSolution2()
        {
            var input = ParseInput(File.ReadLines("Day 14/input"));
            Console.WriteLine(RunStepsCountScore(input.template, input.rules, 40));
        }
    }
}