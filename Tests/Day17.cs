using Advent_of_Code_2021.Day_17;
using Xunit;

namespace Tests
{
    public class Day17Tests
    {
        [Fact]
        public void TestAgainstExample()
        {
            var targetArea = Day17.ParseInput("target area: x=20..30, y=-10..-5");
            var maxStartY = Day17.FindMaxYStartVelocity(targetArea);
            Assert.Equal(9, maxStartY);
            Assert.Equal(45, maxStartY * (maxStartY+1) / 2);
            
            Assert.Equal(112, Day17.CountAllLaunchOptions(targetArea));
        }
    }
}