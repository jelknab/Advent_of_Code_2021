using Advent_of_Code_2021.Day_10;
using Xunit;

namespace Tests
{
    public class Day10Tests
    {
        [Fact]
        public void TestAgainstExample()
        {
            var testInput = new[]
            {
                "[({(<(())[]>[[{[]{<()<>>",
                "[(()[<>])]({[<{<<[]>>(",
                "{([(<{}[<>[]}>{[]{[(<()>",
                "(((({<>}<{<{<>}{[]{[]{}",
                "[[<[([]))<([[{}[[()]]]",
                "[{[{({}]{}}([{[{{{}}([]",
                "{<[[]]>}<{[{[{[]{()[[[]",
                "[<(<(<(<{}))><([]([]()",
                "<{([([[(<>()){}]>(<<{{",
                "<{([{{}}[<[[[<>{}]]]>[]]"
            };
            
            Assert.Equal(26397, Day10.ScoreIllegalCharacters(testInput));
            Assert.Equal(288957, Day10.ScoreFixedCharacters(testInput));
        }
    }
}