using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordLadder.Models2
{
    class LadderSteps
    {
        public List<LadderStep> Steps { get; set; } = new();

        public List<string> SolutionSteps { get; set; } = new();

        public string Solution { get; set; }

        public LadderSteps(string startWord, string endWord, Dictionary<string, string[]> words)
        {
            var currentWord = startWord;

            List<string> wordsGoneTrough = new();

            wordsGoneTrough.Add(currentWord);

            while (!(Steps.Any(s => s.NextSteps.Any(ns => ns == endWord))))
            {
                if (wordsGoneTrough.Count() == words.Count())
                {
                    Solution = "No possible solution";
                    return;
                }

                var step = new LadderStep
                {
                    Word = currentWord,
                    PreviousStep = String.Empty,
                    NextSteps = words[currentWord]
                };

                Steps.Add(step);

                foreach (var nextStep in step.NextSteps)
                {
                    var newStep = new LadderStep
                    {
                        Word = nextStep,
                        PreviousStep = step.Word,
                        NextSteps = words[nextStep]
                    };

                    if (!Steps.Any(s => s.Word == nextStep))
                        Steps.Add(newStep);
                }

                currentWord = Steps.FirstOrDefault(s => s.PreviousStep == wordsGoneTrough.Last() &&
                                                    !Steps.Any(s1 => s1.PreviousStep == s.Word) &&
                                                    !(Steps.Last().PreviousStep == "" && Steps.Last().Word == currentWord))?.Word;

                if (currentWord == null)
                {
                    try
                    {
                        wordsGoneTrough.Add(Steps.First(s => !wordsGoneTrough.Contains(s.PreviousStep) && s.PreviousStep != "").PreviousStep);
                    }
                    catch (InvalidOperationException)
                    {
                        Solution = "No possible solution";
                        return;
                    }
                    currentWord = Steps.FirstOrDefault(s => s.PreviousStep == wordsGoneTrough.Last() && !Steps.Any(s1 => s1.PreviousStep == s.Word))?.Word;
                }
            }

            SolutionSteps.Add(endWord);
            SolutionSteps.Add(Steps.Last().Word);
            SolutionSteps.Add(Steps.Last().PreviousStep);
            
            var lastWord = Steps.Last().PreviousStep;

            while (lastWord != startWord)
            {
                var s = Steps.Single(s => s.Word == lastWord && s.PreviousStep != "");
                lastWord = s.PreviousStep;
                SolutionSteps.Add(lastWord);
            }

            SolutionSteps.Reverse();

            Solution = String.Join(" - ", SolutionSteps.ToArray());
        }
    }
}
