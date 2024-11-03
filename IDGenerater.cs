
namespace Library_Console_App
{
    public class IDGenerator
    {
        public HashSet<int> usedBookIDs = new HashSet<int>();  // Keeps track of used Book IDs
        public HashSet<int> usedAuthorIDs = new HashSet<int>();  // Keeps track of used Authors IDs

        private Random random = new Random();  // Random number generator

        public int BookID { get; private set; } // must be unique
        public int AuthorID { get; private set; } // must be unique

        public IDGenerator()
        {

        }

        public int GenerateUniqueID(bool IsItABook)
        {
            int newID;

            if(IsItABook)
            {
                do
                {
                    newID = random.Next(100000, 1000000);  // Generates a short ISBN number of 6 digit.
                }
                while (usedBookIDs.Contains(newID));  // Ensure it's unique by checking usedIDs
                usedBookIDs.Add(newID);  // Add the unique ID to the set
            }
            else
            {
                do
                {
                    newID = random.Next(100000, 1000000);  // Generates an author ID of 6 digit.
                }
                while (usedAuthorIDs.Contains(newID));  // Ensure it's unique by checking usedIDs
                usedAuthorIDs.Add(newID);  // Add the unique ID to the set
            }
            return newID;
        }
    }
}
