using System.Collections.Generic;

namespace TestForCadwise
{
    public struct Chunk
    {
        public int ChunkNumber { get; private set; }
        public string TextFragment { get; private set; }

        public Chunk(int chunkNumber, string textFragment)
        {
            ChunkNumber = chunkNumber;
            TextFragment = textFragment;
        }
    }
}

