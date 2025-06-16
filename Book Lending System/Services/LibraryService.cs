using Book_Lending_System.Interfaces;
using Book_Lending_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Lending_System.Services
{
    public class LibraryService : ILibraryService
    {
        private List<IBook> _books = new List<IBook>();
        private List<IUser> _users = new List<IUser>();
        private int _nextUserId = 1;

        public void AddBook(string title, string author, string category, string isbn)
        {
            
                if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author) || string.IsNullOrWhiteSpace(category) || string.IsNullOrWhiteSpace(isbn))
                {
                    Console.WriteLine("Book details cannot be empty or whitespace.");
                    return;
                }

                if (_books.Any(b => b.ISBN == isbn))
                {
                    Console.WriteLine("A book with this ISBN already exists.");
                    return;
                }

                // If no duplicates, add the new book
                var book = new Book(title, author, category, isbn);
                _books.Add(book);
                Console.WriteLine($"Book '{title}' added to the library.");            
            
        }

        public void RemoveBook(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
            {
                Console.WriteLine("Enter the isbn");
                return;
            }
            var book = _books.FirstOrDefault(b => b.ISBN == isbn);
            if (book != null)
            {
                _books.Remove(book);
                Console.WriteLine($"Book with ISBN {isbn} removed.");
            }          
        }

        public void UpdateBookInfo(string isbn, string title, string author, string category)
        {
            var book = _books.FirstOrDefault(b => b.ISBN == isbn)?? throw new Exception("Book not found");
            if (book != null)
            {
                book.Title = title;
                book.Author = author;
                book.Category = category;
                Console.WriteLine("Book information updated.");
            }            
        }

        public IBook SearchBook(string searchTerm)
        {
            return _books.FirstOrDefault(b => b.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                              b.Author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        public List<IBook> GetAvailableBooks()
        {
            return _books.Where(b => b.IsAvailable).ToList();
        }

        public List<IBook> GetAllBooks()
        {
            return _books;
        }

        public IUser RegisterUser(string name)
        {
            var user = new User(_nextUserId++, name);
            _users.Add(user);
            Console.WriteLine($"{name} has been registered with User ID: {user.UserId}");
            return user;
        }

        public void UnregisterUser(int userId)
        {
            var user = _users.FirstOrDefault(u => u.UserId == userId);           
                _users.Remove(user);
                Console.WriteLine($"User with ID {userId} has been unregistered.");                 
        }

        public IUser GetUser(int userId)
        {
            return _users.FirstOrDefault(u => u.UserId == userId);
        }

        public List<IUser> GetAllUsers()
        {
            return _users;
        }

        public void BorrowBook(int userId, string isbn)
        {
            var user = GetUser(userId);
            var book = _books.FirstOrDefault(b => b.ISBN == isbn);
            user.BorrowBook(book);
        }

        public void ReturnBook(int userId, string isbn)
        {
            var user = GetUser(userId);
            var book = _books.FirstOrDefault(b => b.ISBN == isbn);
            user.ReturnBook(book);
        }

        public int SelectUserFromUserList()
        {
            try
            {
                var users = GetAllUsers();
                if (users.Count == 0)
                {
                    Console.WriteLine("No User available to remove.");
                    return -1;
                }

                Console.WriteLine("Available Users:");
                for (int i = 0; i < users.Count; i++)
                {
                    Console.WriteLine($"{users[i].UserId} . {users[i].Name}");
                }

                Console.WriteLine("Enter the UserId:");
                int userId = int.Parse(Console.ReadLine());
                var userToRemove = users.FirstOrDefault(x => x.UserId == userId);
                if (userToRemove == null)
                {
                    Console.WriteLine("User not found.");
                    return -1;
                }
                return userId;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return -1;
            }


        }

    }
}
