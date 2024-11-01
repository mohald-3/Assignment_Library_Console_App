namespace Library_Console_App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();

            Userinterface.WelcomePhrase();

            do
            {
                Userinterface.MainMenu();
                int userChoice = UserInput.ValidateNumberInput();

                switch (userChoice)
                {
                    case 1: //Add a new book
                        library.AddBook();
                        break;

                    case 2: //Add a new author
                        library.AddAuthor();
                        break;
                    case 3: //Update existing book
                        library.EditExistingBook();
                        break;

                    case 4: //Update existing author
                        library.EditExistingAuthor();
                        break;

                    case 5: //Remove a book
                        library.RemoveBook();
                        break;

                    case 6: //Remove an author
                        library.RemoveAuthor();
                        break;

                    case 7: //View all existing books and authers
                        library.ShowAllBooksAndAuthors();
                        break;

                    case 8: //Search and filter all books

                        SearchAndFilterChoice(library);
                        break;

                    case 9: //Exit and save changes
                        // use save method to update the json file
                        Environment.Exit(0);
                        break;

                    case 10: //Exit without saving
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalide input");
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

                        break;

                    case 2: //Search by author

                        break;
                    case 3: //Categorise books by genre, author or publishing year
                        CategoriseChoice(library);
                        break;

                    case 4: //List all books according to a specific rating and above

                        break;

                    case 5: //Show all authors and their books

                        break;

                    case 9: // go back to main menu
                        proceed = false;
                        break;

                    default:
                        Console.WriteLine("Invalide input");
                        break;
                }
            } while (proceed == true);
        }

        private static void CategoriseChoice(Library library)
        {
            bool proceed = true;
            do
            {
                Userinterface.CategoriseMenu();
                int userChoice = UserInput.ValidateNumberInput();

                switch (userChoice)
                {
                    case 1: //Categorise books by genre

                        break;

                    case 2: //Categorise books by author

                        break;
                    case 3: //Categorise books by publishing year

                        break;

                    case 9: //Go back to seach and filter menu
                        proceed = false;
                        break;

                    default:
                        Console.WriteLine("Invalide input");
                        break;
                }
            } while (proceed == true);
        }
    }
}
