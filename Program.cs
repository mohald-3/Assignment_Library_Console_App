using System.Text.Json;
using Library_Console_App.Utility;
using Library_Console_App.LibrarySystem;
using static System.Reflection.Metadata.BlobBuilder;
using Library_Console_App.LibrarySystem.Models;
using System.Collections.Generic;

namespace Library_Console_App 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();

            do
            {
                Userinterface.MainMenu();
                int userChoice = UserInput.ValidateNumberInput();

                switch (userChoice)
                {
                    case 1: //Add a new book
                        library.AddItem(library.books);
                        Console.ReadKey();
                        break;

                    case 2: //Add a new author
                        library.AddItem(library.authors);
                        Console.ReadKey();
                        break;
                    case 3: //Update existing book
                        EditExistingBook(library);
                        break;

                    case 4: //Update existing author
                        EditExistingAuthor(library);
                        break;

                    case 5: //Remove a book
                        library.RemoveItem(library.books, book => book.Title, "book");
                        Console.ReadKey();
                        break;

                    case 6: //Remove an author
                        library.RemoveItem(library.authors, author => author.Name, "author");
                        Console.ReadKey();
                        break;

                    case 7: //View all existing books and authers
                        //Userinterface.ClearConsole(false);
                        library.ShowAll(library.books);
                        library.ShowAll(library.authors);
                        Console.ReadKey();
                        break;

                    case 8: //Search and filter all books
                        SearchAndFilterChoice(library); // Leads to a new menu
                        break;

                    case 9: //Exit and save changes
                        library.SaveChanges();
                        Environment.Exit(0);
                        break;

                    case 10: //Exit without saving
                        Environment.Exit(0); // Can add a warning that the changes will be lost
                        break;

                    default:
                        Console.WriteLine(" -- Invalide input -- ");
                        Console.ReadKey();
                        break;
                }
            }while (true);
        }

        private static void EditExistingBook(Library library)
        {
            Userinterface.TitleBanner();
            library.ShowAll(library.books);
            Userinterface.SeparatorLine();

            Console.WriteLine("Enter the title of the book you would like to edit:");
            string bookTitleInput = UserInput.ValidateTextInput();
            var bookToEdit = library.FindABook(bookTitleInput);

            if (bookToEdit != null)
            {
                Console.WriteLine($" -- Book found! Here are the current details: -- ");
                Console.WriteLine(bookToEdit.GetInfo());
                Userinterface.ClearConsole(true);

                bool continueEditing = true;
                do
                {
                    Userinterface.TitleBanner();
                    Console.WriteLine($"-- You currently editing the book \"{bookToEdit.Title}\" -- ");
                    Console.WriteLine(bookToEdit.GetInfo());
                    Console.WriteLine("");
                    Userinterface.EditExistingBookMenu();

                    int userChoice = UserInput.ValidateNumberInput();
                    switch (userChoice)
                    {
                        case 1: //1.Edit title
                            library.EditProperty(bookToEdit, (book, value) => book.Title = value, UserInput.ValidateTextInput, "title");
                            Console.ReadKey();
                            break;

                        case 2: //2.Edit author
                            library.EditProperty(bookToEdit, (book, value) => book.Author = value, UserInput.ValidateTextInput, "author");
                            Console.ReadKey();
                            break;

                        case 3: //3.Edit genre
                            library.EditProperty(bookToEdit, (book, value) => book.Genre = value, UserInput.ValidateTextInput, "genre");
                            Console.ReadKey();
                            break;

                        case 4: //4. Edit publishing year
                            library.EditProperty(bookToEdit, (book, value) => book.PublishingYear = value, UserInput.ValidateNumberInput, "publishing year");
                            Console.ReadKey();
                            break;

                        case 5: //5. Give a review
                            library.GiveReview(bookToEdit);
                            Console.ReadKey();
                            break;

                        case 9:
                            //9. Go back to main menu
                            Console.WriteLine(" -- Returning to the main menu -- ");
                            continueEditing = false;
                            Console.ReadKey();
                            break;

                        default:
                            Console.WriteLine(" -- Invalid option, please select a valid choice --");
                            Console.ReadKey();
                            break;
                    }
                } while (continueEditing);
            }
            else
            {
                Console.WriteLine($" -- Book with title \"{bookTitleInput}\" could not be found -- ");
                Console.ReadKey();
            }
        }

        private static void EditExistingAuthor(Library library)
        {
            Userinterface.TitleBanner();
            library.ShowAll(library.authors);
            Userinterface.SeparatorLine();

            Console.WriteLine("Enter the author you would like to edit");
            string authorNameInput = UserInput.ValidateTextInput();
            var authorToEdit = library.authors.Find(author => string.Equals(author.Name, authorNameInput, StringComparison.OrdinalIgnoreCase));

            if (authorToEdit != null)
            {
                Console.WriteLine($" -- Author found! Here are the current details: -- ");
                Console.WriteLine(authorToEdit.GetInfo());
                bool continueEditing = true;

                do
                {
                    Userinterface.TitleBanner();
                    Console.WriteLine($"-- You currently editing the book \"{authorToEdit.Name}\" -- ");
                    Console.WriteLine(authorToEdit.GetInfo());
                    Console.WriteLine("");
                    Userinterface.EditExistingAuthorMenu();
                    int userChoice = UserInput.ValidateNumberInput();

                    switch (userChoice)
                    {
                        case 1:
                            //1. Edit Authors name
                            library.EditProperty(authorToEdit, (author, value) => author.Name = value, UserInput.ValidateTextInput, "name");
                            Console.ReadKey();
                            break;

                        case 2:
                            //2. Edit Country
                            library.EditProperty(authorToEdit, (author, value) => author.Country = value, UserInput.ValidateTextInput, "country");
                            Console.ReadKey();
                            break;

                        case 9: //9. Go back to main menu
                            Console.WriteLine(" -- Returning to the main menu -- ");
                            continueEditing = false;
                            Console.ReadKey();
                            break;

                        default:
                            Console.WriteLine(" -- Invalid selection. Please enter a valid option -- ");
                            continueEditing = true;
                            Console.ReadKey();
                            break;
                    }
                } while (continueEditing);
            }
            else
            {
                Console.WriteLine($" -- No author found with the name \"{authorToEdit}\". Please check the name and try again -- ");
                Console.ReadKey();
            }
        }

        private static void SearchAndFilterChoice(Library library)
        {
            bool proceed = true;
            do
            {
                Userinterface.SeachAndFilterMenu();
                int userChoice = UserInput.ValidateNumberInput();

                switch (userChoice)
                {
                    case 1: //Search by book title
                        library.SeachBookByTitle();
                        Console.ReadKey();
                        break;

                    case 2: //Search by author
                        library.SeachAuthorByName();
                        Console.ReadKey();
                        break;

                    case 3: //Filter books by genre, author or publishing year
                        FilterChoice(library); // leads to a new menu
                        break;

                    case 4: //Sort books by title, author or publishing year
                        SortChoice(library);
                        break;

                    case 5://List all books according to a specific rating and above
                        library.ListAllBooksAccordingToThreshhold();
                        Console.ReadKey();
                        break;

                    case 6: //Show all authors and their books
                        library.ListAllAuthorsAndTheirBooks();
                        Console.ReadKey();
                        break;

                    case 9: // go back to main menu
                        proceed = false;
                        Console.WriteLine(" -- Returning to the main menu -- ");
                        Console.ReadKey();
                        break;

                    default:
                        Console.WriteLine(" -- Invalide input -- ");
                        Console.ReadKey();
                        break;
                }
            } while (proceed == true);
        }

        private static void FilterChoice(Library library)
        {
            bool proceed = true;
            do
            {
                Userinterface.FilterMenu();
                int userChoice = UserInput.ValidateNumberInput();

                switch (userChoice)
                {
                    case 1: //Filter books by genre
                        library.FilterBooksByGenre();
                        Console.ReadKey();
                        break;

                    case 2: //Filter books by author
                        library.FilterBooksByAuthor();
                        Console.ReadKey();
                        break;

                    case 3: //Filter books by publishing year
                        library.FilterBooksByPublishingYear();
                        Console.ReadKey();
                        break;

                    case 4: // Filter Books By Title Contains
                        library.FilterBooksByTitleContains();
                        Console.ReadKey();
                        break;

                        case 5: // Filter Books By Publishing Year Range
                        library.FilterBooksByPublishingYearRange();
                        Console.ReadKey();
                        break;
                    case 9: //Go back to seach and filter menu
                        Console.WriteLine(" -- Returning to seach and filter menu -- ");
                        proceed = false;
                        Console.ReadKey();
                        break;

                    default:
                        Console.WriteLine(" -- Invalide input -- ");
                        Console.ReadKey();
                        break;
                }
            } while (proceed == true);
        }

        private static void SortChoice(Library library)
        {
            bool proceed = true;
            do
            {
                Userinterface.SortMenu();
                int userChoice = UserInput.ValidateNumberInput();

                switch (userChoice)
                {
                    case 1: //Sort books by title
                        library.SortItems(library.books, book => book.Title, "Below are the books organized by title alphabetically.");
                        Console.ReadKey();
                        break;

                    case 2: //Sort books by author
                        library.SortItems(library.books, book => book.Author, "Below are the books organized by their respective authors.");
                        Console.ReadKey();
                        break;
                    case 3: //Categorise books by publishing year
                        library.SortItems(library.books, book => book.PublishingYear, "Below are the books organized by their respective publishing year.");
                        Console.ReadKey();
                        break;

                    case 9: //Go back to seach and filter menu
                        Console.WriteLine(" -- Returning to seach and filter menu -- ");
                        proceed = false;
                        Console.ReadKey();
                        break;

                    default:
                        Console.WriteLine(" -- Invalide input -- ");
                        Console.ReadKey();
                        break;
                }
            } while (proceed == true);
        }
    }
}
