using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextModell.Classes;

namespace TextModell.Interfaces
{
    public interface IPunctuation : ISentenceItem
    {
        Symbol Content { get; }
    }
}
