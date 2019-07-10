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
            
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Content/dict");
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
            

            var wordBraker = new WordsBreaker("Content/de-test-words.tsv", tempLookup.Min(word=>word.Length));
            wordBraker.FindWordConcatenationInLookup(r.ToDictionary(
                keySelector: word => word,
                elementSelector: word => word
                ));
            wordBraker.PrintResult();
            Console.ReadKey();

        }
    }
}
