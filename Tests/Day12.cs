using System.Collections.Generic;
using System.Linq;
using Advent_of_Code_2021.Day_12;
using Xunit;

namespace Tests
{
    public class Day12Tests
    {
        [Fact]
        public void TestAgainstExample()
        {
            var testInput = new[]
            {
                "start-A",
                "start-b",
                "A-c",
                "A-b",
                "b-d",
                "A-end",
                "b-end"
            };

            var parsed = Day12.ParseInput(testInput);
            Assert.Equal(10, Day12.CountPaths(parsed));

            var test = parsed["start"].FindPathsToEndAllowTwice(new Stack<Node>())
                .Select(array => string.Join(", ", array.Select(node => node.Name)))
                .ToArray();
            
            Assert.Equal(36, Day12.CountPathsPart2(parsed));
        }
    }
}