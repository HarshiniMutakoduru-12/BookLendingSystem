using Book_Lending_System.Interfaces;
using Book_Lending_System.Interfaces;
using Book_Lending_System.Models;
using Book_Lending_System.Services;
using System;

namespace LibrarySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var libraryService = new LibraryService();
            bool isRunning = true;
            //IUser currentUser = null;

            while (isRunning)
            {
                //if (currentUser == null)
                //{
                //    // Register User
                //    Console.WriteLine("Enter your name to register:");
                //    string name = Console.ReadLine();
                //    currentUser = libraryService.RegisterUser(name);
                //}

                // Show menu
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. Add User");
                Console.WriteLine("2. Remove User");
                Console.WriteLine("3. Add Book");
                Console.WriteLine("4. Remove Book");
                Console.WriteLine("5. Update Book Info");
                Console.WriteLine("6. Search Book");
                Console.WriteLine("7. List Available Books");
                Console.WriteLine("8. Get All Books");
                Console.WriteLine("9. Borrow Book");
                Console.WriteLine("10. Return Book");
                //Console.WriteLine("11. Unregister User");
                Console.WriteLine("11. Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // Add User
                        Console.WriteLine("Enter your name to register:");
                        string name = Console.ReadLine();
                        var user = libraryService.RegisterUser(name);
                        Console.WriteLine($"User {user.Name} registered with ID {user.UserId}.");
                        break;
                    case "2":
                        // Remove User
                        try
                        {
                            var removeUserId = libraryService.SelectUserFromUserList();
                            if (removeUserId < 0)
                                throw new Exception("Invalid User");
                            //var users = libraryService.GetAllUsers();
                            //if (users.Count == 0)
                            //{
                            //    Console.WriteLine("No User available to remove.");
                            //    break;
                            //}

                            //// If there are books, show them and allow removal
                            //Console.WriteLine("Available Users:");
                            //for (int i = 0; i < users.Count; i++)
                            //{
                            //    Console.WriteLine($"{users[i].UserId} . {users[i].Name})");
                            //}

                            //Console.WriteLine("Enter the UserId you want to remove:");
                            //try
                            //{
                            //    int userId = int.Parse(Console.ReadLine());
                            //    var userToRemove = users.FirstOrDefault(x => x.UserId == userId);
                            //    if (userToRemove == null)
                            //    {
                            //        Console.WriteLine("User not found.");
                            //        break;
                            //    }

                            libraryService.UnregisterUser(removeUserId);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Something went wrong while removing the user");
                            break;
                        }

                        break;

                    case "3":
                        // Add Book
                        Console.WriteLine("Enter Book Title:");
                        string title = Console.ReadLine();
                        Console.WriteLine("Enter Author:");
                        string author = Console.ReadLine();
                        Console.WriteLine("Enter Category:");
                        string category = Console.ReadLine();
                        Console.WriteLine("Enter ISBN:");
                        string isbn = Console.ReadLine();
                        libraryService.AddBook(title, author, category, isbn);
                        break;

                    case "4":
                        var books = libraryService.GetAllBooks();

                        // Check if there are any books in the library
                        if (books.Count == 0)
                        {
                            Console.WriteLine("No books available to remove.");
                            break;
                        }

                        // If there are books, show them and allow removal
                        Console.WriteLine("Available Books:");
                        for (int i = 0; i < books.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {books[i].Title} by {books[i].Author} (ISBN: {books[i].ISBN})");
                        }

                        Console.WriteLine("Enter the number of the book you want to remove:");
                        int bookIndex = int.Parse(Console.ReadLine()) - 1;

                        if (bookIndex >= 0 && bookIndex < books.Count)
                        {
                            var bookToRemove = books[bookIndex];
                            libraryService.RemoveBook(bookToRemove.ISBN);
                        }
                        else
                        {
                            Console.WriteLine("Invalid selection.");
                        }
                        break;

                    case "5":
                        // Update Book Info
                        Console.WriteLine("Enter ISBN of the book to update:");
                        isbn = Console.ReadLine();
                        Console.WriteLine("Enter new Title:");
                        title = Console.ReadLine();
                        Console.WriteLine("Enter new Author:");
                        author = Console.ReadLine();
                        Console.WriteLine("Enter new Category:");
                        category = Console.ReadLine();
                        libraryService.UpdateBookInfo(isbn, title, author, category);
                        break;

                    case "6":
                        // Search Book
                        Console.WriteLine("Enter title or author to search:");
                        string searchTerm = Console.ReadLine();
                        var book = libraryService.SearchBook(searchTerm);
                        if (book != null)
                        {
                            Console.WriteLine($"Found Book: {book.Title} by {book.Author}");
                        }
                        else
                        {
                            Console.WriteLine("Book not found.");
                        }
                        break;

                    case "7":
                        // List Available Books
                        var availableBooks = libraryService.GetAvailableBooks();
                        Console.WriteLine("Available Books:");
                        foreach (var b in availableBooks)
                        {
                            Console.WriteLine($"{b.Title} by {b.Author} (ISBN: {b.ISBN})");
                        }
                        break;

                    case "8":
                        // Get All Books
                        var allBooks = libraryService.GetAllBooks();
                        Console.WriteLine("All Books in the Library:");
                        foreach (var b in allBooks)
                        {
                            Console.WriteLine($"{b.Title} by {b.Author} (ISBN: {b.ISBN})");
                        }
                        break;

                    case "9":
                        var booksToBorrow = libraryService.GetAvailableBooks();
                        if (booksToBorrow.Count == 0)
                        {
                            Console.WriteLine("No available books to borrow.");
                        }
                        else
                        {
                            Console.WriteLine("Available Books to Borrow:");
                            for (int i = 0; i < booksToBorrow.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {booksToBorrow[i].Title} by {booksToBorrow[i].Author} (ISBN: {booksToBorrow[i].ISBN})");
                            }

                            Console.WriteLine("Enter the number of the book you want to borrow:");
                            try
                            {
                                int borrowIndex = int.Parse(Console.ReadLine()) - 1;

                                if (borrowIndex < 0 && borrowIndex >= booksToBorrow.Count)
                                {
                                    Console.WriteLine("Invalid selection.");
                                    break;
                                }

                                var bookToBorrow = booksToBorrow[borrowIndex];
                                //var usersList = libraryService.GetAllUsers();
                                //Console.WriteLine("Available Users:");
                                //for (int i = 0; i < usersList.Count; i++)
                                //{
                                //    Console.WriteLine($"{usersList[i].UserId} . {usersList[i].Name})");
                                //}

                                //Console.WriteLine("Enter the UserId you want to Add the book:");
                                //int userId = int.Parse(Console.ReadLine());
                                //var userToAddBook = usersList.FirstOrDefault(x => x.UserId == userId);
                                //if (userToAddBook == null)
                                //{
                                //    Console.WriteLine("User not found.");
                                //    break;
                                //}
                                var borrowingUserId = libraryService.SelectUserFromUserList();
                                if (borrowingUserId < 0)
                                    throw new Exception("Invalid User");
                                libraryService.BorrowBook(borrowingUserId, bookToBorrow.ISBN);
                                Console.WriteLine($"{bookToBorrow.Title} Book borrowed by user {libraryService.GetUser(borrowingUserId).Name}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Invalid Selection");
                            }
                        }
                        break;

                    case "10":
                        try
                        {
                            var borrowedUserId = libraryService.SelectUserFromUserList();
                            if (borrowedUserId < 0)
                                throw new Exception("Invalid User");

                            var borrowedBooks = libraryService.GetUser(borrowedUserId).BorrowedBooks;
                            if (borrowedBooks.Count == 0)
                            {
                                Console.WriteLine("You have no books to return.");
                            }
                            else
                            {
                                Console.WriteLine("Your Borrowed Books:");
                                for (int i = 0; i < borrowedBooks.Count; i++)
                                {
                                    Console.WriteLine($"{i + 1}. {borrowedBooks[i].Title} by {borrowedBooks[i].Author} (ISBN: {borrowedBooks[i].ISBN})");
                                }

                                Console.WriteLine("Enter the number of the book you want to return:");

                                int returnIndex = int.Parse(Console.ReadLine()) - 1;

                                if (returnIndex < 0 && returnIndex >= borrowedBooks.Count)
                                {
                                    Console.WriteLine("Invalid selection.");
                                    break;
                                }
                                var bookToReturn = borrowedBooks[returnIndex];
                                libraryService.ReturnBook(borrowedUserId, bookToReturn.ISBN);
                                Console.WriteLine($"Returned {bookToReturn.Title} by {libraryService.GetUser(borrowedUserId).Name} Successfully");

                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Invalid Selection.");
                        }
                        break;

                    //case "11":
                    //    // Unregister User
                    //    libraryService.UnregisterUser(currentUser.UserId);
                    //    currentUser = null;
                    //    break;

                    case "11":
                        // Exit
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}
