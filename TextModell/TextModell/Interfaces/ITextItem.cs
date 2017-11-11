using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextModell.Interfaces
{
    public interface ITextItem
    {
        string Chars { get; }
        int Length { get; }

    }
}
