using System.Collections.Generic;
using System.Linq;
using Advent_of_Code_2021.Day_9;
using Xunit;

namespace Tests
{
    public class Day9Tests
    {
        [Fact]
        public void TestAgainstExample()
        {
            var input = new[]
            {
                "2199943210",
                "3987894921",
                "9856789892",
                "8767896789",
                "9899965678"
            };

            var grid = Day9.ParseGrid(input);
            var lowCells = Day9.FindLowSpots(grid).ToArray();
            
            Assert.Equal(4, lowCells.Length);
            Assert.Equal(new [] {1, 0, 5, 5}, lowCells.Select(cell => cell.value));
            Assert.Equal(15, Day9.GetLowSpotsSum(grid));
            
            Assert.Equal(new [] {3, 9, 14, 9}, lowCells.Select(lowCell => Day9.FindBasinSize(grid, lowCell.x, lowCell.y, new List<(int x, int y)>())).ToArray());
            Assert.Equal(1134, Day9.MultiplyHighest3Basins(grid));
        }
    }
}