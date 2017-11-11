using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextModell.Interfaces;

namespace TextModell.Classes
{
    public class Text : IText
    {
        private List<Sentence> _content;

        public List<Sentence> Content
        {
            get { return _content; }
        }

        public int Length
        {
            get { return Chars.Length; }
        }

        public int SentenceCount
        {
            get { return _content.Count; }
        }

        public int WordCount
        {
            get
            {
                return _content.Sum(s => s.WordCount);
            }
        }

        public string Chars
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var s in _content)
                {
                    sb.Append(s.Chars);
                }
                return sb.ToString();
            }
        }

        public Text()
        {
            _content = new List<Sentence>();
        }

        public Text(List<Sentence> sentence)
        {
            _content = sentence;
        }

        public void Add(Sentence sentence)
        {
            _content.Add(sentence);
        }

        public bool Remove(Sentence sentence)
        {
            return _content.Remove(sentence);
        }

        public ICollection<Word> Words
        {
            get
            {
                return Content.SelectMany(s => s.Words).OrderBy(word => word.Chars).ToList();
            }
        }

        public ICollection<Word> WordsDistinct
        {
            get
            {
                return Words
                        .GroupBy(o => o.Chars, StringComparer.InvariantCultureIgnoreCase)
                        .Select(o => o.FirstOrDefault())
                        .ToList();
            }
        }


        ///предложения текста в порядке возрастания количества слов в каждом из них
        public List<Sentence> SentancesOrderedByWordCount
        {
            get
            {
                return Content.OrderBy(x => x.WordCount).ToList();
            }
        }

        /// Все предложения с вопросительным знаком - '?'
        public List<Sentence> InterrogativeSentances
        {
            get
            {
                return Content.FindAll(x => x.Content.Last().Chars.Contains("?")).ToList();
            }
        }

        /// Все слова из вопросительных предложений в тексте
        public List<Word> InterrogativeSentancesWords()
        {
            return InterrogativeSentances
                    .SelectMany(x => x.Words)
                    .OrderBy(word => word.Chars)
                    .ToList();
        }

        /// distinct слова из вопросительных предложений в тексте.
        public List<Word> InterrogativeSentancesWordsDistinct()
        {
            return InterrogativeSentancesWords()
                    .GroupBy(o => o.Chars, StringComparer.InvariantCultureIgnoreCase)
                    .Select(o => o.FirstOrDefault())
                    .ToList();
        }

        /// Все слова указанной длины из вопросительных предложений в тексте.
        public List<Word> InterrogativeSentancesWords(int length)
        {
            return InterrogativeSentancesWords()
                    .FindAll(x => x.Length == length);
        }

        ///слова указанной длины из вопросительных предложений в тексте
        public List<Word> InterrogativeSentancesWordsDistinct(int length)
        {
            return InterrogativeSentancesWordsDistinct()
                    .FindAll(x => x.Length == length);
        }

        /// Удалите все слова указанной длины и начинайте с заданного символа
        public void RemoveWords(int length, char startChar)
        {
            foreach (var sentence in Content)
            {
                var words = sentence.Words.FindAll(x => x.Chars[0] == startChar && x.Length == length);
                foreach (var w in words)
                {
                    sentence.Remove(w);
                }
            }
        }

        /// Из текста удалить все слова заданной длины, начиная с согласного.

        public void RemoveConsonantWords(int length)
        {
            foreach (var sentence in Content)
            {
                var words = sentence.Words.FindAll(x => !x[0].IsVowel && x.Length == length);
                foreach (var w in words)
                {
                    sentence.Remove(w);
                }
            }
        }

        public void ToFile(string path)
        {
            using (StreamWriter file = new StreamWriter(path))
            {
                file.WriteLine(this.Chars);
            }
        }
    }
}
