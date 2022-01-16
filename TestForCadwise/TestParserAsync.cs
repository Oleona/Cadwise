using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForCadwise
{
    class TestParserAsync
    {
        //Далее рассчитывать размер блока 1 char это 2 байт
        static int BlockSize = 1024;//104857600;
        static int BlockCount = Environment.ProcessorCount;

        public struct Chunk
        {
            public int i;
            //public char[] chars;
            public List<string> fragmentText;

            //public Chunk(int i, char[] chars)
            //{
            //    this.i = i;
            //    this.chars = chars;

            //}

            public Chunk(int i, List<string> fragmentText)
            {
                this.i = i;
                this.fragmentText = fragmentText;
            }


        }
        List<Chunk> chunkList = new List<Chunk>();
        public void Parse(FileInfo inputFile, FileInfo outputFile, int lengthThreshold, bool needDeletePunctuation)
        {
            /* int startPosition = 0;//это позиция в буфере а нужна позиция в файле
             for (int i = 0; i < BlockCount; i++)
             {
                 StreamReader sr = new StreamReader(inputFile.FullName);
                 //sr.BaseStream.Position = startPosition;
                 //char[] chars = new char[BlockSize];
                 // sr.ReadBlock(chars, 0, BlockSize); //из sr будет прочитано BlockSize символов
                 ////sr.Read(chars, 0, BlockSize);
                 //Chunk chunk = new(i, chars);
                 //chunkList.Add(chunk);
                 //startPosition += BlockSize;
             }*/

            //var text = SplitFileChar(inputFile, BlockSize);

            //var text = SplitFileListString(inputFile, BlockSize);

            var text = SplitFileListChunk(inputFile, BlockSize);

            for (int i = 0; i < text.Count; i++)
            {
                for (int j = 0; j < text[i].fragmentText.Count; j++)
                {
                    
                        Console.WriteLine(text[i].fragmentText[j]);
       
                }

                Console.WriteLine($"Закончилась структура{i}") ;

            }


            //Это блок если вызываем преобразование в лист строк и его в структуры
            //for (int i = 0; i < text.Count; i++)
            //{
            //    Chunk chunk = new Chunk(i, text[i]);
            //    chunkList.Add(chunk);
            //}

            Console.Write("test");
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


            List<Chunk> SplitFileListChunk(FileInfo inputFile, int BlockSize)
            {

                List<Chunk> result = new List<Chunk>();

                var fileLen = new FileInfo(inputFile.FullName).Length;
                int counter = 0;
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
                            Chunk chunk = new Chunk(counter, part);
                            result.Add(chunk);
                        }
                    }
                }

                return result;
            }

        }
    }

