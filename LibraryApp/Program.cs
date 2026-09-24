namespace LibraryApp;

public class Program
{
    public static void Main(string[] args)
    {
        var catalog = new Catalog();

        catalog.AddBook(new Book("The Pragmatic Programmer", "David Thomas", "9780135957059", 3));
        catalog.AddBook(new Book("Clean Code", "Robert C. Martin", "9780132350884", 2));
        catalog.AddBook(new Book("The DevOps Handbook", "Gene Kim", "9781942788003", 1));

        Console.WriteLine("=== Library Catalog ===");
        foreach (var book in catalog.Books)
        {
            Console.WriteLine($"{book.Title} by {book.Author} \u2014 {book.AvailableCopies}/{book.TotalCopies} available");
        }
        Console.WriteLine();
        while (true)
        {
            Console.WriteLine("Please choose an option:\n1. Checkout book\n2.Return Book\n3. Exit");
            string userChoice = Console.ReadLine();

            if (userChoice == "1")
            {
                Console.WriteLine("\nEnter the title of the book you would like to check out:");
                foreach (var book in catalog.Books)
                {
                    Console.WriteLine($"{book.Title}");
                }
                string bookTitle = Console.ReadLine();

                foreach (var book in catalog.Books)
                {
                    if(bookTitle == book.Title)
                    {
                        book.CheckOut();
                        Console.WriteLine($"Book as been checked out: {book.AvailableCopies} copies left");
                    }
                }
                Console.ReadLine();
                Console.Clear();
            }
            else if (userChoice == "2")
            {
                Console.WriteLine("\nEnter the title of the book you would like to Return:");
                foreach (var book in catalog.Books)
                {
                    Console.WriteLine($"{book.Title}");
                }
                string bookReturned = Console.ReadLine();
                foreach(var book in catalog.Books)
                {
                    if( bookReturned == book.Title)
                    {
                        book.Return();
                        Console.WriteLine($"\nBook returned: {book.AvailableCopies} copies");
                    }
                }
                Console.ReadLine();
                Console.Clear();
            }
            else if (userChoice == "3")
            {
                break;
            }
            else
            {
                Console.WriteLine("error");
            }
        }




        //Console.WriteLine();
        //Console.WriteLine("Checking out 'Clean Code'...");
        //catalog.CheckOutBook("9780132350884");

        //var cleanCode = catalog.FindByIsbn("9780132350884");
        //Console.WriteLine($"'{cleanCode!.Title}' now has {cleanCode.AvailableCopies}/{cleanCode.TotalCopies} available.");

        //Console.WriteLine();
        //Console.WriteLine($"Total copies available across catalog: {catalog.TotalAvailableCopies()}");
    }
}
