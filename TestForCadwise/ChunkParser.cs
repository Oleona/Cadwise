using System.Text;

namespace TestForCadwise
{
    public class ChunkParser
    {
        private readonly char[] delimeters
            = new char[] { '.', ',', ';', ':', '«', '»', '—', '!', '?', '-', '\"', '(', ')', '…' };
        private const string lineSeparator = "\r\n";

        private readonly Chunk chunk;
        private readonly int lengthThreshold;
        private readonly bool needDeletePunctuation;

        public ChunkParser(Chunk chunk, int lengthThreshold, bool needDeletePunctuation)
        {
            this.chunk = chunk;
            this.lengthThreshold = lengthThreshold;
            this.needDeletePunctuation = needDeletePunctuation;
        }

        public Chunk ProcessChunk()
        {
            var words = chunk.TextFragment.Split(' ');

            var joinedWordsAfterProcessing = ProcessWords(words);
            return new Chunk(chunk.ChunkNumber, joinedWordsAfterProcessing);
        }

        private string ProcessWords(string[] wordArray)
        {
            var joinedWords = new StringBuilder(wordArray.Length * 10);

            foreach (var word in wordArray)
            {
                if (!word.Contains(lineSeparator))
                {
                    ProcessOneWord(joinedWords, word);
                    continue;
                }


                var realWords = word.Split(lineSeparator);
                foreach (var realWord in realWords)
                {
                    ProcessOneWord(joinedWords, realWord);

                    if (realWord != realWords[^1])
                    {
                        joinedWords.Append(lineSeparator);
                    }

                }
            }

            return joinedWords.ToString();
        }

        private void ProcessOneWord(StringBuilder joinedWords, string word)
        {
            var cleanedWord = word.Trim(delimeters);

            if (cleanedWord.Length > lengthThreshold)
            {
                joinedWords.Append(needDeletePunctuation ? cleanedWord : word);
                joinedWords.Append(' ');
            }
            else
            {
                if (needDeletePunctuation)
                {
                    return;
                }

                bool hasPunctuation = false;
                foreach (var delimeter in delimeters)
                {
                    if (word.Contains(delimeter))
                    {
                        hasPunctuation = true;
                        joinedWords.Append(delimeter);
                    }
                }
                if (hasPunctuation)
                {
                    joinedWords.Append(' ');
                }
            }
        }
    }
}
