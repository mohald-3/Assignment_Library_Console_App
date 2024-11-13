using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Library_Console_App.LibrarySystem.Models;

namespace Library_Console_App.LibrarySystem
{
    public class ImportedDB
    {
        [JsonPropertyName("Books")]
        public List<Book> AllBooksFromDB { get; set; }

        [JsonPropertyName("Authors")]
        public List<Author> AllAuthorsFromDB { get; set; }

    }
}
