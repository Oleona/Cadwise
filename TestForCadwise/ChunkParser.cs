using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TestForCadwise
{
    public class ChunkParser
    {
        private readonly Chunk chunk;
        //private const string delimetrs = ".,;:«»—!?-\"()";
        private readonly char[] delimeters 
            = new char[] { '.', ',', ';', ':', '«', '»' , '—', '!' , '?', '-', '\"', '(', ')', '…' };

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
                /*var processedLine = Regex.Replace(line, $"\b[а-яА-Я]{{1,{lengthThreshold}}}\b", string.Empty);
                processedLines.Add(processedLine);*/
            }

            return new Chunk(chunk.ChunkNumber, processedLines);
        }

        private string ProcessWords(string[] wordArray, int lengthThreshold, bool needDeletePunctuation)
        {
            var sb = new StringBuilder(wordArray.Length * 10);
            foreach (var word in wordArray)
            {
                var cleanedWord = word.Trim(delimeters);

                if (cleanedWord.Length > lengthThreshold)
                {
                    sb.Append(needDeletePunctuation ? cleanedWord : word);
                    sb.Append(' ');
                }
                else
                {
                    if (needDeletePunctuation)
                    {
                        continue;
                    }

                    bool hasPunctuation = false;
                    foreach (var delimeter in delimeters)
                    {
                        if (word.Contains(delimeter))
                        {
                            hasPunctuation = true;
                            sb.Append(delimeter);
                        }
                    }
                    if (hasPunctuation)
                    {
                        sb.Append(' ');
                    }
                }
            }

            return sb.ToString();
        }
    }
}
