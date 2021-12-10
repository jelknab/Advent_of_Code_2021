using System.IO;
using System.Linq;
using Advent_of_Code_2021.Day_08;
using Xunit;

namespace Tests
{
    public class Day08Tests
    {
        [Fact]
        public void TestPart1AgainstExample()
        {
            var inputParsed = Day8.ParseInput(File.ReadLines("test inputs/day8")).ToArray();
            Assert.Equal(26, Day8.CountLengthDetectableSegmentNumbers(inputParsed));
        }

        [Fact]
        public void TestPart2AgainstExample()
        {
            var (patterns, output) = Day8.ParseInput(new[] {"acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf"}).First();
            var matchedPatterns = Day8.MatchPattersToSegments(patterns);
            
            Assert.Equal(new [] {8, 5, 2, 3, 7, 9, 6, 4, 0, 1}, matchedPatterns.Select(m => m.value));
            Assert.Equal(5353, Day8.GetOutputValue(patterns, output));
            
            var fullTestInputParsed = Day8.ParseInput(File.ReadLines("test inputs/day8")).ToArray();
            Assert.Equal(61229, fullTestInputParsed.Select(i => Day8.GetOutputValue(i.patterns, i.output)).Sum());
        }
    }
}