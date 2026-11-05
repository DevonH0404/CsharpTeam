namespace LibraryApp;

/// <summary>
/// Represents a single book title in the library catalog.
/// One Book instance represents all copies of that title.
/// </summary>
public class Book
{
    public string Title { get; }
    public string Author { get; }
    public string Isbn { get; }
    public int TotalCopies { get; }
    public int AvailableCopies { get; private set; }
    public List<Member> ReservedMembers { get; set; } = new();
    public List<Member> ReservedHistory { get; set; } = new();

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
        // before adding copies, check to see if there are any reserved members, if so, give the book to the first member in the list and remove them from the reserved list
        if (ReservedMembers.Count > 0)
        {
            var member = ReservedMembers[0];
            member.CheckedOutBooks.Add(this);
            ReservedMembers.RemoveAt(0);
            ReservedHistory.Add(member);
        }
        else AvailableCopies++;
    }

    // Reserve a book for a member, checks to see if it's already been reserved by the member or if the member has it currently reserved
    public void Reserve(Member member)
    {
        if (member is null)
            throw new ArgumentNullException(nameof(member));
        if (!ReservedMembers.Contains(member) && !ReservedHistory.Contains(member))
            ReservedMembers.Add(member);
    }

    // removes member from list of reservedmembers
    public void CancelReservation(Member member)
    {
        if (member is null)
            throw new ArgumentNullException(nameof(member));
        ReservedMembers.Remove(member);
    }

    // checks to see if a member has reserved the book in the past, check this before letting this reserve a book, a user can't reserve a book they've already reserved
    public bool WasReservedBy(Member member)
    {
        if (member is null)
            throw new ArgumentNullException(nameof(member));
        if (ReservedHistory.Contains(member))
            return true;
        else
            return false;
    }
}
