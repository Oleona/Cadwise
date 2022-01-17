using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForCadwise
{
    public class ChunkParser
    {
        private const string delimetrs = ".,;:«»—!?-\"()";
        public Chunk ChunkParse(Chunk chunk, int lengthThreshold, bool needDeletePunctuation)
        {
            
            var result = string.Join(" ", chunk.LinesPart);
            var worldArray = result.Split(' ');
            var text = ProcessWordArray(worldArray, lengthThreshold, needDeletePunctuation);
            var newChunk = new Chunk(chunk.chunkNumber, text);
            return newChunk;




            Console.WriteLine("Взяли порцию");


        }

        private string ProcessWordArray(string[] wordArray, int lengthThreshold, bool needDeletePunctuation)
        {
            var sb = new StringBuilder(wordArray.Length * 10);
            foreach (var word in wordArray)
            {
                var cleanedWord = needDeletePunctuation ? RemovePunctuation(word) : word;
                if (cleanedWord.Length > lengthThreshold)
                {
                    sb.Append(cleanedWord);
                    sb.Append(' ');
                }
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
                }
            }

            return word;
        }
    }
}
