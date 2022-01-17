using System;
using System.Collections.Generic;
using System.IO;
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
            foreach (LinkedList<string> LinesPart in yieldReader.AllLinesFromFile(inputFile.FullName))
            {
                Chunk chunk = new Chunk();
                chunk.chunkNumber = current_part;
                chunk.LinesPart = LinesPart;

               // var chunk = new Chunk(current_part, LinesPart);
               // var newChank = (Chunk)chunk.Clone();

                Console.WriteLine("Current part: " + current_part);
                Console.WriteLine("");


                //Почему в результат не добавляется чанк а предыдущий затирается и в конце 4 одинаковых чанка?

                
                result.Add(chunk);
                List<Chunk> results = new List<Chunk>(result);
                results.Add(chunk);

                if (result.Count % 4 == 0)
                {
                    foreach (Chunk c in result)
                    {
                        Console.WriteLine("chanc number: " + c.chunkNumber.ToString());
                        chunkParser.ChunkParse(c, lengthThreshold, needDeletePunctuation);
                    }
                    var tasks = new List<Task<Chunk>>();
                    //можно foreach если достаточно что у chunk внутри есть номер
                    for (int i = 0; i < result.Count; i++)
                    {
                      //  var task = Task<Chunk>.Run(chunkParser.ChunkParse(result[i], lengthThreshold, needDeletePunctuation));
                       // tasks.Add(task);
                    };
                    result.Clear();
                }

                current_part++;



            }


            Console.WriteLine("вышли из цикла, обработаем остаток записей не в цикле");
            //foreach (Chunk c in result)
            //{
            //    Console.WriteLine("chanc number: " + c.chunkNumber.ToString());
            //}

            // var testParserAsync = new TetParserAsync();

            //List<Chunk> chunks = reader.Parse(inputFile, outputFile, lengthThreshold, needDeletePunctuation);
            //var chunks = new List<Chunk> { reader.Read() 4 раза}

            //var tasks = new List<Task<Chunk>>();
            //for (int i = 0; i < chunks.Count; i++)
            //{
            //    var task = Task<Chunk>.Run(parse(chunks[i]));
            //    });

            //    tasks.Add(task);

            //}




            Console.ReadKey();

        }
    }
}
