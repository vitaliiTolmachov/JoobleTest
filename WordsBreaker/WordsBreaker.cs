using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsBreaker
{
    public class WordsBreaker
    {
        private readonly int _minWordLength;
        private Dictionary<string, string> _lookup;
        private List<string> _testwords;
        private Dictionary<string, HashSet<string>> result = new Dictionary<string, HashSet<string>>();

        public WordsBreaker(int minWordLength)
        {
            _minWordLength = minWordLength;
        }
        public void FindWordConcatenationInLookup(List<string> testWords, Dictionary<string, string> lookup)
        {
            _testwords = testWords;
            _lookup = lookup;

            for (int i = 0; i < testWords.Count; i++)
            {
                FindWord(testWords[i], i);
            }
        }

        private void FindWord(string word, int index)
        {
            result[word] = new HashSet<string>();
            int currentWordLength = word.Length;
            for (var charIndex = _minWordLength; charIndex < currentWordLength; charIndex++)
            {
                var prefix = word.Substring(0, charIndex);
                var suffix = word.Substring(charIndex);

                //We are not interested in suffix wich is smaller than the smallest words
                if (suffix.Length < _minWordLength)
                    break;

                if (_lookup.ContainsKey(prefix))
                {
                    if (_lookup.ContainsKey(suffix))
                    {
                        //If word = prefix + sufix it's contactenated word we are looking for
                        result[word].Add(prefix);
                        result[word].Add(suffix);
                        break;
                    }
                    //Let's find another part to concatenate with prefix
                    FindWord(suffix, index);
                }
            }
        }

        public void PrintResult()
        {
            foreach (KeyValuePair<string, HashSet<string>> entry in result.Where(pair => pair.Value.Any()))
            {
                Console.WriteLine($"{entry.Key} can be braked into parts:\r\n");
                foreach (string part in entry.Value)
                {
                    Console.WriteLine($"\t{part}");
                }
            }
        }
    }
}
