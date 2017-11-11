using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextModell.Classes;

namespace TextModell.Interfaces
{
    public interface IText : ITextItem
    {
        int SentenceCount { get; }
        int WordCount { get; }
        void Add(Sentence item);
        bool Remove(Sentence item);
    }
}
