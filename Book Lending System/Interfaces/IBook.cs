using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Lending_System.Interfaces
{
    public interface IBook
    {
        string Title { get; set; }
        string Author { get; set; }
        string Category { get; set; }
        string ISBN { get; set; }
        bool IsAvailable { get; set; }
    }

}
