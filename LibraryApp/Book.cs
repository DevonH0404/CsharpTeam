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

    public void Return(Member member)
    {
        if (AvailableCopies >= TotalCopies)
            throw new InvalidOperationException($"All copies of '{Title}' are already returned.");
        // before adding copies, check to see if there are any reserved members, if so, give the book to the first member in the list and remove them from the reserved list
        if (member.CheckedOutBooks.Contains(this))
        {
            if (ReservedMembers.Count > 0)
            {
                var reservedMember = ReservedMembers[0];
                reservedMember.CheckedOutBooks.Add(this);
                ReservedMembers.RemoveAt(0);
                ReservedHistory.Add(reservedMember);
            }
            else AvailableCopies++;
            Console.WriteLine($"Successfully returned {Title}.");
        }
        else
        {
            throw new InvalidOperationException($"{member.Name} does not currently have {Title} checked out.");
        }
    }

    // Reserve a book for a member, checks to see if it's already been reserved by the member or if the member has it currently reserved
    public void Reserve(Member member)
    {
        if (member is null)
            throw new ArgumentNullException(nameof(member));
        if (AvailableCopies != 0)
            throw new InvalidOperationException($"Their is currently an available copy of {Title}, please check out the book instead.");
        if (member.CheckedOutBooks.Contains(this))
            throw new InvalidOperationException($"Member '{member.Name}' has already checked out the book '{Title}'.");
        else if (!ReservedMembers.Contains(member) && !ReservedHistory.Contains(member))
        {
            ReservedMembers.Add(member);
            Console.WriteLine($"Successfully checked out {Title} to {member.Name}");
        }
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
