using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsBreaker
{
    class WordsComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            return string.Equals(x, y);
        }

        public int GetHashCode(string obj)
        {
            return obj.Length;
        }
    }
}
