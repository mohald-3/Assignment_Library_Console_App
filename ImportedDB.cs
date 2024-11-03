using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Library_Console_App
{
    public class ImportedDB
    {
        [JsonPropertyName("Books")]
        public List<Book> AllBooksFromDB { get; set; }

        [JsonPropertyName("Authors")]
        public List<Author> AllAuthorsFromDB { get; set; }

    }
}
