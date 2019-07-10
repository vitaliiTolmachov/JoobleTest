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
            ILookupInitializer lookupInitializer = GetLookupInitializer();
            var lookup = lookupInitializer.InitializeLookupFromFile("Content/dict1")
                                          .ToDictionary(
                                           keySelector: word => word,
                                           elementSelector: word => word);

            var wordBraker = new WordsBreaker("Content/de-test-words.tsv", lookup.Min(word => word.Value.Length));
            wordBraker.FindWordConcatenationInLookup(lookup);
            wordBraker.PrintResult();
            Console.ReadKey();

        }

        public static ILookupInitializer GetLookupInitializer()
        {
            return new LookupInitializer();
        }
    }
}
