using Library_Console_App.Utility;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Library_Console_App.LibrarySystem.Models
{
    public class Book : IIdentifiable, IInputStrategy
    {
        public int ID { get; set; }
        public int ISBN { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set; }
        public int PublishingYear { get; set; }
        public List<int> Rating { get; set; } = new List<int>();


        public Book(int bookid, int isbn, string title, string author, string genre, int publishingyear, List<int> rating)
        {
            ID = bookid;
            ISBN = isbn;
            Title = title;
            Author = author;
            Genre = genre;
            PublishingYear = publishingyear;
            Rating = rating;
        }

        public Book()
        {
            Rating = new List<int>();
        }

        public double GetAverageRating()
        {
            if (Rating.Count == 0)
                return 0;
            else
            {
                return Rating.Average();
            }
        }

        public string GetInfo()
        {
            return $"- Title: {Title}, Author: {Author}, Genre: {Genre}, Publishing Year: {PublishingYear}, Average Rating: {GetAverageRating():0.0}";
        }

        public void CollectInput(IDGenerator idgenerator)
        {
            ID = idgenerator.GenerateUniqueID(true);

            Console.WriteLine("Enter the book's ISBN (9 digits):");
            ISBN = UserInput.ValidateNumberInput();

            Console.WriteLine("Enter the book title:");
            Title = UserInput.ValidateTextInput();

            Console.WriteLine("Enter the book's author:");
            Author = UserInput.ValidateTextInput();

            Console.WriteLine("Enter the genre of the book:");
            Genre = UserInput.ValidateTextInput();

            Console.WriteLine("Enter the publishing year of the book:");
            PublishingYear = UserInput.ValidateNumberInput();

            Rating = new List<int>(); // Initialize Rating as an empty list for now

            Console.WriteLine("Enter a rating (1-5):");
            int rateInput = UserInput.ValidateRateInput();
            Rating.Add(rateInput);
        }

    }
}
