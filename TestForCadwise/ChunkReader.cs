using System;
using System.Collections.Generic;
using System.IO;

namespace TestForCadwise
{
    public class ChunkReader
    {
        public IEnumerable<Chunk> ReadLinesFromFile(string filePath, int chunkSize)
        {
            var lines = new List<string>();
            using var reader = new StreamReader(filePath);

            int linesInChunk = 0;
            int chunkNumber = 0;

            while (reader.EndOfStream != true)
            {
                lines.Add(reader.ReadLine());
                linesInChunk++;

                if (linesInChunk == chunkSize)
                {
                    yield return new Chunk(chunkNumber, new List<string>(lines));

                    linesInChunk = 0;
                    chunkNumber++;
                    lines.Clear();
                }
            }

            if (linesInChunk > 0)
            {
                yield return new Chunk(chunkNumber, lines);
            }
        }
    }
}