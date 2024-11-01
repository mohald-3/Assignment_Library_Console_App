

namespace Library_Console_App
{
    public static class Userinterface
    {
        // Main Menu Start
        public static void WelcomePhrase()
        {
            Console.WriteLine("Welcome to the Grand Oak Library System!");
            Console.WriteLine("Where Stories Come Alive and Knowledge Flourishes.");
            SeparatorLine();
        }
        public static void MainMenu()
        {
            Console.WriteLine("How can we help you today?");
            SeparatorLine();
            Console.WriteLine("1. Add a new book");
            Console.WriteLine("2. Add a new author");
            Console.WriteLine("3. Edit existing book");
            Console.WriteLine("4. Edit existing author");
            Console.WriteLine("5. Remove a book");
            Console.WriteLine("6. Remove an author");
            Console.WriteLine("7. View all existing books and authers");
            Console.WriteLine("8. Search and filter all books");
            Console.WriteLine("9. Exit and save changes");
            Console.WriteLine("10. Exit without saving");
        }

        public static void SeachAndFilterMenu()
        {
            Console.WriteLine("Please choose from the following:");
            SeparatorLine();
            Console.WriteLine("1. Search by book title"); // this can be used method for 3. Edit existing book in main menu
            Console.WriteLine("2. Search by author"); // this can be used method for 4. Edit existing author in main menu
            Console.WriteLine("3. Categorise books by genre, author or publishing year");
            Console.WriteLine("4. List all books according to a specific rating and above");
            Console.WriteLine("5. Show all authors and their books");
            Console.WriteLine("9. Go back to main menu");
        }

        public static void CategoriseMenu()
        {
            Console.WriteLine("Please choose from the following:");
            SeparatorLine();
            Console.WriteLine("1. Categorise books by genre");
            Console.WriteLine("2. Categorise books by author");
            Console.WriteLine("3. Categorise books by publishing year");
            Console.WriteLine("8. Go back to seach and filter menu");
            Console.WriteLine("9. Go back to main menu");

        }

        // Main Menu Ends

        //Library start

        public static void EditExistingBookMenu()
        {
            Console.WriteLine("What would you like to edit?");
            Console.WriteLine("1. Edit title");
            Console.WriteLine("2. Edit author");
            Console.WriteLine("3. Edit genre");
            Console.WriteLine("4. Edit publishing year");
            Console.WriteLine("9. Go back to main menu");
        }
        public static void EditExistingAuthorMenu()
        {
            Console.WriteLine("What would you like to edit?");
            Console.WriteLine("1. Edit Authors name");
            Console.WriteLine("2. Edit Country");
            Console.WriteLine("9. Go back to main menu");
        }


        //Library Ends

        public static void SeparatorLine()
        {
            Console.WriteLine("---------------------------------------------------------------------");
        }
    }
}
