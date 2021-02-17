using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordLadder.Models;
using WordLadder.Models2;

namespace WordLadder
{
    class Program2
    {
        static void Main(string[] args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            
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

            if (!dictionary.Contains(args[1]) ||
                !dictionary.Contains(args[2]))
            {
                Console.WriteLine($"Both StartWord and EndWord must exist in the given dictionary");
                return;
            }

            WordCandidates wordCandidates = new(new DicWordsConfigurationOptions
            {
                AllowSpecialCharacters = false,
                WordLength = 4
            });

            wordCandidates.Initialize(dictionary);


            var ladderSteps = new LadderSteps(args[1].ToLower(), args[2].ToLower(), wordCandidates.Candidates);

            if (String.IsNullOrEmpty(ladderSteps.Solution))
            {
                Console.WriteLine($"No solution found for the given words.");
                return;
            }

            //Write file with solution
            File.WriteAllText(arguments.ResultFile, ladderSteps.Solution);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            Console.WriteLine($"Final results \"{ladderSteps.Solution}\" written to results file. Elapsed time: {elapsedMs} milliseconds");

        }
    }
}
