using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Library_Console_App
{
    public class Library
    {
        public List<Book> books;
        public List<Author> authors;
        IDGenerator generator = new IDGenerator();

        public Library() 
        {
            
            //This is where i should initialize JSON file.

            books = new List<Book>() 
            { 
                new Book(generator.GenerateUniqueID(true), "Life", "Eddie", "Development", 1992, new List<int> {1, 4, 3, 5, 2, 5, 5}),
                new Book(generator.GenerateUniqueID(true), "Learn how to play", "Noobzy", "Gaming", 2022,new List<int> {5,4,5,5}),
                new Book(generator.GenerateUniqueID(true), "Cook the right way", "Eddie", "Cooking", 2015, new List<int> {1}),
                new Book(generator.GenerateUniqueID(true), "Express", "Maja", "Development", 2010, new List<int> {5, 4, 5, 5, 5, 5, 5 })
            };
            authors = new List<Author>() 
            { 
                new Author(generator.GenerateUniqueID(false), "Eddie", "Iraq"),
                new Author(generator.GenerateUniqueID(false), "Noobzy", "Sweden"),
                new Author(generator.GenerateUniqueID(false), "Maja", "Croatia")
            };
        }

        //Methods Start
         
        public void AddBook()
        {
            int isbn = generator.GenerateUniqueID(true);

            Console.WriteLine("Please type in the book title");
            string title = UserInput.ValidateTextInput();

            Console.WriteLine("Please type in book Author");
            string author = UserInput.ValidateTextInput();

            Console.WriteLine("Please type in the book genre");
            string genre = UserInput.ValidateTextInput();

            Console.WriteLine("Please type in the publishing year");
            int publishingyear = UserInput.ValidateNumberInput();

            books.Add(new Book(isbn, title, author, genre, publishingyear, []));
        }

        public void AddAuthor()
        {
            int authorid = generator.GenerateUniqueID(false);

            Console.WriteLine("Please type in the authors name");
            string name = UserInput.ValidateTextInput();

            Console.WriteLine("Please type in the authers country");
            string country = UserInput.ValidateTextInput();

            authors.Add(new Author(authorid, name, country));
        }

        public void EditExistingBook()
        {
            Console.WriteLine("Please write title you would like to edit");
            //Suggestion: List all books?
            string bookTitleInput = UserInput.ValidateTextInput();

            var bookToEdit = books.Find(book => string.Equals(book.Title, bookTitleInput, StringComparison.OrdinalIgnoreCase));

            if (bookToEdit != null)
            {
                Console.WriteLine($"Book with title {bookTitleInput} exists, here is the book information:");
                Console.WriteLine($"Titel: {bookToEdit.Title}, Author: {bookToEdit.Author}, Genre: {bookToEdit.Genre}, Publishing year: {bookToEdit.PublishingYear}, with average rating: {bookToEdit.GetAverageRating()}");

                Userinterface.EditExistingBookMenu();

                int userChoice = UserInput.ValidateNumberInput();
                bool proceed = true;
                
                do
                {
                    switch (userChoice)
                    {
                        case 1:
                            //1.Edit title
                            Console.WriteLine("Please type in the new title for the book");
                            string newTitle = UserInput.ValidateTextInput();
                            bookToEdit.Title = newTitle;
                            Console.WriteLine("Title has been updated.");
                            proceed = false;
                            break;

                        case 2:
                            //2.Edit author
                            Console.WriteLine("Please type in the new author for the book:");
                            string newAuthor = UserInput.ValidateTextInput();
                            bookToEdit.Author = newAuthor;  
                            Console.WriteLine("Author has been updated.");
                            proceed = false;
                            break;

                        case 3:
                            //3.Edit genre
                            Console.WriteLine("Please type in the new genre for the book:");
                            string newGenre = UserInput.ValidateTextInput();
                            bookToEdit.Genre = newGenre;
                            Console.WriteLine("Genre has been updated.");
                            proceed = false;
                            break;

                        case 4:
                            //4. Edit publishing year
                            Console.WriteLine("Please type in the new publishing year for the book:");
                            int newYear = UserInput.ValidateNumberInput();
                            bookToEdit.PublishingYear = newYear;
                            Console.WriteLine("Publishing year has been updated.");
                            proceed = false;
                            break;

                        case 9:
                            //9. Go back to main menu
                            Console.WriteLine("Returning to the main menu.");
                            proceed = false;
                            break;

                        default:
                            Console.WriteLine("Invalid input, please try again");
                            proceed = true;
                            break;
                    }
                } while (proceed);
            }
            else
            {
                Console.WriteLine($"Book with title {bookTitleInput} could not be found");
            }
        }

        public void EditExistingAuthor()
        {
            Console.WriteLine("Please write author you would like to edit");
            //Suggestion: List all authors?
            string authorTitleInput = UserInput.ValidateTextInput();

            var authorToEdit = authors.Find(author => string.Equals(author.Name, authorTitleInput, StringComparison.OrdinalIgnoreCase));

            if (authorToEdit != null)
            {
                Console.WriteLine($"Author with name {authorTitleInput} exists, here is the authors information:");
                Console.WriteLine($"Authors name: {authorToEdit.Name}, County: {authorToEdit.Country}");

                Userinterface.EditExistingAuthorMenu();

                int userChoice = UserInput.ValidateNumberInput();
                bool proceed = true;

                do
                {
                    switch (userChoice)
                    {
                        case 1:
                            //1. Edit Authors name
                            Console.WriteLine("Please type in the new name for the authors");
                            string newName = UserInput.ValidateTextInput();
                            authorToEdit.Name = newName;
                            Console.WriteLine("Authors name has been updated.");
                            proceed = false;
                            break;

                        case 2:
                            //2. Edit Country
                            Console.WriteLine("Please type in the new author for the book:");
                            string newCountry = UserInput.ValidateTextInput();
                            authorToEdit.Country = newCountry;
                            Console.WriteLine("Author country has been updated.");
                            proceed = false;
                            break;

                        case 9:
                            //9. Go back to main menu
                            Console.WriteLine("Returning to the main menu.");
                            proceed = false;
                            break;

                        default:
                            Console.WriteLine("Invalid input, please try again");
                            proceed = true;
                            break;
                    }
                } while (proceed);
            }
            else
            {
                Console.WriteLine($"Author with the name {authorToEdit} could not be found");
                return;
            }
        }

        public void RemoveBook()
        {
            Console.WriteLine("Please write title you would like to remove");
            //Suggestion: List all books?
            string titleToRemove = UserInput.ValidateTextInput();

            var bookToRemove = books.Find(book => string.Equals(book.Title, titleToRemove, StringComparison.OrdinalIgnoreCase));

            if (bookToRemove != null)
            {
                books.Remove(bookToRemove);
                Console.WriteLine($"Book with title {bookToRemove} have been removed");
            }
            else
            {
                Console.WriteLine($"Book with title {titleToRemove} could not be found");
            }
        }

        public void RemoveAuthor()
        {
            Console.WriteLine("Please type in the author you would like to remove");
            //Suggestion: List all books?
            string nameOfAuthorToRemove = UserInput.ValidateTextInput();

            var authorToRemove = books.Find(author => string.Equals(author.Author, nameOfAuthorToRemove, StringComparison.OrdinalIgnoreCase));

            if (authorToRemove != null)
            {
                books.Remove(authorToRemove);
                Console.WriteLine($"Author with the name {nameOfAuthorToRemove} have been removed");
            }
            else
            {
                Console.WriteLine($"Book with title {nameOfAuthorToRemove} could not be found");
            }
        }

        public void ShowAllBooksAndAuthors()
        {
            Console.WriteLine("Here is the list of books");
            books.ForEach(book => Console.WriteLine
            (
                $"Titel: {book.Title}, " +
                $"Author: {book.Author}, " +
                $"Genre: {book.Genre}, " +
                $"Publishing year: {book.PublishingYear}, " +
                $"average rating: {book.GetAverageRating():0.0}"
            ));
            Userinterface.SeparatorLine();

            Console.WriteLine("Here is the list of authors");
            authors.ForEach(author => Console.WriteLine
            (
                $"Titel: {author.Name}, " +
                $"Author: {author.Country}"
            ));
        }


    }
        // Methods Ends
    
}
