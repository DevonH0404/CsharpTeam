namespace LibraryApp.Tests;

[TestClass]
public class CatalogIntegrationTests
{
    [TestMethod]
    public void Catalog_Check_Out_Books()
    {
        Book book = new Book("Test", "Author", "123456789", 1);
        Catalog catalog = new Catalog();
        Member member = new Member("Test", 1);
        catalog.AddMember(member);
        catalog.AddBook(book);

        catalog.CheckOutBook("123456789", 1);

        Assert.AreEqual(0, book.AvailableCopies);
    }
    [TestMethod]
    public void Catalog_Return_Book()
    {
        Book book = new Book("Test", "Author", "123456789", 1);
        Catalog catalog = new Catalog();
        Member member = new Member("Test", 1);
        catalog.AddMember(member);
        catalog.AddBook(book);

        catalog.CheckOutBook("123456789", 1);
        catalog.ReturnBook("123456789", 1);

        Assert.AreEqual(1, book.AvailableCopies);
    }
    [TestMethod]
    public void Book_Checking_Out_Last_Book_Updates_IsAvailable()
    {
        Book book = new Book("Test", "Author", "123456789", 1);
        book.CheckOut();
        Assert.AreEqual(false, book.IsAvailable);
    }
    [TestMethod]
    public void Catalog_Checking_Out_Inexistent_ISBN_Returns_Exception()
    {
        Catalog catalog = new Catalog();
        Member member = new Member("Test", 1);
        Assert.Throws<InvalidOperationException>(() => catalog.CheckOutBook("123456789", 1));

    }
}
