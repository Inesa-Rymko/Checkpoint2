using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextModell.Interfaces
{
    public interface ISentence : ITextItem, IEnumerable<ITextItem>
    {

        int Count { get; }
        int WordCount { get; }
        void Add(ISentenceItem item);
        bool Remove(ISentenceItem item);
    }
}
