using System.Collections.Generic;
using System.Text;

namespace TestForCadwise
{
    public class ChunkParser
    {
        private readonly Chunk chunk;
        private const string delimetrs = ".,;:«»—!?-\"()";

        public ChunkParser(Chunk chunk)
        {
            this.chunk = chunk;
        }

        public Chunk ProcessChunk(int lengthThreshold, bool needDeletePunctuation)
        {
            var processedLines = new List<string>(chunk.TextFragments.Count);
            foreach (var line in chunk.TextFragments)
            {
                var words = line.Split(' ');
                var processedLine = ProcessWords(words, lengthThreshold, needDeletePunctuation);
                processedLines.Add(processedLine);
            }

            return new Chunk(chunk.ChunkNumber, processedLines);
        }

        private string ProcessWords(string[] wordArray, int lengthThreshold, bool needDeletePunctuation)
        {
            var sb = new StringBuilder(wordArray.Length * 10);
            foreach (var word in wordArray)
            {
                var cleanedWord = needDeletePunctuation ? RemovePunctuation(word) : word;
                if (cleanedWord.Length <= lengthThreshold)
                {
                    continue;
                }

                sb.Append(cleanedWord);
                sb.Append(' ');
            }

            return sb.ToString();
        }

        private string RemovePunctuation(string word)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (delimetrs.Contains(word[i]))
                {
                    word = word.Replace(word[i].ToString(), "");
                    i--;
                }
            }

            return word;
        }
    }
}
