using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

//Подчеркивал не давал вызвать парсер если ставила List<Chunk>chunks пришлось добавить последний странный юзинг

namespace TestForCadwise
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Задайте полный путь к входному файлу");
            //string inputPath = Console.ReadLine();
            var inputFile = new FileInfo("d:\\sobes\\Cadwise\\input\\InputTest.txt");



            //tще не проверяла
            //while (true)
            //{
            //    if (!inputFile.Exists)
            //    {
            //        Console.WriteLine("Вы задали неправильный путь. Попробуйте снова");
            //        string inputPath = Console.ReadLine();
            //    }
            //    else break;
            //}

            //Console.WriteLine("Задайте полный путь к месту хранения результирующего файла");
            //string outputPath = Console.ReadLine();@"d:\sobes\Cadwise\output\OutputTest.txt"
            var outputFile = new FileInfo(inputFile.Directory.FullName + "\\Output.txt");


            //Console.WriteLine("Введите количество символов для удаления слов меньшей длины");
            //int lengthDeletedWords;
            //string result = Console.ReadLine();

            //while (!Int32.TryParse(result, out lengthDeletedWords))
            //{
            //    Console.WriteLine("Вы ввели не число, попробуйте снова.");
            //    result = Console.ReadLine();
            //}

            int lengthThreshold = 5;
            bool needDeletePunctuation = true;
            //bool needDeletePunctuation = false;

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

            //var textParser = new TextParser();
            //textParser.Parse(inputFile, outputFile, lengthThreshold, needDeletePunctuation);


            //пока основной
            //var reader = new TestReader();
            //reader.Read(inputFile, outputFile, lengthThreshold, needDeletePunctuation);

            var chunkParser = new ChunkParser();

            List<Chunk> result = new List<Chunk>();


            int current_part = 0;

            //var reader = new yieldReader();
            //foreach (LinkedList<string> LinesPart in yieldReader.AllLinesFromFile(inputFile.FullName))

            var reader = new ChunkReader();
            foreach (Chunk LinesPart in reader.ReadLinesFromFile(inputFile.FullName, chunkSize:10))
            {
                Chunk chunk = new Chunk(LinesPart.chunkNumber, LinesPart.Lines);

                /*Chunk chunk = new Chunk();
                chunk.chunkNumber = current_part;
                chunk.LinesPart = new LinkedList<string>(LinesPart);*/

                //Console.WriteLine("Current part: " + current_part);
                //Console.WriteLine("");

                //result.Add(chunk);
                result.Add(LinesPart);

                if (result.Count % 4 == 0)
                {

                    var tasks = new List<Task<Chunk>>();

                    for (int i = 0; i < result.Count; i++)
                    {
                        var task = Task<Chunk>.Run(() => chunkParser.ProcessChunk(result[i], lengthThreshold, needDeletePunctuation));
                        tasks.Add(task);

                    };

                    var continuation = Task.WhenAll(tasks);
                    try
                    {
                        continuation.Wait();
                    }
                    catch (AggregateException)
                    { }
                    if (continuation.Status == TaskStatus.RanToCompletion)
                    {
                        var sortedChunks = continuation.Result.OrderBy(c => c.chunkNumber).ToList();
                        foreach (var chunks in sortedChunks)
                        {
                            File.AppendAllText(outputFile.FullName, chunks.text);
                        }

                    }
                    result.Clear();
                    tasks.Clear();

                }

                current_part++;



            }

            //Еще не доделано- нужно 2 таски на 8 и 9 кусок
            Console.WriteLine("вышли из цикла, обработаем остаток записей не в цикле");
            var tasks1 = new List<Task<Chunk>>();

            for (int i = 0; i < result.Count; i++)
            {
                var task = Task<Chunk>.Run(() => chunkParser.ProcessChunk(result[i], lengthThreshold, needDeletePunctuation));
                tasks1.Add(task);

            };

            var continuation1 = Task.WhenAll(tasks1);
            try
            {
                continuation1.Wait();
            }
            catch (AggregateException)
            { }
            if (continuation1.Status == TaskStatus.RanToCompletion)
            {
                var sortedChunks = continuation1.Result.OrderBy(c => c.chunkNumber).ToList();
                foreach (var chunks in sortedChunks)
                {
                    File.AppendAllText(outputFile.FullName, chunks.text);
                }

            }
            result.Clear();
            tasks1.Clear();


            Console.ReadKey();

        }
    }
}
