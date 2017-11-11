using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextModell.Interfaces;

namespace TextModell.Classes
{
    public sealed class Punctuation : IPunctuation
    {
        private Symbol _content;

        public Symbol Content
        {
            get
            {
                return _content;
            }
            private set
            {
                _content = value;
            }
        }

        public int Length
        {
            get { return Content.Length; }
        }

        public int Row { get; private set; }

        public string Chars
        {
            get { return Content.Chars; }
        }

        public Punctuation(Symbol content, int row)
        {
            _content = content;
            Row = row;
        }
    }
}
