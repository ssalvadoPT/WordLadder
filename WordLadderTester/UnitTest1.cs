using NUnit.Framework;
using System.Linq;
using WordLadder.Models;

namespace WordLadderTester
{
    [TestFixture]
    public class Tests
    {
        private WordCandidates _WordCandidates;

        private DicWordsConfigurationOptions _DicWordsConfigurationOptions;

        private ShortestPathFinder _ShortestPathFinder;

        private string[] _Dictionary = new string[]
                {
                    "spin",
                    "spit",
                    @"\1st",
                    "QWER",
                    "spat",
                    @"\3rd",
                    "qwerty",
                    "spot",
                    "abc",
                    "span",
                };

        [SetUp]
        public void Setup()
        {
            _DicWordsConfigurationOptions = new()
            {
                AllowSpecialCharacters = false,
                WordLength = 4
            };

            _WordCandidates = new(_DicWordsConfigurationOptions);

        }

        /// <summary>
        /// This test will reveal if we can get the correct weight for a candidate, related to his word
        /// The weight measures how many characters are the same, and are in the same position
        /// weight equal word_lenght => the words are equal
        /// weight equal 0 => the word don't have any equal characters in the same position
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="expected"></param>
        [TestCase("spin", "spit", 3)]
        [TestCase("spat", "spin", 2)]
        [TestCase("abce", "spot", 0)]
        [TestCase("span", "span", 4)]
        public void TestWeight(string value1, string value2, int expected)
        {
            int weight = _WordCandidates.GetWeight(value1, value2);
            Assert.IsTrue(weight == expected, $"Weight should be {expected}");
        }

        /// <summary>
        /// This test will reveal if we can discard any unwanted words (not the correct lengt, invalid characters, or not candidates to any word)
        /// </summary>
        /// <param name="numberCharacters"></param>
        /// <param name="expected"></param>
        [TestCase(6)]
        public void TestOptimizeDictionary(int expected)
        {
            var optimized = _WordCandidates.OptimizeDictionary(_Dictionary);
            Assert.IsTrue(optimized.Length == expected, $"There should be {expected} words in the optimized dictionary");
        }


        /// <summary>
        /// Test the number of candidates retrived for a specific word
        /// </summary>
        /// <param name="value"></param>
        /// <param name="expected"></param>
        [TestCase("spin",2)]
        [TestCase("spat", 3)]
        public void TestWordCandidates(string value, int expected)
        {
            _WordCandidates.Initialize(_Dictionary);

            Assert.IsTrue(_WordCandidates.Candidates[value].Length == expected, 
                $"There should be {expected} candidates for the word �{value}�");
        }


        [TestCase("spin", "spot", 3)]
        public void TestShortestPath(string value1, string value2, int expected)
        {
            _WordCandidates.Initialize(_Dictionary);

            _ShortestPathFinder = new()
            {
                WordCandidates = _WordCandidates.Candidates,
                StartWord = value1.ToLower(),
                EndWord = value2.ToLower()
            };

            var result = _ShortestPathFinder.GetShortestPath();

            Assert.IsTrue(result.Split("-").Length == expected,
                $"There should be {expected} steps in the word ladder �{value1}� - �{value2}�");
        }

    }
}