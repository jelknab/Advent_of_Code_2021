using Advent_of_Code_2021.Day_1;
using Xunit;

namespace Tests
{
    public class Day1Tests
    {
        [Fact]
        public void TestAgainstExample()
        {
            int[] input = {199, 200, 208, 210, 200, 207, 240, 269, 260, 263};

            Assert.Equal(7, Day1.SumMeasurementIncrements(input));
            Assert.Equal(5, Day1.SumMeasurementIncrements(Day1.MakeMeasurementWindows(input)));
        }
    }
}