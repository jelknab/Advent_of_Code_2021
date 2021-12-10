using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_10
{
    public class IllegallyClosingTagException : Exception
    {
        public char Character { get; set; }
    }

    public class Day10 : IDay
    {
        private static readonly Dictionary<char, char> TagCombinations = new Dictionary<char, char>
        {
            { '(', ')' },
            { '[', ']' },
            { '{', '}' },
            { '<', '>' },
        };

        private static readonly Dictionary<char, int> ClosingTagScoresPart1 = new Dictionary<char, int>
        {
            { ')', 3 },
            { ']', 57 },
            { '}', 1197 },
            { '>', 25137 },
        };
        
        private static readonly Dictionary<char, int> ClosingTagScoresPart2 = new Dictionary<char, int>
        {
            { ')', 1 },
            { ']', 2 },
            { '}', 3 },
            { '>', 4 },
        };

        private static IEnumerable<string> ReadInput()
        {
            return File.ReadLines("Day 10/input");
        }

        public static int ScoreIllegalCharacters(IEnumerable<string> inputs)
        {
            return inputs.Select(input =>
                {
                    try
                    {
                        TryParse(input);
                    }
                    catch (IllegallyClosingTagException e)
                    {
                        return e;
                    }

                    return null;
                }).Where(exception => exception != null)
                .Select(e => ClosingTagScoresPart1[e.Character])
                .Sum();
        }

        public static long ScoreFixedCharacters(IEnumerable<string> inputs)
        {
            var scores = inputs.Where(input =>
                {
                    try
                    {
                        TryParse(input);
                    }
                    catch (IllegallyClosingTagException)
                    {
                        return false;
                    }

                    return true;
                }).Select(TryFix)
                .OrderBy(score => score)
                .ToArray();

            return scores[scores.Length / 2];
        }

        private static long TryFix(string content)
        {
            var stack = new Stack<char>();

            foreach (var c in content)
            {
                if (TagCombinations.ContainsKey(c))
                {
                    stack.Push(c);
                }
                else
                {
                    stack.Pop();
                }
            }

            var score = 0L;
            while (stack.Count > 0)
            {
                score *= 5;
                score += ClosingTagScoresPart2[TagCombinations[stack.Pop()]];
            }

            return score;
        }


        private static void TryParse(string content)
        {
            var stack = new Stack<char>();

            for (var index = 0; index < content.Length; index++)
            {
                var c = content[index];
                if (TagCombinations.ContainsKey(c))
                {
                    stack.Push(c);
                }
                else
                {
                    if (c != TagCombinations[stack.Pop()])
                    {
                        throw new IllegallyClosingTagException { Character = c };
                    }
                }
            }
        }

        public void PrintSolution1()
        {
            Console.WriteLine(ScoreIllegalCharacters(ReadInput()));
        }

        public void PrintSolution2()
        {
            Console.WriteLine(ScoreFixedCharacters(ReadInput()));
        }
    }
}