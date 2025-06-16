using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Lending_System.Interfaces
{

    public interface ILibraryService
    {
        void AddBook(string title, string author, string category, string isbn);
        void RemoveBook(string isbn);
        void UpdateBookInfo(string isbn, string title, string author, string category);
        IBook SearchBook(string searchTerm);
        List<IBook> GetAvailableBooks();
        List<IBook> GetAllBooks();
        IUser RegisterUser(string name);
        void UnregisterUser(int userId);
        IUser GetUser(int userId);
        void BorrowBook(int userId, string isbn);
        void ReturnBook(int userId, string isbn);
    }
}
