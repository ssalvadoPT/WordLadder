using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordLadder.Models
{
    public class StepCandidate
    {
        public string CurrentWord { get; init; }
        public string FinalWord { get; init; }

        public int Weight
        {
            get
            {
                int weight = 0;
                for (int i = 0; i < CurrentWord.Length; i++)
                    weight += Convert.ToInt32(CurrentWord[i] == FinalWord[i]);
                return weight;
            }
        }
    }
}
