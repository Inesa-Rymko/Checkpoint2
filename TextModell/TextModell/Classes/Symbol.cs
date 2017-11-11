using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextModell.Classes
{
    public class Symbol
    {
        private string _content;

        public string Content
        {
            get { return _content; }
            private set { _content = value; }
        }

        public Symbol(string source)
        {
            _content = source;
        }

        public Symbol(char source)
        {
            _content = String.Format("{0}", source);
        }

        public bool IsUppercase
        {
            get { return _content != null && _content.Length >= 1 && char.IsLetter(_content[0]) && char.IsUpper(_content[0]); }
        }

        public bool IsLower
        {
            get { return _content != null && _content.Length >= 1 && char.IsLetter(_content[0]) && char.IsLower(_content[0]); }
        }

        public bool IsVowel
        {
            get
            {
                var rule = new Regex("[aeiou]", RegexOptions.IgnoreCase);
                return rule.IsMatch(_content);
            }
        }

        public int Length
        {
            get { return _content.Length; }
        }

        public string Chars
        {
            get { return _content; }
        }

    }
}
