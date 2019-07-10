using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericParsing;

namespace WordsBreaker
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Content/de-test-words.tsv");
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File with test words not found in folder: {filePath}\r\nPress any key to exit");
                Console.ReadKey();
                return;
            }
            var parser = new GenericParser(filePath, Encoding.UTF8)
            {
                SkipEmptyRows = true,
                StripControlChars = true,
                FirstRowHasHeader = true,
                TextFieldType = FieldType.Delimited,
                ColumnDelimiter = '\t'
            };
            int minWordLength = Int32.MaxValue;
            List<string> testWords = new List<string>();
            using (parser)
            {
                while (parser.Read())
                {
                    string word = parser[1];

                    if (word.Length < minWordLength)
                        minWordLength = word.Length;
                    testWords.Add(word);
                }
            }
            filePath = Path.Combine(Directory.GetCurrentDirectory(), "Content/dict");
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Dictionary file not found in folder: {filePath}\r\nPress any key to exit");
                Console.ReadKey();
                return;
            }
            var wordsComparer = new WordsComparer();
            var tempLookup = File.ReadAllLines(filePath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(s => s.ToLower())
                .Distinct()
                .ToList();
            var r = new HashSet<string>(tempLookup);
            

            var wordBraker = new WordsBreaker(minWordLength);
            wordBraker.FindWordConcatenationInLookup(testWords, r.ToDictionary(
                keySelector: word => word,
                elementSelector: word => word
                ));
            wordBraker.PrintResult();
            Console.ReadKey();

        }
    }
}
