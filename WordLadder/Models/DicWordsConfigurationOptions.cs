using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordLadder.Models
{
    public class DicWordsConfigurationOptions
    {
        public bool AllowSpecialCharacters { get; init; } = false;

        public int WordLength { get; init; } = 4;
    }
}
