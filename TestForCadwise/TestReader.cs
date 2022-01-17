using System;
using System.Collections.Generic;
using System.IO;

namespace TestForCadwise
{
    class TestReader
    {
        private const int BlockSize = 1024;//104857600;
        private readonly int BlockCount = Environment.ProcessorCount;

        

        public List<Chunk> Read(FileInfo inputFile, FileInfo outputFile, int lengthThreshold, bool needDeletePunctuation)
        {
            var text = SplitFileListChunk(inputFile, BlockSize);
            return text;
        }    

        List<Chunk> SplitFileListChunk(FileInfo inputFile, int BlockSize)
        {

            List<Chunk> result = new List<Chunk>();

            var fileLen = new FileInfo(inputFile.FullName).Length;
            int counter = 0;
            //var totalRead = 0;
            using (var sr = new StreamReader(inputFile.FullName))
            { 
     
                while (!sr.EndOfStream)
                {
                    var part = new List<string>();

                    for (int i = 0; i < 10; i++)

                    {
                        string temp = sr.ReadLine();
                        if (String.IsNullOrEmpty(temp))
                        {
                           
                            continue;
                        }
                        part.Add(temp);
                    }

                    //totalRead += BlockSize;


                    if (part.Count != 0)
                    {
                        Chunk chunk = new Chunk(counter, part);
                        result.Add(chunk);
                    }
                }
            }

            return result;
        }

    }
}

