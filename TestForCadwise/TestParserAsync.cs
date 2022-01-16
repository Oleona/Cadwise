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
            public char[] chars;


            public Chunk(int i, char[] chars)
            {
                this.i = i;
                this.chars = chars;

            }



        }
        List<Chunk> chunkList = new List<Chunk>();
        public void Parse(FileInfo inputFile, FileInfo outputFile, int lengthThreshold, bool needDeletePunctuation)
        {
            /* int startPosition = 0;//это позиция в буфере а нужна позиция в файле
             for (int i = 0; i < BlockCount; i++)
             {

                 ////Если я считываю массив символов надо ли мне учитывать границы слов? Вообще нужно иначе я потом могу удалить часть слова
                 ////посчитав его целым словом короче порога
                 StreamReader sr = new StreamReader(inputFile.FullName);
                 //sr.BaseStream.Position = startPosition;
                 //char[] chars = new char[BlockSize];
                 // sr.ReadBlock(chars, 0, BlockSize); //из sr будет прочитано BlockSize символа  с символа с индексом 0
                 ////sr.Read(chars, 0, BlockSize);
                 //Chunk chunk = new(i, chars);
                 //chunkList.Add(chunk);
                 //Console.Write(chars);
                 //Console.WriteLine("test");
                 //Console.WriteLine("test");
                 //Console.WriteLine("test");
                 //startPosition += BlockSize;

                 int line = 10;
                 //можно попробовать обманку считывать строками до примерного размера
                 //На странице 40  строк, в каждой по 60 байт.
                 //Поэтому объём одной страницы текста 60 x 40 = 2400 байт
                 //один блок из 40 строк сейчас повторяет 4 раза, надо как то продолжить чтение опять с позиции
                 var sb = new StringBuilder();

                 for (int j = 0; j < line; j++)
                 {
                     sb.Append(sr.ReadLine());
                 }
                 Console.WriteLine(sb.ToString());
                 
             }*/
            var text = SplitFileChar(inputFile, BlockSize);
            foreach (var t in text)
            {
                string str = new string(t);
                Console.Write(str);
            }

            Console.Write("test");
        }




        List<char[]> SplitFileChar(FileInfo inputFile, int BlockSize)
        {
            var result = new List<char[]>();

            var fileLen = new FileInfo(inputFile.FullName).Length;

            var totalRead = 0;
            using (var sr = new StreamReader(inputFile.FullName))
            {
                while (totalRead < fileLen)
                {
                    var part = new char[fileLen - totalRead < BlockSize ? fileLen - totalRead : BlockSize];
                    var a = sr.Read(part, 0, part.Length);
                    if (a == 0)
                    {
                        break;
                    }

                    totalRead += BlockSize;

                    result.Add(part);
                }
            }

            return result;
        }


    }
}
