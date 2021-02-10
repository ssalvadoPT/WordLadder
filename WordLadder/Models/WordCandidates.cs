using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordLadder.Models
{
    /// <summary>
    /// This class provides access to the candidates for every word in the provided dictionary.
    /// A candidate is a word that is part of the next ladder step (ie: has only one letter different)
    /// </summary>
    public class WordCandidates
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="numberCharacters"></param>
        public WordCandidates(string[] dictionary, int numberCharacters)
        {
            var filteredDictionary = OptimizeDictionary(dictionary, numberCharacters);
            foreach (var word in filteredDictionary)
            {
                //the wordCandidates are the ones that have weigth equal to (length-1) (4-1) = 3 
                //if a word is 4 characters long, a weight 3 means that one 1 letter is different. 
                //the others remain equal, and in the same position
                var wordCandidates = filteredDictionary.Where(w => GetWeight(w, word) == numberCharacters-1).ToArray();
                //the words that have no adjacent step can be removed, they are dead ends.
                if (wordCandidates.Length > 0)
                    Candidates[word] = wordCandidates;
            }
        }

        /// <summary>
        /// All candidates for an existing word in the dictionary
        /// </summary>
        public Dictionary<string, string[]> Candidates { get; } = new();

        /// <summary>
        /// Filter all words that aren't words (ie: have other characters than letters). 
        /// Discard the ones that have other length than 4 (No need to have other words, since the requisites states that words have to be 4 characters long).
        /// Seize the opportunity to convert them to lowercase.
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="numberCharacters"></param>
        /// <returns></returns>
        public string[] OptimizeDictionary(string[] dictionary, int numberCharacters)
        {
            return dictionary
                .Where(w => w.Length == numberCharacters && 
                            w.All(Char.IsLetter))
                .Select(w => w.ToLower()).ToArray();
        }

        /// <summary>
        /// Get the number of equal letters that are in the same position than a given word.
        /// </summary>
        /// <param name="candidate"></param>
        /// <param name="finalWord"></param>
        /// <returns></returns>
        public int GetWeight(string candidate, string finalWord)
        {
            int weight = 0;
            for (int i = 0; i < candidate.Length; i++)
                weight += Convert.ToInt32(candidate[i] == finalWord[i]);
            return weight;
        }

        
    }
}
