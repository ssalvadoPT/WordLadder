using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordLadder.Models
{
    public class WordLadderStep
    {
        public string CurrentWord { get; set; }
        public string FinalWord { get; init; }
        public List<string> Iterations { get; set; } = new();

        public List<string> FailedIterations { get; set; } = new();

        public int GetWeight(string candidate, string finalWord)
        {
            int weight = 0;
            for (int i=0;i<candidate.Length;i++)
                weight += Convert.ToInt32(candidate[i] == finalWord[i]);
            return weight;
        }

        public bool WordIsCandidate(string word) => GetWeight(word, this.CurrentWord) == 3;

        public void TraceWord(string value) => Iterations.Add(value);

        public void BlackListWord(string value)
        {
            FailedIterations.Add(value);
            Iterations.Remove(value);
        }

    }
}
