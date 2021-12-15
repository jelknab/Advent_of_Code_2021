using System.IO;
using Advent_of_Code_2021.Day_14;
using Xunit;

namespace Tests
{
    public class Day14Tests
    {
        [Fact]
        public void TestAgainstExample()
        {
            var input = Day14.ParseInput(File.ReadLines("test inputs/day14"));
            Assert.Equal(1588, Day14.RunStepsCountScore(input.template, input.rules, 10));
        }
    }
}