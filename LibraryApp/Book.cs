namespace LibraryApp;

/// <summary>
/// Represents a single book title in the library catalog.
/// One Book instance represents all copies of that title.
/// </summary>
public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Isbn { get; set; }
    public int TotalCopies { get; set; }
    public int AvailableCopies { get; private set; }

    public Book(string title, string author, string isbn, int totalCopies)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));
        if (string.IsNullOrWhiteSpace(author))
            throw new ArgumentException("Author cannot be empty.", nameof(author));
        if (string.IsNullOrWhiteSpace(isbn))
            throw new ArgumentException("ISBN cannot be empty.", nameof(isbn));
        if (totalCopies < 0)
            throw new ArgumentException("Total copies cannot be negative.", nameof(totalCopies));

        Title = title;
        Author = author;
        Isbn = isbn;
        TotalCopies = totalCopies;
        AvailableCopies = totalCopies;
    }

    public bool IsAvailable => AvailableCopies > 0;

    public void CheckOut()
    {
        if (!IsAvailable)
            throw new InvalidOperationException($"No available copies of '{Title}' to check out.");

        AvailableCopies--;
    }

    public void Return()
    {
        if (AvailableCopies >= TotalCopies)
            throw new InvalidOperationException($"All copies of '{Title}' are already returned.");

        AvailableCopies++;
    }
}
