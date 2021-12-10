using System.Linq;
using Advent_of_Code_2021.Day_05;
using Xunit;

namespace Tests
{
    public class Day05Tests
    {
        [Fact]
        public void TestAgainstExample()
        {
            var input = new string[]
            {
                "0,9 -> 5,9",
                "8,0 -> 0,8",
                "9,4 -> 3,4",
                "2,2 -> 2,1",
                "7,0 -> 7,4",
                "6,4 -> 2,0",
                "0,9 -> 2,9",
                "3,4 -> 1,4",
                "0,0 -> 8,8",
                "5,5 -> 8,2"
            };

            var lines = Day5.ParseLines(input).ToList();
            
            Assert.Equal(5, Day5.CountOverlappingPoints(Day5.MapLines(Day5.GetStraightLines(lines))));
            Assert.Equal(12, Day5.CountOverlappingPoints(Day5.MapLines(lines)));
        }
    }
}