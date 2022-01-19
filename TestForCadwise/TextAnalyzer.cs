using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TestForCadwise
{
    public class TextAnalyzer
    {
        private readonly FileInfo inputFile;
        private readonly FileInfo outputFile;
        private readonly int lengthThreshold;
        private readonly bool needDeletePunctuation;

        public TextAnalyzer(FileInfo inputFile, FileInfo outputFile, int lengthThreshold, bool needDeletePunctuation)
        {
            this.inputFile = inputFile;
            this.outputFile = outputFile;
            this.lengthThreshold = lengthThreshold;
            this.needDeletePunctuation = needDeletePunctuation;
        }

        public async Task Analyze()
        {
            var reader = new ChunkReader();

            if (outputFile.Exists)
            {
                File.Delete(outputFile.FullName);
            }

            var currentBlock = new List<Chunk>();
            foreach (var chunk in reader.ReadLinesFromFile(inputFile.FullName, chunkSize: 1024 * 1024))
            {
                currentBlock.Add(chunk);
                if (currentBlock.Count % Environment.ProcessorCount != 0)
                {
                    continue;
                }

                await ProcessChunksInParallel(currentBlock);
            }

            await ProcessChunksInParallel(currentBlock);
        }


        private async Task ProcessChunksInParallel(List<Chunk> currentBlock)
        {
            var tasks = new List<Task<Chunk>>();
            foreach (var chunk in currentBlock)
            {
                var chunkParser = new ChunkParser(chunk);
                var task = Task.Run(() => chunkParser.ProcessChunk(lengthThreshold, needDeletePunctuation));
                
                tasks.Add(task);
            };

            var processResult = await Task.WhenAll(tasks);
            var sortedChunks = processResult.OrderBy(c => c.ChunkNumber).ToList();
            
            foreach (var chunks in sortedChunks)
            {
                File.AppendAllLines(outputFile.FullName, chunks.TextFragments);
            }
        }
    }
}
