using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextModell.Interfaces;

namespace TextModell.Classes
{
    public class Sentence : ISentence
    {
        private List<ISentenceItem> _items;
        public List<ISentenceItem> Content
        {
            get { return _items; }
        }

        public List<Word> Words
        {
            get { return Content.OfType<Word>().ToList(); }
        }

        /// количество символов в предложении
        public int Length
        {
            get { return Chars.Length; }
        }

        /// количество элементов в предложении
        public int Count
        {
            get { return _items.Count; }
        }

        /// количество слов в предложении
        public int WordCount
        {
            get { return Words.Count(); }
        }

        public string Chars
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in Content)
                {
                    sb.Append(item.Chars);
                }
                return sb.ToString();
            }
        }

        public Sentence()
        {
            _items = new List<ISentenceItem>();
        }

        public Sentence(List<ISentenceItem> source)
        {
            _items = source;
        }

        public void Add(ISentenceItem item)
        {
            if (item != null)
            {
                _items.Add(item);
            }
            else
            {
                throw new NullReferenceException("");
            }
        }

        public void Replace(int length, string subString)
        {
            SeparatorContainer sp = new SeparatorContainer();
            Parser parser = new Parser(sp);

            var words = Words.FindAll(w => w.Length == length);
            foreach (var word in words)
            {
                var subSentence = parser.Parse(subString, word.Row);
                var index = _items.IndexOf(word);
                Remove(word);
                _items.InsertRange(index, subSentence);
            }
        }

        public bool Remove(ISentenceItem item)
        {
            if (item != null)
            {
                return _items.Remove(item);
            }
            else
            {
                throw new NullReferenceException("");
            }
        }

        public IEnumerator<ITextItem> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
