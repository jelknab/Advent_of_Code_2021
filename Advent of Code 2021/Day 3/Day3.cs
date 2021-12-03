using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021.Day_3
{
    public class Day3: IDay
    {
        private static string[] ReadBytes()
        {
            return File.ReadLines("Day 3/input").ToArray();
        }

        private static char FindMostCommonBit(IReadOnlyCollection<string> input, int position)
        {
            var ones = input.Count(stringBits => stringBits[position] == '1');
            return ones > input.Count / 2 ? '1' : '0';
        }

        public static string MakeNewBitArrayFromCommonBits(string[] input)
        {
            var newBits = "";
            for (var i = 0;; i++)
            {
                try
                {
                    newBits += FindMostCommonBit(input, i);
                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }
            }

            return newBits;
        }

        public static string FlipBitArray(string bits)
        {
            return new string(bits.ToCharArray().Select(c => c == '1' ? '0' : '1').ToArray());
        }
        
        public void PrintSolution1()
        {
            var gammaBits = MakeNewBitArrayFromCommonBits(ReadBytes());
            var epsilonBits = FlipBitArray(gammaBits);
            
            var gamma = Convert.ToInt32(gammaBits, 2);
            var epsilon = Convert.ToInt32(epsilonBits, 2);
            Console.WriteLine(gamma * epsilon);
        }

        private static (int ones, int zeros) CountBits(string[] input, int position)
        {
            return (input.Count(i => i[position] == '1'), input.Count(i => i[position] == '0'));
        }

        private static string FindRatingValue(string[] input, Func<(int ones, int zeros), char> bitComparer)
        {
            var values = input;
            
            for (var i = 0;; i++)
            {
                var greatestOrOne = bitComparer(CountBits(values, i));
                values = values.Where(val => val[i] == greatestOrOne).ToArray();

                if (values.Length == 1) break;
            }

            return values[0];
        }

        public static string FindOxygenGeneratorRating(string[] input)
        {
            return FindRatingValue(input, tuple => tuple.ones >= tuple.zeros ? '1' : '0');
        }
        
        public static string FindCo2ScrubberRating(string[] input)
        {
            return FindRatingValue(input, tuple => tuple.ones >= tuple.zeros ? '0' : '1');
        }

        public void PrintSolution2()
        {
            var input = ReadBytes();
            
            var oxygenGeneratorRating = Convert.ToInt32(FindOxygenGeneratorRating(input), 2);
            var co2ScrubberRating = Convert.ToInt32(FindCo2ScrubberRating(input), 2);
            Console.WriteLine(oxygenGeneratorRating * co2ScrubberRating);
        }
    }
}