using System.Text.Json;

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
                        library.AddBook();
                        Userinterface.ClearConsole(true);
                        break;

                    case 2: //Add a new author
                        library.AddAuthor();
                        Userinterface.ClearConsole(true);
                        break;
                    case 3: //Update existing book
                        Userinterface.ClearConsole(false);
                        library.EditExistingBook();
                        Userinterface.ClearConsole(true);
                        break;

                    case 4: //Update existing author
                        Userinterface.ClearConsole(false);
                        library.EditExistingAuthor();
                        Userinterface.ClearConsole(true);
                        break;

                    case 5: //Remove a book
                        library.RemoveBook();
                        Userinterface.ClearConsole(true);
                        break;

                    case 6: //Remove an author
                        library.RemoveAuthor();
                        Userinterface.ClearConsole(true);
                        break;

                    case 7: //View all existing books and authers
                        Userinterface.ClearConsole(false);
                        library.ShowAllBooks();
                        library.ShowAllAuthors();
                        Userinterface.ClearConsole(true);
                        break;

                    case 8: //Search and filter all books
                        Userinterface.ClearConsole(false);
                        SearchAndFilterChoice(library); // Leads to a new menu
                        break;

                    case 9: //Exit and save changes
                        Environment.Exit(0);
                        break;

                    case 10: //Exit without saving
                        Environment.Exit(0); // Can add a warning that the changes will be lost
                        break;

                    default:
                        Console.WriteLine("Invalide input");
                        Userinterface.ClearConsole(true);
                        break;
                }
            }while (true);
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
                        Userinterface.ClearConsole(true);
                        break;

                    case 2: //Search by author
                        library.SeachAuthorByName();
                        Userinterface.ClearConsole(true);
                        break;

                    case 3: //Filter books by genre, author or publishing year
                        Userinterface.ClearConsole(false);
                        FilterChoice(library); // leads to a new menu
                        break;

                    case 4: //Sort books by title, author or publishing year
                        Userinterface.ClearConsole(false);
                        SortChoice(library);
                        break;

                    case 5://List all books according to a specific rating and above
                        library.ListAllBooksAccordingToThreshhold();
                        Userinterface.ClearConsole(true);
                        break;

                    case 6: //Show all authors and their books
                        library.ListAllAuthorsAndTheirBooks();
                        Userinterface.ClearConsole(true);
                        break;

                    case 9: // go back to main menu
                        proceed = false;
                        Userinterface.ClearConsole(false);
                        break;

                    default:
                        Console.WriteLine("Invalide input");
                        Userinterface.ClearConsole(true);
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
                        Userinterface.ClearConsole(true);
                        break;

                    case 2: //Filter books by author
                        library.FilterBooksByAuthor();
                        Userinterface.ClearConsole(true);
                        break;

                    case 3: //Filter books by publishing year
                        library.FilterBooksByPublishingYear();
                        Userinterface.ClearConsole(true);
                        break;

                    case 9: //Go back to seach and filter menu
                        proceed = false;
                        Userinterface.ClearConsole(false);
                        break;

                    default:
                        Console.WriteLine("Invalide input");
                        Userinterface.ClearConsole(true);
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
                        library.SortBooksByTitle();
                        Userinterface.ClearConsole(true);
                        break;

                    case 2: //Sort books by author
                        library.SortBooksByAuthor();
                        Userinterface.ClearConsole(true);
                        break;
                    case 3: //Categorise books by publishing year
                        library.SortBooksByPublishingYear();
                        Userinterface.ClearConsole(true);
                        break;

                    case 9: //Go back to seach and filter menu
                        proceed = false;
                        Userinterface.ClearConsole(false);
                        break;

                    default:
                        Console.WriteLine("Invalide input");
                        Userinterface.ClearConsole(true);
                        break;
                }
            } while (proceed == true);
        }
    }
}
