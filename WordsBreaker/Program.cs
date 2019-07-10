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
            var lookup = File.ReadAllLines(filePath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(s => s.ToLower())
                .Distinct()
                .ToDictionary(
                    keySelector: word => word,
                    elementSelector: word => word
                );

            var wordBraker = new WordsBreaker("Content/de-test-words.tsv", lookup.Min(word => word.Value.Length));
            wordBraker.FindWordConcatenationInLookup(lookup);
            wordBraker.PrintResult();
            Console.ReadKey();

        }
    }
}
