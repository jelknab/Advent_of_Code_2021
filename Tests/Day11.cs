using System.Linq;
using Advent_of_Code_2021.Day_11;
using Xunit;

namespace Tests
{
    public class Day11Tests
    {
        [Fact]
        public void TestAgainstExample()
        {
            var testInput = new string[]
            {
                "5483143223",
                "2745854711",
                "5264556173",
                "6141336146",
                "6357385478",
                "4167524645",
                "2176841721",
                "6882881134",
                "4846848554",
                "5283751526"
            };

            var grid = Day11.ParseInput(testInput);

            var flashes = Enumerable.Range(0, 100).Select(day => Day11.RunStep(grid)).Sum();
            Assert.Equal(1656, flashes);
            
            
            grid = Day11.ParseInput(testInput);
            Assert.Equal(195, Day11.FindSyncStep(grid));
        }
    }
}