namespace LibraryApp;

public class Program
{
    // make these accessible for entire program.cs
    private static Catalog catalog = new Catalog();

    // just create a member on program run for demo, if we implement a csv, store members in there
    private static Member member = new Member("TestMember", 1);

    public static void Main(string[] args)
    {
        // optimally for book reservation, we load from a csv and if a member is reserving a book and it's available, we check it out for them automatically
        try
        {
            Member member2 = new Member("TestMember2", 2);
            catalog.AddMember(member2);

            // create member for testing
            catalog.AddMember(member);

            var catalog = new Catalog();
            catalog.AddBook(new Book("The Pragmatic Programmer", "David Thomas", "9780135957059", 3));
            catalog.AddBook(new Book("Clean Code", "Robert C. Martin", "9780132350884", 2));

            // this is the book with no copies available, use this for testing reservation
            catalog.AddBook(new Book("The DevOps Handbook", "Gene Kim", "9781942788003", 1));

            // member2 checks out book to test reservation and unavailable functionality
            catalog.CheckOutBook("9781942788003", member2.MemberId);

            DisplayMenu();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    // this method keeps smoke test from auto failing on console.clear hit
    private static void ClearConsole()
    {
        // smoke tests will fail if we just console.clear on display menu
        if (!Console.IsOutputRedirected)
        {
            Console.Clear();
        }
    }

    public static void DisplayMenu()
    {
        ClearConsole();

        while (true)
        {
            Console.WriteLine("Please choose an option:\n1. Checkout book\n2. Return Book\n3. Reserve a Book\n4. Exit");
            string? userChoice = Console.ReadLine();

            // menu choices go to the associated method
            if (userChoice == "1")
            {
                CheckOutBook();
            }
            else if (userChoice == "2")
            {
                ReturnBook();
            }
            else if (userChoice == "3")
            {
                ReserveBook();
            }
            // exits program, maybe just change this to if they give a blank string, so if we add more menu options they dont have to be 5,6,7 etc...
            else if (userChoice == "4")
            {
                Console.WriteLine("Exiting the application.");
                break;
            }
            else
            {
                Console.WriteLine("Error: Invalid Input");
            }
        }
    }

    public static void CheckOutBook()
    {
        while (true)
        {
            try
            {
                ClearConsole();
                Console.WriteLine("Enter the number of the book you would like to check out:");

                // only show available books
                var availableBooks = catalog.Books
                    .Where(x => x.IsAvailable)
                    .ToList();

                // if their are available books display them
                if (availableBooks.Count > 0)
                {
                    for (int i = 0; i < availableBooks.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {availableBooks[i].Title} by {availableBooks[i].Author} � {availableBooks[i].AvailableCopies}/{availableBooks[i].TotalCopies} available");
                    }

                    string? choice = Console.ReadLine();

                    // check input
                    if (int.TryParse(choice, out int bookChoice))
                    {
                        int index = bookChoice - 1;

                        if (index >= 0 && index < availableBooks.Count)
                        {
                            // select book and check it out
                            Book selectedBook = availableBooks[index];

                            catalog.CheckOutBook(selectedBook.Isbn, member.MemberId);
                        }
                        else
                        {
                            Console.WriteLine("Invalid book selection.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please enter a number.");
                    }
                }
                else
                {
                    Console.WriteLine("There are no books currently available.");
                }

                Console.WriteLine("Press 'enter' to continue.");
                Console.ReadLine();
                break;
            }
            catch (Exception)
            {

            }
        }
    }

    public static void ReturnBook()
    {
        ClearConsole();

        try
        {
            // only show books that are being checked out currently by the member
            var returnableBooks = catalog.Books
                .Where(x => member.CheckedOutBooks.Contains(x))
                .ToList();

            // if their are books, display them
            if (returnableBooks.Count > 0)
            {
                Console.WriteLine("Enter the number of the book you would like to return:");

                for (int i = 0; i < returnableBooks.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {returnableBooks[i].Title}");
                }

                string? choice = Console.ReadLine();

                // check input
                if (int.TryParse(choice, out int bookChoice))
                {
                    int index = bookChoice - 1;

                    if (index >= 0 && index < returnableBooks.Count)
                    {
                        // return the book
                        Book selectedBook = returnableBooks[index];

                        catalog.ReturnBook(selectedBook.Isbn, member.MemberId);
                    }
                    else
                    {
                        Console.WriteLine("Invalid book selection.");
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a number.");
                }
            }
            else
            {
                Console.WriteLine("You currently have no books checked out.");
            }

            Console.WriteLine("Press 'enter' to continue.");
            Console.ReadLine();
            ClearConsole();
        }
        catch (Exception)
        {
        }
    }

    public static void ReserveBook()
    {
        ClearConsole();

        // only show books where their are no available copies, and the member doesn't currently have it reserved
        var reservableBooks = catalog.Books
            .Where(x => x.AvailableCopies == 0 && !x.ReservedMembers.Contains(member))
            .ToList();

        // display reservable books if their are any
        if (reservableBooks.Count > 0)
        {
            Console.WriteLine("Enter the number of the book you would like to reserve:");

            for (int i = 0; i < reservableBooks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {reservableBooks[i].Title}");
            }

            string? choice = Console.ReadLine();

            // check input
            if (int.TryParse(choice, out int bookChoice))
            {
                int index = bookChoice - 1;

                if (index >= 0 && index < reservableBooks.Count)
                {
                    // reserve the book
                    Book selectedBook = reservableBooks[index];

                    selectedBook.Reserve(member);
                }
                else
                {
                    Console.WriteLine("Invalid book selection.");
                }
            }
            else
            {
                Console.WriteLine("Please enter a number.");
            }
        }
        else
        {
            Console.WriteLine("All books are currently available.");
        }

        Console.WriteLine("Press 'enter' to continue.");
        Console.ReadLine();
        ClearConsole();
    }
