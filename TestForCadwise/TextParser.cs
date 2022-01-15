using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForCadwise
{
    internal class TextParser
    {
        private const string delimetrs = ".,;:!?-\"()";

        public void Parse(FileInfo inputFile, FileInfo outputFile, int lengthThreshold, bool needDeletePunctuation)
        {
            string readText = "";
            try
            {
                readText = File.ReadAllText(inputFile.FullName);
                Console.WriteLine(readText);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            var worldArray = readText.Split(' ');

            //Если так то лепит все без пустых строк а иначе почему то  в каком то вар оставляет Я в начале уже не воспроизвести)

            //var worldArray = readText.Split(new char[] { ' ', '\r', '\n', '\t' }/*, StringSplitOptions.RemoveEmptyEntries*/);


            var sb = new StringBuilder();


            foreach (var world in worldArray)
            {
                if (needDeletePunctuation)
                {
                    var cleanWorld = Clean(world);
                    if (cleanWorld.Length > lengthThreshold)
                    {
                        sb.Append(cleanWorld);
                        sb.Append(' ');
                    }
                }
                else
                {
                    if (world.Length > lengthThreshold)
                    {
                        sb.Append(world);
                        sb.Append(' ');
                    }
                    else if(world.Length == lengthThreshold)
                    {
                        if (!delimetrs.Contains(world[world.Length-1]))
                        {  
                            sb.Append(world);
                            sb.Append(' ');
                        }
                        /// <summary>
                        /// Если слово короче порога, оставляем только знак препинания
                        /// </summary>
                        else
                        {
                            sb.Append(world[world.Length - 1]);
                            sb.Append(' ');
                        }

                    }

                }
            }
            Console.WriteLine(sb);

            string Clean(string world)
            {
                for (int i = 0; i < world.Length; i++)
                {
                    
                        if (delimetrs.Contains(world[i]))
                        {
                            world = world.Replace(world[i].ToString(), "");
                            //world = world.Substring(0, world.Length - 1);
                            return world;
                        }
                   

                }
                return world;
            }



        }
    }
}
