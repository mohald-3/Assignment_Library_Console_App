
namespace Library_Console_App
{
    public class Author
    {
        public int AuthorID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public Author(int authorid, string name, string country)
        {
            AuthorID = authorid;
            Name = name;
            Country = country;
        }
    }
}
