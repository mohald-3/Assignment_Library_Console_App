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
            MenuManager menuManager = new MenuManager(library);
            menuManager.DisplayMainMenu();
        }
    }
}
