using System.Linq;
using Advent_of_Code_2021.Day_7;
using Xunit;

namespace Tests
{
    public class Day7Tests
    {
        [Fact]
        public void TestAgainstExample()
        {
            int[] subs = {16, 1, 2, 0, 4, 2, 7, 1, 2, 14};

            Assert.Equal(37, Day7.CalculateFuelCosts(subs).OrderBy(s => s.fuelCost).First().fuelCost);
            Assert.Equal(168, Day7.CalculateFuelCosts(subs).OrderBy(s => s.triangularFuelCost).First().triangularFuelCost);
        }
    }
}