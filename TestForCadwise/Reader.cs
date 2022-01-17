using System;
using System.Collections.Generic;
using System.IO;


namespace TestForCadwise
{
    class Reader
    {
        private const int blockSize = 1024;//104857600;
        private readonly int blockCount = Environment.ProcessorCount;

        List<Chunk> chunkList = new List<Chunk>();

        public List<Chunk> Parse(FileInfo inputFile, FileInfo outputFile, int lengthThreshold, bool needDeletePunctuation)
        {
             var sr = new StreamReader(inputFile.FullName);

             for (int i = 0; i < blockCount; i++)
             {          
                char[] buffer = new char[blockSize];
                sr.ReadBlock(buffer, 0, blockSize); 

                chunkList.Add(new Chunk(i, buffer));
            }

            //var text = SplitFileChar(inputFile, BlockSize);

            //var text = SplitFileListString(inputFile, BlockSize);

            var text = SplitFileListChunk(inputFile);


            //Это для проверки текста- вывод на констоль
            //for (int i = 0; i < text.Count; i++)
            //{
            //    for (int j = 0; j < text[i].fragmentText.Count; j++)
            //    {

            //        Console.WriteLine(text[i].fragmentText[j]);

            //    }

            //    Console.WriteLine($"Закончилась структура{i}");

            //}


            //Это блок если вызываем преобразование в лист строк и его в структуры
            //for (int i = 0; i < text.Count; i++)
            //{
            //    Chunk chunk = new Chunk(i, text[i]);
            //    chunkList.Add(chunk);
            //}

            //Console.Write("test");
            return text;
        }




        //List<char[]> SplitFileChar(FileInfo inputFile, int BlockSize)
        //{
        //    var result = new List<char[]>();

        //    var fileLen = new FileInfo(inputFile.FullName).Length;

        //    var totalRead = 0;
        //    using (var sr = new StreamReader(inputFile.FullName))
        //    {
        //        while (totalRead < fileLen)
        //        {

        //            var part = new char[BlockSize];
        //            var a = sr.Read(part, 0, part.Length);
        //            if (a == 0)
        //            {
        //                break;
        //            }

        //            totalRead += BlockSize;

        //            result.Add(part);
        //        }
        //    }

        //    return result;
        //}


        List<List<string>> SplitFileListString(FileInfo inputFile, int BlockSize)
        {

            List<List<string>> result = new List<List<string>>();

            var fileLen = new FileInfo(inputFile.FullName).Length;

            var totalRead = 0;
            using (var sr = new StreamReader(inputFile.FullName))
            {
                while (totalRead < fileLen)

                {
                    var part = new List<string>();

                    for (int i = 0; i < 10; i++)

                    {
                        string temp = sr.ReadLine();
                        if (String.IsNullOrEmpty(temp))
                        {
                            //Пытаюсь сделать чтобы в каждую секцию писалось 10 строк но тогда в конце уходим
                            //в вечный цикл так как I все время
                            //вычитается. wile никак так как в последнем блоке не 10 строк а меньше
                            // i--;
                            //Предполагаю что можно пренебресь так как размеры блоков будут большие
                            //и разница в несколько строк не важна
                            continue;
                        }
                        part.Add(temp);
                    }

                    totalRead += BlockSize;

                    if (part.Count != 0)
                    {
                        result.Add(part);
                    }
                }
            }

            return result;
        }


        List<Chunk> SplitFileListChunk(FileInfo inputFile)
        {
            var result = new List<Chunk>();

            int counter = 0;
            var readCount = 0;

            using (var sr = new StreamReader(inputFile.FullName))
            {
                while (readCount < inputFile.Length)
                {
                    var part = new List<string>();
                    for (int i = 0; i < 10; i++)
                    {
                        part.Add(sr.ReadLine());
                    }

                    readCount += blockSize;


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
