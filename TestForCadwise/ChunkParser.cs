using System.Collections.Generic;
using System.Text;

namespace TestForCadwise
{
    public class ChunkParser
    {
        private const string delimetrs = ".,;:«»—!?-\"()";
        public Chunk ProcessChunk(Chunk chunk, int lengthThreshold, bool needDeletePunctuation)
        {
            var processedLines = new List<string>(chunk.Lines.Count);
            foreach (var line in chunk.Lines)
            {
                var wordArray = line.Split(' ');
                var processedLine = ProcessWordArray(wordArray, lengthThreshold, needDeletePunctuation);
                processedLines.Add(processedLine);
                processedLines.Add("\n");
            }

            return new Chunk(chunk.chunkNumber, processedLines);
            /*var result = string.Join(" ", chunk.Lines);
            var worldArray = result.Split(' ');
            var text = ProcessWordArray(worldArray, lengthThreshold, needDeletePunctuation);
            var newChunk = new Chunk(chunk.chunkNumber, text);
            return newChunk;*/
        }

        private string ProcessWordArray(string[] wordArray, int lengthThreshold, bool needDeletePunctuation)
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
