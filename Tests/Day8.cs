using System.IO;
using System.Linq;
using Advent_of_Code_2021.Day_8;
using Xunit;

namespace Tests
{
    public class Day8Tests
    {
        [Fact]
        public void TestAgainstExample()
        {
            var input = File.ReadLines("test inputs/day8");

            var inputParsed = Day8.ParseInput(input).ToArray();
            
            Assert.Equal(26, Day8.CountEasySegmentNumbers(inputParsed));

            var part2Input = Day8.ParseInput(new[] {"acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf"}).First();
            var matchedPatterns = Day8.MatchNumbersToPatters(part2Input.patterns);
            
            Assert.Equal(new [] {8, 5, 2, 3, 7, 9, 6, 4, 0, 1}, matchedPatterns.Select(m => m.value));
            
            Assert.Equal(5353, Day8.GetValueOfPatternsAndOutput(part2Input.patterns, part2Input.output));

            var testAgainstFullTestInput =
                inputParsed.Select(i => Day8.GetValueOfPatternsAndOutput(i.patterns, i.output));
            Assert.Equal(61229, testAgainstFullTestInput.Sum());
        }
    }
}