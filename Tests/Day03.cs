using System;
using Advent_of_Code_2021.Day_03;
using Xunit;

namespace Tests
{
    public class Day03Tests
    {
        [Fact]
        public void TestAgainstExample()
        {
            string[] input =
            {
                "00100",
                "11110",
                "10110",
                "10111",
                "10101",
                "01111",
                "00111",
                "11100",
                "10000",
                "11001",
                "00010",
                "01010"
            };

            var gammaBits = Day3.MakeNewBitArrayFromCommonBits(input);
            var epsilonBits = Day3.FlipBitArray(gammaBits);
            
            Assert.Equal("10110", gammaBits);
            Assert.Equal("01001", epsilonBits);
            
            var gamma = Convert.ToInt32(gammaBits, 2);
            var epsilon = Convert.ToInt32(epsilonBits, 2);
            
            Assert.Equal(198, gamma * epsilon);

            var oxygenGeneratorReading = Day3.FindOxygenGeneratorRating(input);
            var co2ScrubberRating = Day3.FindCo2ScrubberRating(input);
            
            Assert.Equal("10111", oxygenGeneratorReading);
            Assert.Equal("01010", co2ScrubberRating);
        }
        
    }
}