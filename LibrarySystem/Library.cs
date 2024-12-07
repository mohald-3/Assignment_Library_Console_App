using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Library_Console_App.LibrarySystem.Models;
using Library_Console_App.Utility;

namespace Library_Console_App.LibrarySystem
{
    public class Library
    {
        string DataJSONfilePath = "LibrarySystem\\Output\\LibraryData.json";
        public List<Book> books;
        public List<Author> authors;
        IDGenerator idgenerator = new IDGenerator();

        public Library()
        {
            // Importing data from a JSON file.
            string AllDataFromJSON = File.ReadAllText(DataJSONfilePath);
            ImportedDB ImportedDB = JsonSerializer.Deserialize<ImportedDB>(AllDataFromJSON)!;

            // Seperating importedDB into different collections.
            books = ImportedDB!.AllBooksFromDB;
            authors = ImportedDB.AllAuthorsFromDB;

            books.ForEach(book => idgenerator.usedBookIDs.Add(book.ID));
            authors.ForEach(author => idgenerator.usedAuthorIDs.Add(author.ID));
        }

        // Start library methods 

        public void AddItem<T>(List<T> collection) where T : IIdentifiable, IInputStrategy, new()
        {
            T itemToAdd = new T();

            // Use the CollectInput method specific to each type
            itemToAdd.CollectInput(idgenerator);

            collection.Add(itemToAdd);
            Console.WriteLine($" -- {itemToAdd.GetType().Name} added successfully! -- ");
            Console.WriteLine(itemToAdd.GetInfo());

        }

        public void EditProperty<T, TProperty>(T item, Action<T, TProperty> propertySetter, Func<TProperty> getUserInput, string propertyName) where T : IIdentifiable, IInputStrategy, new()
        {
            Console.WriteLine($"Enter the new {propertyName}:");
            TProperty newValue = getUserInput();
            propertySetter(item, newValue);
            Console.WriteLine($" -- {propertyName} updated successfully! -- ");
            Console.WriteLine(item?.GetInfo());
        }

        public void GiveReview(Book? bookToEdit)
        {
            Console.WriteLine("Enter a rating between 1 and 5:");
            int ratingInput = UserInput.ValidateRateInput();
            bookToEdit.Rating.Add(ratingInput);
            Console.WriteLine($" -- Rating added successfully! -- ");
        }

        public void RemoveItem<T>(List<T> items, Func<T, string> propertySelector, string itemType)
        {
            Console.WriteLine($"Enter the {itemType} you would like to remove:");
            string itemToRemove = UserInput.ValidateTextInput();

            var item = items.Find(i => string.Equals(propertySelector(i), itemToRemove, StringComparison.OrdinalIgnoreCase));

            if (item != null)
            {
                items.Remove(item);
                Console.WriteLine($" -- The {itemType} \"{itemToRemove}\" has been successfully removed from the library. -- ");
            }
            else
            {
                Console.WriteLine($" -- No {itemType} with the name \"{itemToRemove}\" was found. Please check the name and try again. -- ");
            }
        }

        public void ShowAll<T>(List<T> ListToPrint)
        {
            Userinterface.SeparatorLine();
            Console.WriteLine($" -- List of Available {typeof(T).Name}s -- ");
            Userinterface.SeparatorLine();
            PrintInfo(books);
        }

        public void SeachBookByTitle()
        {
            Console.WriteLine("Enter the book title you're looking for:");
            string bookTitleInput = UserInput.ValidateTextInput();

            var bookToEdit = books.Find(book => string.Equals(book.Title, bookTitleInput, StringComparison.OrdinalIgnoreCase));

            if (bookToEdit != null)
            {
                Console.WriteLine($" -- Book found! -- ");
                Console.WriteLine(bookToEdit.GetInfo());
            }
            else
            {
                Console.WriteLine($" -- Book titled \"{bookTitleInput}\" could not be found. -- ");
            }
        }

        public void SeachAuthorByName()
        {
            Console.WriteLine("Enter the name of the author you're looking for:");
            string authorNameInput = UserInput.ValidateTextInput();
            var authorToEdit = authors.Find(author => string.Equals(author.Name, authorNameInput, StringComparison.OrdinalIgnoreCase));

            if (authorToEdit != null)
            {
                Console.WriteLine($" -- Author Found! -- ");
                Console.WriteLine(authorToEdit.GetInfo());
            }
            else
            {
                Console.WriteLine($" -- Author named \"{authorNameInput}\" could not be found. -- ");
                return;
            }
        }

        public void ListAllBooksAccordingToThreshhold()
        {
            Console.Write("Enter a rating threshold (1-5): ");
            int thresholdRating = UserInput.ValidateRateInput();

            List<Book> booksWithThresholdRatingAndAbove = books
                .Where(book => book.GetAverageRating() > thresholdRating)
                .ToList();
            if (booksWithThresholdRatingAndAbove.Count > 0)
            {
                Console.WriteLine($" -- Books with an average rating of {thresholdRating} and above: -- ");
                PrintInfo(booksWithThresholdRatingAndAbove);
            }
            else
            {
                Console.WriteLine($" -- No books found with an average rating of {thresholdRating} or above. -- ");
            }
        }

        public void ListAllAuthorsAndTheirBooks()
        {
            var groupedBooksByAuthor = books.GroupBy(book => book.Author);

            if (groupedBooksByAuthor.Count() > 0)
            {
                foreach (var group in groupedBooksByAuthor)
                {
                    Console.WriteLine($" -- Author: {group.Key} -- ");
                    Userinterface.SeparatorLine();

                    foreach (Book book in group)
                    {
                        Console.WriteLine(book.GetInfo());
                    }
                    Console.WriteLine("");
                }
            }
            else
            {
                Console.WriteLine(" -- No authors found in the library. -- ");
                return;
            }

        }

        public void FilterItemsWithPredicate<T>(List<T> collection, Func<T, bool> predicate, string message) where T : IIdentifiable
        {
            var filteredItems = collection.Where(predicate).ToList();

            if (filteredItems.Count > 0)
            {
                Console.WriteLine($" -- {message} -- ");
                PrintInfo(filteredItems);
            }
            else
            {
                Console.WriteLine(" -- No items found matching the filter. -- ");
            }
        }

        public void FilterBooksByGenre()
        {
            Console.WriteLine("Enter the genre you would like to filter by:");
            string genreToFilter = UserInput.ValidateTextInput();

            FilterItemsWithPredicate(books, book => book.Genre.Equals(genreToFilter, StringComparison.OrdinalIgnoreCase),
                $"There are books in the '{genreToFilter}' genre.");
        }

        public void FilterBooksByAuthor()
        {
            Console.WriteLine("Enter the name of the author you would like to filter by:");
            string authorToFilter = UserInput.ValidateTextInput();

            FilterItemsWithPredicate(books, book => book.Author.Equals(authorToFilter, StringComparison.OrdinalIgnoreCase),
                $"There are books written by the author '{authorToFilter}':");
        }

        public void FilterBooksByPublishingYear()
        {
            Console.WriteLine("Enter the publishing year you would like to filter by:");
            int publishingYearToFilter = UserInput.ValidateNumberInput();

            FilterItemsWithPredicate(books, book => book.PublishingYear == publishingYearToFilter,
                $"There are books published in the year {publishingYearToFilter}:");
        }

        public void FilterBooksByTitleContains()
        {
            Console.WriteLine("Enter a part of the title you would like to filter by:");
            string titleKeyword = UserInput.ValidateTextInput();

            FilterItemsWithPredicate(books, book => book.Title.Contains(titleKeyword, StringComparison.OrdinalIgnoreCase),
                $"There are books with titles containing '{titleKeyword}':");
        }

        public void FilterBooksByPublishingYearRange()
        {
            Console.WriteLine("Enter the start publishing year:");
            int startYear = UserInput.ValidateNumberInput();

            Console.WriteLine("Enter the end publishing year:");
            int endYear = UserInput.ValidateNumberInput();

            FilterItemsWithPredicate(books, book => book.PublishingYear >= startYear && book.PublishingYear <= endYear,
                $"There are books published between {startYear} and {endYear}:");
        }

        public void SortItems<T>(List<T> collection, Func<T, object> keySelector, string message) where T : IIdentifiable
        {
            var sortedItems = collection.OrderBy(keySelector).ToList();

            Console.WriteLine($" -- {message} -- ");
            PrintInfo(sortedItems);
        }

        // End library methods

        // Start Print helping methods 

        public void PrintInfo<T>(List<T> listToPrint) where T : IIdentifiable
        {
            foreach (var item in listToPrint)
            {
                Console.WriteLine(item.GetInfo());
            }
        }

        public Book FindABook(string? bookTitleInput)
        {
            return books.Find(book => string.Equals(book.Title, bookTitleInput, StringComparison.OrdinalIgnoreCase));
        }

        // End Print helping functions

        //Start JSON methods
        public void SaveChanges() // JSON file export 
        {
            ImportedDB updatedTemporaryDB = new ImportedDB
            {
                AllBooksFromDB = books,
                AllAuthorsFromDB = authors
            };

            // Serialize the data back to JSON format
            string json = JsonSerializer.Serialize(updatedTemporaryDB, new JsonSerializerOptions { WriteIndented = true });

            // Write the JSON string to the file
            File.WriteAllText(DataJSONfilePath, json);
        }
    }
}
