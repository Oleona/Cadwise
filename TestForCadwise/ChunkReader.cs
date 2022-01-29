using System.Collections.Generic;
using System.IO;

namespace TestForCadwise
{
    public class ChunkReader
    {
        private const int WhiteSpacesInChunk = 500;

        public IEnumerable<Chunk> ReadFromFile(string filePath)
        {
            using var reader = new StreamReader(filePath);

            int chunkNumber = 0;
            int whiteSpaceCounter = 0;
            var chars = new List<char>();

            while (reader.EndOfStream != true)
            {
                char symbol = (char)reader.Read();
                chars.Add(symbol);

                if (char.IsWhiteSpace(symbol))
                {
                    if (whiteSpaceCounter != WhiteSpacesInChunk)
                    {
                        whiteSpaceCounter++;
                        continue;
                    }

                    yield return new Chunk(chunkNumber, charListToString(chars));

                    chunkNumber++;
                    whiteSpaceCounter = 0;
                    chars.Clear();
                }
            }


            yield return new Chunk(chunkNumber, charListToString(chars));
        }

        private string charListToString(List<char> chars) => new(chars.ToArray());

    }
}