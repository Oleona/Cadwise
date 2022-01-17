using System;
using System.Collections.Generic;
using System.IO;

namespace TestForCadwise
{
    class yieldReader
    {

        public static IEnumerable<LinkedList<string>> AllLinesFromFile(string a_file_path)
        {
            LinkedList<string> Lines = new LinkedList<string>();

            using (StreamReader r = new StreamReader(a_file_path))
            {
                int count = 0;
                while (r.EndOfStream != true)
                {
                    Lines.AddLast(r.ReadLine());
                    count++;

                    if (count == 10)
                    {
                        count = 0;
                        yield return Lines;
                        // Удаление текущих узлов списка перед заполнением следующей порцией строк
                        Lines.Clear();
                    }
                }
                if (count > 0 && count <= 10)
                {
                    yield return Lines;
                }
            }
        }

    }
}
