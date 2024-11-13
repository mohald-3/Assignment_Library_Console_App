namespace Library_Console_App.LibrarySystem.Models
{
    public interface IIdentifiable
    {
        int ID { get; set; }
        string GetInfo();
    }
}
