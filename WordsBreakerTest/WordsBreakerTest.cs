using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using WordsBreaker;

namespace WordsBreakerTest
{
    [TestClass]
    public class WordsBreakerTest
    {
        [TestMethod]
        public void WordSplitsCorrectly()
        {
            //Arrange
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Content/de-test-words.tsv");
            IWordsBreaker wordBrakerStub = Substitute.For<WordsBreaker.WordsBreaker>(filePath, 2);
            var lookup = GenerateLookup("krankenhaus", "kranken", "haus","ranken","en");
            
            //Act
            wordBrakerStub.FindWord("krankenhaus", lookup);

            //Assert
            var expectedSequence = new HashSet<string>() { "kranken", "haus" };
            var resultSequence = wordBrakerStub.Result["krankenhaus"];
            Assert.IsTrue(Enumerable.SequenceEqual(expectedSequence, resultSequence));
        }

        private Dictionary<string,string> GenerateLookup(params string [] words)
        {
            if (words.Length > 0)
            {
                var lookup = new Dictionary<string, string>(words.Length);
                foreach (string word in words.Select(w => w.ToLowerInvariant()))
                {
                    if (!lookup.ContainsKey(word))
                    {
                        lookup.Add(word, word);
                    }
                }
                return lookup;
            }
            throw new ArgumentException("Words for creating lookup are not provided!");
        }
    }
}
