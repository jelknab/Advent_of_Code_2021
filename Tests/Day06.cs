using System.Linq;
using Advent_of_Code_2021.Day_06;
using Xunit;

namespace Tests
{
    public class Day06Tests
    {
        [Fact]
        public void TestAgainstExample()
        {
            int[] input = {3, 4, 3, 1, 2};
            
            Assert.Equal(26, Day6.SimulateDays(input, 18));
            Assert.Equal(5934, Day6.SimulateDays(input, 80));
            Assert.Equal(26984457539, Day6.SimulateDays(input, 256));
        }
    }
}