using System.Collections.Generic;

namespace TestForCadwise
{
    public struct Chunk
    {
        public int chunkNumber;
        public List<string> textFragments;
        public char[] charFragments;
        public LinkedList<string> LinesPart;

        public Chunk(int chunkNumber, char[] charFragments)
        {
            this.chunkNumber = chunkNumber;
            this.charFragments = charFragments;
            this.textFragments = null;
            this.LinesPart = null;

        }

        public Chunk(int chunkNumber, List<string> textFragments)
        {
            this.chunkNumber = chunkNumber;
            this.textFragments = textFragments;
            this.charFragments = null;
            this.LinesPart = null;
        }

        public Chunk(int chunkNumber, LinkedList<string> LinesPart)
        {
            this.chunkNumber = chunkNumber;
            this.LinesPart = LinesPart;
            this.charFragments = null;
            this.textFragments = null;

        }

    }
}

