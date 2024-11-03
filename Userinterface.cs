

namespace Library_Console_App
{
    public static class Userinterface
    {

        public static void TitleBanner()
        {
            Console.WriteLine("-------------- the Grand Oak Library --------------");
            Console.WriteLine(" Where Stories Come Alive and Knowledge Flourishes");
            SeparatorLine();
            Console.WriteLine("");
        }

        //Start Menu
        public static void MainMenu()
        {
            TitleBanner();
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
            TitleBanner();
            Console.WriteLine("Please choose from the following:");
            SeparatorLine();
            Console.WriteLine("1. Search by book title");
            Console.WriteLine("2. Search by author");
            Console.WriteLine("3. Filter books by genre, author or publishing year");
            Console.WriteLine("4. Sort books by title, author or publishing year");
            Console.WriteLine("5. List all books according to a specific rating and above");
            Console.WriteLine("6. Show all authors and their books");
            Console.WriteLine("9. Go back to main menu");
        }

        public static void FilterMenu()
        {
            TitleBanner();
            Console.WriteLine("Please choose from the following:");
            SeparatorLine();
            Console.WriteLine("1. Filter books by genre");
            Console.WriteLine("2. Filter books by author");
            Console.WriteLine("3. Filter books by publishing year");
            Console.WriteLine("9. Go back to seach and filter menu");

        }

        public static void SortMenu()
        {
            TitleBanner();
            Console.WriteLine("Please choose from the following:");
            SeparatorLine();
            Console.WriteLine("1. Sort books by title");
            Console.WriteLine("2. Sort books by author");
            Console.WriteLine("3. Sort books by publishing year");
            Console.WriteLine("9. Go back to seach and filter menu");

        }

        public static void EditExistingBookMenu()
        {
            TitleBanner();
            Console.WriteLine("Choose an option to edit the book:");
            Console.WriteLine("1. Edit title");
            Console.WriteLine("2. Edit author");
            Console.WriteLine("3. Edit genre");
            Console.WriteLine("4. Edit publishing year");
            Console.WriteLine("5. Add a review");

            Console.WriteLine("9. Go back to main menu");
        }
        public static void EditExistingAuthorMenu()
        {
            TitleBanner();
            Console.WriteLine("What would you like to edit?");
            Console.WriteLine("1. Edit Authors name");
            Console.WriteLine("2. Edit Country");
            Console.WriteLine("9. Go back to main menu");
        }

        //End Menu

        public static void SeparatorLine()
        {
            Console.WriteLine("---------------------------------------------------");
        }

        public static void ClearConsole(bool withkey)
        {
            Console.WriteLine("Press any key to continue");
            if (withkey)
            {
                Console.ReadKey();
            }
            Console.Clear();
        }
    }
}
