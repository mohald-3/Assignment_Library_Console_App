using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Library_Console_App
{
    public class Library
    {
        string DataJSONfilePath = "LibraryData.json";
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

            books.ForEach(book => idgenerator.usedBookIDs.Add(book.BookID));
            authors.ForEach(author => idgenerator.usedAuthorIDs.Add(author.AuthorID));
        }

        //Start Methods 
         
        public void AddBook()
        {
            int bookid = idgenerator.GenerateUniqueID(true);

            Console.WriteLine("Enter the book's ISBN (9 digits):");
            int isbn = UserInput.ValidateNumberInput();

            Console.WriteLine("Enter the book title:");
            string title = UserInput.ValidateTextInput();

            Console.WriteLine("Enter the book's author:");
            string author = UserInput.ValidateTextInput();

            Console.WriteLine("Enter the genre of the book:");
            string genre = UserInput.ValidateTextInput();

            Console.WriteLine("Enter the publishing year of the book:");
            int publishingyear = UserInput.ValidateNumberInput();

            books.Add(new Book(bookid, isbn, title, author, genre, publishingyear, []));
            Console.WriteLine($" -- Book added successfully! -- ");
            Console.WriteLine($"- Title: {title}, Author: {author}, Genre: {genre}, Published: {publishingyear} -");
        }

        public void AddAuthor()
        {
            int authorid = idgenerator.GenerateUniqueID(false);

            Console.WriteLine("Enter the author's name:");
            string name = UserInput.ValidateTextInput();

            Console.WriteLine("Enter the author's country of origin:");
            string country = UserInput.ValidateTextInput();

            authors.Add(new Author(authorid, name, country));
            Console.WriteLine($" -- Author added successfully! -- ");
            Console.WriteLine($"- Name: {name}, Country: {country} -");
        }

        public void EditExistingBook()
        {
            ShowAllBooks();
            Userinterface.SeparatorLine();

            Console.WriteLine("Enter the title of the book you would like to edit:");
            string bookTitleInput = UserInput.ValidateTextInput();

            var bookToEdit = books.Find(book => string.Equals(book.Title, bookTitleInput, StringComparison.OrdinalIgnoreCase));

            if (bookToEdit != null)
            {
                Console.WriteLine($" -- Book found! Here are the current details: -- ");
                PrintSingleBookInfo(bookToEdit);

                bool continueEditing = true;
                do
                {
                    Userinterface.EditExistingBookMenu();
                    int userChoice = UserInput.ValidateNumberInput();

                    switch (userChoice)
                    {
                        case 1:
                            //1.Edit title
                            Console.WriteLine("Enter the new author for the book:");
                            string newTitle = UserInput.ValidateTextInput();
                            bookToEdit.Title = newTitle;
                            Console.WriteLine(" -- Title updated successfully! -- ");
                            PrintSingleBookInfo(bookToEdit);
                            Userinterface.ClearConsole(true);
                            break;

                        case 2:
                            //2.Edit author
                            Console.WriteLine("Please type in the new author for the book:");
                            string newAuthor = UserInput.ValidateTextInput();
                            bookToEdit.Author = newAuthor;
                            Console.WriteLine(" -- Author updated successfully! -- ");
                            Userinterface.ClearConsole(true);
                            break;

                        case 3:
                            //3.Edit genre
                            Console.WriteLine("Enter the new genre for the book:");
                            string newGenre = UserInput.ValidateTextInput();
                            bookToEdit.Genre = newGenre;
                            Console.WriteLine(" -- Genre updated successfully! -- ");
                            PrintSingleBookInfo(bookToEdit);
                            Userinterface.ClearConsole(true);
                            break;

                        case 4:
                            //4. Edit publishing year
                            Console.WriteLine("Enter the new publishing year for the book:");
                            int newYear = UserInput.ValidateNumberInput();
                            bookToEdit.PublishingYear = newYear;
                            Console.WriteLine(" -- Genre updated successfully! -- ");
                            PrintSingleBookInfo(bookToEdit);
                            Userinterface.ClearConsole(true);
                            break;

                        case 5:
                            //5. Give a review
                            int ratingInput;
                            do
                            {
                                Console.WriteLine("Enter a rating for the book (1 to 5):");
                                ratingInput = UserInput.ValidateNumberInput();

                            } while (ratingInput < 1 || ratingInput > 5);

                            bookToEdit.Rating.Add(ratingInput);
                            Console.WriteLine($" -- Rating added successfully. New average rating: {bookToEdit.GetAverageRating():0.0}! -- ");
                            PrintSingleBookInfo(bookToEdit);
                            Userinterface.ClearConsole(true);
                            break;

                        case 9:
                            //9. Go back to main menu
                            Console.WriteLine(" -- Returning to the main menu. -- ");
                            continueEditing = false;
                            Userinterface.ClearConsole(false);
                            break;

                        default:
                            Console.WriteLine("Invalid option, please select a valid choice");
                            Userinterface.ClearConsole(true);
                            break;
                    }
                } while (continueEditing);
            }
            else
            {
                Console.WriteLine($" -- Book with title \"{bookTitleInput}\" could not be found -- ");
            }
        }

        public void EditExistingAuthor()
        {
            ShowAllAuthors();
            Console.WriteLine("Please write author you would like to edit");
            string authorNameInput = UserInput.ValidateTextInput();

            var authorToEdit = authors.Find(author => string.Equals(author.Name, authorNameInput, StringComparison.OrdinalIgnoreCase));

            if (authorToEdit != null)
            {
                Console.WriteLine($" -- Author found! -- ");
                PrintSingleAuthorInfo(authorToEdit);

                bool continueEditing = true;
                do
                {
                    Userinterface.EditExistingAuthorMenu();
                    int userChoice = UserInput.ValidateNumberInput();

                    switch (userChoice)
                    {
                        case 1:
                            //1. Edit Authors name
                            Console.WriteLine("Enter the new name for the author:");
                            string newName = UserInput.ValidateTextInput();
                            authorToEdit.Name = newName;
                            Console.WriteLine(" -- Author's name has been successfully updated! -- ");
                            PrintSingleAuthorInfo(authorToEdit);
                            Userinterface.ClearConsole(true);
                            break;

                        case 2:
                            //2. Edit Country
                            Console.WriteLine("Enter the new country for the author:");
                            string newCountry = UserInput.ValidateTextInput();
                            authorToEdit.Country = newCountry;
                            Console.WriteLine(" -- Author's country has been successfully updated! -- ");
                            PrintSingleAuthorInfo(authorToEdit);
                            Userinterface.ClearConsole(true);
                            break;

                        case 9:
                            //9. Go back to main menu
                            Console.WriteLine(" -- Returning to the main menu. -- ");
                            continueEditing = false;
                            Userinterface.ClearConsole(false);
                            break;

                        default:
                            Console.WriteLine(" -- Invalid selection. Please enter a valid option. -- ");
                            continueEditing = true;
                            Userinterface.ClearConsole(true);

                            break;
                    }
                } while (continueEditing);
            }
            else
            {
                Console.WriteLine($" -- No author found with the name \"{authorToEdit}\". Please check the name and try again. -- ");
                return;
            }
        }

        public void RemoveBook()
        {
            Console.WriteLine("Enter the title of the book you would like to remove:");
            string titleToRemove = UserInput.ValidateTextInput();

            var bookToRemove = books.Find(book => string.Equals(book.Title, titleToRemove, StringComparison.OrdinalIgnoreCase));

            if (bookToRemove != null)
            {
                books.Remove(bookToRemove);
                Console.WriteLine($" -- The book titled \"{bookToRemove.Title}\" has been successfully removed from the library. -- ");
            }
            else
            {
                Console.WriteLine($" -- No book with the title \"{titleToRemove}\" was found. Please check the title and try again. -- ");
            }
        }

        public void RemoveAuthor()
        {
            Console.WriteLine("Enter the name of the author you would like to remove:");
            string nameOfAuthorToRemove = UserInput.ValidateTextInput();

            var authorToRemove = books.Find(author => string.Equals(author.Author, nameOfAuthorToRemove, StringComparison.OrdinalIgnoreCase));

            if (authorToRemove != null)
            {
                books.Remove(authorToRemove);
                Console.WriteLine($" -- Author \"{nameOfAuthorToRemove}\" has been successfully removed from the library. -- ");
            }
            else
            {
                Console.WriteLine($" -- No author with the name \"{nameOfAuthorToRemove}\" was found. Please check the name and try again. -- ");
            }
        }

        public void ShowAllBooks()
        {
            Userinterface.SeparatorLine();
            Console.WriteLine(" -- List of Available Books -- ");
            Userinterface.SeparatorLine();
            PrintBooksInfo(books);
        }

        public void ShowAllAuthors()
        {
            Userinterface.SeparatorLine();
            Console.WriteLine(" -- List of Available Authors -- ");
            Userinterface.SeparatorLine();
            PrintAuthorsInfo(authors);
        }

        public void SeachBookByTitle()
        {
            Console.WriteLine("Enter the book title you're looking for:");
            string bookTitleInput = UserInput.ValidateTextInput();

            var bookToEdit = books.Find(book => string.Equals(book.Title, bookTitleInput, StringComparison.OrdinalIgnoreCase));

            if (bookToEdit != null)
            {
                Console.WriteLine($" -- Book found! -- ");
                PrintSingleBookInfo(bookToEdit);
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
                PrintSingleAuthorInfo(authorToEdit);
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
            double thresholdRating = UserInput.ValidateDoubleInput();

            List<Book> booksWithThresholdRatingAndAbove = books
                .Where(book => book.GetAverageRating() > thresholdRating)
                .ToList();
            if (booksWithThresholdRatingAndAbove.Count > 0)
            {
                Console.WriteLine($" -- Books with an average rating of {thresholdRating} and above: -- ");
                PrintBooksInfo(booksWithThresholdRatingAndAbove);
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
                        Console.WriteLine($"Titel: {book.Title}, Genre: {book.Genre}, Publishing year: {book.PublishingYear}, Average rating: {book.GetAverageRating():0.0}");
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

        public void FilterBooksByGenre()
        {
            Console.WriteLine("Enter the genre you would like to filter by:");
            string genreToFilter = UserInput.ValidateTextInput();

            var filteredBooks = books.Where(book => book.Genre
            .Equals(genreToFilter, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (filteredBooks.Count > 0)
            {
                Console.WriteLine($" -- There are {filteredBooks.Count} books in the '{genreToFilter}' genre: -- ");
                PrintBooksInfo(filteredBooks);
            }
            else
            {
                Console.WriteLine($" -- No books found in the '{genreToFilter}' genre. -- ");
            }
        }

        public void FilterBooksByAuthor()
        {
            Console.WriteLine("Enter the name of the author you would like to filter by:");
            string authorToFilter = UserInput.ValidateTextInput();
            var filteredBooks = books.Where(book => book.Author
            .Equals(authorToFilter, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (filteredBooks.Count > 0)
            {
                Console.WriteLine($" -- There are {filteredBooks.Count} books written by the author '{authorToFilter}': -- ");
                PrintBooksInfo(filteredBooks);
            }
            else
            {
                Console.WriteLine($" -- No books found written by the author \"{authorToFilter}\". -- ");
            }
        }

        public void FilterBooksByPublishingYear()
        {
            Console.WriteLine("Enter the publishing year you would like to filter by:");
            int publishingYearToFilter = UserInput.ValidateNumberInput();

            var filteredBooks = books.Where(book => book.PublishingYear
            .Equals(publishingYearToFilter))
                .ToList();

            if (filteredBooks.Count > 0)
            {
                Console.WriteLine($" -- There are {filteredBooks.Count} books published in the year {publishingYearToFilter}: -- ");
                PrintBooksInfo(filteredBooks);
            }
            else
            {
                Console.WriteLine($" -- No books found published in the year {publishingYearToFilter}. -- ");
            }
        }

        public void SortBooksByTitle()
        {
            var sortedBooks = books.OrderBy(book => book.Title).ToList();

            Console.WriteLine(" -- Below are the books organized by title alphabetically: -- ");
            PrintBooksInfo(sortedBooks);
            
        }

        public void SortBooksByAuthor()
        {
            var sortedBooks = books.OrderBy(book => book.Author).ToList();

            Console.WriteLine(" -- Below are the books organized by their respective authors: -- ");
            PrintBooksInfo(sortedBooks);
        }

        public void SortBooksByPublishingYear()
        {
            var sortedBooks = books.OrderBy(book => book.PublishingYear).ToList();

            Console.WriteLine(" -- Below are the books organized by their respective publishing year: -- ");
            PrintBooksInfo(sortedBooks);
        }

        // Start Print helping functions 
        public void PrintBooksInfo(List<Book> listtoprint)
        {
            listtoprint.ForEach(book => Console.WriteLine
            (
                $"- Titel: {book.Title}, " +
                $"Author: {book.Author}, " +
                $"Genre: {book.Genre}, " +
                $"Publishing year: {book.PublishingYear}, " +
                $"Average rating: {book.GetAverageRating():0.0}"
            ));
        }

        public void PrintSingleBookInfo(Book book)
        {
            Console.WriteLine($"- Titel: {book.Title}, Author: {book.Author}, Genre: {book.Genre}, Publishing year: {book.PublishingYear}, Average rating: {book.GetAverageRating():0.0} -");

        }

        public void PrintAuthorsInfo(List<Author> listtoprint)
        {
            listtoprint.ForEach(author => Console.WriteLine
            (
                $"- Author: {author.Name}, " +
                $"Country: {author.Country}"
            ));
        }

        public void PrintSingleAuthorInfo(Author author)
        {
            Console.WriteLine($"- Authors name: {author.Name}, " +
                $"County: {author.Country} -");

        }
        // End Print helping functions

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
    // End Methods
}
