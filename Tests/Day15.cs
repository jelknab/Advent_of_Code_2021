using System.IO;
using System.Linq;
using Advent_of_Code_2021.Day_14;
using Advent_of_Code_2021.Day_15;
using Xunit;

namespace Tests
{
    public class Day15Tests
    {
        [Fact]
        public void TestAgainstExample()
        {
            var input = Day15.ParseInput(File.ReadLines("test inputs/day15"));
            var inputPart2 = Day15.ExtendGrid5Times(input);
            
            Assert.Equal(40, Day15.FindLowestRiskPath(input).First().TotalWeight);
            Assert.Equal(315, Day15.FindLowestRiskPath(inputPart2).First().TotalWeight);
        }
    }
}