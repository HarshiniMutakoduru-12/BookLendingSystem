using Book_Lending_System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Lending_System.Models
{
    public class Book : IBook
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string ISBN { get; set; }
        public bool IsAvailable { get; set; } = true;

        public Book(string title, string author, string category, string isbn)
        {
            Title = title;
            Author = author;
            Category = category;
            ISBN = isbn;
        }
    }
}
