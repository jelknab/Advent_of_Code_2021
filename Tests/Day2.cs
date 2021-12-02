using System.Linq;
using Advent_of_Code_2021.Day_2;
using Xunit;

namespace Tests
{
    public class Day2Tests
    {
        [Fact]
        public void TestAgainstExample()
        {
            string[] input =
            {
                "forward 5",
                "down 5",
                "forward 8",
                "up 3",
                "down 8",
                "forward 2"
            };

            var parsedCommands = Day2.ReadCommands(input).ToArray();
            var ranCommandsPart1 = Day2.RunCommandsPart1(parsedCommands);
            
            Assert.Equal((15, 10), ranCommandsPart1);
            
            
            var ranCommandsPart2 = Day2.RunCommandsPart2(parsedCommands);
            Assert.Equal((15, 60), ranCommandsPart2);
        }
    }
}