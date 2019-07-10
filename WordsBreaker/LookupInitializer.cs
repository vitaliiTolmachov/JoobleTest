using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsBreaker
{
    internal interface ILookupInitializer
    {
        IEnumerable<string> InitializeLookupFromFile(string filePath);
    }
    class LookupInitializer : ILookupInitializer
    {
        IEnumerable<string> ILookupInitializer.InitializeLookupFromFile(string filePath)
        {
            string fullFilePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
            if (!File.Exists(fullFilePath))
            {
                Console.WriteLine($"Dictionary file not found in folder: {filePath}\r\nPress any key to exit");
                Console.ReadKey();
                return Enumerable.Empty<string>();
            }
            return File.ReadAllLines(fullFilePath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(s => s.ToLower())
                .Distinct();
        }
    }
}
