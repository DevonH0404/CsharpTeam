namespace LibraryApp.Tests;

[TestClass]
public class CatalogTests
{
    [TestMethod]
    public void add_book_should_add_book_to_catalog()
    {
        // arrange
        Catalog catalog = new();
        Book book = new("Sample Title", "Sample Author", "0000000000000", 3);
        //act
        catalog.AddBook(book);
        //assert
        Assert.IsTrue(catalog.Books.Contains(book));
    }
    [TestMethod]
    public void AddBook_RaisesError_IfDuplicateIsbn()
    {
        // arrange
        Catalog catalog = new();
        Book book1 = new("Sample Title", "Sample Author", "0000000000000", 3);
        Book book2 = new("Another Title", "Another Author", "0000000000000", 2);
        //act
        catalog.AddBook(book1);
        //assert
        Assert.Throws<InvalidOperationException>(() => catalog.AddBook(book2));
    }
    [TestMethod]
    public void FindByIsbn_ReturnsBook_WhenBookExists()
    {
        //arrange
        Catalog catalog = new();
        Book book = new("Sample Title", "Sample Author", "0000000000000", 3);
        //act
        catalog.AddBook(book);
        var foundBook = catalog.FindByIsbn("0000000000000");
        //assert
        Assert.AreEqual(book, foundBook);
    }
    [TestMethod]
    public void FindByIsbn_ReturnsNull_WhenBookDoesNotExist()
    {
        //arrange
        Catalog catalog = new();
        // act
        var foundBook = catalog.FindByIsbn("0000000000000");
        //assert
        Assert.IsNull(foundBook);
    }
    [TestMethod]
    public void SearchByTitle_ReturnsMatchingBooks()
    {
        //arrange
        Catalog catalog = new();
        Book book1 = new("Sample Title", "Sample Author", "0000000000000", 3);
        Book book2 = new("Another Sample Title", "Another Author", "1111111111111", 2);
        catalog.AddBook(book1);
        catalog.AddBook(book2);
        // act
        var results = catalog.SearchByTitle("Sample");
        // assert
        Assert.AreEqual(2, results.Count);
        Assert.IsTrue(results.Contains(book1));
        Assert.IsTrue(results.Contains(book2));
    }
}
