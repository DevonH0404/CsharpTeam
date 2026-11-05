namespace LibraryApp;

/// <summary>
/// Manages the full collection of books in the library and the
/// operations students will write tests against: adding, finding,
/// checking out, and returning books.
/// </summary>
public class Catalog
{
    private readonly List<Book> _books = new();
    private readonly List<Member> _members = new();

    public IReadOnlyList<Book> Books => _books.AsReadOnly();
    public IReadOnlyList<Member> Members => _members.AsReadOnly();

    public void AddBook(Book book)
    {
        if (book is null)
            throw new ArgumentNullException(nameof(book));

        if (_books.Any(b => b.Isbn == book.Isbn))
            throw new InvalidOperationException($"A book with ISBN '{book.Isbn}' already exists in the catalog.");

        _books.Add(book);
    }

    public Book? FindByIsbn(string isbn)
    {
        return _books.FirstOrDefault(b => b.Isbn == isbn);
    }

    public List<Book> SearchByTitle(string titleQuery)
    {
        if (string.IsNullOrWhiteSpace(titleQuery))
            return new List<Book>();

        return _books
            .Where(b => b.Title.Contains(titleQuery, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public List<Book> SearchByAuthor(string authorQuery)
    {
        if (string.IsNullOrWhiteSpace(authorQuery))
            return new List<Book>();

        return _books
            .Where(b => b.Author.Contains(authorQuery, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    // Member functions - primarily mirrors the book functions, but for members
    public void AddMember(Member member)
    {
        if (member is null)
            throw new ArgumentNullException(nameof(member));

        if (_members.Any(m => m.MemberId == member.MemberId))
            throw new InvalidOperationException($"A member with ID '{member.MemberId}' already exists in the catalog.");

        _members.Add(member);
    }
    public Member? FindMemberById(int memberId)
    {
        return _members.FirstOrDefault(m => m.MemberId == memberId);
    }
    public List<Member> SearchMembersByName(string nameQuery)
    {
        if (string.IsNullOrWhiteSpace(nameQuery))
            return new List<Member>();

        return _members
            .Where(m => m.Name.Contains(nameQuery, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public void CheckOutBook(string isbn, int memberId)
    {
        var book = FindByIsbn(isbn);
        if (book is null)
            throw new InvalidOperationException($"No book found with ISBN '{isbn}'.");
        var member = FindMemberById(memberId);
        if (member is null)
            throw new InvalidOperationException($"No member found with the ID of '{memberId}'.");

        member.CheckOut(book);
        book.CheckOut();
    }

    public void ReturnBook(string isbn, int memberId)
    {
        var book = FindByIsbn(isbn);
        if (book is null)
            throw new InvalidOperationException($"No book found with ISBN '{isbn}'.");
        var member = FindMemberById(memberId);
        if (member is null)
            throw new InvalidOperationException($"No member found with the ID of '{memberId}'.");

        member.Return(book);
        book.Return();
    }

    public int TotalAvailableCopies()
    {
        return _books.Sum(b => b.AvailableCopies);
    }
}
