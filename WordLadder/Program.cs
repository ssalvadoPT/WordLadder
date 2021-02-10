using System;
using System.IO;
using System.Linq;
using WordLadder.Models;

namespace WordLadder
{
    class Program
    {
        static void Main(string[] args)
        {
            //check all parameters are provided
            if (args.Length != 4)
            {
                Console.WriteLine("Please provide all the inputs");
                return;
            }

            if (args[1].Length != args[2].Length || args[1].Length != 4)
            {
                Console.WriteLine($"The StartWord and EndWord must both be 4 characters long.");
                return;
            }

            //get all parameters
            Arguments arguments = new()
            {
                DictionaryFile = args[0],
                StartWord = args[1],
                EndWord = args[2],
                ResultFile = args[3]
            };

            //check dictionary file exists
            if (!File.Exists(arguments.DictionaryFile))
            {
                Console.WriteLine($"Check dictionary file actually exists in: {arguments.DictionaryFile}");
                return;
            }

            //Read textFile
            string[] dictionary = File.ReadAllLines(arguments.DictionaryFile);

            if (dictionary.Length == 0)
            {
                Console.WriteLine($"dictionary file is empty");
                return;
            }

            //Get all word candidates for each word in dictionary
            var wordCandidates = new WordCandidates(dictionary, 4);

            if (!wordCandidates.Candidates.Keys.Contains(args[1]) ||
                !wordCandidates.Candidates.Keys.Contains(args[2]))
            {
                Console.WriteLine($"Both StartWord and EndWord must exist in the given dictionary");
                return;
            }

            ShortestPathFinder shortestPathFinder = new()
            {
                WordCandidates = wordCandidates.Candidates,
                StartWord = args[1],
                EndWord = args[2]
            };


            //Get the word ladder steps
            string solution = shortestPathFinder.GetShortestPath();

            if (String.IsNullOrEmpty(solution))
            {
                Console.WriteLine($"No solution found for the given words.");
                return;
            }

            //Write file with solution
            File.WriteAllText(arguments.ResultFile, solution);

            Console.WriteLine($"Final results \"{solution}\" written to results file.");




            ////Initialize first step of the ladder, with the StartWord
            //var step = new WordLadderStep
            //{
            //    CurrentWord = arguments.StartWord,
            //    FinalWord = arguments.EndWord
            //};
            ////Trace this word step
            //step.TraceWord(arguments.StartWord);

            ////Repeat these till our last iteration word is the one we want (EndWord)
            //while (step.Iterations.Last() != step.FinalWord)
            //{
            //    //#1 Get candidates for current stepword (skip any words already iterated, and any that was blacklisted)
            //    var candidates = dictionary
            //    .Where(w => w.Length == 4 &&
            //        step.WordIsCandidate(w) &&
            //        !step.Iterations.Contains(w) &&
            //        !step.FailedIterations.Contains(w))
            //    .Select(w => new StepCandidate
            //    {
            //        CurrentWord = w,
            //        FinalWord = arguments.EndWord
            //    });
            //    //#2 If any candidate is found, choose the one with higher weight (same characters in the same place that the EndWord)
            //    if (candidates.Any())
            //    {
            //        var bestCandidate = candidates.First(c => c.Weight == candidates.Max(w => w.Weight));
            //        //Set this candidate to be the next word step
            //        step.CurrentWord = bestCandidate.CurrentWord;
            //        //Trace this word step
            //        step.TraceWord(bestCandidate.CurrentWord);
            //    }
            //    else
            //    {
            //        //If there are no candidates in the first step (StartWord), puzzle is impossible to solve with current StartWord 
            //        //and current dictionary file.
            //        if (step.CurrentWord == arguments.StartWord)
            //        {
            //            Console.WriteLine($"Impossible to solve. No word to match \"{step.CurrentWord}\" in the given dictionary.");
            //            return;
            //        }

            //        //Take a step back, and find a different candidate
            //        step.BlackListWord(step.CurrentWord);
            //        step.CurrentWord = step.Iterations.Last();
            //    }

            //}

            ////Write file with solution
            //File.WriteAllText(arguments.ResultFile, String.Join(" - ", step.Iterations.ToArray()));

            //Console.WriteLine($"Final results \"{String.Join(" - ", step.Iterations.ToArray())}\" written to results file.");
        }
    }
}
