using System;
using System.IO;
using System.Threading.Tasks;

namespace TestForCadwise
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine(@"Данная программа позволяет проводить обработку текстового файла. Имеются следующие возможности:
- удалить все слова меньше заданной длины
- удалить пунктуацию
Имеется возможность работать с файлами большого размера, в том числе не помещающимися в оперативную память.
Программа написана под платформу .NET 5.0 на языке C# версии 9.0 с использованием Visual Studio 2019.
");


            Console.WriteLine("Задайте полный путь к входному файлу, например, C:\\work\\input.txt");
            string inputPath = "d:\\sobes\\Cadwise\\input\\InputTest.txt";//Console.ReadLine();
            var inputFile = new FileInfo(inputPath);

            while (true)
            {
                if (!inputFile.Exists)
                {
                    Console.WriteLine("Вы задали неправильный путь. Попробуйте снова");
                    inputPath = Console.ReadLine();
                    inputFile = new FileInfo(inputPath);
                }
                
                break;
            }

            Console.WriteLine("Задайте имя выходного файла, например, output.txt");
            string outputFileName = "output.txt";//Console.ReadLine();
            var outputFile = new FileInfo(inputFile.Directory.FullName + '\\' + outputFileName);


            Console.WriteLine("Введите количество символов для удаления слов меньшей длины");
            string result = "5";//Console.ReadLine();
            int minimalLength;

            while (!int.TryParse(result, out minimalLength) || minimalLength < 0)
            {
                Console.WriteLine("Необходимо ввести положительное число");
                result = Console.ReadLine();
            }

            bool needDeletePunctuation;
            Console.WriteLine("Необходимо ли удалять пунктуацию (y/n)?");
            string punctuationMark = Console.ReadLine();
            while (true)
            {
                if (punctuationMark.Equals("y", StringComparison.CurrentCultureIgnoreCase))
                {
                    needDeletePunctuation = true;
                    break;
                }
                if (punctuationMark.Equals("n", StringComparison.CurrentCultureIgnoreCase))
                {
                    needDeletePunctuation = false;
                    break;
                }
                Console.WriteLine("Вы ввели неправильный символ, попробуйте снова.");
                punctuationMark = Console.ReadLine();
            }

            try
            {
                var textAnalyzer = new TextAnalyzer(inputFile, outputFile, minimalLength, needDeletePunctuation);
                await textAnalyzer.Analyze();

                Console.WriteLine(
                    $"Обработка файла {inputFile.Name} завершена успешно. Результат записан в файл {outputFile.Name}");
            } 
            catch (Exception e)
            {
                Console.WriteLine($"Обработка файла {inputFile.Name} завершилась неудачей: {e.Message}.");
            }
                   
        }
    }
}
