using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextModell.Interfaces;

namespace TextModell.Classes
{
    public class Parser
    {
        private SeparatorContainer _separatorContainer;

        protected SeparatorContainer SeparatorContainer
        {
            get { return _separatorContainer; }
            set { _separatorContainer = value; }
        }

        public Text Parse(TextReader reader)
        {
            var orderedSentenceSeparators = SeparatorContainer.SentenceSeparators().OrderByDescending(x => x.Length);

            Text textResult = new Text();
            int sentenceSeparatorPlace = -1;

            Sentence currentSentence = new Sentence();
            string line = RemoveWS(reader.ReadLine());

            int row = 1;
            while (line != null)
            {
                line += "\r\n";
                var sentenceSeparator = FindSeparator(orderedSentenceSeparators, line, out sentenceSeparatorPlace);
                while (sentenceSeparatorPlace >= 0)
                {
                    var currentSentenceString = line.Substring(0, sentenceSeparatorPlace + sentenceSeparator.Length);
                    ParseSentence(currentSentenceString, currentSentence, row);
                    textResult.Add(currentSentence);

                    currentSentence = new Sentence();
                    line = line.Substring(sentenceSeparatorPlace + sentenceSeparator.Length);
                    sentenceSeparator = FindSeparator(orderedSentenceSeparators, line, out sentenceSeparatorPlace);
                }

                ParseSentence(line, currentSentence, row);

                line = RemoveWS(reader.ReadLine());
                row++;
            }
            return textResult;
        }

        public ICollection<ISentenceItem> Parse(string String, int row)
        {
            Sentence currentSentence = new Sentence();
            ParseSentence(String, currentSentence, row);
            return currentSentence.Content;
        }

        private string RemoveWS(string source)
        {
            if (source != null)
            {
                return Regex.Replace(source, "[\\t ]+", " ");
            }
            return source;
        }

        private Symbol FindSeparator(IEnumerable<Symbol> items, string line, out int index)
        {
            int minIndex = line.Length;
            Symbol result = null;
            foreach (var item in items)
            {
                index = line.IndexOf(item.Content);
                if (index >= 0 && index < minIndex)
                {
                    minIndex = index;
                    result = item;
                }
            }
            if (minIndex < line.Length)
            {
                index = minIndex;
                return result;
            }
            else
            {
                index = -1;
                return null;
            }
        }

        protected void ParseSentence(string source, Sentence sentence, int row)
        {
            int index = -1;
            var orderedWordSeparators = SeparatorContainer.All().OrderByDescending(x => x.Length);
            var wordSeparator = FindSeparator(orderedWordSeparators, source, out index);
            while (index >= 0)
            {
                var currentWordString = source.Substring(0, index);
                if (currentWordString.Length > 0)
                {
                    sentence.Add(new Word(currentWordString, row));
                }
                sentence.Add(new Punctuation(wordSeparator, row));

                source = source.Substring(index + wordSeparator.Length);
                wordSeparator = FindSeparator(orderedWordSeparators, source, out index);
            }
            if (source != "" && source != null)
            {
                sentence.Add(new Word(source, row));
            }

        }

        public Parser(SeparatorContainer separatorContainer)
        {
            this.SeparatorContainer = separatorContainer;
        }
    }
}

