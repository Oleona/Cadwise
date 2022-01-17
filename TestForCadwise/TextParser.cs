using System;
using System.IO;
using System.Text;

namespace TestForCadwise
{
    internal class TextParser
    {
        private const string delimetrs = ".,;:«»—!?-\"()";


        public void Parse(FileInfo inputFile, FileInfo outputFile, int lengthThreshold, bool needDeletePunctuation)
        {
            string readText = "";
            try
            {
                readText = File.ReadAllText(inputFile.FullName);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            var worldArray = readText.Split(' ');
            var text = ProcessWordArray(worldArray, lengthThreshold, needDeletePunctuation);

            File.WriteAllText(outputFile.FullName, text);
        }

        private string ProcessWordArray(string[] wordArray, int lengthThreshold, bool needDeletePunctuation)
        {
            var sb = new StringBuilder(wordArray.Length * 10);
            foreach (var word in wordArray)
            {
                var cleanedWord = needDeletePunctuation ? RemovePunctuation(word) : word;
                if (cleanedWord.Length > lengthThreshold)
                {
                    sb.Append(cleanedWord);
                    sb.Append(' ');
                }
            }

            return  sb.ToString();
        }

        private string RemovePunctuation(string word)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (delimetrs.Contains(word[i]))
                {
                    word = word.Replace(word[i].ToString(), "");
                }
            }

            return word;
        }

    }
}
