﻿using System;
using System.IO;

namespace TestForCadwise
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Задайте полный путь к входному файлу");
            //string inputPath = Console.ReadLine();
            var inputFile = new FileInfo("d:\\sobes\\Cadwise\\input\\InputTest.txt");
            if (!inputFile.Exists)
            {
                //....
                return;
            }


            //Console.WriteLine("Задайте полный путь к месту хранения результирующего файла");
            //string outputPath = Console.ReadLine();@"d:\sobes\Cadwise\output\OutputTest.txt"
            var outputFile = new FileInfo(inputFile.Directory.FullName + "Output.txt");


            //Console.WriteLine("Введите количество символов для удаления слов меньшей длины");
            //int lengthDeletedWords;
            //string result = Console.ReadLine();

            //while (!Int32.TryParse(result, out lengthDeletedWords))
            //{
            //    Console.WriteLine("Вы ввели не число, попробуйте снова.");
            //    result = Console.ReadLine();
            //}

            int lengthThreshold = 5;
           //bool needDeletePunctuation = true;
            bool needDeletePunctuation = false;

            //Console.WriteLine("Введите y если удаляем знаки препинания или n, если не удаляем");
            //string punctuationMark = Console.ReadLine();
            //while (true)
            //{
            //    if (punctuationMark.Equals("y".ToUpper()) || punctuationMark.Equals("n".ToUpper()))
            //    {
            //        if (punctuationMark.Equals("y".ToUpper()))
            //            {
            //            needDeletePunctuation = true;
            //             }
            //        else
            //        {
            //            needDeletePunctuation = false;
            //        }

            //        break;
            //    }

            //    else
            //    {
            //        Console.WriteLine("Вы ввели неправильный символ, попробуйте снова.");
            //        punctuationMark = Console.ReadLine();
            //    }
            //}

            var textParser = new TextParser();
            textParser.Parse(inputFile, outputFile, lengthThreshold, needDeletePunctuation);

            Console.ReadKey();

        }
    }
}
