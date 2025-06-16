using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Lending_System.Interfaces
{
    public interface IUser
    {
        int UserId { get; }
        string Name { get; set; }
        List<IBook> BorrowedBooks { get; set; }
        void BorrowBook(IBook book);
        void ReturnBook(IBook book);
    }
}
