using System;
using System.IO;
using System.Linq;
using WordLadder.Models;

namespace WordLadder
{
    class Program
    {
        private static  WordLadderStep _word;

        static void Main(string[] args)
        {
            //check all parameters are provided
            if (args.Length != 4)
            {
                Console.WriteLine("Please provide all the inputs");
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

            //Initialize first step of the ladder
            var step = new WordLadderStep
            {
                CurrentWord = arguments.StartWord,
                FinalWord = arguments.EndWord
            };
            step.TraceWord(arguments.StartWord);

            //#1 Get candidates for StartWord (the ones that has weight equal one)

            while (step.Iterations.Last() != step.FinalWord)
            {
                var candidates = dictionary
                .Where(w => w.Length == 4 &&
                    step.WordIsCandidate(w) &&
                    !step.Iterations.Contains(w) &&
                    !step.FailedIterations.Contains(w))
                .Select(w => new StepCandidate
                {
                    CurrentWord = w,
                    FinalWord = arguments.EndWord
                })
                .OrderByDescending(w => w.Weight);

                if (candidates.Count() > 0)
                {
                    var bestCandidate = candidates.First(c => c.Weight == candidates.Max(w => w.Weight));
                    step.CurrentWord = bestCandidate.CurrentWord;
                    step.TraceWord(bestCandidate.CurrentWord);
                }
                else
                {
                    if (step.CurrentWord == arguments.StartWord)
                    {
                        Console.WriteLine($"Impossible to solve. No word to match \"{step.CurrentWord}\" in the given dictionary.");
                        return;
                    }
                        
                    step.BlackListWord(step.CurrentWord);
                    step.CurrentWord = step.Iterations.Last();
                }

            }

            File.WriteAllText(arguments.ResultFile, String.Join(" - ", step.Iterations.ToArray()));
            

            //var num = candidates.Count();

            //#2 Assign weights to candidates (weight according resemblance to EndWord)

            //#3 Choose candidates that has higher weight

            //Repeat the process for each chosen word #1, #2 #3

            //Stop when any of the candidates in step #1 is the EndWord

            Console.WriteLine("Hello World!");
        }

        public static int GetWeight(string candidate, string finalWord)
        {
            int weight = 0;
            for (int i = 0; i < candidate.Length; i++)
                weight += Convert.ToInt32(candidate[i] == finalWord[i]);
            return weight;
        }
    }
}
