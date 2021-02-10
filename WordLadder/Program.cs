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

            if (!dictionary.Contains(args[1]) ||
                !dictionary.Contains(args[2]))
            {
                Console.WriteLine($"Both StartWord and EndWord must exist in the given dictionary");
                return;
            }

            //Get rid of the words with special characters and get only the words with 4 characters
            WordCandidates wordCandidates = new(new DicWordsConfigurationOptions
            {
                AllowSpecialCharacters = false,
                WordLength = 4
            });

            wordCandidates.Initialize(dictionary);

            ShortestPathFinder shortestPathFinder = new()
            {
                WordCandidates = wordCandidates.Candidates,
                StartWord = args[1],
                EndWord = args[2]
            };

            //Get the word ladder steps
            string solution = shortestPathFinder.GetShortestPath();

            /*Solution 2 */

            if (String.IsNullOrEmpty(solution))
            {
                Console.WriteLine($"No solution found for the given words.");
                return;
            }

            //Write file with solution
            File.WriteAllText(arguments.ResultFile, solution);

            Console.WriteLine($"Final results \"{solution}\" written to results file.");
        }
    }
}
