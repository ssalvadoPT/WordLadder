using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordLadder.Models2
{
    public class LadderStep
    {
        public string PreviousStep { get; set; }

        public string Word { get; set; }

        public string[] NextSteps { get; set; }
    }
}
