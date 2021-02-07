using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordLadder.Models
{
    public class Arguments
    {
        public string DictionaryFile { get; init; }

        public string StartWord { get; init; }

        public string EndWord { get; init; }

        public string ResultFile { get; init; }
    }
}
