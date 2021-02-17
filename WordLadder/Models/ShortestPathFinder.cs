using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordLadder.Models
{
    public class ShortestPathFinder
    {
        public string StartWord { get; init; }

        public string EndWord { get; init; }

        public Dictionary<string, string[]> WordCandidates { get; init; }

        private Dictionary<string, string> _WordPossibilities = new();

        public string GetShortestPath()
        {
            //It doesn't hurt to try this condition and saves us a lot of iterations
            if (StartWord == EndWord)
            {
                //This has to be the shortest ladder
                return (StartWord);
            }

            /// Initialize all our parameters:
            /// candidates - Start from the StartWord. Get all candidates for StartWord. Then we will get all candidates for every candidate found.
            /// currentWord - This will be the candidate word that will make us iterate it's candidates.
            /// index - Will be the index of the candidate that we want to get candidates to iterate.

            var candidates = WordCandidates[StartWord];
            var currentWord = StartWord;
            int index = 0;
            List<string> ladderSteps = new();

            //If we iterate all possibilities and haven't got to the EndWord, exit loop without possible solution
            while (WordCandidates.Count() > _WordPossibilities.Count())
            {
                foreach (var candidate in candidates)
                {
                    //If any candidate is the EndWord, we have all the iterations of candidates that we need.
                    if (candidate == EndWord)
                    {
                        //Since we began from the StartWord and found an iteration that reached the EndWord,
                        //let's move backwards using our _WordPossibilites dictionary that holds the information of
                        //which word (value) originated a specific candidate (key)
                        
                        //Add EndWord to out word ladder
                        ladderSteps.Add(EndWord);
                        //Add the word that found the EndWord as a candidate
                        ladderSteps.Add(currentWord);

                        var step = currentWord;
                        while (step != StartWord)
                        {
                            //Add each word that originated the candidate
                            //set the candidate to that word till we reach our StartWord
                            ladderSteps.Add(step = _WordPossibilities[step]);
                        }

                        //Reverse the list so we don't get an upside-down word ladder
                        ladderSteps.Reverse();
                        //Return the Word Ladder
                        return String.Join(" - ", ladderSteps.ToArray());
                    }
                    //Save the candidate (key) and the word that originated the candidate (value)
                    _WordPossibilities[candidate] = currentWord;
                }
                //Get next candidate to find possible candidates
                try
                {
                    currentWord = _WordPossibilities.Keys.ElementAt(index++);
                }
                catch (ArgumentOutOfRangeException)
                {
                    //Means there are no candidates for a word. Reached a dead end.
                    return "";
                }
                //Get the candidates for this candidate, but don't get the ones that we already iterate
                candidates = WordCandidates[currentWord].Where(w => !_WordPossibilities.Keys.Contains(w)).ToArray();
            }
            return "";
        }


    }
}
