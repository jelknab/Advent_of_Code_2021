using System;
using System.IO;
using System.Linq;
using Advent_of_Code_2021.Day_13;
using Xunit;

namespace Tests
{
    public class Day13Tests
    {
        [Fact]
        public void TestAgainstExample()
        {
            var input = Day13.ParseInput(File.ReadLines("test inputs/day13"));

            var singleFold = Day13.Fold(input.dotGrid, input.instructions.First());
            Assert.Equal(17, singleFold.Cast<int>().Count(dots => dots > 0));
        }
    }
}