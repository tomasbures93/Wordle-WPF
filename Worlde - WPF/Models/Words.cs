using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worlde___WPF.Models
{
    public class Words
    {
        private List<string> _words = new List<string>();

        public Words()
        {
            LoadWords();
        }

        public string GenerateWord()
        {
            Random rnd = new Random();
            string word = _words[rnd.Next(0, _words.Count)];
            return word;
        }

        public List<string> GetAllWords()
        {
            return _words;
        }

        private void LoadWords()
        {
            string pathWords = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "words.txt");
            using (StreamReader sr = new StreamReader(pathWords))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Trim();
                    _words.Add(line);
                }
            }
        }

        public bool IsWordInDictionary(string word)
        {
            foreach (string w in _words)
            {
                if (w == word) return true;
            }
            return false;
        }
    }
}
