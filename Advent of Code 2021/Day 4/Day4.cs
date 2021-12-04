using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2021.Day_4
{
    public class Day4: IDay
    {
        private static string[] ReadInput()
        {
            return File.ReadLines("Day 4/input").ToArray();
        }
        
        public static (int[] drawnNumbers, int[] boardNumbers) ParseInput(string[] input)
        {
            var drawnNumbers = input[0].Split(',').Select(int.Parse).ToArray();
            var boardString = input
                .Skip(1)
                .Where(s => s.Trim() != string.Empty)
                .Aggregate((s, s1) => s + ' ' + s1);

            var boardNumbers = Regex
                .Split(boardString.Trim(), "\\s+")
                .Select(int.Parse)
                .ToArray();

            return (drawnNumbers, boardNumbers);
        }

        private static IEnumerable<int> FindWinnerBoardIndices(int[] boardNumbers, int[] exclude)
        {
            var boardCount = boardNumbers.Length / 25;

            for (var boardIndex = 0; boardIndex < boardCount; boardIndex++)
            {
                if (Array.IndexOf(exclude, boardIndex) != -1) continue;
                
                for (var winIndex = 0; winIndex < 5; winIndex++)
                {
                    var rowSum = 0;
                    var colSum = 0;
                    
                    for (var rowColIndex = 0; rowColIndex < 5; rowColIndex++)
                    {
                        rowSum += boardNumbers[boardIndex * 25 + winIndex * 5 + rowColIndex];
                        colSum += boardNumbers[boardIndex * 25 + winIndex + rowColIndex * 5];
                    }

                    if (rowSum == -5 || colSum == -5)
                    {
                        yield return boardIndex;
                    }
                }
            }
        }

        private static int BoardScore(IEnumerable<int> boardNumbers, int winnerIndex, int drawnNumber)
        {
            return boardNumbers
                .Skip(winnerIndex * 25)
                .Take(25)
                .Where(v => v != -1)
                .Sum() * drawnNumber;
        }

        public static IEnumerable<(int board, int score)> GetWinnersOrdered(int[] boardNumbers, IEnumerable<int> drawNumbers)
        {
            var winnerHistory = new List<int>();
            foreach (var drawNumber in drawNumbers)
            {
                boardNumbers = boardNumbers.Select(n => n == drawNumber ? -1 : n).ToArray();
                var winners = FindWinnerBoardIndices(boardNumbers, winnerHistory.ToArray());

                foreach (var winner in winners)
                {
                    winnerHistory.Add(winner);
                    yield return (winner, BoardScore(boardNumbers, winner, drawNumber));
                }
            }
        }
        
        public void PrintSolution1()
        {
            var (numbers, boardNumbers) = ParseInput(ReadInput());
            Console.WriteLine(GetWinnersOrdered(boardNumbers, numbers).First().score);
        }

        public void PrintSolution2()
        {
            var (numbers, boardNumbers) = ParseInput(ReadInput());
            Console.WriteLine(GetWinnersOrdered(boardNumbers, numbers).Last().score);
        }
    }
}