using Book_Lending_System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Lending_System.Models
{
    public class User : IUser
    {
        public int UserId { get; private set; }
        public string Name { get; set; }
        public List<IBook> BorrowedBooks { get; set; } = new List<IBook>();

        public User(int userId, string name)
        {
            UserId = userId;
            Name = name;
        }

        public void BorrowBook(IBook book)
        {
            if (book.IsAvailable)
            {
                BorrowedBooks.Add(book);
                book.IsAvailable = false;
            }
            else
            {
                Console.WriteLine("Sorry, the book is currently unavailable.");
            }
        }

        public void ReturnBook(IBook book)
        {
            if (BorrowedBooks.Contains(book))
            {
                BorrowedBooks.Remove(book);
                book.IsAvailable = true;
            }
            else
            {
                Console.WriteLine("This book was not borrowed by you.");
            }
        }
    }
}
