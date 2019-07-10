using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericParsing;

namespace WordsBreaker
{
    public interface IWordsBreaker
    {
        void FindWordConcatenationInLookup(Dictionary<string, string> lookup);
        void FindWord(string word, Dictionary<string, string> lookup);
        Dictionary<string, HashSet<string>> Result { get; }
    }
    public class WordsBreaker: IWordsBreaker
    {
        private readonly int _minWordLength;
        private Dictionary<string, HashSet<string>> result = new Dictionary<string, HashSet<string>>();
        private GenericParser _parser;

        public Dictionary<string, HashSet<string>> Result => result;

        public WordsBreaker(string testFilePath, int minWordLength)
        {
            _minWordLength = minWordLength;
            if (!File.Exists(testFilePath))
            {
                Console.WriteLine($"Test words file not found in folder: {testFilePath}\r\nPress any key to exit");
                Console.ReadKey();
                return;
            }
            _parser = new GenericParser(testFilePath, Encoding.UTF8)
            {
                SkipEmptyRows = true,
                StripControlChars = true,
                FirstRowHasHeader = true,
                TextFieldType = FieldType.Delimited,
                ColumnDelimiter = '\t'
            };
        }
        public void FindWordConcatenationInLookup(Dictionary<string, string> lookup)
        {
            using (_parser)
            {
                while (_parser.Read())
                {
                    string word = _parser[1];
                    FindWord(word, lookup);
                }
            }
        }

        public void FindWord(string word, Dictionary<string, string> lookup)
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

                if (lookup.ContainsKey(prefix) && lookup.ContainsKey(suffix))
                {
                    //If word = prefix + sufix it's contactenated word we are looking for
                    result[word].Add(prefix);
                    result[word].Add(suffix);
                    break;
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
