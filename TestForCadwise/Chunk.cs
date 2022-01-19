using System;
using System.Collections.Generic;

namespace TestForCadwise
{
    public struct Chunk
    {
        public int ChunkNumber { get; private set; }
        public List<string> TextFragments { get; private set; }


        public Chunk(int chunkNumber, List<string> textFragments)
        {
            ChunkNumber = chunkNumber;
            TextFragments = textFragments;
        }
    }
}

