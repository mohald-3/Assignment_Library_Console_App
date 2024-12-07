using Library_Console_App.Utility;
using System.Reflection.Emit;

namespace Library_Console_App.LibrarySystem.Models
{
    public class Author : IIdentifiable, IInputStrategy
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public Author(int authorid, string name, string country)
        {
            ID = authorid;
            Name = name;
            Country = country;
        }

        public Author()
        {
        }

        public string GetInfo()
        {
            return $"- Author: {Name}, Country: {Country}";
        }

        public void CollectInput(IDGenerator idgenerator)
        {
            ID = idgenerator.GenerateUniqueID(false);

            Console.WriteLine("Enter the author's name:");
            Name = UserInput.ValidateTextInput();

            Console.WriteLine("Enter the author's country of origin:");
            Country = UserInput.ValidateTextInput();
        }
    }
}
